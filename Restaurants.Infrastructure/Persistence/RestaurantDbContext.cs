using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : IdentityDbContext<User>(options)
    {

        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }
        internal DbSet<Order> Orders { get; set; }
        internal DbSet<OrderItem> Items { get; set; }
        internal DbSet<Cart> Carts { get; set; }
        internal DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User 
            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(r => r.Address);


                entity.HasMany(r => r.Restaurants)
                       .WithOne(o => o.Owner)
                      .HasForeignKey(r => r.OwnerId);

                entity.HasMany(o => o.Orders)
                 .WithOne(c => c.Customer)
                 .HasForeignKey(c => c.CustomerId);
            });

            #endregion

            #region Restaurant 
            modelBuilder.Entity<Restaurant>(entity =>
            {
                modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);


                entity.HasMany(r => r.Dishes)
                   .WithOne()
                   .HasForeignKey(m => m.RestaurantId);
            });
            #endregion

            #region Order 
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasMany(i => i.Items)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId);
            });
            #endregion

            #region Cart
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasMany(c => c.Items)
                    .WithOne(i => i.Cart)
                    .HasForeignKey(i => i.CartId);

                entity.HasOne(c => c.User)
                    .WithMany()
                    .HasForeignKey(c => c.UserId);

                entity.HasIndex(c => c.UserId)
                    .IsUnique();
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(i => i.Dish)
                    .WithMany()
                    .HasForeignKey(i => i.DishId);
            });
            #endregion

        }
    }
}
