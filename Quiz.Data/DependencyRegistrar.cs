using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Core.Interfaces.Database;
using Quiz.Core.Interfaces.Infrastructure;

namespace Quiz.Data
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QuizContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("Quiz")), ServiceLifetime.Scoped);
            services.AddTransient(typeof(IDatabaseTable<>), typeof(QuizDatabaseTable<>));
        }
    }
}
