using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class ConvergeQuery:BaseEntity
    {
        [Required(ErrorMessage = "Course faculty is required.")]
        public int? CourseFacultyId { get; set; }

        [Required(ErrorMessage = "Semester course is required.")]
        public int? SemesterCourseId { get; set; }

        [Required(ErrorMessage = "To user is required.")]
        public int? ToUserId { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [MaxLength(100, ErrorMessage = "Subject cannot exceed 200 characters.")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Body is required.")]
        [MinLength(5, ErrorMessage = "Body must be at least 5 characters long.")]
        public string? Body { get; set; }

        public bool? IsOwner { get; set; }

        public int? ThreadId { get; set; }

        public int? ThreadStatusId { get; set; }

        public bool? IsRead { get; set; } = false;

        [Required(ErrorMessage = "From user is required.")]
        public int? FromUserId { get; set; }

        public virtual List<string>? DocumentUrl { get; set; }
    }
}
