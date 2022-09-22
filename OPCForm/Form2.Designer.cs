namespace OPCForm
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.labClientId = new System.Windows.Forms.Label();
            this.labUserName = new System.Windows.Forms.Label();
            this.labPassword = new System.Windows.Forms.Label();
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.labAddress = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtTopic = new System.Windows.Forms.TextBox();
            this.labTopic = new System.Windows.Forms.Label();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.NumericUpDown();
            this.txtClear = new System.Windows.Forms.Button();
            this.txtSubscribe = new System.Windows.Forms.Button();
            this.btnUnSubscribe = new System.Windows.Forms.Button();
            this.listSubscribe = new System.Windows.Forms.ListBox();
            this.msgListView = new System.Windows.Forms.ListView();
            this.msgImageList = new System.Windows.Forms.ImageList(this.components);
            this.labSubscribeList = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort)).BeginInit();
            this.SuspendLayout();
            // 
            // labClientId
            // 
            this.labClientId.AutoSize = true;
            this.labClientId.Location = new System.Drawing.Point(66, 180);
            this.labClientId.Name = "labClientId";
            this.labClientId.Size = new System.Drawing.Size(54, 20);
            this.labClientId.TabIndex = 0;
            this.labClientId.Text = "设备ID";
            // 
            // labUserName
            // 
            this.labUserName.AutoSize = true;
            this.labUserName.Location = new System.Drawing.Point(66, 232);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(54, 20);
            this.labUserName.TabIndex = 1;
            this.labUserName.Text = "用户名";
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(66, 283);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(39, 20);
            this.labPassword.TabIndex = 2;
            this.labPassword.Text = "密码";
            // 
            // txtClientId
            // 
            this.txtClientId.Location = new System.Drawing.Point(138, 177);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(368, 27);
            this.txtClientId.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(138, 229);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(368, 27);
            this.txtUserName.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(138, 280);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(368, 27);
            this.txtPassword.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(379, 329);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labAddress
            // 
            this.labAddress.AutoSize = true;
            this.labAddress.Location = new System.Drawing.Point(68, 71);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(52, 20);
            this.labAddress.TabIndex = 7;
            this.labAddress.Text = "IP地址";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(74, 122);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(39, 20);
            this.labPort.TabIndex = 8;
            this.labPort.Text = "端口";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(138, 68);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(368, 27);
            this.txtAddress.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(142, 329);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 29);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "启动链接";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(262, 329);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(94, 29);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "关闭链接";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtTopic
            // 
            this.txtTopic.Location = new System.Drawing.Point(667, 68);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(536, 27);
            this.txtTopic.TabIndex = 7;
            this.txtTopic.Text = "$oc/devices/{id}/sys/messages/";
            // 
            // labTopic
            // 
            this.labTopic.AutoSize = true;
            this.labTopic.Location = new System.Drawing.Point(610, 71);
            this.labTopic.Name = "labTopic";
            this.labTopic.Size = new System.Drawing.Size(39, 20);
            this.labTopic.TabIndex = 14;
            this.labTopic.Text = "主题";
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(26, 419);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(480, 127);
            this.txtSend.TabIndex = 16;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(529, 456);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(94, 44);
            this.btnSend.TabIndex = 17;
            this.btnSend.Text = "发送消息";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(138, 122);
            this.txtPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(150, 27);
            this.txtPort.TabIndex = 1;
            this.txtPort.Value = new decimal(new int[] {
            1883,
            0,
            0,
            0});
            // 
            // txtClear
            // 
            this.txtClear.Location = new System.Drawing.Point(26, 562);
            this.txtClear.Name = "txtClear";
            this.txtClear.Size = new System.Drawing.Size(94, 29);
            this.txtClear.TabIndex = 12;
            this.txtClear.Text = "清空消息";
            this.txtClear.UseVisualStyleBackColor = true;
            this.txtClear.Click += new System.EventHandler(this.txtClear_Click);
            // 
            // txtSubscribe
            // 
            this.txtSubscribe.Location = new System.Drawing.Point(1076, 113);
            this.txtSubscribe.Name = "txtSubscribe";
            this.txtSubscribe.Size = new System.Drawing.Size(94, 29);
            this.txtSubscribe.TabIndex = 8;
            this.txtSubscribe.Text = "订阅主题";
            this.txtSubscribe.UseVisualStyleBackColor = true;
            this.txtSubscribe.Click += new System.EventHandler(this.txtSubscribe_Click);
            // 
            // btnUnSubscribe
            // 
            this.btnUnSubscribe.Location = new System.Drawing.Point(1088, 517);
            this.btnUnSubscribe.Name = "btnUnSubscribe";
            this.btnUnSubscribe.Size = new System.Drawing.Size(94, 29);
            this.btnUnSubscribe.TabIndex = 9;
            this.btnUnSubscribe.Text = "取消订阅";
            this.btnUnSubscribe.UseVisualStyleBackColor = true;
            this.btnUnSubscribe.Click += new System.EventHandler(this.btnUnSubscribe_Click);
            // 
            // listSubscribe
            // 
            this.listSubscribe.FormattingEnabled = true;
            this.listSubscribe.ItemHeight = 20;
            this.listSubscribe.Location = new System.Drawing.Point(667, 157);
            this.listSubscribe.Name = "listSubscribe";
            this.listSubscribe.Size = new System.Drawing.Size(536, 324);
            this.listSubscribe.TabIndex = 18;
            this.listSubscribe.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listSubscribe_MouseDoubleClick);
            // 
            // msgListView
            // 
            this.msgListView.Location = new System.Drawing.Point(26, 607);
            this.msgListView.Name = "msgListView";
            this.msgListView.Size = new System.Drawing.Size(1177, 214);
            this.msgListView.TabIndex = 19;
            this.msgListView.UseCompatibleStateImageBehavior = false;
            // 
            // msgImageList
            // 
            this.msgImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.msgImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("msgImageList.ImageStream")));
            this.msgImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.msgImageList.Images.SetKeyName(0, "error.png");
            this.msgImageList.Images.SetKeyName(1, "success.png");
            this.msgImageList.Images.SetKeyName(2, "send.png");
            this.msgImageList.Images.SetKeyName(3, "message.png");
            this.msgImageList.Images.SetKeyName(4, "subscribe.png");
            this.msgImageList.Images.SetKeyName(5, "warning.png");
            // 
            // labSubscribeList
            // 
            this.labSubscribeList.AutoSize = true;
            this.labSubscribeList.Location = new System.Drawing.Point(592, 232);
            this.labSubscribeList.Name = "labSubscribeList";
            this.labSubscribeList.Size = new System.Drawing.Size(69, 20);
            this.labSubscribeList.TabIndex = 20;
            this.labSubscribeList.Text = "主题列表";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 833);
            this.Controls.Add(this.labSubscribeList);
            this.Controls.Add(this.msgListView);
            this.Controls.Add(this.listSubscribe);
            this.Controls.Add(this.btnUnSubscribe);
            this.Controls.Add(this.txtSubscribe);
            this.Controls.Add(this.txtClear);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.labTopic);
            this.Controls.Add(this.txtTopic);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.labPort);
            this.Controls.Add(this.labAddress);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtClientId);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.labUserName);
            this.Controls.Add(this.labClientId);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.txtPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labClientId;
        private Label labUserName;
        private Label labPassword;
        private TextBox txtClientId;
        private TextBox txtUserName;
        private TextBox txtPassword;
        private Button btnSave;
        private Label labAddress;
        private Label labPort;
        private TextBox txtAddress;
        private Button btnStart;
        private Button btnStop;
        private TextBox txtTopic;
        private Label labTopic;
        private TextBox txtSend;
        private Button btnSend;
        private NumericUpDown txtPort;
        private Button txtClear;
        private Button txtSubscribe;
        private Button btnUnSubscribe;
        private ListBox listSubscribe;
        private ListView msgListView;
        private ImageList msgImageList;
        private Label labSubscribeList;
    }
}