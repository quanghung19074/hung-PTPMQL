using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace DemoMVC.Data    

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Daily> Dailys { get; set; }
        public DbSet<HeThongPhanPhoi> HeThongPhanPhois { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> Person { get; set; }
    }
}
