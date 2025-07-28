using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using Image = SixLabors.ImageSharp.Image;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace HealthEase.Helpers
{
    public class FilesManagementHelper
    {
        private readonly HttpClient _httpClient;
        private readonly Cloudinary _cloudinary;

        public FilesManagementHelper(HttpClient httpClient, Cloudinary cloudinary)
        {
            _httpClient = httpClient;
            _cloudinary = cloudinary;
        }


        public async Task<string> UploadImageAsync(IFormFile file, int maxSizeKB = 200)
        {
            if (file == null || file.Length == 0)
                throw new InvalidOperationException("Image file is missing.");

            if (file.Length > maxSizeKB * 1024)
                throw new InvalidOperationException($"File size cannot be more than {maxSizeKB} KB.");

            var allowedMimeTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp", "image/webp" };

            if (!allowedMimeTypes.Contains(file.ContentType.ToLower()))
                throw new InvalidOperationException("Only jpeg, jpg, png, gif, bmp, webp image files are allowed.");

            var guid = Guid.NewGuid();
            var originalName = Path.GetFileNameWithoutExtension(file.FileName);
            originalName = Regex.Replace(originalName, @"[^a-zA-Z0-9_\-\.]", "_");
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = $"{guid}_{originalName}",
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            throw new InvalidOperationException("Image upload failed.");
        }

        public async Task<bool> IsValidImageAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var image = Image.Load(stream);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }


        public async Task<bool> DeleteImageHelperAsync(string imageUrl)
        {
            if (Uri.TryCreate(imageUrl, UriKind.Absolute, out _))
            {
                var publicId = GetPublicIdByImageUrl(imageUrl);
                if (!string.IsNullOrEmpty(publicId))
                {
                    var deletionParams = new DeletionParams(publicId);
                    var result = await _cloudinary.DestroyAsync(deletionParams);
                    return result.Result == "ok";
                }
            }
            return true;
        }

        public static string GetPublicIdByImageUrl(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            var path = uri.AbsolutePath;

            var fileName = Path.GetFileNameWithoutExtension(path);
            return HttpUtility.UrlDecode(fileName);
        }



    }
}
