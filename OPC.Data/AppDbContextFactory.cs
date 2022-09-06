using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OPC.Data
{
    class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {

#if DEBUG
        private static string sqlbaseDir = GetApplicationPath();
#else
        private static string sqlbaseDir = AppDomain.CurrentDomain.BaseDirectory;
#endif

        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlite("Data Source=.\\AppData\\OpcDB.db");               
            optionsBuilder.UseSqlite("Data Source=" + Path.Combine(sqlbaseDir, "AppData\\OpcDB.db"));

            return new AppDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// 获取项目根目录
        /// </summary>
        /// <returns></returns>
        private static string GetApplicationPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = String.Empty;
            while (folderName.ToLower() != "bin")
            {
                path = path.Substring(0, path.LastIndexOf("\\"));
                folderName = path.Substring(path.LastIndexOf("\\") + 1);
            }
            return path.Substring(0, path.LastIndexOf("\\") + 1);
        }
    }
}
