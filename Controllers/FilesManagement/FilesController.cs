using HealthEase.Utilities;
using Microsoft.AspNetCore.Mvc;
using HealthEase.Services.FilesManagement;


namespace HealthEase.Controllers.FilesManagement
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FilestService _filesService;

        public FilesController(FilestService filesService)
        {
            _filesService = filesService;
        }


        [HttpPost("PhotoUrl")]
        public async Task<IActionResult> UpdatePhotoUrl(IFormFile file, string userId)
        {
            try
            {
                var filePath = await _filesService.UpdatePhotoUrlServiceeAsync(file, userId);

                if (filePath.StartsWith("Error:"))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse(new List<string> { filePath.Replace("Error: ", "") }, 400, "Validation failed"));
                }

                return Ok(ApiResponse<string>.SuccessResponse(filePath, 200, "successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 500, "Internal server error"));
            }
        }



        [HttpPost("image/upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                var filePath = await _filesService.UploadImagServiceeAsync(file);

                if (filePath.StartsWith("Error:"))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse(new List<string> { filePath.Replace("Error: ", "") }, 400, "Validation failed"));
                }

                return Ok(ApiResponse<string>.SuccessResponse(filePath, 200, "successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }, 500, "Internal server error"));
            }
        }


        [HttpDelete("image/delete")]
        public IActionResult DeleteFile(string filePath)
        {
            var result = _filesService.DeleteImageServiceAsync(filePath);

            if (result.Result)
            {
                return Ok(ApiResponse<string>.SuccessResponse(filePath, 200, "File deleted successfully."));
            }

            return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "File not found." }, 404, "Validation failed"));
        }
    }
}