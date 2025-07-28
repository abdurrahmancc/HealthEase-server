using HealthEase.DTOs.Auth;
using HealthEase.Interfaces;
using HealthEase.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthEase.Controllers.Auth
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
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

    }
}
