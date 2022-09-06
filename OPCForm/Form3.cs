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
        //IRepository<NodeInfo> repository = new Repository<NodeInfo>();
        AppDbContext _db = new AppDbContext();

        public Form3()
        {
            InitializeComponent();

            dataGridView1.DataSource = GetList();
        }

        public List<NodeInfo> GetList()
        {
            var where = LambdaHelper.True<NodeInfo>();
            //var list = repository.GetList(where, x => x.Id);

            var list = _db.NodeInfo.Where(where).OrderBy(x => x.Id).ToList();

            return list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetList();
        }
    }
}
