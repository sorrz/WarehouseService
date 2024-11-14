using Bogus;
using Microsoft.EntityFrameworkCore;
using WarehouseService.Models;

namespace WarehouseService.Database
{
    public class ItemDbContext : DbContext
    {
        public DbSet<Item> Products { get; set; }

        public ItemDbContext(DbContextOptions<ItemDbContext> options)
            : base(options)
        {
        }

        public void SeedData()
        {
            if (!Products.Any())
            {
                // Set up a Faker for the Item model
                var faker = new Faker<Item>()
                    .RuleFor(i => i.Name, f => f.Commerce.ProductName())
                    .RuleFor(i => i.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(i => i.Details, f => f.Lorem.Paragraph())
                    .RuleFor(i => i.PictureUrl, f => f.Image.PicsumUrl())
                    .RuleFor(i => i.Quantity, f => f.Random.Int(50, 200));

                // Generate a list of 10 fake items
                var fakeItems = faker.Generate(100);

                Products.AddRange(fakeItems);
                SaveChanges();
            }
        }
    }
}