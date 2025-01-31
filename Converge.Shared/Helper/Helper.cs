using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using ErrorLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;



namespace Converge.Shared.Helper
{
    public static class Helper
    {
        public static async Task<(string FileName, string FileUrl)> AttachFileAsync(IFormFile attachFile, IWebHostEnvironment environment, int? Type)
        {
            try
            {
                var allowedTypes = new[]
                {
                "image/jpeg", "image/png", "image/gif",
                "application/pdf",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "application/vnd.ms-excel",
                "text/plain",
                "application/msword",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "application/x-rar-compressed"};

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".xlsx", ".xls", ".doc", ".docx", ".rar",".zip" };

                var fileExtension = Path.GetExtension(attachFile.FileName).ToLower();

                if (!allowedTypes.Contains(attachFile.ContentType) || !allowedExtensions.Contains(fileExtension))
                {
                    return (null, null);
                }
                string? selectFolder = "";
                var baseUploadPath = Path.Combine(environment.WebRootPath, "Attachments", selectFolder);
                Directory.CreateDirectory(baseUploadPath);


                var sanitizedFileName = Path.GetFileNameWithoutExtension(attachFile.FileName).Replace(" ", "_").Replace("..", "");
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var actualFileName = $"{sanitizedFileName}_{timestamp}{fileExtension}";

                var filePath = Path.Combine(baseUploadPath, actualFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await attachFile.CopyToAsync(stream);
                }
                var fileUrl = $"/Attachments/{selectFolder}/{actualFileName}";
                return (actualFileName, fileUrl);
            }
            catch (Exception ex)
            {

            }
            return (null, null);
        }
        public static string GenerateEntityId(int EntityId)
        {
            var year = DateTime.Now.Year % 100;
            var randomDigits = new Random().Next(1000, 9999);
            return EntityId == (int)EnumHelper.Role.Faculty
                ? $"CF{year}{randomDigits}"
                : EntityId == (int)EnumHelper.Role.Student
                ? $"CS{year}{randomDigits}"
                : "";
        }

        public static int UserId(IHttpContextAccessor? _httpContextAccessor)
        {
            if (_httpContextAccessor?.HttpContext?.User == null)
            {
                return 0;
            }

            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out var parsedUserId))
            {
                return parsedUserId;
            }
            return 0;
        }

     }
}
