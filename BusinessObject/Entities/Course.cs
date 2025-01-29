using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class Course : BaseEntity
    {
        [Required(ErrorMessage = "Program is required.")]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course code is required.")]
        //[RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Course code can only contain alphabetic characters.")]
        [StringLength(10, ErrorMessage = "Course code cannot exceed 10 characters.")]
        public string CourseCode { get; set; }
        
        public string? PictureUrl { get; set; }
        
        public string? Description { get; set; }

        [Required(ErrorMessage = "Enabled is required.")]
        public bool IsEnabled { get; set; }
        public virtual bool IsDisabled { get; set; }
    }
}
