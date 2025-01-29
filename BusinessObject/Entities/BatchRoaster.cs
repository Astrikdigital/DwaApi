using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class BatchRoaster: BaseEntity
    {
        public int SemesterDesignId { get; set; }
        public string? Day { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? Date { get; set; }
        public int RoomId { get; set; }
        public int BatchRoasterStatusId { get; set; }
        public int? RoasterType { get; set; }
        public string? Title { get; set; }
        public int? FacultyId { get; set; }
        public int CourseFacultyId { get; set; }
        public virtual int TotalRecords { get; set; }
        public virtual string? RoomName { get; set; }
        
    }
}
