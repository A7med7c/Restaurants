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

            #region User 
            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(r => r.Address);


               entity.HasMany(r => r.Restaurants)
                      .WithOne()
                     .HasForeignKey(r => r.OwnerId);
            });
            #endregion

            #region Restaurant 
            modelBuilder.Entity<Restaurant>(entity =>
            {
                modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);

                modelBuilder.Entity<Restaurant>()
              .HasMany(r => r.MenuCategories)
              .WithOne()
              .HasForeignKey(d => d.RestaurantId);

                entity.HasMany(r => r.MenuCategories)
                      .WithOne()
                      .HasForeignKey(c => c.RestaurantId);

                entity.HasMany(r => r.Dishes)
                      .WithOne()
                      .HasForeignKey(m => m.RestaurantId);
            });
            #endregion

            #region Menu Category 
            modelBuilder.Entity<MenuCategory>(entity =>
            {

                entity.HasMany(c => c.Dishes)
                      .WithOne()
                      .HasForeignKey(m => m.MenuCategoryId);
            });
            #endregion

        }
    }
}
