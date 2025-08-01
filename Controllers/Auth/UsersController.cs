using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using HealthEase.DTOs.Auth;
using HealthEase.Enums;
using HealthEase.Interfaces;
using HealthEase.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using HealthEase.DTOs;
using HealthEase.Services.FilesManagement;

namespace HealthEase.Controllers.Auth
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetLoginUser")]
        [Authorize]
        public async Task<IActionResult> GetLoginUser()
        {
            try
            {
                var user = await _userService.GetLoginUserService();
                return Ok(ApiResponse<UserDto>.SuccessResponse(user, 200, "Login successful")); ;
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 500, "Validation failed"));
            }
        }

        [HttpGet("GetUsers")]
        [Authorize]
        public async Task<IActionResult> GetUsers( int pageNumber = 1, int pageSize = 10, string search = null, UserStatus? status = null, string role = null, string country = null)
        {
            try
            {
                var responseData = await _userService.GetUsersService( pageNumber,  pageSize,  search,  status,  role,  country);
                return Ok(ApiResponse<PaginatedResult<UserDto>>.SuccessResponse(responseData, 200, "Get successful")); ;
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 500, "Validation failed"));
            }
        }


        [HttpPatch("UpdateRole")]
        [Authorize]
        public async Task<IActionResult> UpdateRole([FromQuery] Guid id, UserRole role  )
        {
            try {
                var res = await _userService.UpdateRoleService(id, role);
                return Ok(ApiResponse<object>.SuccessResponse(res, 200, "Get successful")); ;
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 500, "Server Error"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { "An unexpected error occurred." }, 500, "Unexpected Error"));
            }
        }

        [HttpGet("GetUserById")]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromQuery] Guid id)
        {
            try
            {
                var res = await _userService.GetUserByIdService(id);
                return Ok(ApiResponse<object>.SuccessResponse(res, 200, "Get successful")); ;
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 500, "Server Error"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { "An unexpected error occurred." }, 500, "Unexpected Error"));
            }
        }

        [HttpGet("GetUserByEmail")]
        [Authorize]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            try
            {
                var res = await _userService.GetUserByEmailService(email);
                return Ok(ApiResponse<object>.SuccessResponse(res, 200, "Get successful")); ;
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 500, "Server Error"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { "An unexpected error occurred." }, 500, "Unexpected Error"));
            }
        }

        [HttpPost("UpdatePhotoUrl")]
        [Authorize]
        public async Task<IActionResult> UpdatePhotoUrl(IFormFile file)
        {
            try
            {
                var filePath = await _userService.UpdatePhotoUrlServiceeAsync(file);

                return Ok(ApiResponse<string>.SuccessResponse(filePath, 200, "successful"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 401, "Unauthorized"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 400, "Invalid input"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 400, "Validation failed"));
            }
        }

    }
}
