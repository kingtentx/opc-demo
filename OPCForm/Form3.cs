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

            BindData();
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
            BindData();
        }

        public void BindData()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("Id", "序号");
            dataGridView1.Columns[0].DataPropertyName = "Id";

            dataGridView1.Columns.Add("NodeName", "名称");
            dataGridView1.Columns[1].DataPropertyName = "NodeName";

            dataGridView1.Columns.Add("NodeId", "节点");
            dataGridView1.Columns[2].DataPropertyName = "NodeId";
            dataGridView1.Columns[2].Width = 320;

            dataGridView1.Columns.Add("DataType", "数据类型");
            dataGridView1.Columns[3].DataPropertyName = "DataType";

            dataGridView1.Columns.Add("CreateTime", "添加时间");
            dataGridView1.Columns[4].DataPropertyName = "CreateTime";

            dataGridView1.DataSource = new BindingList<NodeInfo>(GetList());
        }
    }
}
