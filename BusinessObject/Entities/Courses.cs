using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class CoursesResponseDto
    {
        public int ProgramId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public bool IsActive { get; set; }
        public string CoursePictureUrl { get; set; }
        public string CoursePictureName { get; set; }
        public string Description { get; set; }
    }
}
