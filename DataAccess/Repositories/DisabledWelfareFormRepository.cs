﻿using BusinessObjectsLayer.Entities;
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
        public async Task<dynamic> InsertUpdateBeneficiary(DisabledWelFareForm disabledWelfareForm)
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
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<dynamic> GetBeneficiary(int? Id = null, string? SerachText = null, int? PageSize = 20, int? PageNumber = 1, string gender = null)
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
                    PhoneNumber= donor.PhoneNumber,
                    PictureUrl= donor.PictureUrl
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
        public async Task<dynamic> GetDonor(int? Id,string SearchText)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id=Id,
                    SearchText = SearchText
                };
                return (await con.QueryAsync("GetDonor",param: parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception ex)
            {
                return (null);
            }

        }
        public async Task<dynamic> GetDonation(int? Id)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    Id = Id
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


        #region EmployeeRepository

        public async Task<dynamic> GetEmployee(string? serachText = null, int? stafTypeId = null, int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var param = new
                {
                    searchText = serachText,
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

        public async Task<dynamic> GetAllVolunteers(string? serachText = null,int PageSize = 20, int? PageNumber = 1)
        {
            try
            {
                var param = new
                {
                    searchText = serachText,
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
    }


}
