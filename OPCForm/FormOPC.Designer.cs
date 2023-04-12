
namespace OPCForm
{
    partial class FormOPC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.labNode = new System.Windows.Forms.Label();
            this.textBox_nodeId = new System.Windows.Forms.TextBox();
            this.label_time_spend = new System.Windows.Forms.Label();
            this.BrowseNodesTV = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.DisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccessLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_opc = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labAddress
            // 
            this.labAddress.AutoSize = true;
            this.labAddress.Location = new System.Drawing.Point(29, 25);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(82, 20);
            this.labAddress.TabIndex = 0;
            this.labAddress.Text = "opc服务器";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(136, 22);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(643, 27);
            this.txtAddress.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(797, 22);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(94, 29);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(35, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Browser :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(179, 71);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(114, 24);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Fast Access";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(331, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 5;
            this.button2.Text = " Subscript";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // labNode
            // 
            this.labNode.AutoSize = true;
            this.labNode.Location = new System.Drawing.Point(320, 72);
            this.labNode.Name = "labNode";
            this.labNode.Size = new System.Drawing.Size(84, 20);
            this.labNode.TabIndex = 6;
            this.labNode.Text = "当前节点：";
            // 
            // textBox_nodeId
            // 
            this.textBox_nodeId.Location = new System.Drawing.Point(410, 69);
            this.textBox_nodeId.Name = "textBox_nodeId";
            this.textBox_nodeId.ReadOnly = true;
            this.textBox_nodeId.Size = new System.Drawing.Size(598, 27);
            this.textBox_nodeId.TabIndex = 7;
            // 
            // label_time_spend
            // 
            this.label_time_spend.AutoSize = true;
            this.label_time_spend.Location = new System.Drawing.Point(1042, 71);
            this.label_time_spend.Name = "label_time_spend";
            this.label_time_spend.Size = new System.Drawing.Size(39, 20);
            this.label_time_spend.TabIndex = 8;
            this.label_time_spend.Text = "0ms";
            // 
            // BrowseNodesTV
            // 
            this.BrowseNodesTV.Location = new System.Drawing.Point(12, 103);
            this.BrowseNodesTV.Name = "BrowseNodesTV";
            this.BrowseNodesTV.Size = new System.Drawing.Size(281, 434);
            this.BrowseNodesTV.TabIndex = 9;
            this.BrowseNodesTV.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.BrowseNodesTV_BeforeExpand);
            this.BrowseNodesTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.BrowseNodesTV_AfterSelect);
            this.BrowseNodesTV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BrowseNodesTV_MouseClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Image,
            this.DisplayName,
            this.Value,
            this.Type,
            this.AccessLevel,
            this.Description});
            this.dataGridView1.Location = new System.Drawing.Point(320, 107);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(778, 89);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // Image
            // 
            this.Image.HeaderText = "";
            this.Image.MinimumWidth = 6;
            this.Image.Name = "Image";
            this.Image.Width = 20;
            // 
            // DisplayName
            // 
            this.DisplayName.HeaderText = "Name";
            this.DisplayName.MinimumWidth = 6;
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.ReadOnly = true;
            this.DisplayName.ToolTipText = "参数的显示名称";
            this.DisplayName.Width = 145;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 6;
            this.Value.Name = "Value";
            this.Value.ToolTipText = "参数的实际数据值";
            this.Value.Width = 200;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 6;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.ToolTipText = "参数的类型";
            this.Type.Width = 80;
            // 
            // AccessLevel
            // 
            this.AccessLevel.HeaderText = "AccessLevel";
            this.AccessLevel.MinimumWidth = 6;
            this.AccessLevel.Name = "AccessLevel";
            this.AccessLevel.Width = 200;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.Width = 125;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel_opc});
            this.statusStrip1.Location = new System.Drawing.Point(0, 642);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1109, 26);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(96, 20);
            this.toolStripStatusLabel1.Text = "Opc Status: ";
            // 
            // toolStripStatusLabel_opc
            // 
            this.toolStripStatusLabel_opc.Name = "toolStripStatusLabel_opc";
            this.toolStripStatusLabel_opc.Size = new System.Drawing.Size(0, 20);
            // 
            // FormOPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 668);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.BrowseNodesTV);
            this.Controls.Add(this.label_time_spend);
            this.Controls.Add(this.textBox_nodeId);
            this.Controls.Add(this.labNode);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.labAddress);
            this.Name = "FormOPC";
            this.Text = "FormOPC";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labAddress;
        private TextBox txtAddress;
        private Button btnConnect;
        private Label label1;
        private CheckBox checkBox1;
        private Button button2;
        private Label labNode;
        private TextBox textBox_nodeId;
        private Label label_time_spend;
        private TreeView BrowseNodesTV;
        private DataGridView dataGridView1;
        private DataGridViewImageColumn Image;
        private DataGridViewTextBoxColumn DisplayName;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewTextBoxColumn AccessLevel;
        private DataGridViewTextBoxColumn Description;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel_opc;
    }
}