using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPCForm
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public int[] s = { 0, 0, 0 };//用来记录窗体是否打开过
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (s[tabControl1.SelectedIndex] == 0)
            {
                btnX_Click(sender, e);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string formClass = "OPCForm.Form1";
            GenerateForm(formClass, tabControl1);
        }

        public void GenerateForm(string form, object sender)
        {
            //反射生成窗体
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);
            //设置窗体没有边框，加入到选项卡中
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();
            s[((TabControl)sender).SelectedIndex] = 1;
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            string formClass = ((TabControl)sender).SelectedTab.Tag.ToString();

            GenerateForm(formClass, sender);

        }


    }
}
