using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class Registration
    {
        public int? Id { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
        public string? SearchText { get; set; }
        public string? Gender { get; set; }
    }

    public class RegistrationModel
    {
        public int Id { get; set; }
        public string Project { get; set; }
        //public string Image { get; set; }
        public DateTime Date { get; set; }
        public string SerialNo { get; set; }
        public string CodeNo { get; set; }
        public string CNIC { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string Religion { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string WhatsAppNo { get; set; }
        public string Email { get; set; }
        public string Disability { get; set; }
        public string CauseDisability { get; set; }
        public string Reference { get; set; }
        public string NeedsRemarks { get; set; }
      //  public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
       // public string ModifiedBy { get; set; }
       // public DateTime? ModifiedOn { get; set; }
    }

}
