using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class SemesterDesign : BaseEntity
    {
        public int BatchId { get; set; }
        public int SemesterId { get; set; }
        public int SectionId { get; set; }
        public int ProgramId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<int>? CourseFacultyIds { get; set; }
    }
}
