using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class SemesterCourse : BaseEntity
    {
        public int SemesterDesignID { get; set; }
        public int CourseFacultyID { get; set; }
    }
}
