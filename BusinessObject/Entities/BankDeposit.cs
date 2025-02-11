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
    public class BankDeposit
    { 
        public string? TransactionId { get; set; }
        public int? BankId { get; set; } 
        public string? Slip { get; set; }  
        public IFormFile? DocSlip { get; set; }
    } 
}



 

