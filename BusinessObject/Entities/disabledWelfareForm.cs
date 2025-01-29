using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class DisabledWelFareForm
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public string? Image { get; set; }
        public DateTime? Date { get; set; }
        public string? SeriolNo { get; set; }
        public string? CodeNo { get; set; }
        public string? CNIC { get; set; }
        public int? GenderId { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        public int? ReligionId { get; set; }
        public int? QualificationId { get; set; }
        public string? Experience { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? WhatsAppNo { get; set; }
        public string? Email { get; set; }
        public int? DisabilityId { get; set; }
        public int? CauseDisabilityId { get; set; }
        public string? Reference { get; set; }
        public string? NeedsRemarks { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
