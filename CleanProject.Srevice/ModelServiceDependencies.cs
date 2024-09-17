using CleanProject.Srevice.IServices;
using CleanProject.Srevice.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanProject.Srevice
{
    public static class ModelServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            return services;
        }

    }
}
