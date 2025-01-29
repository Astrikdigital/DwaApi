using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class ProgramCourses: BaseEntity
    {
        public int CourseId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string PictureUrl { get; set; }
        public bool IsEnabled { get; set; }
        public int TotalRecords { get; set; }
        public string Description { get; set; }
    }
}
