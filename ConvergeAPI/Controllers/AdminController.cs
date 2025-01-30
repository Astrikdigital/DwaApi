﻿using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Response;
using BusinessLogicLayer.Service;
using BusinessObjectsLayer.Entities;
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

        [HttpPost("insert-update-registration")]
        public async Task<IActionResult> InsertUpdateRegistration([FromForm] DisabledWelFareForm disabledWelFareForm, IFormFile? attachProfilePicture = null)
        
        {
            try
            {
                var result = await _formService.InsertUpdateRegistration(disabledWelFareForm, attachProfilePicture);
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

        [HttpGet("get-registration")]
        public async Task<IActionResult> GetRegistration(int? Id = null, string? SerachText = null, int? PageSize = 20, int? PageNumber = 1,string?gender = null)
        { 
            try
            {
                var result = await _formService.GetRegistration(Id, SerachText, PageSize,PageNumber, gender);
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

        [HttpPost("delete-registration")]
        public async Task<IActionResult> DeleteRegistration(int Id)
        {
            try
            {
                var result = await _formService.DeleteRegistration(Id);
                return Ok(ResponseHelper.GetSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }

        [HttpGet("get-registrationddl")]
        public async Task<IActionResult> GetRegistrationDDL()
        {
            try
            {
                var result = await _formService.GetRegistrationDDL();
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
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                var result = await _formService.GetInventory();
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
        public async Task<IActionResult> GetDonor(string? SearchText)
        {
            try
            {
                var result = await _formService.GetDonor(SearchText);
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
        public async Task<IActionResult> GetEmployee(string serachText = null, int? stafTypeId = null,int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var result = await _formService.GetEmployee(serachText, stafTypeId, PageSize,PageNumber);
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

        #endregion


    }
}


