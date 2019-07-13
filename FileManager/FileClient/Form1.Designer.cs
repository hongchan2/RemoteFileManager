namespace FileClient
{
    partial class Client
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.label1 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.btnServer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnPath = new System.Windows.Forms.Button();
            this.trvDir = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.lvwDir = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmuListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.DetailImgList = new System.Windows.Forms.ImageList(this.components);
            this.cmuListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP :";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(86, 40);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(668, 35);
            this.txtIp.TabIndex = 1;
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(219, 153);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(146, 44);
            this.btnServer.TabIndex = 2;
            this.btnServer.Text = "서버연결";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "다운로드 경로 :";
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(219, 98);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(920, 35);
            this.txtPath.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(798, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "PORT :";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(891, 40);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(248, 35);
            this.txtPort.TabIndex = 6;
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(781, 153);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(146, 44);
            this.btnDir.TabIndex = 7;
            this.btnDir.Text = "폴더열기";
            this.btnDir.UseVisualStyleBackColor = true;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(497, 153);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(146, 44);
            this.btnPath.TabIndex = 8;
            this.btnPath.Text = "경로설정";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // trvDir
            // 
            this.trvDir.ImageIndex = 0;
            this.trvDir.ImageList = this.imgList;
            this.trvDir.Location = new System.Drawing.Point(3, 222);
            this.trvDir.Name = "trvDir";
            this.trvDir.SelectedImageIndex = 0;
            this.trvDir.Size = new System.Drawing.Size(287, 687);
            this.trvDir.TabIndex = 9;
            this.trvDir.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvDir_BeforeExpand);
            this.trvDir.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvDir_BeforeSelect);
            this.trvDir.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDir_AfterSelect);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "avi.png");
            this.imgList.Images.SetKeyName(1, "folder.png");
            this.imgList.Images.SetKeyName(2, "image.png");
            this.imgList.Images.SetKeyName(3, "music.png");
            this.imgList.Images.SetKeyName(4, "temp.png");
            this.imgList.Images.SetKeyName(5, "text.png");
            // 
            // lvwDir
            // 
            this.lvwDir.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colFileSize,
            this.colFileDate});
            this.lvwDir.ContextMenuStrip = this.cmuListView;
            this.lvwDir.LargeImageList = this.imgList;
            this.lvwDir.Location = new System.Drawing.Point(290, 222);
            this.lvwDir.Name = "lvwDir";
            this.lvwDir.Size = new System.Drawing.Size(876, 687);
            this.lvwDir.SmallImageList = this.imgList;
            this.lvwDir.TabIndex = 10;
            this.lvwDir.UseCompatibleStateImageBehavior = false;
            this.lvwDir.DoubleClick += new System.EventHandler(this.lvwDir_DoubleClick);
            // 
            // colFileName
            // 
            this.colFileName.Text = "파일이름";
            this.colFileName.Width = 150;
            // 
            // colFileSize
            // 
            this.colFileSize.Text = "파일크기";
            this.colFileSize.Width = 50;
            // 
            // colFileDate
            // 
            this.colFileDate.Text = "수정한날짜";
            this.colFileDate.Width = 160;
            // 
            // cmuListView
            // 
            this.cmuListView.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmuListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMore,
            this.mnuDownload,
            this.toolStripMenuItem1,
            this.mnuDetail,
            this.mnuList,
            this.mnuSmall,
            this.mnuLarge});
            this.cmuListView.Name = "contextMenuStrip1";
            this.cmuListView.Size = new System.Drawing.Size(219, 226);
            // 
            // mnuMore
            // 
            this.mnuMore.Name = "mnuMore";
            this.mnuMore.Size = new System.Drawing.Size(218, 36);
            this.mnuMore.Text = "상세정보";
            this.mnuMore.Click += new System.EventHandler(this.mnuMore_Click);
            // 
            // mnuDownload
            // 
            this.mnuDownload.Name = "mnuDownload";
            this.mnuDownload.Size = new System.Drawing.Size(218, 36);
            this.mnuDownload.Text = "다운로드";
            this.mnuDownload.Click += new System.EventHandler(this.mnuDownload_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(215, 6);
            // 
            // mnuDetail
            // 
            this.mnuDetail.Name = "mnuDetail";
            this.mnuDetail.Size = new System.Drawing.Size(218, 36);
            this.mnuDetail.Text = "자세히";
            this.mnuDetail.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // mnuList
            // 
            this.mnuList.Name = "mnuList";
            this.mnuList.Size = new System.Drawing.Size(218, 36);
            this.mnuList.Text = "간단히";
            this.mnuList.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // mnuSmall
            // 
            this.mnuSmall.Name = "mnuSmall";
            this.mnuSmall.Size = new System.Drawing.Size(218, 36);
            this.mnuSmall.Text = "작은 아이콘";
            this.mnuSmall.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // mnuLarge
            // 
            this.mnuLarge.Name = "mnuLarge";
            this.mnuLarge.Size = new System.Drawing.Size(218, 36);
            this.mnuLarge.Text = "큰 아이콘";
            this.mnuLarge.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // DetailImgList
            // 
            this.DetailImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("DetailImgList.ImageStream")));
            this.DetailImgList.TransparentColor = System.Drawing.Color.Transparent;
            this.DetailImgList.Images.SetKeyName(0, "avi.png");
            this.DetailImgList.Images.SetKeyName(1, "folder.png");
            this.DetailImgList.Images.SetKeyName(2, "image.png");
            this.DetailImgList.Images.SetKeyName(3, "music.png");
            this.DetailImgList.Images.SetKeyName(4, "temp.png");
            this.DetailImgList.Images.SetKeyName(5, "text.png");
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 906);
            this.Controls.Add(this.lvwDir);
            this.Controls.Add(this.trvDir);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label1);
            this.Name = "Client";
            this.Text = "File Manager - Client";
            this.cmuListView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.TreeView trvDir;
        private System.Windows.Forms.ListView lvwDir;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ContextMenuStrip cmuListView;
        private System.Windows.Forms.ToolStripMenuItem mnuMore;
        private System.Windows.Forms.ToolStripMenuItem mnuDownload;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuList;
        private System.Windows.Forms.ToolStripMenuItem mnuSmall;
        private System.Windows.Forms.ToolStripMenuItem mnuLarge;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colFileSize;
        private System.Windows.Forms.ColumnHeader colFileDate;
        private System.Windows.Forms.ImageList DetailImgList;
    }
}

