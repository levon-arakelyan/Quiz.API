using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Quiz.Core.Interfaces.Infrastructure
{
    public interface IDependencyRegistrar
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
