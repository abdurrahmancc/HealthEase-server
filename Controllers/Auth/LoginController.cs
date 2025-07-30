using HealthEase.DTOs.Auth;
using HealthEase.Interfaces;
using HealthEase.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HealthEase.Controllers.Auth
{
    [ApiController]
    [Route("v1/api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestDto loginInfo)
        {
            try
            {
                var result = await _loginService.LoginUserService(loginInfo);

                if (result == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse( new List<string> { "Invalid email or password." }, 404, "Validation failed"));
                }

                return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result, 200, "Login successful"));
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse( new List<string> { ex.Message }, 500, "Server Error"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse( new List<string> { "An unexpected error occurred." }, 500, "Unexpected Error"));
            }
        }
    }
}
