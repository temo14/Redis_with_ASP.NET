using Microsoft.EntityFrameworkCore;
using Redis_ASP.NET_Way.Models;

namespace Redis_ASP.NET_Way.Db
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
