using BusinessObjectsLayer.Entities;
using Dapper;
using DataAccess.DbContext;
using DocumentFormat.OpenXml.Office2010.Excel;
using ErrorLog;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DataAccessLayer
{
    public class DisabledWelfareFormRepository
    {
        private readonly DapperContext _context;
        public DisabledWelfareFormRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<dynamic> InsertUpdateRegistration(DisabledWelFareForm disabledWelfareForm)
        {
            try
            {
                try
                {
                    var param = new
                    {
                        Id = disabledWelfareForm.Id,
                        ProjectId = disabledWelfareForm.ProjectId,
                        Image = disabledWelfareForm.Image,
                        Date = disabledWelfareForm.Date,
                        CNIC = disabledWelfareForm.CNIC,
                        GenderId = disabledWelfareForm.GenderId,
                        Name = disabledWelfareForm.Name,
                        FatherName = disabledWelfareForm.FatherName,
                        DOB = disabledWelfareForm.DOB,
                        Age = disabledWelfareForm.Age,
                        ReligionId = disabledWelfareForm.ReligionId,
                        QualificationId = disabledWelfareForm.QualificationId,
                        Experience = disabledWelfareForm.Experience,
                        Address = disabledWelfareForm.Address,
                        PhoneNo = disabledWelfareForm.PhoneNo,
                        MobileNo = disabledWelfareForm.MobileNo,
                        WhatsAppNo = disabledWelfareForm.WhatsAppNo,
                        Email = disabledWelfareForm.Email,
                        DisabilityId = disabledWelfareForm.DisabilityId,
                        CauseDisabilityId = disabledWelfareForm.CauseDisabilityId,
                        Reference = disabledWelfareForm.Reference,
                        NeedsRemarks = disabledWelfareForm.NeedsRemarks,
                    };
                    using (IDbConnection con = _context.CreateConnection())
                    {
                        var resp = (await con.QueryAsync<dynamic>("InsertUpdateRegistration", param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                        disabledWelfareForm.Id = resp.Id;
                        return disabledWelfareForm;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

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
                var param = new
                {
                    Id = Id,
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    SearchText = SerachText,
                    Gender = gender
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("GetRegistration", param: param, commandType: CommandType.StoredProcedure)).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> DeleteRegistration(int Id)
        {
            var result = new
            {
                IsSuccess = false,
                Message = string.Empty,
                Status = string.Empty
            };

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new
                    {
                        Id = Id
                    };
                    await connection.ExecuteAsync("DeleteRegistration", parameters, commandType: CommandType.StoredProcedure);
                    result = new
                    {
                        IsSuccess = true,
                        Message = "registration has been deleted successfully.",
                        Status = "Success"
                    };
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }
        
        public async Task<dynamic> InsertUpdateInventory(Inventory inventory)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id = inventory.Id,
                    ItemName = inventory.ItemName,
                    Date = inventory.Date,
                    CategoryId = inventory.CategoryId,
                    Quantity = inventory.Quantity,
                    ConditionId = inventory.ConditionId,
                    StatusId =inventory.StatusId,   
                    Description = inventory.Description,
                    Image = inventory.Image
                };
                return (await con.QueryAsync("InsertUpdateInventory",param: parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();  
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetRegistrationDDL()
        {
            try
            {
                using var con = _context.CreateConnection();
                using var multi = await con.QueryMultipleAsync("GetRegistrationDDL", commandType: CommandType.StoredProcedure);
                var Project = (await multi.ReadAsync<dynamic>()).ToList();
                var Disability = (await multi.ReadAsync<dynamic>()).ToList();
                var CauseOfDisability = (await multi.ReadAsync<dynamic>()).ToList();
                var Qualification = (await multi.ReadAsync<dynamic>()).ToList();
                var Religion = (await multi.ReadAsync<dynamic>()).ToList();
                return new { Project, Disability, CauseOfDisability, Qualification, Religion };
            }
            catch (Exception ex)
            {
                return (null);
            }

        }

        public async Task<List<RegistrationModel>> GetRegistrationDataAsync()
        {
            try
            {
                using (IDbConnection con = _context.CreateConnection())
                {
                    var result = await con.QueryAsync<RegistrationModel>(
                        "DownloadExcel",
                        commandType: CommandType.StoredProcedure
                    );
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
