using Amazon.Runtime;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Response;
using BusinessLogicLayer.Service;
using BusinessObjectsLayer.Entities;
using Converge.Shared.Helper;
using DocumentFormat.OpenXml.Wordprocessing;
using ErrorLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
namespace ConvergeAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region Depedenices
        private readonly DisabledWelfareFormService _formService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        public AdminController(IWebHostEnvironment webHostEnvironment,DisabledWelfareFormService formService)
        {
            _formService = formService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("insert-update-beneficiary")]
        public async Task<IActionResult> InsertUpdateBeneficiary([FromForm] DisabledWelFareForm disabledWelFareForm, IFormFile? attachProfilePicture = null)    
        {
            try
            {
                var result = await _formService.InsertUpdateBeneficiary(disabledWelFareForm, attachProfilePicture);
                if (result.Id == 0)
                {
                    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                }
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpGet("get-beneficiary")]
        public async Task<IActionResult> GetBeneficiary(int? Id = null, string? searchText = null, int? PageSize = 20, int? PageNumber = 1,string?gender = null)
        { 
            try
            {
                var result = await _formService.GetBeneficiary(Id, searchText, PageSize,PageNumber, gender);
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpPost("delete-beneficiary")]
        public async Task<IActionResult> DeleteBeneficiary(int Id)
        {
            try
            {
                var result = await _formService.DeleteBeneficiary(Id);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-beneficiaryddl")]
        public async Task<IActionResult> GetBeneficiaryDDL()
        {
            try
            {
                var result = await _formService.GetBeneficiaryDDL();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpPost("InsertUpdateInventory")]
        public async Task<IActionResult> InsertUpdateInventory([FromForm] Inventory inventory, IFormFile? attachProfilePicture = null)
        {
            try
            {
                var result = await _formService.InsertUpdateInventory(inventory, attachProfilePicture);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("GetInventory")]
        public async Task<IActionResult> GetInventory(string? SearchText, int? PageNumber, int? PageSize)
        {
            try
            {
                var result = await _formService.GetInventory(SearchText, PageNumber, PageSize);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("UpdateDonationStatus")]
        public async Task<IActionResult> UpdateDonationStatus(int? Id,int? DonationStatusId)
        {
            try
            {
                var result = await _formService.UpdateDonationStatus(Id, DonationStatusId);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpPost("InsertUpdateDonor")]
        public async Task<IActionResult> InsertUpdateDonor([FromForm] DonorModel donor)
        {
            try
            {
                var result = await _formService.InsertUpdateDonor(donor);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        } 
        [HttpGet("GetDonor")]
        public async Task<IActionResult> GetDonor(int? Id, string? SearchText,int? PageNumber,int? PageSize,bool? IsView)
        {
            try
            {
                var result = await _formService.GetDonor(Id,SearchText, PageNumber, PageSize,IsView);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
          [HttpPost("InsertUpdateDonation")]
        public async Task<IActionResult> InsertUpdateDonation([FromForm] Donation donation)
        {
            try
            {
                var result = await _formService.InsertUpdateDonation(donation);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("GetDonation")]
        public async Task<IActionResult> GetDonation(int? Id, int? PageNumber, int? PageSize, string? SearchText)
        {
            try
            {
                var result = await _formService.GetDonation(Id, PageNumber,PageSize, SearchText);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("DeleteTableRow")]
        public async Task<IActionResult> DeleteTableRow(string? tableName,int? Id)
        {
            try
            {
                var result = await _formService.DeleteTableRow(tableName, Id);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("download-excel")]
        public async Task<IActionResult> DownloadExcel()
        {
            try
            {
                var fileStream = await _formService.GetRegistrationDataAndGenerateExcelAsync();
                var fileName = "dwa-all-registration.xlsx";
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "downloads", fileName);
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                using (var fileStreamToSave = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await fileStream.CopyToAsync(fileStreamToSave);
                }
                var fileUrl = Url.Content($"~/downloads/{fileName}");
                return Ok(ResponseHelper.GetSuccessResponse(fileUrl)); 
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }



        #region Employee
        [HttpGet("get-employee")]
        public async Task<IActionResult> GetEmployee(string searchText = null, int? stafTypeId = null,int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var result = await _formService.GetEmployee(searchText, stafTypeId, PageSize,PageNumber);
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }


        [HttpPost("insert-update-employee")]
        public async Task<IActionResult> InsertUpdateEmployee([FromForm] EmployeeForm employeeForm)

        {
            try
            {
                var result = await _formService.InsertUpdateEmployee(employeeForm);
                if (result.Id == 0)
                {
                    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                }
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }


        [HttpGet("get-employee-by-id")]
        public async Task<IActionResult> GetEmployeeById(int? Id = null)
        {
            try
            {
                var result = await _formService.GetEmployeeById(Id);
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        #endregion


        #region OG_TABLES
        [HttpPost("insert-og")]
        public async Task<IActionResult> InsertOGTable(OGTables ogTables)

        {
            try
            {
                var result = await _formService.InsertOGTable(ogTables);
                if (result.Id == 0)
                {
                    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                }
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpGet("get-designation")]
        public async Task<IActionResult> GetDesignation()
        {
            try
            {
                var result = await _formService.GetDesignation();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-department")]
        public async Task<IActionResult> GetDepartment()
        {
            try
            {
                var result = await _formService.Getdepartment();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-gender")]
        public async Task<IActionResult> GetGender()
        {
            try
            {
                var result = await _formService.GetGender();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-shift")]
        public async Task<IActionResult> GetShift()
        {
            try
            {
                var result = await _formService.GetShift();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-status")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var result = await _formService.GetStatus();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        [HttpGet("get-cities")]
        public async Task<IActionResult> GetCities()
        {
            try
            {
                var result = await _formService.GetCities();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        [HttpGet("get-religion")]
        public async Task<IActionResult> GetReligion()
        {
            try
            {
                var result = await _formService.GetReligion();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        [HttpGet("get-marital-status")]
        public async Task<IActionResult> GetMaritalStatus()
        {
            try
            {
                var result = await _formService.GetMaritalStatus();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-contract-type")]
        public async Task<IActionResult> GetContractType()
        {
            try
            {
                var result = await _formService.GetContractType();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-donation-type")]
        public async Task<IActionResult> GetDonationType()
        {
            try
            {
                var result = await _formService.GetDonationType(); 
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        [HttpGet("get-donation-detail-type")]
        public async Task<IActionResult> GetDonationDetailType()
        {
            try
            {
                var result = await _formService.GetDonationDetailType();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        [HttpGet("get-donation-status")]
        public async Task<IActionResult> GetDonationStatus()
        {
            try
            {
                var result = await _formService.GetDonationStatus();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        [HttpGet("get-employment-type")]
        public async Task<IActionResult> GetEmploymentType()
        {
            try
            {
                var result = await _formService.GetEmploymentType();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-assignment")]
        public async Task<IActionResult> GetAssignment()
        {
            try
            {
                var result = await _formService.GetAssignment();
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        #endregion

        #region Volunteer
        [HttpGet("get-volunteer")]
        public async Task<IActionResult> GetAllVolunteers(string searchText = null, int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var result = await _formService.GetAllVolunteers(searchText, PageSize, PageNumber);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }


        [HttpPost("insert-volunteer")]
        public async Task<IActionResult> InsertVolunteer([FromForm] VolunteerForm volunteerForm)

        {
            try
            {
                var result = await _formService.InsertVolunteer(volunteerForm);
                if (result.Id == 0)
                {
                    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                }
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }
        }

        [HttpGet("get-volunteer-by-id")]
        public async Task<IActionResult> GetvolunteerById(int? Id = null)
        {
            try
            {
                var result = await _formService.GetvolunteerById(Id);
                //if (result.Id == 0)
                //{
                //    return Ok(ResponseHelper.GetDublicateResponse(result, ResponseMessage.DuplicateUser));
                //}
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        #endregion


        [HttpGet("get-beneficiary-type")]
        public async Task<IActionResult> GetBeneficiaryType()
        {
            try
            {
                var result = await _formService.GetBeneficiaryType();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("GetIncomeTypes")]
        public async Task<IActionResult> GetIncomeTypes()
        {
            try
            {
                var result = await _formService.GetIncomeTypes();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("GetBanks")]
        public async Task<IActionResult> GetBanks()
        {
            try
            {
                var result = await _formService.GetBanks();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("GetInventoryUtilizationDll")]
        public async Task<IActionResult> GetInventoryUtilizationDll()
        {
            try
            {
                var result = await _formService.GetInventoryUtilizationDll();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpPost("InsertUpdateTransaction")]
        public async Task<IActionResult> InsertUpdateTransaction(TransactionModel model)
        {
            try
            {
                var result = await _formService.InsertUpdateTransaction(model);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("GetDebitTransactions")]
        public async Task<IActionResult> GetDebitTransactions(int? Id, int? PageNumber, int? PageSize)
        {
            try
            {
                var result = await _formService.GetDebitTransactions(Id, PageNumber, PageSize);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }

        }
        [HttpGet("GetInventoryUtilization")]
        public async Task<IActionResult> GetInventoryUtilization(int? Id, int? PageNumber, int? PageSize)
        {
            try
            {
                var result = await _formService.GetInventoryUtilization(Id, PageNumber, PageSize);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            } 
        }
        [HttpPost("InsertUpdateInventoryUtilization")]
        public async Task<IActionResult> InsertUpdateInventoryUtilization(InventoryUtilization inventory)
        {
            try
            {
                var result = await _formService.InsertUpdateInventoryUtilization(inventory);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }
        [HttpGet("GetProject")]
        public async Task<IActionResult> GetProject()
        {
            try
            {
                var result = await _formService.GetProject();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }
        [HttpGet("GetDonorDll")]
        public async Task<IActionResult> GetDonorDll()
        {
            try
            {
                var result = await _formService.GetDonorDll();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }
        [HttpGet("GetCityByCountryId")]
        public async Task<IActionResult> GetCityByCountryId(int? CountryId)
        {
            try
            {
                var result = await _formService.GetCityByCountryId(CountryId);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }
        [HttpGet("GetDashboard")]
        public async Task<IActionResult> GetDashboard(int? DonationYear, int? ExpenseYear, int? ExpenseMonth)
        {
            try
            {
                var result = await _formService.GetDashboard(DonationYear, ExpenseYear, ExpenseMonth);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }
        [HttpGet("GetCountry")]
        public async Task<IActionResult> GetCountry()
        {
            try
            {
                var result = await _formService.GetCountry();
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }
        [HttpGet("get-all-cnic")]
        public async Task<IActionResult> GetAllCNIC(string? cnic)
        {
            try
            {
                var result = await _formService.GetAllCNIC(cnic);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }


        [HttpGet("get-all-transactions")]
        public async Task<IActionResult> GetAllTransactions(int? bankId = null, int? transactionTypeId = null, int PageSize = 20, int? PageNumber = 0)
        {
            try
            {
                var result = await _formService.GetAllTransactions(bankId, transactionTypeId, PageSize, PageNumber);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-all-bank-deposit")]
        public async Task<IActionResult> GetAllBankDeposit(int? donorId = null, int PageSize = 20, int? PageNumber = 0)
        {
            try
            {
                var result = await _formService.GetAllBankDeposit(donorId, PageSize, PageNumber);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

 [HttpPost("InsertUpdateBankDeposit")]
        public async Task<IActionResult> InsertUpdateBankDeposit([FromForm] BankDeposit bankDeposit)
        {
            try
            {
                var result = await _formService.InsertUpdateBankDeposit(bankDeposit);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }
        [HttpGet("GetDepositBankSlip")]
        public async Task<IActionResult> GetDepositBankSlip(int? PageNumber,int? PageSize)
        {
            try
            {
                var result = await _formService.GetDepositBankSlip(PageNumber,PageSize);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());

            }
        }

    }
}


