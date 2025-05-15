using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Restaurants.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;
using Restaurants.Infrastructure.Authorization.Requirements.OwnsTwoRestaurants;


namespace Restaurants.Infrastructure.Extensions
{
    // group all internal services in infrastructure and regisered by add infra extension method
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(ConnectionString)
                   .EnableSensitiveDataLogging()

            );

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantDbContext>();

            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimsTypes.Nationality, "Egyptian"))
                .AddPolicy(PolicyNames.AtLeast20,
                builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.OwnsTwoRestaurants,
                builder => builder.AddRequirements(new OwnsTwoRestaurantsReqirement(2)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, OwnsTwoRestaurantsReqirementHandler>();


            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();

            services.AddScoped<IRestauratntAuthorizationServices, RestauratntAuthorizationServices>();


           
        }
    }
}
