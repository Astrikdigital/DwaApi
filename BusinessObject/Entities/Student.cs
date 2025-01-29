using System.ComponentModel.DataAnnotations;

namespace BusinessObjectsLayer.Entities
{
    public class Student : BaseEntity
    {
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name can only contain alphabetic characters.")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        
        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last name can only contain alphabetic characters.")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        
        [Required(ErrorMessage = "Gender is required.")]
        public int GenderId { get; set; }
        
        public string? PictureUrl { get; set; }

        
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 digits.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number can only contain numeric characters.")]
        public string Phone { get; set; }

        
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(400, ErrorMessage = "Address cannot be longer than 400 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.'-]*$", ErrorMessage = "Street address contains invalid characters. Only letters, numbers, spaces, commas, periods, and hyphens are allowed.")]
        public string Address { get; set; }

        
        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }
        
        
        [Required(ErrorMessage = "Country is required.")]
        public int CountryId { get; set; }

        
        [Required(ErrorMessage = "State is required.")]
        public int StateId { get; set; }

        
        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(10, ErrorMessage = "Postal Code cannot be longer than 10 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Postal Code can only contain alphanumeric characters.")]
        public string PostalCode { get; set; }

        
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 digits.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number can only contain numeric characters.")]
        public string EmergencyPhone { get; set; }

        
        [Required(ErrorMessage = "Emergency Person Relation is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Emergency Person Relation can only contain alphabetic characters.")]
        public string EmergencyPersonRelation { get; set; }


        [Required(ErrorMessage = "User name is required.")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "User name can only contain alphabetic characters.")]
        [StringLength(200, ErrorMessage = "User Name must be between 1 and 200 characters.", MinimumLength = 3)]
        public string? UserName { get; set; }
    }


    public class StudentEnrollment : BaseEntity
    {
        public string? EnrollmentNumber { get; set; }
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
        public int BatchId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }

    public class StudentEnrollmentCourse : BaseEntity
    {
        public int StudentEnrollmentId { get; set; }
        public int SemesterCourseId { get; set; }
        public int SemesterId { get; set; }
        public int CourseStatusId { get; set; }
        public virtual List<int> SemesterCourseIds { get; set; }
        public virtual int CourseId { get; set; }
    }
    public class StudentEnrollmentDto : BaseEntity
    {
        public string? ProgramTitle { get; set; }
        public string? BatchTitle { get; set; }
        public string? EnrollmentNumber { get; set; }
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
        public int BatchId { get; set; }
        public DateTime? EnrollmentDate { get; set; } = DateTime.UtcNow;
    }

    public class GetAllStudent : BaseEntity
    {
        public int? TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public string? FullName { get; set; }
        public string? EnrollmentNumber { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string? Email { get; set; }
        public string? EnrolledProgram { get; set; }
        public string? BatchNo { get; set; }
        public string? PictureUrl { get; set; }
    }
    public class StudentUploadAssignmentDto : BaseEntity
    {
        public string? DocumentUrl { get; set; }
    }
}
