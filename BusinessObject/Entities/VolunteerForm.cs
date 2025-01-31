using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessObjectsLayer.Entities
{
    public class VolunteerForm
    {
        public int? Id { get; set; }
        public int? VolunteerRoleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProjectAssigmentId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? ReligionId { get; set; }
        public int? CityId { get; set; }
        public int? StatusId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfExit { get; set; }
        public string? Location { get; set; }
        public string? EmergencyContactNo { get; set; }
        public string? EmergencyContactRelation { get; set; }
        public string? Cnic { get; set; }
        public string? AvailabilityTime { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? FatherName { get; set; }
        public string? PersonalPhoneNumber { get; set; }
        public string? PersonalEmail { get; set; }
        public string? PermanentAddress { get; set; }
        public string? ResidentialAddress { get; set; }
        public string? Attachment { get; set; }
        public IFormFile? Profile { get; set; }
        public string? AvailabilityDayIds { get; set; } // Comma-separated values
        public string? ProfilePicture { get; set; }
        public IFormFile? attachmentUrl { get; set; }
    }
}
