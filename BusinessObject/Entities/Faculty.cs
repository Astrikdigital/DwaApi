using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class Faculty : BaseEntity
    {
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name can only contain alphabetic characters.")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name can only contain alphabetic characters.")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string? Email { get; set; }
        public string? PictureUrl { get; set; }
        public string? FacultyCode { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Date Of Joining is required.")]
        public DateTime? DateOfJoining { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 digits.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number can only contain numeric characters.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Street address is required.")]
        [StringLength(400, ErrorMessage = "Street address cannot be longer than 400 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.'-]*$", ErrorMessage = "Street address contains invalid characters. Only letters, numbers, spaces, commas, periods, and hyphens are allowed.")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Country residence is required.")]
        public int CountryResidenceId { get; set; }

        [Required(ErrorMessage = "Nationality is required.")]
        public int NationalityId { get; set; }

        
        [Required(ErrorMessage = "User name is required.")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "User Name can only contain alphabetic characters.")]
        [StringLength(200, ErrorMessage = "User Name must be between 1 and 200 characters.", MinimumLength = 3)]
        public string UserName { get; set; }

        [JsonIgnore]
        public virtual List<int>? CourseIds { get; set; }

        //public int? GenderId { get; set; }
        //public string? Phone { get; set; }
        //public string? StreetAddress { get; set; }
        //public int CountryId { get; set; }
        //public int CountryResidenceId { get; set; }
        //public int NationalityId { get; set; }
        //public string? UserName { get; set; }
        //[JsonIgnore]
        ////public virtual string? CourseIds { get; set; }
        //public virtual List<int>? CourseIds { get; set; }

    }

    public class FacultyResponseDto
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GenderId { get; set; }
        public string? Phone { get; set; }
        public string? StreetAddress { get; set; }
        public int? CountryId { get; set; }
        public int? CountryResidenceId { get; set; }
        public int? NationalityId { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public List<int>? CourseIds { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GetAllFaculties
    {
        public int? Id { get; set; }
        public int? TotalRecords { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public int? GenderId { get; set; }
        public int? NationalityId { get; set; }
        public int? CountryResidenceId { get; set; }
        public int? CountryId { get; set; }
        public string? PictureUrl { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? StreetAddress { get; set; }
        public string? Country { get; set; }
        public int? CurrentCountryResidence { get; set; }
        public int? Nationality { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public List<int>? CourseIds { get; set; } 
    }

    public class FacultyCourseMapping : BaseEntity
    {
        public string? ProgramTitle { get; set; }
        public int FacultyId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }
    }

    public class CourseFacultyDto
    {
        public int? CourseFacultyId { get; set; }
        public string? CourseFaculty { get; set; }
    }

    public class SemesterDesignDto
    {
        public int? SemesterDesignId { get; set; }
        public string? Title { get; set; }
    }


    public class FaculyDashboadrHeadCounts
    {
        public int? FacultyMemberSince { get; set; }
        public int? AssingedCourses { get; set; }
        public int? ActiveStudents { get; set; }
        public int? AssignmentsToCheck { get; set; }

    }
    public class CourseMaterial : BaseEntity
    {
        public int? CourseFacultyId { get; set; }
        public int? SemesterCourseId { get; set; }
        public int? CourseMaterialTypeId { get; set; }
        public int? ReadingTypeId { get; set; }
        public string? Topic { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Link { get; set; }
        public virtual string? DocumentUrl { get; set; }
    }

    public class CourseMaterialDocument : BaseEntity
    {
        public int? CourseMaterialId { get; set; }
        public string? DocumentUrl { get; set; }
    }

    public class CourseMaterialType : BaseEntity
    {
        public string? Title { get; set; }
    }

    public class ReadingType : BaseEntity
    {
        public string? Title { get; set; }
    }


    public class CourseMaterialResponse
    {
        public int? CourseMaterialTypeId { get; set; }
        public string? MaterialTypeTitle { get; set; }
        public int? ReadingTypeId { get; set; }
        public string? ReadingTypeTitle { get; set; }
        public int? Id { get; set; }
        public string? Topic { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Link { get; set; }
        public string? DocumentUrl { get; set; }
    }


}
