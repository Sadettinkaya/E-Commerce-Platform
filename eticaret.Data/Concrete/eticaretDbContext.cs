using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Data.Concrete;

public class eticaretDbContext:DbContext
{
    public eticaretDbContext(DbContextOptions<eticaretDbContext> options):base(options)
    {

    }
    public DbSet<Product>Products=>Set<Product>();
    public DbSet<Category>Categories=>Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
        .HasMany(e=>e.Categories)
        .WithMany(e=>e.Products)
        .UsingEntity<ProductCategory>();

         modelBuilder.Entity<Category>()
         .HasIndex(u=>u.Url)
         .IsUnique();

        modelBuilder.Entity<Product>().HasData(
            new List<Product>(){
                new(){Id=1,Name="samsung s24",Price=20000,Description="iyi tel"},
                new(){Id=2,Name="samsung s23",Price=10000,Description="iyi tel"},
                new(){Id=3,Name="samsung s22",Price=30000,Description="iyi tel"},
                new(){Id=4,Name="samsung s21",Price=40000,Description="iyi tel"},
                new(){Id=5,Name="samsung s10",Price=10000,Description="iyi tel"},
              
            }
        );
        modelBuilder.Entity<Category>().HasData(
            new List<Category>(){
                new(){Id=1, Name="Telefon",Url="telefon"},
                new(){Id=2, Name="Elektronik",Url="elektronik"},
                new(){Id=3, Name="Beyaz Eşya",Url="beyaz-esya"}
            }
        );

        modelBuilder.Entity<ProductCategory>().HasData(
            new List<ProductCategory>(){
                new ProductCategory(){ProductId=1,CategoryId=1},
                new ProductCategory(){ProductId=2,CategoryId=1},
                new ProductCategory(){ProductId=3,CategoryId=1},
                new ProductCategory(){ProductId=4,CategoryId=1},
                new ProductCategory(){ProductId=5,CategoryId=2},
              
               
            }
        );

    }


}