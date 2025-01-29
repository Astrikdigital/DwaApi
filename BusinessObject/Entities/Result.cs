using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class Result
    {
        public int? Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public object? Data { get; set; }
        public object? List { get; set; }
        public int RecordCount { get; set; }
        public int EntityId { get; set; }
    }
}
