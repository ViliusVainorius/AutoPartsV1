using AutoPartsV1.Auth.Model;
using AutoPartsV1.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsV1.Data
{
    public class ForumDbContext : IdentityDbContext<AutoPartsV1RestUser>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarPart> CarParts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=ForumDb2");
        }
    }
}
