using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Core.Interfaces.Infrastructure;
using Quiz.Core.Interfaces.Services;
using Quiz.Core.Interfaces.UserManagement;

namespace Quiz.Services
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IClaimsService, ClaimsService>();
            services.AddHostedService<StartupService>();
        }
    }
}
