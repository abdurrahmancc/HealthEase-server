using HealthEase.Data;
using HealthEase.DTOs.Doctor;
using HealthEase.Models.Doctor;
using HealthEase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;
using HealthEase.Services.FilesManagement;
using HealthEase.Utilities;
using HealthEase.Helpers;
using CloudinaryDotNet.Actions;


namespace HealthEase.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly FilesManagementHelper _filesManagementHelper;

        public DoctorService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper, FilesManagementHelper filesManagementHelper)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _filesManagementHelper = filesManagementHelper;
        }

        public async Task<DoctorBasicInfoCreateDto> UpdateBasicInfoService(DoctorBasicInfoCreateDto info)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId)) throw new UnauthorizedAccessException("User is not authorized");

                if (!Guid.TryParse(userId, out var userGuid)) throw new ArgumentException("Invalid User ID");

                var isDoctor = _httpContextAccessor.HttpContext.User.IsInRole("Doctor");

                if (isDoctor)
                {
                    var doctor = await _appDbContext.Doctors.FirstOrDefaultAsync(d => d.UserId == userGuid);
                    if (doctor == null)
                    {
                        doctor = new DoctorModel
                        {
                            DoctorId = Guid.NewGuid(),
                            UserId = userGuid
                        };
                        _appDbContext.Doctors.Add(doctor);
                        await _appDbContext.SaveChangesAsync();
                    }

                    var existingInfo = await _appDbContext.DoctorBasicInfos.FirstOrDefaultAsync(b => b.DoctorId == doctor.DoctorId);
                    var existingMemberships = await _appDbContext.DoctorMemberships.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                    if (existingInfo != null)
                    {
                        _mapper.Map(info, existingInfo);
                        _appDbContext.DoctorBasicInfos.Update(existingInfo);
                    }
                    else
                    {
                        var newInfo = _mapper.Map<DoctorBasicInfoModel>(info);
                        newInfo.BasicInfoId = Guid.NewGuid();
                        newInfo.DoctorId = doctor.DoctorId;

                        _appDbContext.DoctorBasicInfos.Add(newInfo);
                    }


                    _appDbContext.DoctorMemberships.RemoveRange(existingMemberships);

                    foreach (var membershipDto in info.DoctorMemberships)
                    {
                        var membership = _mapper.Map<DoctorMembershipModel>(membershipDto);
                        membership.MembershipId = Guid.NewGuid();
                        membership.DoctorId = doctor.DoctorId;

                        _appDbContext.DoctorMemberships.Add(membership);
                    }

                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new UnauthorizedAccessException("You are a not authorized access to perform this action");
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<string> UpdatePhotoUrlServiceeAsync(IFormFile file)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId)) throw new UnauthorizedAccessException("User is not authorized");

                if (!Guid.TryParse(userId, out var userGuid))
                    throw new ArgumentException("Invalid User ID");

                if (!_httpContextAccessor.HttpContext.User.IsInRole("Doctor"))
                    throw new UnauthorizedAccessException("You are not authorized to perform this action");


                var doctor = await _appDbContext.Doctors.FirstOrDefaultAsync(d => d.UserId == userGuid);
                if (doctor == null)
                {
                    doctor = new DoctorModel
                    {
                        DoctorId = Guid.NewGuid(),
                        UserId = userGuid
                    };
                    _appDbContext.Doctors.Add(doctor);
                    await _appDbContext.SaveChangesAsync();
                }

                var existingInfo = await _appDbContext.DoctorBasicInfos.FirstOrDefaultAsync(b => b.DoctorId == doctor.DoctorId);
                if (existingInfo == null)
                {
                    var newInfo = new DoctorBasicInfoModel
                    {
                        BasicInfoId = Guid.NewGuid(),
                        DoctorId = doctor.DoctorId
                    };
                    _appDbContext.DoctorBasicInfos.Add(newInfo);
                }
                else if (!string.IsNullOrEmpty(existingInfo.PhotoUrl))
                {
                    await _filesManagementHelper.DeleteImageHelperAsync(existingInfo.PhotoUrl);
                }

                var imageUrl = await _filesManagementHelper.UploadImageAsync(file);
                existingInfo.PhotoUrl = imageUrl;
                _appDbContext.DoctorBasicInfos.Update(existingInfo);
                await _appDbContext.SaveChangesAsync();

                await _appDbContext.SaveChangesAsync();



                return imageUrl;

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }
}
