using DeliveryShop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryShop.Database;

public class DBContext:DbContext
{
   
     public DBContext()
    {
        Database.Migrate();
    }
    public DbSet<Addresses> Addresses { get; set; }
    public DbSet<Baskets> Baskets { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Ratings> Ratings { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Statuses> Statuses { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<FavoriteProducts> FavoriteProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
        var connString = config.GetConnectionString("MarketDB");
        optionsBuilder.UseNpgsql(connString);
    }

}