using ProductsApi.Enums;
using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApi.Tests;

public static class SeedData
{
    public static List<Category> GetCategoriesSample()
    {
        return new List<Category>
        {
            new Category { Id = 1, Name = "Electronics", Description = "Description", Subcategories = new List<Subcategory>() },
            new Category { Id = 2, Name = "Fashion", Description = "Description", Subcategories = new List<Subcategory>() },
            new Category { Id = 3, Name = "Home", Description = "Description", Subcategories = new List<Subcategory>() },
            new Category { Id = 4, Name = "Sports", Description = "Description", Subcategories = new List<Subcategory>() },
            new Category { Id = 5, Name = "Books", Description = "Description", Subcategories = new List<Subcategory>() },
        };
    }

    public static Category GetCategorySample() => new() { Id = 1, Name = "Electronics", Description = "Description", Subcategories = new List<Subcategory>() };

    public static List<Subcategory> GetSubcategoriesSample()
    {
        return new List<Subcategory>
        {
            new Subcategory { Id = 1, Name = "Cell Phones", Description = "Description", Priority = Priority.High },
            new Subcategory { Id = 2, Name = "Computers", Description = "Description", Priority = Priority.Medium },
            new Subcategory { Id = 3, Name = "Tablets", Description = "Description", Priority = Priority.Low },
            new Subcategory { Id = 4, Name = "Laptops", Description = "Description", Priority = Priority.High },
            new Subcategory { Id = 5, Name = "Televisions", Description = "Description", Priority = Priority.Low},
        };
    }
    public static Subcategory GetSubcategorySample() => new() { Id = 1, Name = "Cell Phones", Description = "Description", Priority = Priority.High };

    public static List<Product> GetProductsSample()
    {
        return new List<Product>
        {
            new Product { Id = 1, Name = "Cell Phone", Price = 100, CategoryId = 1, Subcategories = new List<Subcategory>() },
            new Product { Id = 2, Name = "Computer", Price = 200, CategoryId = 2, Subcategories = new List<Subcategory>() },
            new Product { Id = 3, Name = "Tablet", Price = 300, CategoryId = 3, Subcategories = new List<Subcategory>() },
            new Product { Id = 4, Name = "Laptop", Price = 400, CategoryId = 4, Subcategories = new List<Subcategory>() },
            new Product { Id = 5, Name = "Television", Price = 500, CategoryId = 5, Subcategories = new List<Subcategory>() },
            };

    }

    public static Product GetProductSample() => new() { Id = 1, Name = "Cell Phone", Price = 100, CategoryId = 1, Subcategories = new List<Subcategory>() };

    public static List<User> GetUsersSample()
    {
        return new List<User>
        {
            new User { Id = 1, Firstname = "John", Lastname = "Doe", Email = "johndoe@email.com", Password = "password", Role = Role.User },
            new User { Id = 2, Firstname = "Peter", Lastname = "Robson", Email = "peterrobson@email.com", Password = "password", Role = Role.User },
            new User { Id = 3, Firstname = "Rick", Lastname = "Thompson", Email = "rickthompson@email.com", Password = "password", Role = Role.User },
            new User { Id = 4, Firstname = "Michael", Lastname = "Scott", Email = "michaelscott@email.com", Password = "password", Role = Role.User },
            new User { Id = 5, Firstname = "James", Lastname = "Bond", Email = "jamesbond@email.com", Password = "password", Role = Role.User },
        };
    }

    public static User GetUserSample() => new()
    {
        Id = 1,
        Firstname = "John",
        Lastname = "Doe",
        Email = "johndoe@email.com",
        Password = "password",
        Role = Role.User
    };
}
