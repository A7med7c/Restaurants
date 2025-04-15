using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
namespace Restaurants.Application.Extensions
{
    // group all internal services in application and regisered by add infra extension method
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantsService, RestaurantsService>();

            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
            
    }
}
