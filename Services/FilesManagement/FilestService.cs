using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using HealthEase.Helpers;
using HealthEase.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace HealthEase.Services.FilesManagement
{
    public class FilestService
    {
        private readonly Cloudinary _cloudinary;
        private readonly FilesManagementHelper _filesManagementHelper;
        private readonly AppDbContext _appDbContext;

        public FilestService(Cloudinary cloudinary, FilesManagementHelper filesManagementHelper, AppDbContext appDbContext)
        {
            _cloudinary = cloudinary;
            _filesManagementHelper = filesManagementHelper;
            _appDbContext = appDbContext;
        }

        public async Task<string> UpdatePhotoUrlServiceeAsync(IFormFile file, string userId)
        {
            try
            {
                if (!Guid.TryParse(userId, out var parsedUserId))
                {
                    throw new ArgumentException("Invalid user ID.");
                }



                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);


                if (user == null)
                {
                    throw new InvalidOperationException("User not found.");
                }


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
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


        public async Task<string> UploadImagServiceeAsync(IFormFile file)
        {
            try
            {
                var imageUrl = await _filesManagementHelper.UploadImageAsync(file);
                return imageUrl;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }



        public async Task<bool> DeleteImageServiceAsync(string imageUrl)
        {

            if (Uri.TryCreate(imageUrl, UriKind.Absolute, out var uri))
            {
                
                await _filesManagementHelper.DeleteImageHelperAsync(imageUrl);
                return true;
            }
            else
            {
                return true;
            }

        }
    }
}