using BusinessObjectsLayer.Entities;
using Dapper;
using DataAccess.DbContext;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ErrorLog;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Transactions;
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
        public async Task<dynamic> InsertUpdateBeneficiary(DisabledWelFareForm disabledWelfareForm)
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
                        FirstName = disabledWelfareForm.FirstName,
                        LastName = disabledWelfareForm.LastName,
                        BusinessName = disabledWelfareForm.BusinessName,
                        BusinessType = disabledWelfareForm.BusinessType,
                        BeneficiaryTypeId = disabledWelfareForm.BeneficiaryTypeId,
                        CountryId = disabledWelfareForm.CountryId,
                        CityId = disabledWelfareForm.CityId,
                    };
                    using (IDbConnection con = _context.CreateConnection())
                    {
                        var resp = (await con.QueryAsync<dynamic>("InsertUpdateBeneficiary", param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                        disabledWelfareForm.Id = resp.Id;
                        return disabledWelfareForm;
                    }

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
                var param = new
                {
                    Id = Id,
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    SearchText = searchText,
                    Gender = gender
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("GetBeneficiary", param: param, commandType: CommandType.StoredProcedure)).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<dynamic> DeleteBeneficiary(int Id)
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
                    await connection.ExecuteAsync("DeleteBeneficiary", parameters, commandType: CommandType.StoredProcedure);
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
                    Id = donation.Id,
                    DonationTypeId = donation.DonationTypeId,
                    DonationDetailTypeId = donation.DonationDetailTypeId,
                    DonorId = donation.DonorId,
                    DonationStatusId = donation.DonationStatusId,
                    TransactionId = donation.TransactionId,
                    Amount = donation.Amount,
                    Date = donation.Date,
                    ProjectId = donation.ProjectId,
                    Attachment = donation.Attachment,
                    InventoryId = donation.InventoryId,
                    BankId = donation.BankId,
                    IncomeTypeId = donation.IncomeTypeId,
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
                    Address = donor.Address,
                    PhoneNumber= donor.PhoneNumber,
                    CountryId = donor.CountryId,
                    CityId = donor.CityId,
                    DonorTypeId = donor.DonorTypeId,
                    PictureUrl = donor.PictureUrl
                };
                var Id =  (await con.QueryAsync<int>("InsertUpdateDonor",param: parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                if (donor.Id==null) { 
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
                donation.ProjectId = donor.ProjectId;
                donation.BankId = donor.BankId;
                    donation.IncomeTypeId  = donor.IncomeTypeId;
                    donation.DonorId = Id;       
                InsertUpdateDonation(donation);
                }
                return true;
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
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id = Id,
                    DonationStatusId = DonationStatusId
                };
                return (await con.QueryAsync("UpdateDonationStatus",param: parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetInventory(string? SearchText,int? PageNumber,int? PageSize)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    SearchText = SearchText,
                    PageNumber = PageNumber,
                    PageSize = PageSize

                };
                return (await con.QueryAsync("GetInventory",param: parameters,   commandType: CommandType.StoredProcedure)).ToList();
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
        public async Task<dynamic> GetDonor(int? Id,string SearchText, int? PageNumber, int? PageSize)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id=Id,
                    SearchText = SearchText,PageNumber = PageNumber,PageSize=PageSize
                };
                return (await con.QueryAsync("GetDonor",param: parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetDonation(int? Id,int? PageNumber,int? PageSize, string? SearchText)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id = Id,
                    PageNumber=PageNumber,
                    PageSize= PageSize,
                    SearchText = SearchText
                };
                return (await con.QueryAsync("GetDonation",param: parameters,   commandType: CommandType.StoredProcedure)).ToList();
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
                return (await con.QueryAsync("InsertupdateInventory", param: parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();  
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetBeneficiaryDDL()
        {
            try
            {
                using var con = _context.CreateConnection();
                using var multi = await con.QueryMultipleAsync("GetBeneficiaryDDL", commandType: CommandType.StoredProcedure);
                var Project = (await multi.ReadAsync<dynamic>()).ToList();
                var Disability = (await multi.ReadAsync<dynamic>()).ToList();
                var CauseOfDisability = (await multi.ReadAsync<dynamic>()).ToList();
                var Qualification = (await multi.ReadAsync<dynamic>()).ToList();
                var Religion = (await multi.ReadAsync<dynamic>()).ToList();
                var TransactionType = (await multi.ReadAsync<dynamic>()).ToList();
                var Banks = (await multi.ReadAsync<dynamic>()).ToList();
                var Donors = (await multi.ReadAsync<dynamic>()).ToList();
                return new { Project, Disability, CauseOfDisability, Qualification, Religion, TransactionType, Banks, Donors };
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


        #region EmployeeRepository

        public async Task<dynamic> GetEmployee(string? searchText = null, int? stafTypeId = null, int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var param = new
                {
                    searchText = searchText,
                    stafTypeId = stafTypeId,
                    PageSize = PageSize,
                    PageNumber = PageNumber
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("API_SELECT_Employee", param: param, commandType: CommandType.StoredProcedure)).ToList();
                    return res;
                }
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
                var param = new
                {
                    Id = Id
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("GetEmployeeById", param: param, commandType: CommandType.StoredProcedure)).ToList();
                    return res;
                }
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
                try
                {
                    var param = new
                    {
                        Id = employeeForm.Id,
                        DesignationId = employeeForm.DesignationId,
                        DepartmentId = employeeForm.DepartmentId,
                        EmployementTypeId = employeeForm.EmployementTypeId,
                        ContractTypeId = employeeForm.ContractTypeId,
                        ShiftId = employeeForm.ShiftId,
                        GenderId = employeeForm.GenderId,
                        MaritalStatusId = employeeForm.MaritalStatusId,
                        ReligionId = employeeForm.ReligionId,
                        CityId = employeeForm.CityId,
                        StatusId = employeeForm.StatusId,
                        Name = employeeForm.Name,
                        Email = employeeForm.Email,
                        Phone = employeeForm.Phone,
                        DateOfJoining = employeeForm.DateOfJoining,
                        DateOfExit = employeeForm.DateOfExit,
                        Salary = employeeForm.Salary,
                        Location = employeeForm.Location,
                        EmergencyContactNo = employeeForm.EmergencyContactNo,
                        EmergencyContactRelation = employeeForm.EmergencyContactRelation,
                        CNIC = employeeForm.CNIC,
                        DateOfBirth = employeeForm.DateOfBirth,
                        FatherName = employeeForm.FatherName,
                        PersonalPhoneNumber = employeeForm.PersonalPhoneNumber,
                        PersonalEmail = employeeForm.PersonalEmail,
                        PermanentAddress = employeeForm.PermanentAddress,
                        ResidentialAddress = employeeForm.ResidentialAddress,
                        Attachment = employeeForm.Attachment,
                        ProfilePicture = employeeForm.ProfilePicture,

                    };
                    using (IDbConnection con = _context.CreateConnection())
                    {
                        return (await con.QueryAsync<dynamic>("API_Insert_Update_Employee", param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
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
        #endregion

        #region OG_TABLES

        public async Task<dynamic> GetDesignation()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_Designation", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }
        public async Task<dynamic> Getdepartment()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_Department", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        public async Task<dynamic> GetGender()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_Gender", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }
        public async Task<dynamic> GetShift()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_Shift", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        public async Task<dynamic> GetStatus()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_Status", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }
        public async Task<dynamic> GetCities()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_Cities", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }
        public async Task<dynamic> GetReligion()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_Religion", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        public async Task<dynamic> GetMaritalStatus()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_MaritalStatus", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        
        public async Task<dynamic> GetDonationType()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_DonationType", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }
        
        public async Task<dynamic> GetDonationDetailType()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_DonationDetailType", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }
        public async Task<dynamic> GetDonationStatus()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_DonationStatus", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }
        public async Task<dynamic> ContractType()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("API_Select_ContractType", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        public async Task<dynamic> GetEmploymentType()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("GetEmploymentType", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        public async Task<dynamic> GetAssingment()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("GetAssignment", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        public async Task<dynamic> InsertOGTable(OGTables ogTable)
        {
            try
            {
                try
                {
                    var param = new
                    {
                        tableName = ogTable.TableName,
                        titlename = ogTable.TitleName,
                    };
                    using (IDbConnection con = _context.CreateConnection())
                    {
                        return (await con.QueryAsync<dynamic>("API_Insert_OG", param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
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




        #endregion


        #region Volunteer


        public async Task<dynamic> InsertVolunteer(VolunteerForm volunteerForm)
        {
            try
            {
               
                    var param = new
                    {
                        Id = volunteerForm.Id,
                        AvailabilityDayIds = volunteerForm.AvailabilityDayIds,
                        VolunteerRoleId = volunteerForm.VolunteerRoleId,
                        DepartmentId = volunteerForm.DepartmentId,
                        ProjectAssigmentId = volunteerForm.ProjectAssigmentId,
                        GenderId = volunteerForm.GenderId,
                        MaritalStatusId = volunteerForm.MaritalStatusId,
                        ReligionId = volunteerForm.ReligionId,
                        CityId = volunteerForm.CityId,
                        StatusId = volunteerForm.StatusId,
                        Name = volunteerForm.Name,
                        Email = volunteerForm.Email,
                        Phone = volunteerForm.Phone,
                        DateOfJoining = volunteerForm.DateOfJoining,
                        DateOfExit = volunteerForm.DateOfExit,
                        Location = volunteerForm.Location,
                        EmergencyContactNo = volunteerForm.EmergencyContactNo,
                        EmergencyContactRelation = volunteerForm.EmergencyContactRelation,
                        Cnic = volunteerForm.Cnic,
                        AvailabilityTime = volunteerForm.AvailabilityTime,
                        DateOfBirth = volunteerForm.DateOfBirth,
                        FatherName = volunteerForm.FatherName,
                        PersonalPhoneNumber = volunteerForm.PersonalPhoneNumber,
                        PersonalEmail = volunteerForm.PersonalEmail,
                        PermanentAddress = volunteerForm.PermanentAddress,
                        ResidentialAddress = volunteerForm.ResidentialAddress,
                        Attachment = volunteerForm.Attachment,
                        ProfilePicture = volunteerForm.ProfilePicture

                    };
                    using (IDbConnection con = _context.CreateConnection())
                    {
                        return (await con.QueryAsync<dynamic>("InsertVolunteer", param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }



        }

        public async Task<dynamic> GetAllVolunteers(string? searchText = null,int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var param = new
                {
                    searchText = searchText,
                    PageSize = PageSize,
                    PageNumber = PageNumber
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("GetAllVolunteers", param: param, commandType: CommandType.StoredProcedure)).ToList();
                    return res;
                }
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
                var param = new
                {
                    Id = Id
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("GetVolunteerById", param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        public async Task<dynamic> GetIncomeTypes()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("GetIncomeTypes", commandType: CommandType.StoredProcedure)).ToList();
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
                using var con = _context.CreateConnection();
                using var multi = await con.QueryMultipleAsync("GetInventoryUtilizationDll", commandType: CommandType.StoredProcedure);
                var mainhead = (await multi.ReadAsync<dynamic>()).ToList(); 
                var head = (await multi.ReadAsync<dynamic>()).ToList(); 
                var subhead = (await multi.ReadAsync<dynamic>()).ToList(); 
                var employee = (await multi.ReadAsync<dynamic>()).ToList(); 
                var project = (await multi.ReadAsync<dynamic>()).ToList();
                var beneficiary = (await multi.ReadAsync<dynamic>()).ToList();
                var inventory = (await multi.ReadAsync<dynamic>()).ToList();
                var banks = (await multi.ReadAsync<dynamic>()).ToList();
                return new { mainhead, head, subhead, employee, project , beneficiary, inventory,banks };
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        
         
        public async Task<dynamic> InsertUpdateTransaction(TransactionModel transaction)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id = transaction.Id,
                    TransactionTypeId = transaction.TransactionTypeId,
                    BankId = transaction.BankId,
                    Amount = transaction.Amount,
                    ProjectId = transaction.ProjectId,
                    TransactionId = transaction.TransactionId,
                    DonationId = transaction.DonationId,
                    MainHeadId = transaction.MainHeadId,
                    HeadId = transaction.HeadId,
                    Date = transaction.Date,
                    SubHeadId = transaction.SubHeadId
                };
                return (await con.QueryAsync("InsertUpdateTransaction",param: parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> InsertUpdateInventoryUtilization(InventoryUtilization utili)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id = utili.Id,
                    BeneficiaryId = utili.BeneficiaryId,
                    Quantity =  utili.Quantity  ,
                    ProjectId = utili.ProjectId,
                    InventoryId = utili.InventoryId
                };
                return (await con.QueryAsync("InsertUpdateInventoryUtilization", param: parameters, commandType: CommandType.StoredProcedure)).ToList();
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
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    DonationYear = DonationYear,
                    ExpenseYear = ExpenseYear,
                    ExpenseMonth = ExpenseMonth
                };
                using var multi = await con.QueryMultipleAsync("GetDashboard",param:parameters, commandType: CommandType.StoredProcedure);
                var head = (await multi.ReadAsync<dynamic>()).ToList(); 
                var chartdonation = (await multi.ReadAsync<dynamic>()).ToList(); 
                var chartexpense = (await multi.ReadAsync<dynamic>()).ToList(); 
                var summary = (await multi.ReadAsync<dynamic>()).ToList(); 
                return new { head, chartdonation, chartexpense, summary};
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
                using var con = _context.CreateConnection();

                return (await con.QueryAsync("GetProject", commandType: CommandType.StoredProcedure)).ToList();
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
                using var con = _context.CreateConnection();

                return (await con.QueryAsync("GetBanks", commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
       public async Task<dynamic> GetBenificiaryType()
        {
            try
            {
                using var con = _context.CreateConnection();
                return (await con.QueryAsync("select Id,Title from BeneficiaryType where IsActive = 1", commandType: CommandType.Text)).ToList();
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
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id = Id,
                    PageNumber = PageNumber,PageSize = PageSize
                };
                return (await con.QueryAsync("GetDebitTransactions",param: parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetInventoryUtilization(int? Id,int? PageNumber,int? PageSize)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id = Id,
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };
                return (await con.QueryAsync("GetInventoryUtilization",param: parameters, commandType: CommandType.StoredProcedure)).ToList();
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
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    CountryId = CountryId
                };
                return (await con.QueryAsync("GetCityByCountryId", param: parameters, commandType: CommandType.StoredProcedure)).ToList();
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
                using var con = _context.CreateConnection(); 
                 
                using var multi = await con.QueryMultipleAsync("GetDonorDll",  commandType: CommandType.StoredProcedure);
                var Project = (await multi.ReadAsync<dynamic>()).ToList(); 
                var DonationType = (await multi.ReadAsync<dynamic>()).ToList(); 
                var DonationDetailType = (await multi.ReadAsync<dynamic>()).ToList(); 
                var Inventory = (await multi.ReadAsync<dynamic>()).ToList();
                var DonationStatus = (await multi.ReadAsync<dynamic>()).ToList();
                var IncomeType = (await multi.ReadAsync<dynamic>()).ToList();
                var Bank = (await multi.ReadAsync<dynamic>()).ToList();
                var Countries = (await multi.ReadAsync<dynamic>()).ToList();
                var DonorType = (await multi.ReadAsync<dynamic>()).ToList();
                return new { Project, DonationType, DonationDetailType, 
                    Inventory, DonationStatus, IncomeType,Bank ,Countries,DonorType
                };
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
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("GetCountry",  commandType: CommandType.StoredProcedure)).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> GetAllCNIC(string? cnic)
        {
            try
            {
                using (IDbConnection con = _context.CreateConnection())
                {
                    var parameters = new
                    {
                        CNIC = cnic
                    };
                    var res = (await con.QueryAsync<int>("Get_ALL_CNIC",param:parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<dynamic> GetAllTransactions(int? bankId = null, int? transactionTypeId = null, int PageSize = 20, int? PageNumber = 0)
        {
            try
            {
                var param = new
                {
                    bankId = bankId,
                    transactionTypeId = transactionTypeId,
                    PageSize = PageSize,
                    PageNumber = PageNumber
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("GetTransactions", param: param, commandType: CommandType.StoredProcedure)).ToList();
                    return res;
                }
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
                var param = new
                {
                    donorId = donorId,
                    PageSize = PageSize,
                    PageNumber = PageNumber
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    var res = (await con.QueryAsync<dynamic>("GetBankDepositSlip", param: param, commandType: CommandType.StoredProcedure)).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }


}
