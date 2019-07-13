using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PacketDefine;

namespace FileClient
{
    public partial class Client : Form
    {
        // Cliet
        private TcpClient m_client;
        private bool m_bConnect = false;

        // Client Packet
        private NetworkStream m_stream;
        private byte[] sendBuffer = new byte[1024 * 4];
        private byte[] readBuffer = new byte[1024 * 4];

        // Dialog
        FormDetail fileDialog;
        Dictionary<string, FileInfo> fileInfoDIc = new Dictionary<string, FileInfo>();

        public Client()
        {
            InitializeComponent();
        }

        public void Connect()
        {
            string ip = txtIp.Text;
            int port = int.Parse(txtPort.Text);
            m_client = new TcpClient();
            IPAddress ipAddr = IPAddress.Parse(ip);

            try
            {
                m_client.Connect(ipAddr, port);
            }
            catch
            {
                m_bConnect = false;
                return;
            }

            m_bConnect = true;
            m_stream = m_client.GetStream();

            /* Communicate with Server to Print TreeView and ListView*/
            Init();
        }
        
        public void Disconnect()
        {
            if (!m_bConnect)
                return;

            /* Clear ListView And TreeView */
            lvwDir.Clear();
            trvDir.Nodes.Clear();

            /* Send Server to Alert Disconnection */
            ExitConnection ec = new ExitConnection();
            ec.Type = (int)PacketType.exitConnection;

            Packet.Serialize(ec).CopyTo(sendBuffer, 0);
            Send();

            m_bConnect = false;
            m_client.Close();
            m_stream.Close();
        }

        public void Send()
        {
            m_stream.Write(sendBuffer, 0, sendBuffer.Length);
            m_stream.Flush();

            for (int i = 0; i < 1024 * 4; i++)
            {
                sendBuffer[i] = 0;
            }
        }

        public void Init()
        {
            /* Send Init to Server */
            Initialize init = new Initialize();
            init.Type = (int)PacketType.init;
            init.path = "";

            Packet.Serialize(init).CopyTo(sendBuffer, 0);
            Send();

            /* Receive Path from Server */
            int bytes = m_stream.Read(readBuffer, 0, 1024 * 4);
            string path = Encoding.UTF8.GetString(readBuffer, 0, bytes);

            /* Set TreeView */
            TreeNode root;
            root = trvDir.Nodes.Add(path);
            root.ImageIndex = 1; // folder
            if (trvDir.SelectedNode == null)
                trvDir.SelectedNode = root;
            root.SelectedImageIndex = root.ImageIndex;
            root.Nodes.Add("");

            /* Set ListView */
            BeforeSelectMethod(path);
        }

        public void BeforeSelectMethod(string dirPath)
        {
            /* Send BeforeSelect to Server */
            BeforeSelect bs = new BeforeSelect();
            bs.Type = (int)PacketType.beforeSelect;
            bs.path = dirPath;

            Packet.Serialize(bs).CopyTo(sendBuffer, 0);
            Send();

            /* Receive Dir and File Array from Server */
            m_stream.Read(readBuffer, 0, 1024 * 4);
            BeforeSelect m_beforeSelectClass = (BeforeSelect)Packet.Desserialize(readBuffer);

            /* Set ListView */
            ListViewItem item;
            lvwDir.Items.Clear();
            foreach (DirectoryInfo tdis in m_beforeSelectClass.diArray)
            {
                item = lvwDir.Items.Add(tdis.Name);
                item.SubItems.Add("");
                item.SubItems.Add(tdis.LastWriteTime.ToString());
                item.ImageIndex = 1;
                item.Tag = "D";
            }

            fileInfoDIc.Clear();
            foreach (FileInfo fis in m_beforeSelectClass.fiArray)
            {
                /* To Use File Detail */
                if (!fileInfoDIc.ContainsKey(fis.Name))
                    fileInfoDIc.Add(fis.Name, fis);

                item = lvwDir.Items.Add(fis.Name);
                item.SubItems.Add(fis.Length.ToString());
                item.SubItems.Add(fis.LastWriteTime.ToString());
                if (fis.Extension == ".avi" || fis.Extension == ".wmv" || fis.Extension == ".mpg")
                    item.ImageIndex = 0;
                else if (fis.Extension == ".jpg" || fis.Extension == ".png" || fis.Extension == ".jpeg")
                    item.ImageIndex = 2;
                else if (fis.Extension == ".mp3" || fis.Extension == ".wav")
                    item.ImageIndex = 3;
                else if (fis.Extension == ".txt")
                    item.ImageIndex = 5;
                else
                    item.ImageIndex = 4;
                item.Tag = "F";
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string dirPath = fbd.SelectedPath;
                txtPath.Text = dirPath;
            }
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("경로를 선택해주세요.");
            }
            else
            {
                if (btnServer.Text == "서버연결")
                {
                    Connect();

                    if (m_bConnect)
                    {
                        btnServer.Text = "서버끊기";
                        btnServer.ForeColor = Color.Red;
                    }
                }
                else
                {
                    Disconnect();
                    btnServer.Text = "서버연결";
                    btnServer.ForeColor = Color.Black;
                }
            }
        }

        private void trvDir_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            /* Send BeforeExpand to Server */
            BeforeExpand be = new BeforeExpand();
            be.Type = (int)PacketType.beforeExpand;
            be.path = e.Node.FullPath;

            Packet.Serialize(be).CopyTo(sendBuffer, 0);
            Send();

            /* Receive Dir Array And Dictionary from Server */
            m_stream.Read(readBuffer, 0, 1024 * 4);
            BeforeExpand m_beforeExpandClass = (BeforeExpand)Packet.Desserialize(readBuffer);
            Dictionary<string, int> dirDic = m_beforeExpandClass.diAdd;

            /* Set TreeView */
            TreeNode node;
            e.Node.Nodes.Clear();
            foreach (DirectoryInfo dirs in m_beforeExpandClass.diArray)
            {
                node = e.Node.Nodes.Add(dirs.Name);
                node.ImageIndex = 1;

                /* Set Plus */
                if(dirDic[dirs.Name] == 1)
                {
                    node.Nodes.Add("");
                }
            }
        }

        private void trvDir_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            BeforeSelectMethod(e.Node.FullPath);
        }

        private void mnuView_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            mnuDetail.Checked = false;
            mnuList.Checked = false;
            mnuSmall.Checked = false;
            mnuLarge.Checked = false;

            switch (item.Text)
            {
                case "자세히":
                    mnuDetail.Checked = true;
                    lvwDir.View = View.Details;
                    break;
                case "간단히":
                    mnuList.Checked = true;
                    lvwDir.View = View.List;
                    break;
                case "작은 아이콘":
                    mnuSmall.Checked = true;
                    lvwDir.View = View.SmallIcon;
                    break;
                case "큰 아이콘":
                    mnuLarge.Checked = true;
                    lvwDir.View = View.LargeIcon;
                    break;
            }
        }

        public void OpenItem(ListViewItem item)
        {
            TreeNode node;
            TreeNode child;

            if(item.Tag.ToString() == "D")
            {
                node = trvDir.SelectedNode;
                /* BeforeExpand Event */
                node.Expand();

                child = node.FirstNode;

                while(child != null)
                {
                    if(child.Text == item.Text)
                    {
                        /* BeforeSelect Event */
                        trvDir.SelectedNode = child;
                        trvDir.Focus();
                    }
                    child = child.NextNode;
                }
            }
            else if(item.Tag.ToString() == "F")
            {
                // Already Clicked Change
                fileDialog = new FormDetail();
                bool isTrue = SetDetail();
                if (isTrue)
                    fileDialog.Show();
            }
        }

        private void lvwDir_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection siList;
            siList = lvwDir.SelectedItems;

            foreach (ListViewItem item in siList)
            {
                OpenItem(item);
            }
        }

        private void trvDir_AfterSelect(object sender, TreeViewEventArgs e)
        {
            trvDir.SelectedNode.SelectedImageIndex = 1;
        }

        public bool SetDetail()
        {
            ListView.SelectedListViewItemCollection siList = lvwDir.SelectedItems;
            ListViewItem lvwItem = siList[0];

            if (lvwItem.Tag.ToString() == "D")
            {
                MessageBox.Show("폴더는 상세정보를 지원하지 않습니다.");
                return false;
            }
            else
            {
                FileInfo fi = fileInfoDIc[lvwItem.SubItems[0].Text];

                /* Change Labels And Image */
                fileDialog.txtName.Text = fi.Name;
                fileDialog.picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                if (fi.Extension == ".avi" || fi.Extension == ".wmv" || fi.Extension == ".mpg")
                    fileDialog.picBox.Image = DetailImgList.Images[0];
                else if (fi.Extension == ".jpg" || fi.Extension == ".png" || fi.Extension == ".jpeg")
                    fileDialog.picBox.Image = DetailImgList.Images[2];
                else if (fi.Extension == ".mp3" || fi.Extension == ".wav")
                    fileDialog.picBox.Image = DetailImgList.Images[3];
                else if (fi.Extension == ".txt")
                    fileDialog.picBox.Image = DetailImgList.Images[5];
                else
                    fileDialog.picBox.Image = DetailImgList.Images[4];

                fileDialog.lblFormat.Text = fi.Extension.Substring(1); /* Erase '.' */
                fileDialog.lblLocation.Text = fi.DirectoryName;
                fileDialog.lblSize.Text = fi.Length.ToString() + " 바이트";

                fileDialog.lblMade.Text = fi.CreationTime.ToString();
                fileDialog.lblChange.Text = fi.LastWriteTime.ToString();
                fileDialog.lblAccess.Text = fi.LastAccessTime.ToString();

                return true;
            }
        }

        private void mnuMore_Click(object sender, EventArgs e)
        {
            fileDialog = new FormDetail();
            bool isTrue = SetDetail();
            if(isTrue)
                fileDialog.Show();
        }

        private void mnuDownload_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection siList = lvwDir.SelectedItems;
            ListViewItem lvwItem = siList[0];

            if (lvwItem.Tag.ToString() == "D")
            {
                MessageBox.Show("폴더는 다운로드를 지원하지 않습니다.");
            }
            else
            {
                FileInfo fi = fileInfoDIc[lvwItem.SubItems[0].Text];
                //long currentLength = 0;
                long totalLength = fi.Length;
                byte[] fileRecvBuffer = new byte[1024];

                /* Send File Path To Server */
                FileTransfer ft = new FileTransfer();
                ft.Type = (int)PacketType.fileTransfer;
                ft.path = fi.DirectoryName + "\\" + lvwItem.SubItems[0].Text;
                ft.size = totalLength;

                Packet.Serialize(ft).CopyTo(sendBuffer, 0);
                Send();

                /* Create File And FileStream */
                string path = txtPath.Text + "\\" + lvwItem.SubItems[0].Text;
                FileStream fStr = new FileStream(path, FileMode.Create, FileAccess.Write);
                BinaryWriter bWriter = new BinaryWriter(fStr);
                long loopCnt = (long)(totalLength / 1024 + 1);

                /* Download File */
                int reSize = 1024;
                //int sum = 0;
                for (long i = 0; i < loopCnt; i++)
                {
                    if (i == loopCnt - 1)
                        reSize = (int)(totalLength - (1024 * (loopCnt - 1)));
                    
                    m_stream.Read(fileRecvBuffer, 0, reSize);
                    bWriter.Write(fileRecvBuffer, 0, reSize);
                    //sum += reSize;
                }
                fStr.Close();
                bWriter.Close();
            }
        }
    }
}
