namespace OPCForm
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labPassword = new System.Windows.Forms.Label();
            this.labUserName = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.labServer = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.nodesTreeView = new System.Windows.Forms.TreeView();
            this.nodesImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnStop = new System.Windows.Forms.Button();
            this.labNode = new System.Windows.Forms.Label();
            this.txtNode = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.labTime = new System.Windows.Forms.Label();
            this.btnSubscription = new System.Windows.Forms.Button();
            this.btnUnSubscription = new System.Windows.Forms.Button();
            this.logListView = new System.Windows.Forms.ListView();
            this.infoTime = new System.Windows.Forms.ColumnHeader();
            this.logImageList = new System.Windows.Forms.ImageList(this.components);
            this.listSubscribe = new System.Windows.Forms.ListBox();
            this.labLog = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.NumericUpDown();
            this.chkIsLoop = new System.Windows.Forms.CheckBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.chkPush = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime)).BeginInit();
            this.SuspendLayout();
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(311, 169);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(54, 20);
            this.labPassword.TabIndex = 16;
            this.labPassword.Text = "密码：";
            // 
            // labUserName
            // 
            this.labUserName.AutoSize = true;
            this.labUserName.Location = new System.Drawing.Point(296, 117);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(69, 20);
            this.labUserName.TabIndex = 15;
            this.labUserName.Text = "用户名：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(375, 166);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(294, 27);
            this.txtPassword.TabIndex = 14;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(375, 114);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(294, 27);
            this.txtUserName.TabIndex = 13;
            // 
            // labServer
            // 
            this.labServer.AutoSize = true;
            this.labServer.Location = new System.Drawing.Point(13, 13);
            this.labServer.Name = "labServer";
            this.labServer.Size = new System.Drawing.Size(100, 20);
            this.labServer.TabIndex = 12;
            this.labServer.Text = "OPC服务器：";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(957, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(94, 29);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(119, 10);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(801, 27);
            this.txtAddress.TabIndex = 10;
            this.txtAddress.Text = "opc.tcp://127.0.0.1:49320";
            // 
            // nodesTreeView
            // 
            this.nodesTreeView.ImageIndex = 0;
            this.nodesTreeView.ImageList = this.nodesImageList;
            this.nodesTreeView.Location = new System.Drawing.Point(12, 60);
            this.nodesTreeView.Name = "nodesTreeView";
            this.nodesTreeView.SelectedImageIndex = 0;
            this.nodesTreeView.Size = new System.Drawing.Size(251, 504);
            this.nodesTreeView.TabIndex = 9;
            this.nodesTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.NodesTreeViewAfterExpand);
            this.nodesTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.nodesTreeView_NodeMouseClick);
            // 
            // nodesImageList
            // 
            this.nodesImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.nodesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("nodesImageList.ImageStream")));
            this.nodesImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.nodesImageList.Images.SetKeyName(0, "ObjectNode.png");
            this.nodesImageList.Images.SetKeyName(1, "FolderNode.png");
            this.nodesImageList.Images.SetKeyName(2, "MethodNode.png");
            this.nodesImageList.Images.SetKeyName(3, "VariableNode.png");
            this.nodesImageList.Images.SetKeyName(4, "PropertyNode.png");
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(1090, 8);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(94, 29);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "断开";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // labNode
            // 
            this.labNode.AutoSize = true;
            this.labNode.Location = new System.Drawing.Point(281, 61);
            this.labNode.Name = "labNode";
            this.labNode.Size = new System.Drawing.Size(84, 20);
            this.labNode.TabIndex = 18;
            this.labNode.Text = "当前节点：";
            // 
            // txtNode
            // 
            this.txtNode.Location = new System.Drawing.Point(375, 60);
            this.txtNode.Name = "txtNode";
            this.txtNode.ReadOnly = true;
            this.txtNode.Size = new System.Drawing.Size(545, 27);
            this.txtNode.TabIndex = 19;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(311, 343);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(94, 29);
            this.btnRead.TabIndex = 20;
            this.btnRead.Text = "读取值";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Location = new System.Drawing.Point(308, 278);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(79, 20);
            this.labTime.TabIndex = 22;
            this.labTime.Text = "间隔(毫秒)";
            // 
            // btnSubscription
            // 
            this.btnSubscription.Location = new System.Drawing.Point(575, 343);
            this.btnSubscription.Name = "btnSubscription";
            this.btnSubscription.Size = new System.Drawing.Size(94, 29);
            this.btnSubscription.TabIndex = 23;
            this.btnSubscription.Text = "订阅";
            this.btnSubscription.UseVisualStyleBackColor = true;
            this.btnSubscription.Click += new System.EventHandler(this.btnSubscription_Click);
            // 
            // btnUnSubscription
            // 
            this.btnUnSubscription.ForeColor = System.Drawing.Color.Black;
            this.btnUnSubscription.Location = new System.Drawing.Point(692, 343);
            this.btnUnSubscription.Name = "btnUnSubscription";
            this.btnUnSubscription.Size = new System.Drawing.Size(94, 29);
            this.btnUnSubscription.TabIndex = 24;
            this.btnUnSubscription.Text = "取消订阅";
            this.btnUnSubscription.UseVisualStyleBackColor = true;
            this.btnUnSubscription.Click += new System.EventHandler(this.btnUnSubscription_Click);
            // 
            // logListView
            // 
            this.logListView.Location = new System.Drawing.Point(12, 607);
            this.logListView.Name = "logListView";
            this.logListView.Size = new System.Drawing.Size(1206, 222);
            this.logListView.TabIndex = 28;
            this.logListView.UseCompatibleStateImageBehavior = false;
            // 
            // logImageList
            // 
            this.logImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.logImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("logImageList.ImageStream")));
            this.logImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.logImageList.Images.SetKeyName(0, "success.png");
            this.logImageList.Images.SetKeyName(1, "error.png");
            this.logImageList.Images.SetKeyName(2, "warning.png");
            // 
            // listSubscribe
            // 
            this.listSubscribe.FormattingEnabled = true;
            this.listSubscribe.ItemHeight = 20;
            this.listSubscribe.Location = new System.Drawing.Point(938, 60);
            this.listSubscribe.Name = "listSubscribe";
            this.listSubscribe.Size = new System.Drawing.Size(280, 504);
            this.listSubscribe.TabIndex = 29;
            // 
            // labLog
            // 
            this.labLog.AutoSize = true;
            this.labLog.Location = new System.Drawing.Point(29, 579);
            this.labLog.Name = "labLog";
            this.labLog.Size = new System.Drawing.Size(69, 20);
            this.labLog.TabIndex = 31;
            this.labLog.Text = "消息日志";
            // 
            // txtTime
            // 
            this.txtTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtTime.Location = new System.Drawing.Point(402, 274);
            this.txtTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtTime.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(110, 27);
            this.txtTime.TabIndex = 32;
            this.txtTime.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // chkIsLoop
            // 
            this.chkIsLoop.AutoSize = true;
            this.chkIsLoop.Location = new System.Drawing.Point(411, 346);
            this.chkIsLoop.Name = "chkIsLoop";
            this.chkIsLoop.Size = new System.Drawing.Size(91, 24);
            this.chkIsLoop.TabIndex = 33;
            this.chkIsLoop.Text = "循环读取";
            this.chkIsLoop.UseVisualStyleBackColor = true;
            this.chkIsLoop.CheckedChanged += new System.EventHandler(this.chkIsLoop_CheckedChanged);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(119, 575);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(94, 29);
            this.btnClearLog.TabIndex = 34;
            this.btnClearLog.Text = "清空日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // chkPush
            // 
            this.chkPush.AutoSize = true;
            this.chkPush.Location = new System.Drawing.Point(575, 409);
            this.chkPush.Name = "chkPush";
            this.chkPush.Size = new System.Drawing.Size(97, 24);
            this.chkPush.TabIndex = 35;
            this.chkPush.Text = "mqtt推送";
            this.chkPush.UseVisualStyleBackColor = true;
            this.chkPush.CheckedChanged += new System.EventHandler(this.chkPush_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(311, 409);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 36;
            this.btnSave.Text = "保存设备";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(311, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 37;
            this.button1.Text = "保存节点";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 833);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkPush);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.chkIsLoop);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.labLog);
            this.Controls.Add(this.listSubscribe);
            this.Controls.Add(this.logListView);
            this.Controls.Add(this.btnUnSubscription);
            this.Controls.Add(this.btnSubscription);
            this.Controls.Add(this.labTime);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.txtNode);
            this.Controls.Add(this.labNode);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.labUserName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.labServer);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.nodesTreeView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.txtTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labPassword;
        private Label labUserName;
        private TextBox txtPassword;
        private TextBox txtUserName;
        private Label labServer;
        private Button btnConnect;
        private TextBox txtAddress;
        private TreeView nodesTreeView;
        private ImageList nodesImageList;
        private Button btnStop;
        private Label labNode;
        private TextBox txtNode;
        private Button btnRead;
        private Label labTime;
        private Button btnSubscription;
        private Button btnUnSubscription;
        private ListView logListView;
        private ImageList logImageList;
        private ListBox listSubscribe;
        private Label labLog;
        private NumericUpDown txtTime;
        private ColumnHeader infoTime;
        private CheckBox chkIsLoop;
        private Button btnClearLog;
        private CheckBox chkPush;
        private Button btnSave;
        private Button button1;
    }
}