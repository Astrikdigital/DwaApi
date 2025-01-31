using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Transactions;
using Microsoft.AspNetCore.Http;

namespace BusinessObjectsLayer.Entities
{
    public class Donor  
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? PhoneNumber { get; set; }
        public string? PictureUrl { get; set; }  
    }
    public class Donation
    {
        public int? Id { get; set; }
        public int? DonationTypeId { get; set; }
        public int? DonationDetailTypeId { get; set; }
        public string? TransactionId { get; set; }
        public int? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? DonorId { get; set; }
        public int? DonationStatusId { get; set; }
        public string? Attachment { get; set; }
        public IFormFile? AttachmentDocument { get; set; }
        public int? InventoryId { get; set; }
        public int? Quantity { get; set; } 

    }
    public class DonorModel
    {
        public int? Id { get; set; }
        public IFormFile? AttachProfilePicture { get; set; }
        public IFormFile? AttachmentDocument { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PictureUrl { get; set; }
        public int? DonationTypeId { get; set; }
        public int? DonationStatusId { get; set; }

        public int? DonationDetailTypeId { get; set; }
        public string? TransactionId { get; set; }
        public int? InventoryId { get; set; }
        public string? DonorId { get; set; }
        public int? Amount { get; set; }
        public int? Quantity { get; set; }
        
        public DateTime? Date { get; set; }
        public string? Attachment { get; set; }

    }
}



 

