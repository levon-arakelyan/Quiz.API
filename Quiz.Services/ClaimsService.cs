using Microsoft.AspNetCore.Http;
using Quiz.Core.Enums.UserManagement;
using Quiz.Core.Interfaces.UserManagement;
using System.Security.Claims;

namespace Quiz.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private ClaimsPrincipal _principal => _httpContextAccessor.HttpContext?.User;

        public int? UserProfileId => GetHeaderKeyVal("UserProfileId");
        public IEnumerable<UserRole> Roles
        {
            get
            {
                List<UserRole> roles = new List<UserRole>();
                return Enum.GetNames(typeof(UserRole)).Select(roleString => Enum.TryParse(roleString, out UserRole role) ? role : UserRole.Participant);
            }
        }

        public bool HasRole(UserRole role)
        {
            return _principal.IsInRole(role.ToString());
        }

        private int? GetHeaderKeyVal(string headerKey)
        {
            int? headerVal = null;
            var headers = _httpContextAccessor.HttpContext?
                .Request?
                .Headers?
                .TryGetValue(headerKey, out var authValue);
            if (headers.HasValue)
            {
                var headerStringValue = authValue.Count > 0 ? authValue[0] : null;
                if (int.TryParse(headerStringValue, out var iHeaderValue))
                {
                    headerVal = iHeaderValue;
                }
            }
            return headerVal;
        }
    }
}