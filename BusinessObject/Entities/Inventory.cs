using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessObjectsLayer.Entities
{
    public class Inventory
    {
        public int? Id { get; set; } 
        public string? ItemName { get; set; }
        public DateTime? Date { get; set; }
        public int? CategoryId { get; set; }
        public int? Quantity { get; set; }
        public int? ConditionId { get; set; }
        public int? StatusId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        







    }
}
