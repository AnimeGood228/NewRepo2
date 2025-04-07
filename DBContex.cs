// AppDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace Variant6
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=dbsrv\\dub2024;Database=Pididi;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}