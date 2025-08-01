using HealthEase.DTOs.Auth;
using HealthEase.DTOs.Doctor;
using HealthEase.Interfaces;
using HealthEase.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthEase.Controllers
{
    [ApiController]
    [Route("v1/api/doctor")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("UpdateBasicInfo")]
        [Authorize]
        public async Task<ActionResult> UpdateBasicInfo([FromBody] DoctorBasicInfoCreateDto info)
        {
            try
            {
                var res = await _doctorService.UpdateBasicInfoService(info);
                return Ok(ApiResponse<DoctorBasicInfoCreateDto>.SuccessResponse(res, 200, "Update successfull"));
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 401, "Unauthorized"));
            }            
            catch(ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 400, "Invalid input"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 400, "Validation failed"));
            }
        }


        [HttpPost("UpdatePhotoUrl")]
        [Authorize]
        public async Task<ActionResult> UpdatePhotoUrl(IFormFile file)
        {
            try
            {
                var url = await _doctorService.UpdatePhotoUrlServiceeAsync(file);

                return Ok(ApiResponse<string>.SuccessResponse(url, 200, "successful"));
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
