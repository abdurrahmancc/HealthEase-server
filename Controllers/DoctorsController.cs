using CloudinaryDotNet;
using HealthEase.DTOs;
using HealthEase.DTOs.Auth;
using HealthEase.DTOs.Doctor;
using HealthEase.Enums;
using HealthEase.Interfaces;
using HealthEase.Models.Doctor;
using HealthEase.Services.Auth;
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

        [HttpGet("GetBasicInfo")]
        [Authorize]
        public async Task<ActionResult> GetBasicInfo([FromQuery] string userId)
        {
            try
            {
                var res = await _doctorService.GetBasicInfoService(userId);
                return Ok(ApiResponse<DoctorBasicInfoCreateDto>.SuccessResponse(res, 200, "Update successfull"));
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


        [HttpPost("UpdateMemberships")]
        [Authorize]
        public async Task<ActionResult> UpdateMemberships([FromBody] List<DoctorMembershipCreateDto> memberships, [FromQuery] string userId)
        {
            try
            {
                var info = await _doctorService.UpdateMembershipsService(memberships, userId);
                return Ok(ApiResponse<List<DoctorMembershipReadDto>>.SuccessResponse(info, 200, "successful"));
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


        [HttpGet("GetMemberships")]
        [Authorize]
        public async Task<ActionResult> GetMemberships([FromQuery] string userId)
        {
            try
            {
                var info = await _doctorService.GetMembershipsService( userId);
                return Ok(ApiResponse<List<DoctorMembershipReadDto>>.SuccessResponse(info, 200, "successful"));
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

        [HttpPost("AddUpdateExperience")]
        [Authorize]
        public async Task<ActionResult> AddUpdateExperiences([FromForm] List<DoctorExperienceCreateDto> doctorExperience, [FromQuery] string userId)
        {
            try
            {
                Console.WriteLine("Count received: " + doctorExperience.Count);

                if (doctorExperience.Count == 0)
                {
                    return BadRequest("No experience data received. Check FormData field names.");
                }

                var info = await _doctorService.AddUpdateExperiencesService(doctorExperience, userId);
                return Ok(ApiResponse<List<DoctorExperienceReadDto>>.SuccessResponse(info, 200, "successful"));
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


        [HttpGet("GetExperiences")]
        [Authorize]
        public async Task<ActionResult> GetExperiences([FromQuery] string userId)
        {
            try
            {
                var info = await _doctorService.GetExperiencesService( userId);
                return Ok(ApiResponse<List<DoctorExperienceReadDto>>.SuccessResponse(info, 200, "successful"));
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


        [HttpPost("AddUpdateEducations")]
        [Authorize]
        public async Task<ActionResult> AddUpdateEducations([FromBody] List<DoctorEducationCreateDto> doctorEdocations, [FromQuery] string userId)
        {
            try
            {
                Console.WriteLine("Count received: " + doctorEdocations.Count);

                if (doctorEdocations.Count == 0)
                {
                    return BadRequest("No education data received. Check FormData field names.");
                }

                var info = await _doctorService.AddUpdateEducationsService(doctorEdocations, userId);
                return Ok(ApiResponse<List<DoctorEducationReadDto>>.SuccessResponse(info, 200, "successful"));
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


        [HttpGet("GetEducations")]
        [Authorize]
        public async Task<ActionResult> GetEducations([FromQuery] string userId)
        {
            try
            {
                var info = await _doctorService.GetEducationsService(userId);
                return Ok(ApiResponse<List<DoctorEducationReadDto>>.SuccessResponse(info, 200, "successful"));
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


        [HttpPost("AddUpdateClinics")]
        [Authorize]
        public async Task<ActionResult> AddUpdateClinics([FromForm] List<DoctorClinicCreateDto> doctorClinics, [FromQuery] string userId)
        {
            try
            {
                Console.WriteLine("Count received: " + doctorClinics.Count);

                if (doctorClinics.Count == 0)
                {
                    return BadRequest("No clinics data received. Check FormData field names.");
                }

                var info = await _doctorService.AddUpdateClinicsService(doctorClinics, userId);
                return Ok(ApiResponse<List<DoctorClinicReadDto>>.SuccessResponse(info, 200, "successful"));
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


        [HttpGet("GetClinics")]
        [Authorize]
        public async Task<ActionResult> GetClinics([FromQuery] string userId)
        {
            try
            {
                var info = await _doctorService.GetClinicsService(userId);
                return Ok(ApiResponse<List<DoctorClinicReadDto>>.SuccessResponse(info, 200, "successful"));
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
