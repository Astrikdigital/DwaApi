using BusinessObjectsLayer.Entities;
using Dapper;
using DataAccess.DbContext;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
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
        public async Task<dynamic> InsertUpdateDonation(Donation donation)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    DonationTypeId = donation.DonationTypeId,
                    DonationDetailTypeId = donation.DonationDetailTypeId,
                    TransactionId = donation.TransactionId,
                    Amount = donation.Amount,
                    Date = donation.Date,
                    Attachment = donation.Attachment,
                    InventoryId = donation.InventoryId,
                    Quantity = donation.Quantity
                };
                var Id = (await con.QueryAsync("InsertUpdateDonation", param: parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return true;
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
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id=donor.Id,
                    Name=   donor.Name,
                    Email= donor.Email,
                    PhoneNumber= donor.PhoneNumber,
                    PictureUrl= donor.PictureUrl
                };
                var Id =  (await con.QueryAsync("InsertUpdateDonor",param: parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                Donation donation = new Donation(); 
                donation.InventoryId = donor.InventoryId;
                donation.DonationTypeId = donor.DonationTypeId;
                donation.DonationStatusId = donor.DonationStatusId;
                donation.DonationDetailTypeId = donor.DonationDetailTypeId;
                donation.Date = donor.Date;
                donation.Quantity = donor.Quantity;
                donation.TransactionId = donor.TransactionId;
                donation.Amount = donor.Amount;
                donation.Attachment = donor.Attachment;
                donation.DonorId = Id;       
                InsertUpdateDonation(donation);
                return true;
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
                using var con = _context.CreateConnection(); 
                return (await con.QueryAsync("GetInventory",   commandType: CommandType.StoredProcedure)).ToList();
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
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    tableName = TableName,Id= Id
                };
                return (await con.QueryAsync("DeleteTableRow", param: parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetDonor(string SearchString)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    SearchString = SearchString
                };
                return (await con.QueryAsync("GetDonor",param: parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }

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
