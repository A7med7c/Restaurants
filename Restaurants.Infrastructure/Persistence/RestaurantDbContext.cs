using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : IdentityDbContext<User>(options)
    {

        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }
        internal DbSet<MenuCategory> MenuCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .OwnsOne(r=>r.Address);

            modelBuilder.Entity<User>()
                .OwnsOne(r => r.Address);


            modelBuilder.Entity<Restaurant>()
                 .HasMany(r => r.MenuCategories)
                 .WithOne()
                 .HasForeignKey(d => d.RestaurantId);
         
            modelBuilder.Entity<MenuCategory>()
                 .HasMany(mc => mc.Dishes)
                 .WithOne()
                 .HasForeignKey(d => d.MenuCategoryId);

            modelBuilder.Entity<User>()
                  .HasMany(r => r.Restaurants)
                  .WithOne()
                 .HasForeignKey(r => r.OwnerId);

        }
    }
}
