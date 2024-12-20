using GR30323_Web.API.Data;
using GR30323_Web.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GR30323_Web.API
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<AppDbContext>();

            // Ensure the database is created
            await dbContext.Database.MigrateAsync();

            // Check if categories exist, add them if not
            if (!dbContext.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Седаны", NormalizedName = "SEDANS" },
                    new Category { Name = "Внедорожники", NormalizedName = "SUVS" },
                    new Category { Name = "Электромобили", NormalizedName = "ELECTRIC" }
                };

                await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }

            // Check if cars exist, add them if not
            if (!dbContext.Cars.Any())
            {
                var categories = dbContext.Categories.ToList();

                var cars = new[]
                {
                    new Car { Name = "Tesla Model S", Description = "Электрический седан", Price = 79999.99m, Image = "Images/tesla.jpg", CategoryId = categories.FirstOrDefault(c => c.NormalizedName == "ELECTRIC")?.Id ?? 0 },
                    new Car { Name = "Toyota Corolla", Description = "Седан C-класса", Price = 20999.99m, Image = "Images/corolla.jpg", CategoryId = categories.FirstOrDefault(c => c.NormalizedName == "SEDANS")?.Id ?? 0 },
                    new Car { Name = "Ford Explorer", Description = "Внедорожник", Price = 34999.99m, Image = "Images/explorer.jpg", CategoryId = categories.FirstOrDefault(c => c.NormalizedName == "SUVS")?.Id ?? 0 },
                    new Car { Name = "VW Polo Sedan", Description = "Седан B-класса", Price = 34999.99m, Image = "Images/polo.jpg", CategoryId = categories.FirstOrDefault(c => c.NormalizedName == "SEDANS")?.Id ?? 0 }
                };

                await dbContext.Cars.AddRangeAsync(cars);
                await dbContext.SaveChangesAsync();

                // Copy images to wwwroot/Images
                var imagesPath = Path.Combine(app.Environment.WebRootPath, "Images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var sourceImages = new[]
                {
                    ("tesla.jpg", "Images/tesla.jpg"),
                    ("corolla.jpg", "Images/corolla.jpg"),
                    ("explorer.jpg", "Images/explorer.jpg"),
                    ("polo.jpg", "Images/polo.jpg")
                };

                foreach (var (fileName, dbPath) in sourceImages)
                {
                    var sourcePath = Path.Combine("SeedImages", fileName); // Assuming images are stored in a folder called "SeedImages"
                    var destinationPath = Path.Combine(imagesPath, fileName);

                    if (File.Exists(sourcePath) && !File.Exists(destinationPath))
                    {
                        File.Copy(sourcePath, destinationPath);
                    }
                }
            }
        }
    }
}
