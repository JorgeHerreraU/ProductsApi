using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Subcategory> Subcategories { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.Property(p => p.Name).IsRequired();
            e.Property(p => p.Price).IsRequired();
        });

        modelBuilder.Entity<Subcategory>(e =>
        {
            e.Property(p => p.Name).IsRequired();
            e.Property(p => p.Description).IsRequired();
            e.Property(p => p.Priority).IsRequired();
        });

        modelBuilder.Entity<Category>(e =>
        {
            e.Property(p => p.Name).IsRequired();
            e.Property(p => p.Description).IsRequired();
            e.Property(p => p.Priority).IsRequired();
        });

        modelBuilder.Entity<User>(e =>
        {
            e.Property(p => p.Email).IsRequired();
            e.Property(p => p.Password).IsRequired();
            e.Property(p => p.Role).IsRequired();
            e.Property(p => p.Firstname).IsRequired();
            e.Property(p => p.Lastname).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}