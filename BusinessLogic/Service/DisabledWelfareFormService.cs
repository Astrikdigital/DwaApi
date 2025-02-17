﻿using BusinessObjectsLayer.Entities;
using Converge.Shared.Helper;
using DataAccessLayer;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using ErrorLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
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
        public async Task<dynamic> InsertUpdateBeneficiary(DisabledWelFareForm disabledWelfareForm, IFormFile? attachProfilePicture = null)
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
                var res = await _disabledWelfareFormRepository.InsertUpdateBeneficiary(disabledWelfareForm);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetBeneficiary(int? Id = null, string? searchText = null, int? PageSize = 20, int? PageNumber = 1, string gender = null)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetBeneficiary(Id, searchText, PageSize, PageNumber, gender);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> DeleteBeneficiary(int Id)
        {
            var res = await _disabledWelfareFormRepository.DeleteBeneficiary(Id);
            return res;
        }
        public async Task<dynamic> GetBeneficiaryDDL()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetBeneficiaryDDL();
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
        
       public async Task<dynamic> InsertUpdateDonor(DonorModel donor)
        {
            try
            {
                string ProfilePictureName = null;
                string ProfilePictureUrl = null;
                if (donor.AttachProfilePicture != null && donor.AttachProfilePicture.Length > 0)
                {
                    (ProfilePictureName, ProfilePictureUrl) = await Helper.AttachFileAsync(donor.AttachProfilePicture, _environment, 1);
                    donor.PictureUrl = ProfilePictureUrl;
                }
                if (donor.AttachmentDocument != null && donor.AttachmentDocument.Length > 0)
                {
                   var (filename, fileurl) = await Helper.AttachFileAsync(donor.AttachmentDocument, _environment, 1);
                    donor.Attachment = fileurl;
                }
                var res = await _disabledWelfareFormRepository.InsertUpdateDonor(donor);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        
        public async Task<dynamic> UpdateDonationStatus(int? Id, int? DonationStatusId)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.UpdateDonationStatus(Id, DonationStatusId);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetInventory(string? SearchText, int? PageNumber, int? PageSize)
        {
            try
            {
               
                var res = await _disabledWelfareFormRepository.GetInventory(SearchText,PageNumber,PageSize);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        
        public async Task<dynamic> InsertUpdateDonation(Donation donation)
        {
            try
            {
                if (donation.AttachmentDocument != null && donation.AttachmentDocument.Length > 0)
                {
                    var (filename, fileurl) = await Helper.AttachFileAsync(donation.AttachmentDocument, _environment, 1);
                    donation.Attachment = fileurl;
                }
                var res = await _disabledWelfareFormRepository.InsertUpdateDonation(donation);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetDonor(int? Id, string SearchText, int? PageNumber, int? PageSize, bool? IsView)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetDonor(Id, SearchText, PageNumber,PageSize, IsView);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetDonation(int? Id, int? PageNumber, int? PageSize, string? SearchText)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetDonation(Id, PageNumber,PageSize, SearchText);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> DeleteTableRow(string TableName,int? Id)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.DeleteTableRow(TableName, Id);
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


        #region EmployeeService
        public async Task<dynamic> GetEmployee(string searchText = null, int? stafTypeId = null, int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetEmployee(searchText, stafTypeId, PageSize, PageNumber);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetEmployeeById(int? Id = null)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetEmployeeById(Id);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<dynamic> InsertUpdateEmployee(EmployeeForm employeeForm)
        {
            try
            {
                string ProfilePictureName = null;
                string ProfilePictureUrl = null;
                string attachmentName = null;
                string attachmentUrl = null;



                if (employeeForm.Profile != null && employeeForm.Profile.Length > 0)
                {
                    (ProfilePictureName, ProfilePictureUrl) = await Helper.AttachFileAsync(employeeForm.Profile, _environment, 1);
                    employeeForm.ProfilePicture = ProfilePictureUrl;
                }

                if (employeeForm.attachmentUrl != null && employeeForm.attachmentUrl.Length > 0)
                {
                    (attachmentName, attachmentUrl) = await Helper.AttachFileAsync(employeeForm.attachmentUrl, _environment, 1);
                    employeeForm.Attachment = attachmentUrl;
                }
                var res = await _disabledWelfareFormRepository.InsertUpdateEmployee(employeeForm);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region OG_Service
        public async Task<dynamic> InsertOGTable(OGTables ogTables)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.InsertOGTable(ogTables);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetDesignation()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetDesignation();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> Getdepartment()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.Getdepartment();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetGender()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetGender();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetShift()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetShift();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetAssignment()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetAssingment();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetStatus()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetStatus();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetCities()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetCities();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetReligion()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetReligion();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetMaritalStatus()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetMaritalStatus();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<dynamic> GetContractType()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.ContractType();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<dynamic> GetDonationDetailType()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetDonationDetailType();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetDonationStatus()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetDonationStatus();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetEmploymentType()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetEmploymentType();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetDonationType()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetDonationType();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region Volunteer
        public async Task<dynamic> GetAllVolunteers(string searchText = null, int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetAllVolunteers(searchText, PageSize, PageNumber);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<dynamic> InsertVolunteer(VolunteerForm volunteerForm)
        {
            try
            {
                string ProfilePictureName = null;
                string ProfilePictureUrl = null;
                string attachmentName = null;
                string attachmentUrl = null;



                if (volunteerForm.Profile != null && volunteerForm.Profile.Length > 0)
                {
                    (ProfilePictureName, ProfilePictureUrl) = await Helper.AttachFileAsync(volunteerForm.Profile, _environment, 1);
                    volunteerForm.ProfilePicture = ProfilePictureUrl;
                }

                if (volunteerForm.attachmentUrl != null && volunteerForm.attachmentUrl.Length > 0)
                {
                    (attachmentName, attachmentUrl) = await Helper.AttachFileAsync(volunteerForm.attachmentUrl, _environment, 1);
                    volunteerForm.Attachment = attachmentUrl;
                }
                var res = await _disabledWelfareFormRepository.InsertVolunteer(volunteerForm);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }



        }

        public async Task<dynamic> GetvolunteerById(int? Id = null)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetvolunteerById(Id);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        public async Task<dynamic> GetBeneficiaryType()
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetBenificiaryType();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetIncomeTypes()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetIncomeTypes();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        
        public async Task<dynamic> GetInventoryUtilizationDll()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetInventoryUtilizationDll();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        
       public async Task<dynamic> InsertUpdateTransaction(TransactionModel model)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.InsertUpdateTransaction(model);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetDebitTransactions(int? Id, int? PageNumber, int? PageSize)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetDebitTransactions(Id, PageNumber, PageSize    );
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        } 
        public async Task<dynamic> InsertUpdateInventoryUtilization(InventoryUtilization inventory)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.InsertUpdateInventoryUtilization(inventory);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }


        public async Task<dynamic> GetInventoryUtilization(int? Id, int? PageNumber, int? PageSize)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetInventoryUtilization(Id, PageNumber,PageSize);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetBanks()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetBanks();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetBanksById(int? Id)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetBanksById(Id);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> GetProjectById(int? Id)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetProjectById(Id);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetProject()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetProject();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetDashboard(int? DonationYear, int? ExpenseYear, int? ExpenseMonth)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetDashboard(DonationYear, ExpenseYear,  ExpenseMonth);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetCountry()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetCountry();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetCityByCountryId(int? CountryId)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetCityByCountryId(CountryId);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetDonorDll()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetDonorDll();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        
  
        public async Task<dynamic> GetAllCNIC(string? cnic)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetAllCNIC(cnic);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> GetAllTransactions(int? bankId = null, int? transactionTypeId = null, int PageSize = 20, int? PageNumber = 0)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetAllTransactions(bankId, transactionTypeId, PageSize, PageNumber);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetAllBankDeposit(int? donorId = null, int PageSize = 20, int? PageNumber = 0)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.GetAllBankDeposit(donorId, PageSize, PageNumber);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<dynamic> InsertUpdateBankDeposit(BankDeposit bankDeposit)
        {
            try
            {
                string ProfilePictureName = null;
                string ProfilePictureUrl = null;

                if (bankDeposit.DocSlip != null && bankDeposit.DocSlip.Length > 0)
                {
                    (ProfilePictureName, ProfilePictureUrl) = await Helper.AttachFileAsync(bankDeposit.DocSlip, _environment, 1);
                    bankDeposit.Slip = ProfilePictureUrl;
                }
                var res = await _disabledWelfareFormRepository.InsertUpdateBankDeposit(bankDeposit);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> GetDepositBankSlip(int? PageNumber,int? PageSize)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetDepositBankSlip(PageNumber, PageSize);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> InsertUpdateBank(Bank model)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.InsertUpdateBank(model);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> InsertUpdateProject(Projects model)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.InsertUpdateProject(model);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> InsertUpdateMainhead(HeadsModel model)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.InsertUpdateMainhead(model);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> InsertUpdateHead(HeadsModel model)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.InsertUpdateHead(model);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> InsertUpdateSubHead(HeadsModel model)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.InsertUpdateSubHead(model);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetHeadById(int? Id)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetHeadById(Id);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetMainHeadById(int? Id)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetMainHeadById(Id);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetSubHeadById(int? Id)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetSubHeadById(Id);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> GetSubHead()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetSubHead();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> GetHead()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetHead();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> DeleteDepositBankSlip(int? Id)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.DeleteDepositBankSlip(Id);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> GetReportMainHead()
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetReportMainHead();
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> GetReportHeadByMainHeadId(int? Id)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetReportHeadByMainHeadId(Id);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<dynamic> GetReportSubHead(int? HeadId, int? MainHeadId)
        {
            try
            {

                var res = await _disabledWelfareFormRepository.GetReportSubHead(HeadId, MainHeadId);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> InsertUpdateDesignation(Designation model)
        {
            try
            {
                var res = await _disabledWelfareFormRepository.InsertUpdateDesignation(model);
                return res;
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
    }
}
