using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class Cities:BaseEntity
    {
        public string? City { get; set; }
        public int StateId { get; set; }
    }
}
