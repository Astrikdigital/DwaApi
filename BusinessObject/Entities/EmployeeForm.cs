using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class EmployeeForm
    {
        public int? Id { get; set; }
        public int? DisignationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? EmployementTypeId { get; set; }
        public int? ContractTypeId { get; set; }
        public int? ShiftId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? ReligionId { get; set; }
        public int? CityId { get; set; }
        public int? StatusId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateofJoining { get; set; }
        public DateTime? DateofExit { get; set; }
        public int? Salary { get; set; }
        public string? Location { get; set; }
        public int? EmergencyContactNo { get; set; }
        public string? EmergencyContactRelation { get; set; }
        public string? CNIC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? FatherName { get; set; }
        public int? PersonalPhoneNumber { get; set; }
        public string? PersonalEmail { get; set; }
        public string? PermanentAddress { get; set; }
        public string? ResidentialAddress { get; set; }
        public string? Attachment { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
