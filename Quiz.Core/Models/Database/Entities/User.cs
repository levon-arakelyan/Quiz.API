using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Models.Database.Entities
{
    public class User : AuditableUtcEntity
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is not User other)
            {
                return false;
            }
            return other.FirstName == FirstName && other.LastName == LastName;
        }

        public override int GetHashCode()
        {
            int fNameHashCode = FirstName == null ? 0 : FirstName.GetHashCode();
            int lNameHashCode = LastName == null ? 0 : LastName.GetHashCode();
            return fNameHashCode ^ lNameHashCode;
        }
    }
}
