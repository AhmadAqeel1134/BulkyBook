using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{

    //Application DB Context will be implementing DbContext of EF Core

    //Application DB Context will be a bridge between C# code and EF Core
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }  //base configurations to set up entity framework core

        //convert class -> DB table
        public DbSet<Category> Categories { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData( 
                new Category { Id=  1, Name = "Fiction", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Scifi",   DisplayOrder = 2 },
                new Category { Id = 3, Name = "Haunted", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Category>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(i => i.DisplayOrder).IsUnique();
        }

    }
}
