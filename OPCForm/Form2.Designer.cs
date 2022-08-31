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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.NumericUpDown();
            this.txtClear = new System.Windows.Forms.Button();
            this.txtSubscribe = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort)).BeginInit();
            this.SuspendLayout();
            // 
            // labClientId
            // 
            this.labClientId.AutoSize = true;
            this.labClientId.Location = new System.Drawing.Point(91, 180);
            this.labClientId.Name = "labClientId";
            this.labClientId.Size = new System.Drawing.Size(54, 20);
            this.labClientId.TabIndex = 0;
            this.labClientId.Text = "设备ID";
            // 
            // labUserName
            // 
            this.labUserName.AutoSize = true;
            this.labUserName.Location = new System.Drawing.Point(91, 232);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(54, 20);
            this.labUserName.TabIndex = 1;
            this.labUserName.Text = "用户名";
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(91, 283);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(39, 20);
            this.labPassword.TabIndex = 2;
            this.labPassword.Text = "密码";
            // 
            // txtClientId
            // 
            this.txtClientId.Location = new System.Drawing.Point(163, 177);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(368, 27);
            this.txtClientId.TabIndex = 3;
            this.txtClientId.Text = "mytest";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(163, 229);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(368, 27);
            this.txtUserName.TabIndex = 4;
            this.txtUserName.Text = "admin";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(163, 280);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(368, 27);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Text = "123qwe";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(170, 647);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // labAddress
            // 
            this.labAddress.AutoSize = true;
            this.labAddress.Location = new System.Drawing.Point(93, 71);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(52, 20);
            this.labAddress.TabIndex = 7;
            this.labAddress.Text = "IP地址";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(99, 122);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(39, 20);
            this.labPort.TabIndex = 8;
            this.labPort.Text = "端口";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(163, 68);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(251, 27);
            this.txtAddress.TabIndex = 9;
            this.txtAddress.Text = "10.47.102.70";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(167, 329);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 29);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "启动链接";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(287, 329);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(94, 29);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "关闭链接";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtTopic
            // 
            this.txtTopic.Location = new System.Drawing.Point(163, 415);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(368, 27);
            this.txtTopic.TabIndex = 13;
            this.txtTopic.Text = "opc/pc/test";
            // 
            // labTopic
            // 
            this.labTopic.AutoSize = true;
            this.labTopic.Location = new System.Drawing.Point(106, 418);
            this.labTopic.Name = "labTopic";
            this.labTopic.Size = new System.Drawing.Size(39, 20);
            this.labTopic.TabIndex = 14;
            this.labTopic.Text = "主题";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(561, 68);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(557, 439);
            this.txtMessage.TabIndex = 15;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(561, 622);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(557, 149);
            this.txtSend.TabIndex = 16;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(1015, 587);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(94, 29);
            this.btnSend.TabIndex = 17;
            this.btnSend.Text = "发送消息";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(167, 123);
            this.txtPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(150, 27);
            this.txtPort.TabIndex = 18;
            this.txtPort.Value = new decimal(new int[] {
            1883,
            0,
            0,
            0});
            // 
            // txtClear
            // 
            this.txtClear.Location = new System.Drawing.Point(583, 517);
            this.txtClear.Name = "txtClear";
            this.txtClear.Size = new System.Drawing.Size(94, 29);
            this.txtClear.TabIndex = 19;
            this.txtClear.Text = "清空消息";
            this.txtClear.UseVisualStyleBackColor = true;
            this.txtClear.Click += new System.EventHandler(this.txtClear_Click);
            // 
            // txtSubscribe
            // 
            this.txtSubscribe.Location = new System.Drawing.Point(170, 486);
            this.txtSubscribe.Name = "txtSubscribe";
            this.txtSubscribe.Size = new System.Drawing.Size(94, 29);
            this.txtSubscribe.TabIndex = 20;
            this.txtSubscribe.Text = "订阅主题";
            this.txtSubscribe.UseVisualStyleBackColor = true;
            this.txtSubscribe.Click += new System.EventHandler(this.txtSubscribe_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 833);
            this.Controls.Add(this.txtSubscribe);
            this.Controls.Add(this.txtClear);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtMessage);
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
        private TextBox txtMessage;
        private TextBox txtSend;
        private Button btnSend;
        private NumericUpDown txtPort;
        private Button txtClear;
        private Button txtSubscribe;
    }
}