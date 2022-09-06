using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPC.Data
{
    //构造方法
    public class AppDbContext : DbContext
    {

        #region 访问路径

#if DEBUG
        private static string sqlbaseDir = GetApplicationPath();
#else
        private static string sqlbaseDir = AppDomain.CurrentDomain.BaseDirectory;
#endif

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
        #endregion

        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //    : base(options)
        //{ }

        protected override void OnConfiguring(DbContextOptionsBuilder options)        
           => options.UseSqlite("Data Source = " + Path.Combine(sqlbaseDir, "AppData\\OpcDB.db"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        #region 数据表格

        public DbSet<User> User { get; set; }

        public DbSet<NodeInfo> NodeInfo { get; set; }

        #endregion

    }

}
