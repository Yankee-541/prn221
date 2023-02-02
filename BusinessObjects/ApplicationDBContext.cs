using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json", optional: true, reloadOnChange: true);
            //IConfiguration configuration = builder.Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("Server=YANKEE; uid=sa;pwd=dang050401;db=myStore"));
            optionsBuilder.UseSqlServer("Server=YANKEE; uid=sa;pwd=dang050401;Database=myStore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Shoe" },
                 new Category { CategoryId = 2, CategoryName = "Dep" },
                  new Category { CategoryId = 3, CategoryName = "Quan" },
                   new Category { CategoryId = 4, CategoryName = "Ao" }
                   );
        }
    }
}
