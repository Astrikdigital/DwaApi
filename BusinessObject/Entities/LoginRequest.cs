using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "UserId must be between 5 and 100 characters.")]
        public string UserId { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        //[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one letter and one number.")]
        public string Password { get; set; }
    }
    public class ChangePassword
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Current password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Current password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Current password must be at least 8 characters long, contain at least one letter and one number.")]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "New password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "New password must be at least 8 characters long, contain at least one letter and one number.")]
        public string? NewPassword { get; set; }
    }
}
