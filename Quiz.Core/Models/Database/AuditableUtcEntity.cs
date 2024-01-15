using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Models.Database
{
    public class AuditableUtcEntity
    {
        public DateTime CreatedOnUtc { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
