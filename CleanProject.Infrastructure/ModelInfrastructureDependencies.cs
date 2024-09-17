using CleanProject.Infrastructure.InfrastructureBases;
using CleanProject.Infrastructure.Repositories.Implmentation;
using CleanProject.Infrastructure.Repositories.IRepository;
using Microsoft.Extensions.DependencyInjection;

namespace CleanProject.Infrastructure
{
    public static class ModelInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}
