using AutoMapper;
using HealthEase.Data;
using HealthEase.DTOs.Auth;
using HealthEase.Interfaces;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using HealthEase.Models.Auth;
using HealthEase.DTOs;
using HealthEase.Enums;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using HealthEase.Helpers;

namespace HealthEase.Services.Auth
{
    public class UserService :IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FilesManagementHelper _filesManagementHelper;

        public UserService(AppDbContext appDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, FilesManagementHelper filesManagementHelper) {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _filesManagementHelper = filesManagementHelper;
        }

        public async Task<UserDto> GetLoginUserService()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return null;

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }


        public async Task<PaginatedResult<UserDto>> GetUsersService( int pageNumber, int pageSize, string search , UserStatus? status , string role , string country)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var usersQuery = _appDbContext.Users.AsQueryable();

            // Search filter: Name, Email, Username
            if (!string.IsNullOrWhiteSpace(search))
            {
                usersQuery = usersQuery.Where(u =>
                    u.FirstName.Contains(search) ||
                    u.LastName.Contains(search) ||
                    u.Email.Contains(search) ||
                    u.Username.Contains(search));
            }

            // Status Filter
            if (status.HasValue)
            {
                var statusStr = status.ToString();
                usersQuery = usersQuery.Where(u => u.Status == statusStr);
            }

            // Role Filter
            if (!string.IsNullOrWhiteSpace(role))
            {
                usersQuery = usersQuery.Where(u => u.Role == role);
            }

            // Country Filter
            if (!string.IsNullOrWhiteSpace(country))
            {
                usersQuery = usersQuery.Where(u => u.Country == country);
            }

            // Total user count (without pagination)
            var totalItems = await usersQuery.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages));
            var skip = (pageNumber - 1) * pageSize;

            // Pagination & projection
            var pagedUsers = await usersQuery
                .OrderByDescending(u => u.CreateAt)
                .Skip(skip)
                .Take(pageSize)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginatedResult<UserDto>
            {
                Items = pagedUsers,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartPage = Math.Max(1, pageNumber - 2),
                EndPage = Math.Min(totalPages, pageNumber + 2)
            };
        }


      public async  Task<object> UpdateRoleService( Guid id, UserRole role)
        {
            try {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                if(user == null)
                {
                    return new NotFoundObjectResult("User was not found");
                }

                user.Role = role.ToString();
                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();

                return new { message = "User role updated successfully.", userId = user.Id, role= user.Role };

            }
            catch(Exception ex)
            {
                throw new ApplicationException("An error occurred in.", ex);
            }
        }


        public async Task<UserDto> GetUserByIdService(Guid id)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                var newUser = _mapper.Map<UserDto>(user);
                return newUser;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred in.", ex);
            }
        }

        public async Task<UserDto> GetUserByEmailService(string email)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
                var newUser = _mapper.Map<UserDto>(user);
                return newUser;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred in.", ex);
            }
        }


        public async Task<string> UpdatePhotoUrlServiceeAsync(IFormFile file)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId)) throw new UnauthorizedAccessException("User is not authorized");

                if (!Guid.TryParse(userId, out var parsedUserId)) throw new ArgumentException("Invalid user ID.");

                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);

                if (user == null) throw new InvalidOperationException("User not found.");

                if (!string.IsNullOrEmpty(user.PhotoUrl))
                {
                    await _filesManagementHelper.DeleteImageHelperAsync(user.PhotoUrl);
                }

                var imageUrl = await _filesManagementHelper.UploadImageAsync(file);
                user.PhotoUrl = imageUrl;
                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();

                return imageUrl;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
