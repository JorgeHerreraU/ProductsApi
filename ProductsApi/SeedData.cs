using Microsoft.EntityFrameworkCore;
using ProductsApi.DAL;
using ProductsApi.Enums;
using ProductsApi.Models;
using ProductsApi.Services;

namespace ProductsApi;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context =
            new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        // Migrate
        context.Database.Migrate();

        // Look for any products.
        if (context.Products.Any()) return; // DB has been seeded

        var user = new User { Firstname = "John", Lastname = "Doe", Email = "admin@email.com", Password = new PasswordService().HashPassword("password"), Role = Role.Admin };
        var category1 = new Category { Name = "Food", Description = "Products for human consumption.", Priority = Priority.Medium };
        var category2 = new Category { Name = "Electronics", Description = "Electronic devices.", Priority = Priority.High };
        var category3 = new Category { Name = "Healthy", Description = "Healthy living goods.", Priority = Priority.Low };
        var subcategory1 = new Subcategory { Name = "Fruit", Description = "A fruit is the seed-bearing structure in flowering plants that is formed from the ovary after flowering.", Priority = Priority.High, Categories = new List<Category> { category1, category3 } };
        var subcategory2 = new Subcategory { Name = "Vegetables", Description = "A vegetable is a plant or part of a plant, especially a leafy plant, whose structure is adapted to survive in the soil.", Priority = Priority.Low, Categories = new List<Category> { category1, category3 } };
        category1.Subcategories = new List<Subcategory> { subcategory1, subcategory2 };
        category3.Subcategories = new List<Subcategory> { subcategory1, subcategory2 };
        var product1 = new Product { Name = "Tomato", Price = 1.99, Category = category1, Subcategories = new List<Subcategory> { subcategory1, subcategory2 } };
        var product2 = new Product { Name = "Laptop", Price = 99.99, Category = category2 };
        context.Categories.AddRange(category1, category2);
        context.Subcategories.AddRange(subcategory1, subcategory2);
        context.Products.AddRange(product1, product2);
        context.Users.Add(user);
        context.SaveChanges();
    }
}