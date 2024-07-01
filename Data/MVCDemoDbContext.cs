using Microsoft.EntityFrameworkCore;
using todo_api.Models.Domain;

namespace todo_api.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees{ get; set; }
    }
}
