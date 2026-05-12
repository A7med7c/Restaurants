using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
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
using Restaurants.Domain.Repositories;
using Restaurants.Application.Payments;
using Restaurants.Infrastructure.Payments;


namespace Restaurants.Infrastructure.Extensions
{
    // group all internal services in infrastructure and regisered by add infra extension method
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetValidatedConnectionString(configuration);
            services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(connectionString)
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
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.Configure<PaymobSettings>(configuration.GetSection("Paymob"));
            services.AddHttpClient();
            services.AddScoped<IPaymentService, PaymobPaymentService>();


            services.AddScoped<IRestauratntAuthorizationServices, RestauratntAuthorizationServices>();

        }

        private static string GetValidatedConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'RestaurantsDb' is missing. Add it under the 'ConnectionStrings' section.");
            }

            try
            {
                _ = new SqlConnectionStringBuilder(connectionString);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(
                    "Connection string 'RestaurantsDb' is invalid. Please check the SQL Server configuration value.",
                    ex);
            }

            return connectionString;
        }
    }
}
