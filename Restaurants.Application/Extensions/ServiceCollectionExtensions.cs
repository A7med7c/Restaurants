using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
namespace Restaurants.Application.Extensions
{
    // group all internal services in application and regisered by add infra extension method
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationassembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddAutoMapper(applicationassembly);
            services.AddValidatorsFromAssembly(applicationassembly)
                .AddFluentValidationAutoValidation();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationassembly));
        }

    }
}
