using BusinessObjectsLayer.Entities;
using Converge.Shared.Helper;
using DataAccessLayer;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using ErrorLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Reflection;


namespace BusinessLogicLayer.Service
{
    public class DisabledWelfareFormService
    {
        #region Depedenices

        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        public DisabledWelfareFormService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        private readonly IWebHostEnvironment _environment;
        private readonly DisabledWelfareFormRepository _disabledWelfareFormRepository;
        public DisabledWelfareFormService(IWebHostEnvironment environment, DisabledWelfareFormRepository disabledWelfareFormRepository)
        {
            _environment = environment;
            _disabledWelfareFormRepository = disabledWelfareFormRepository;
        }
        public async Task<dynamic> InsertUpdateRegistration(DisabledWelFareForm disabledWelfareForm, IFormFile? attachProfilePicture = null)
        {
            try
            {
                string ProfilePictureName = null;
                string ProfilePictureUrl = null;

                if (attachProfilePicture != null && attachProfilePicture.Length > 0)
                {
                    (ProfilePictureName, ProfilePictureUrl) = await Helper.AttachFileAsync(attachProfilePicture, _environment, 1);
                    disabledWelfareForm.Image = ProfilePictureUrl;
                }
                var res = await _disabledWelfareFormRepository.InsertUpdateRegistration(disabledWelfareForm);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetRegistration(int? Id = null, string? SerachText = null, int? PageSize = 20, int? PageNumber = 1, string gender = null)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetRegistration(Id, SerachText, PageSize, PageNumber, gender);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> DeleteRegistration(int Id)
        {
            var res = await _disabledWelfareFormRepository.DeleteRegistration(Id);
            return res;
        }
        public async Task<dynamic> GetRegistrationDDL()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetRegistrationDDL();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async   Task<dynamic> InsertUpdateInventory(Inventory inventory, IFormFile? attachProfilePicture = null)
        {
            try
            {
                string ProfilePictureName = null;
                string ProfilePictureUrl = null;
                if (attachProfilePicture != null && attachProfilePicture.Length > 0)
                {
                    (ProfilePictureName, ProfilePictureUrl) = await Helper.AttachFileAsync(attachProfilePicture, _environment, 1);
                    inventory.Image = ProfilePictureUrl;
                }
                var res = await _disabledWelfareFormRepository.InsertUpdateInventory(inventory);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetInventory()
        {
            try
            {
               
                var res = await _disabledWelfareFormRepository.GetInventory();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<MemoryStream> GetRegistrationDataAndGenerateExcelAsync()
        {
            try
            {
                List<RegistrationModel> registrationModels = new List<RegistrationModel>();
                registrationModels = await _disabledWelfareFormRepository.GetRegistrationDataAsync();
                var memoryStream = GenerateUserDataExcel(registrationModels);
                return memoryStream;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public async Task<string> GenerateAndSaveRegistrationExcelAsync()
        //{
        //    try
        //    {
        //        // Fetch registration data
        //        var registrationModels = await _disabledWelfareFormRepository.GetRegistrationDataAsync();

        //        // Generate Excel file in memory
        //        var memoryStream = GenerateUserDataExcel(registrationModels);

        //        // Determine file path
        //        var fileName = "dwa-all-registration.xlsx";
        //        var webRootPath = _webHostEnvironment.WebRootPath;
        //        if (string.IsNullOrEmpty(webRootPath))
        //        {
        //            throw new InvalidOperationException("The WebRootPath is not set. Ensure your project is correctly configured.");
        //        }

        //        var filePath = Path.Combine(webRootPath, "downloads", fileName);
        //        var directoryPath = Path.GetDirectoryName(filePath);

        //        // Ensure directory exists
        //        if (!Directory.Exists(directoryPath))
        //        {
        //            Directory.CreateDirectory(directoryPath);
        //        }

        //        // Save the file to disk
        //        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //        {
        //            memoryStream.Seek(0, SeekOrigin.Begin);
        //            await memoryStream.CopyToAsync(fileStream);
        //        }

        //        // Return relative file URL
        //        return filePath;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception (optional)
        //        // Log.Error(ex, "Error generating or saving the registration Excel file.");
        //        return null;
        //    }
        //}

        public MemoryStream GenerateUserDataExcel(List<RegistrationModel> userData)
        {
            var memoryStream = new MemoryStream();
            using (var package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets.Add("dwa-all-registration");
                var properties = typeof(RegistrationModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var headers = properties.Select(p => p.Name).ToArray();
                for (int col = 0; col < headers.Length; col++)
                {
                    var headerCell = worksheet.Cells[1, col + 1];
                    headerCell.Value = headers[col];
                    headerCell.Style.Font.Bold = true;
                    headerCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#9c3134"));
                    headerCell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    headerCell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); // Add border to header
                }
                int row = 2;
                foreach (var user in userData)
                {
                    for (int col = 0; col < properties.Length; col++)
                    {
                        var value = properties[col].GetValue(user);
                        var cell = worksheet.Cells[row, col + 1];
                        cell.Value = value ?? "";
                        if (value is DateTime dateValue)
                        {
                            cell.Style.Numberformat.Format = "mm/dd/yyyy";
                        }
                        cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }
                    row++;
                }
                worksheet.Cells.AutoFitColumns();
                package.Save();
            }
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
