using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (context.Products.Any()) return;

            context.Products.AddRange(
                new Product
                {
                    Name = "Unsteady chair",
                    Description = "Secretly give your opponent a disadvantage",
                    Category = "Chess",
                    Price = 29.95m
                },
                new Product
                {
                    Name = "Kayak",
                    Description = "A boat for one person",
                    Category = "WaterSports",
                    Price = 275
                },
                new Product
                {
                    Name = "LifeJacket",
                    Description = "Protective and fashionable",
                    Category = "WaterSports",
                    Price = 48.95m
                },
                new Product
                {
                    Name = "Soccer ball",
                    Description = "FIFA approved size and weight",
                    Category = "Soccer",
                    Price = 34.95m
                },
                new Product
                {
                    Name = "Corner Flags",
                    Description = "Flat-packed 35,000-seat stadium",
                    Category = "Soccer",
                    Price = 79500
                },
                new Product
                {
                    Name = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    Category = "Chess",
                    Price = 16
                },
                new Product
                {
                    Name = "Human Chess board",
                    Description = "Fun game for the family",
                    Category = "Chess",
                    Price = 75
                },
                new Product
                {
                    Name = "Bling-Bling King",
                    Description = "Gold-Plated, diamond-studded King",
                    Category = "Chess",
                    Price = 1200
                }
            );
            context.SaveChanges();
        }
    }
}