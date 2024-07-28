using Barely_Cooking_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barely_Cooking_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Cake> Cakes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Cake>().HasData(
                new Cake
                {
                    CakeId = 1,
                    Name = "Fast Slow-Ride",
                    Description = "Cake",
                    SpecialTags = "Top Rated",
                    Category = "Wedding",
                    Price = 209.00,
                    ImageFile = "https://barelycooking.blob.core.windows.net/cakephotos/cake.jpg"
                },
                new Cake
                {
                    CakeId = 2,
                    Name = "Fast Slow-Ride",
                    Description = "Cake",
                    SpecialTags = "Top Rated",
                    Category = "Chocolate",
                    Price = 209.00,
                    ImageFile = "https://barelycooking.blob.core.windows.net/cakephotos/cake2.jpg"
                },
                new Cake
                {
                    CakeId = 3,
                    Name = "Fast Slow-Ride",
                    Description = "Cake",
                    SpecialTags = "",
                    Category = "Wedding",
                    Price = 209.00,
                    ImageFile = "https://barelycooking.blob.core.windows.net/cakephotos/cake3.jpg"
                },
                new Cake
                {
                    CakeId = 4,
                    Name = "Fast Slow-Ride",
                    Description = "Cake",
                    SpecialTags = "Top Rated",
                    Category = "Chocolate",
                    Price = 209.00,
                    ImageFile = "https://barelycooking.blob.core.windows.net/cakephotos/cake4.jpg"
                }
                );
        }

    }
}
