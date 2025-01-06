using ClientServerApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServerApp.DAL.Extensions
{
    public static class UserRepositoryExtension
    {
        public static IServiceCollection AddUserRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options => 
            {
                options.UseNpgsql(connectionString);
            }, ServiceLifetime.Singleton);
            
            services.AddSingleton<UserRepository>();
            return services;
        }
    }
}