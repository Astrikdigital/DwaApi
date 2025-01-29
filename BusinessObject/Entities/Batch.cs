using BusinessObjectsLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class Batch: BaseEntity
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string? BatchTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Year must be a valid 4-digit number.")]
        public string? Year { get; set; }
        public virtual int TotalRecords { get; set; }
    }
}