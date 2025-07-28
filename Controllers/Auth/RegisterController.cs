using HealthEase.DTOs.Auth;
using HealthEase.Interfaces;
using HealthEase.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;




namespace HealthEase.Controllers.Auth
{
    [ApiController]
    [Route("v1/api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterRequestDto registerData)
        {
            try
            {
                var result = await _registerService.RegisterUserService(registerData);
                return Ok(ApiResponse<RegisterResponseDto>.SuccessResponse(result, 200, "Register successfull"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 400, "Validation failed"));
            }


        }
    }
}
