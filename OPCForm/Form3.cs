using OPC.Data;
using OPC.Helper;
using OPC.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPCForm
{
    public partial class Form3 : Form
    {
        private IRepository<NodeInfo> _nodeinfoRepository;

        public Form3()
        {
            InitializeComponent();

            #region 获取数据表

            _nodeinfoRepository = (IRepository<NodeInfo>)Program.ServiceProvider.GetService(typeof(IRepository<NodeInfo>));

            #endregion
            dataGridView1.DataSource = GetList();
        }

        public List<NodeInfo> GetList()
        {
            var where = LambdaHelper.True<NodeInfo>();
            return _nodeinfoRepository.GetList(where);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetList();
        }
    }
}
