using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class Assignment : BaseEntity
    {
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public int? TotalMark { get; set; }
        public int? PassingMark { get; set; }
        public DateTime? Deadline { get; set; }
        //public bool? IsCompleted { get; set; }
        public virtual string? DocumentUrl { get; set; }
        public virtual string? SemesterCourseIds { get; set; }
    }

    public class AssignmentDetail : BaseEntity
    {
        public int? SemesterCourseId { get; set; }
        public int? AssignmentId { get; set; }
    }

    public class AssignmentStudent : BaseEntity
    {
        public int? AssignmentDetailId { get; set; }
        public int? StudentId { get; set; }
        public int? SubmissionStatusId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string? SubmissionType { get; set; }
        public double? ObtainedMark { get; set; }
        public int? ResultStatusId { get; set; }
        public string? Document { get; set; }
        public string? Feedback { get; set; }
        public bool? IsResubmission { get; set; }
    }

    public class AssignmentDocument : BaseEntity
    {
        public int AssignmentId { get; set; }
        public string? DocumentUrl { get; set; }
    }

    public class ResultStatus : BaseEntity
    {
        public string? Title { get; set; }
    }
    public class SubmissionType : BaseEntity
    {
        public string? Title { get; set; }
    }

    public class SubmissionStatus : BaseEntity
    {
        public string? Title { get; set; }
    }

}
