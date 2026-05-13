using Restaurants.Api.Extensions;
using Restaurants.Api.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
namespace Restaurants.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddPresentation();

            builder.Services.AddApplication();

            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

            await seeder.Seed();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeLoggingMiddleware>();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }


            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.MapGroup("api/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application Startup Failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }
}
