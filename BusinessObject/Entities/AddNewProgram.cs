using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class AddNewProgramDto
    {
        public int? Id { get; set; }
        public string ProgramName { get; set; }
        public bool IsActive { get; set; }
        public int TotalSemesters { get; set; }
        public int TotalCourses { get; set; }
    }

    public class BatchDto
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; }
        public string BatchTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class SemesterDto
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; }
        public string SemesterTitle { get; set; }
    }
    public class SectionDto
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; }
        public string SectionTitle { get; set; }
    }
    public class SemesterMappedCourses
    {
        
        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? SectionId { get; set; }
        public int? ProgramId { get; set; }
        public int? SemesterDesignId { get; set; }
    }
    public class NewSemesterDesignDto
    {
        public int BatchId { get; set; }
        public int SemesterId { get; set; }
        public int SectionId { get; set; }
        public int ProgramId { get; set; }
        public string CourseFacultyMappingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public int IsActive { get; set; }
        //public string CreatedBy { get; set; }
    }
    public class NewSemesterDesign
    {
        public int Id { get; set; }
        public int? BatchId { get; set; }
        public string? BatchTitle { get; set; }
        public int SemesterId { get; set; }
        public string? SemesterTitle { get; set; }
        public int? SectionId { get; set; }
        public int? SemesterStatusId { get; set; }
        public string? SectionTitle { get; set; }
        public int ProgramId { get; set; }
        public string? ProgramTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TotalRecords { get; set; }
    }
    public class SemesterMappedCoursesDto
    {
        public int? Id { get; set; }
        public int? CourseFacultyId { get; set; }
        public string? ProgramTitle { get; set; }
        public int CourseId { get; set; }
        public string? FacultyName { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }
        public string? PictureUrl { get; set; }
    }
    

    public class BatchRoasterDto
    {
        public int? Id { get; set; }
        public string? Session { get; set; }
        public string? Monday { get; set; }
        public string? Tuesday { get; set; }
        public string? Wednesday { get; set; }
        public string? Thrusday { get; set; }
        public string? Friday { get; set; }
        public string? Saturday { get; set; }
        public string? Sunday { get; set; }
    }

    public class GetBatchRoasterDto
    {
       
        public string? Session { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? HTML { get; set; }
    }

}
