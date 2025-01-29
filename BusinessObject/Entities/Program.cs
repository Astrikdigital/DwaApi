using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class Program : BaseEntity
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Program short code is required.")]
        [StringLength(50, ErrorMessage = "Program Short Code cannot exceed 50 characters.")]
        public string? ProgramShortCode { get; set; }

        [Required(ErrorMessage = "Total courses is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Total Courses must be a positive integer.")]
        public int? TotalCourses { get; set; }

        [Required(ErrorMessage = "Total semester is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Total Semesters must be a positive integer.")]
        public int? TotalSemesters { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }
        public string? ProgramPicUrl { get; set; }
        public bool? IsEnabled { get; set; } = true;
        public virtual int? TotalRecords { get; set; }
    }

    public class NewlyEnrolledStudents
    {
        public string? PictureUrl { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? StudentName { get; set; }
        public DateTime? DateOfAdmission { get; set; }
        public string? Program { get; set; }
    }

    public class HeaderCounts
    {
        public int? TotalStudents { get; set; }
        public int? TotalCourses { get; set; }
        public int? TotalPrograms { get; set; }
        public int? TotalFaculty { get; set; }
    }
    public class ProgramInfo
    {
        public string? ProgramPicUrl { get; set; }
        public string? ProgramName { get; set; }
        public int? StudentCount { get; set; }
    }
    public class FacultyInfo
    {
        public string? ProgramPicUrl { get; set; }
        public string? FacultyName { get; set; }
        public int? CourseCount { get; set; }
    }
    public class Notification
    {
        public string? IconUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedOn { get; set; }
    }


    public class GetMappedBatches
    {
        public int? BatchId { get; set; }
        public string? BatchTitle { get; set; }
    }

    public class GetMappedSemesters
    {
        public int SemesterId { get; set; }
        public string? SemesterTitle { get; set; }
    }

    public class GetMappedSection
    {
        public int SectionId { get; set; }
        public string? SectionTitle { get; set; }
    }
    public class Days
    {
        public int Id { get; set; }
        public string? Day { get; set; }
    }


    public class BatchesByProgramIds : BaseEntity
    {
        public string? Title { get; set; }
        public int CourseId { get; set; }
    }

    public class BatchroasterStatus
    {
        public int? Id { get; set; }
        public string? Status { get; set; }
    }

}

