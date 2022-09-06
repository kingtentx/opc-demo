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

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);         
          
        }

        #region 数据区域
        public DbSet<User> User { get; set; }

        public DbSet<NodeInfo> NodeInfo { get; set; }
        #endregion

    }

}
