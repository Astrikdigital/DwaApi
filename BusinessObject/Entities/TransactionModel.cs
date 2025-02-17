using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BusinessObjectsLayer.Entities
{
    public class TransactionModel  
    {
        public int? Id { get; set; } 
        public int? TransactionTypeId { get; set; } 
        public int? BankId { get; set; } 
        public int? Amount { get; set; } 
        public DateTime? Date { get; set; }
        public int? ProjectId { get; set; } 
        public int? DonationId { get; set; } 
        public int? MainHeadId { get; set; } 
        public int? HeadId { get; set; } 
        public int? SubHeadId { get; set; } 
        public string?  TransactionId { get; set; } 
      
    }

    public class InventoryUtilization
    {
        public int? Id { get; set; }
        public int? BeneficiaryId { get; set; }
        public int? Quantity { get; set; } 
        public int? ProjectId { get; set; } 
        public int? InventoryId { get; set; } 

    }

    public class Bank
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? Amount { get; set; }
        public string? Title { get; set; }
        public string? IBAN { get; set; }
        public string? Account { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }

    }


    public class Projects
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
    }

    public class HeadsModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public string? MainHeadId { get; set; }
        public string? HeadId { get; set; }

        
    }

    public class Designation
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? UserId { get; set; }


    }

}
