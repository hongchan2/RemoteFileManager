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

namespace FileManager
{
    public partial class server : Form
    {
        // Server
        public bool m_bStop = false;
        private TcpListener m_server;
        private Thread m_thServer;

        // Server Packet
        public NetworkStream m_stream;
        private byte[] sendBuffer = new byte[1024 * 4];
        private byte[] readBuffer = new byte[1024 * 4];

        public Initialize m_initializeClass;
        public BeforeSelect m_beforeSelectClass;
        public BeforeExpand m_beforeExpandClass;
        public FileTransfer m_fileTransferClass;
        public ExitConnection m_exitConnectionClass;

        // Client and Connect
        public bool m_bConnect = false;
        TcpClient m_client;

        private string dirPath;

        public server()
        {
            InitializeComponent();
        }

        public void WriteLog(string msg)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                txtlog.AppendText(msg + "\n");
            }));
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

        public void ServerStart()
        {
            try
            {
                string ip = txtIp.Text;
                int port = int.Parse(txtPort.Text);
                IPAddress ipAddr = IPAddress.Parse(ip);

                m_server = new TcpListener(ipAddr, port);
                m_server.Start();

                m_bStop = true;
                WriteLog("클라이언트 접속 대기중...");

                while (m_bStop)
                {
                    m_client = m_server.AcceptTcpClient();

                    if (m_client.Connected)
                    {
                        m_bConnect = true;
                        WriteLog("클라이언트 접속");
                        m_stream = m_client.GetStream();
                    }

                    while (m_bConnect)
                    {
                        try
                        {
                            m_stream.Read(readBuffer, 0, 1024 * 4);
                        }
                        catch
                        {
                            WriteLog("서버에서 데이터를 읽는데 에러가 발생해 서버를 종료합니다.");
                            ServerStop();
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                btnServer.Text = "서버켜기";
                                btnServer.ForeColor = Color.Black;
                            }));
                            return;
                        }

                        Packet packet = (Packet)Packet.Desserialize(readBuffer);

                        switch ((int)packet.Type)
                        {
                            case (int)PacketType.init:
                                {
                                    m_initializeClass = (Initialize)Packet.Desserialize(readBuffer);
                                    WriteLog("초기화 데이터 요청..");

                                    /* Send Path to client */
                                    byte[] bytePath = Encoding.UTF8.GetBytes(dirPath);
                                    m_stream.Write(bytePath, 0, bytePath.Length);
                                    break;
                                }
                            case (int)PacketType.beforeSelect:
                                {
                                    m_beforeSelectClass = (BeforeSelect)Packet.Desserialize(readBuffer);
                                    WriteLog("beforeSelect 데이터 요청..");
                                    string path = m_beforeSelectClass.path;
                                    
                                    /* Send Dir and File Array to client */
                                    DirectoryInfo di;
                                    BeforeSelect bs = new BeforeSelect();
                                    try
                                    {
                                        di = new DirectoryInfo(path);
                                        bs.Type = (int)PacketType.beforeSelect;
                                        bs.diArray = di.GetDirectories();
                                        bs.fiArray = di.GetFiles();
                                        Packet.Serialize(bs).CopyTo(sendBuffer, 0);
                                        Send();
                                    }
                                    catch(Exception ex)
                                    {
                                        WriteLog("BeforeSelect error " + ex.Message);
                                    }
                                    break;
                                }
                            case (int)PacketType.beforeExpand:
                                {
                                    m_beforeExpandClass = (BeforeExpand)Packet.Desserialize(readBuffer);
                                    WriteLog("beforeExpand 데이터 요청..");
                                    string path = m_beforeExpandClass.path;

                                    /* Send Dir Array And Dictionary to client */
                                    DirectoryInfo di;
                                    DirectoryInfo diPlus;
                                    DirectoryInfo[] diArrayPlus;
                                    BeforeExpand be = new BeforeExpand();
                                    try
                                    {
                                        di = new DirectoryInfo(path);
                                        be.Type = (int)PacketType.beforeExpand;
                                        be.diArray = di.GetDirectories();

                                        /* To Set Plus */
                                        be.diAdd = new Dictionary<string, int>();
                                        foreach (DirectoryInfo dir in be.diArray)
                                        {
                                            diPlus = new DirectoryInfo(dir.FullName);
                                            diArrayPlus = diPlus.GetDirectories();
                                            if (diArrayPlus.Length > 0)
                                                be.diAdd.Add(dir.Name, 1);
                                            else
                                                be.diAdd.Add(dir.Name, 0);
                                        }
                                        Packet.Serialize(be).CopyTo(sendBuffer, 0);
                                        Send();                                
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLog("BeforeExpand error " + ex.Message);
                                    }
                                    break;
                                }
                            case (int)PacketType.fileTransfer:
                                {
                                    m_fileTransferClass = (FileTransfer)Packet.Desserialize(readBuffer);
                                    WriteLog("파일 전송 요청..");
                                    string path = m_fileTransferClass.path;
                                    long size = m_fileTransferClass.size;
                                    byte[] fileSendBuffer = new byte[1024];

                                    /* Create File Stream */
                                    FileStream fStr = new FileStream(path, FileMode.Open, FileAccess.Read);
                                    BinaryReader bReader = new BinaryReader(fStr);
                                    long loopCnt = (long)(size / 1024 + 1);

                                    try
                                    {
                                        /* Send File */
                                        int reSize = 1024;
                                        for (long i = 0; i < loopCnt; i++)
                                        {
                                            if (i == loopCnt - 1)
                                                reSize = (int)(size - (1024 * (loopCnt - 1)));

                                            fileSendBuffer = bReader.ReadBytes(reSize);
                                            m_stream.Write(fileSendBuffer, 0, reSize);
                                            m_stream.Flush();

                                            /* Reset Array */
                                            for (int j = 0; j < reSize; j++)
                                            {
                                                fileSendBuffer[j] = 0;
                                            }
                                        }

                                        WriteLog(path + " 파일 전송 완료");

                                    }
                                    catch(Exception ex)
                                    {
                                        WriteLog("FileTransfer error " + ex.Message);
                                    }
                                    finally
                                    {
                                        fStr.Close();
                                        bReader.Close();
                                    }
                                    break;
                                }
                            case (int)PacketType.exitConnection:
                                {
                                    WriteLog("클라이언트 연결 해제");

                                    /* Disconnection */
                                    m_bConnect = false;
                                    m_stream.Close();
                                    break;
                                }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message + " 예외 발생으로 인해 서버를 종료합니다.");
                ServerStop();
                this.Invoke(new MethodInvoker(delegate ()
                {
                    btnServer.Text = "서버켜기";
                    btnServer.ForeColor = Color.Black;
                }));
                return;
            }
        }

        public void ServerStop()
        {
            if (!m_bStop)
                return;

            m_bStop = false;
            m_bConnect = false;
            m_server.Stop();
            m_stream.Close();
            m_thServer.Abort();
            WriteLog("서버 종료");
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("경로를 선택해주세요.");
            }
            else
            {
                if(btnServer.Text == "서버켜기")
                {
                    m_thServer = new Thread(new ThreadStart(ServerStart));
                    m_thServer.Start();

                    btnServer.Text = "서버끊기";
                    btnServer.ForeColor = Color.Red;
                }
                else
                {
                    ServerStop();
                    btnServer.Text = "서버켜기";
                    btnServer.ForeColor = Color.Black;
                }
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                dirPath = fbd.SelectedPath;
                txtPath.Text = dirPath;
                WriteLog(dirPath + "로 경로가 수정되었습니다.");
            }
        }
    }
}
