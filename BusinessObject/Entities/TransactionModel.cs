using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class TransactionModel  
    {
        public int? Id { get; set; } 
        public int? TransactionTypeId { get; set; } 
        public int? BankId { get; set; } 
        public int? Amount { get; set; } 
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

}
