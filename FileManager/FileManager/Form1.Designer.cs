namespace FileManager
{
    partial class server
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.btnServer = new System.Windows.Forms.Button();
            this.txtlog = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "PORT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "PATH";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(147, 21);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(742, 35);
            this.txtIp.TabIndex = 3;
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(935, 32);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(197, 62);
            this.btnServer.TabIndex = 6;
            this.btnServer.Text = "서버켜기";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // txtlog
            // 
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.Location = new System.Drawing.Point(3, 31);
            this.txtlog.Multiline = true;
            this.txtlog.Name = "txtlog";
            this.txtlog.Size = new System.Drawing.Size(1092, 526);
            this.txtlog.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtlog);
            this.groupBox1.Location = new System.Drawing.Point(34, 187);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1098, 560);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "log";
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(147, 132);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(742, 35);
            this.txtPath.TabIndex = 9;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(147, 77);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(742, 35);
            this.txtPort.TabIndex = 10;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(935, 118);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(197, 49);
            this.btnPath.TabIndex = 11;
            this.btnPath.Text = "경로선택 ";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 760);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "server";
            this.Text = "File Manager - Server";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.TextBox txtlog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.FolderBrowserDialog fbd;
    }
}

