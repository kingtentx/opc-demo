using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OPC.Data
{
    class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=OpcDB.db");
            //optionsBuilder.UseSqlite(@"Data Source=|DataDirectory|\AppData\OpcDB.db");
            //optionsBuilder.UseSqlite(@"|DataDirectory|\AppData\OpcDB.dbb");           

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
