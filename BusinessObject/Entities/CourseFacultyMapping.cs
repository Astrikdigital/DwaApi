using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class CourseFacultyMapping : BaseEntity
    {
        public int FacultyId { get; set; }
        public int CourseId { get; set; }
    }
}
