using System.Collections.Generic;
using GraphQLTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTest.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var categories = new List<Category>();
            var products = new List<Product>();
            var j = 1;
            var k = 100;
            for (var i = 1; i < 100; i++)
            {
                categories.Add(new Category
                {
                    Id = i,
                    Name = $"Category{i}"
                });
                
                for ( ;j < k; j++)
                {
                    products.Add( new Product
                    {
                        CategoryId = i,
                        Id = j,
                        Name = $"Product{j}"
                    });
                }

                k += 100;
            }
            builder.Entity<Category>().HasData(categories);
            builder.Entity<Product>().HasData(products);
        }
    }
}