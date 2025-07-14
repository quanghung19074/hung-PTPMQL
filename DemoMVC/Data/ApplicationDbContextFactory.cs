using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DemoMVC.Data;


namespace DemoMVC.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite("Data Source=app.db"); // Nếu bạn đang dùng SQLite

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
