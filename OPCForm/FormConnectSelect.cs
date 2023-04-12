using Opc.Ua;
using OpcUaHelper;
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
    public partial class FormConnectSelect : Form
    {
        private OpcUaClient m_OpcUaClient;

        public FormConnectSelect(OpcUaClient opcUaClient)
        {
            InitializeComponent();
            this.m_OpcUaClient = opcUaClient;
        }
        private void FormConnectSelect_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 匿名登录
            m_OpcUaClient.UserIdentity = new UserIdentity(new AnonymousIdentityToken());
            DialogResult = DialogResult.OK;
            return;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 用户名密码登录
            //m_OpcUaClient = new OpcUaClient();
            m_OpcUaClient.UserIdentity = new UserIdentity(txtUserName.Text, txtPassword.Text);
            DialogResult = DialogResult.OK;
            return;
        }

       
    }
}
