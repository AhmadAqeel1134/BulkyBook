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
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Scifi"  },
                new Category { Id = 3, Name = "History"}
             );



        }

    }
}
