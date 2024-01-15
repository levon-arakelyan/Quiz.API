using Quiz.Core.Enums.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Interfaces.UserManagement
{
    public interface IClaimsService
    {
        int? UserProfileId { get; }

        IEnumerable<UserRole> Roles { get; }

        bool HasRole(UserRole role);
    }
}
