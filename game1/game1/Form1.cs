using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace game1
{
    public partial class Form1 : Form
    {
        private NetworkStream stream;
        private TcpClient tcpClient = new TcpClient();

        public Form1()
        {
            InitializeComponent();

            //音乐
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = @"轻快.wav";
            sp.PlayLooping();

            //图片
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 30000;
            int i = 0;
            timer.Elapsed += delegate
            {
                i++;
                Image image = Image.FromFile(i + ".jpg");
                //图片的文件名从1.jpg开始
                pictureBox1.Image = image;
                if (i == 3) i = 0;
                //当文件名为3.jpg时即将i重置为0
            };
            timer.Start();


            try
            {
                //向指定的IP地址的服务器发出连接请求
                tcpClient.Connect("10.1.230.74", 3900);
                listBox1.Items.Add("连接成功！");
                stream = tcpClient.GetStream();
                byte[] data = new byte[1024];
                //判断网络流是否可读            
                if (stream.CanRead)
                {
                    int len = stream.Read(data, 0, data.Length);
                    //Encoding ToEncoding = Encoding.GetEncoding("UTF-8");
                    //Encoding FromEncoding = Encoding.GetEncoding("GB2312");
                    //data=Encoding.Convert(FromEncoding, ToEncoding, data);
                    //string msg = Encoding.UTF8.GetString(data, 0, data.Length);
                    string msg = Encoding.Default.GetString(data, 0, data.Length);
                    string str = "\r\n";
                    char[] str1 = str.ToCharArray();
                    string[] msg1 = msg.Split(str1);
                    for (int j = 0; j < msg1.Length; j++)
                    {
                        listBox1.Items.Add(msg1[j]);
                    }
                }
            }
            catch
            {
                listBox1.Items.Add("服务器未启动！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            //判断连接是否断开
            if (tcpClient.Connected)
            {
                //向服务器发送数据
                string msg = textBox1.Text;
                Byte[] outbytes = System.Text.Encoding.Default.GetBytes(msg + "\n");
                stream.Write(outbytes, 0, outbytes.Length);
                byte[] data = new byte[1024];
                //接收服务器回复数据
                if (stream.CanRead)
                {
                    int len = stream.Read(data, 0, data.Length);
                    string msg1 = Encoding.Default.GetString(data, 0, data.Length);
                    string str = "\r\n";
                    char[] str1 = str.ToCharArray();
                    string[] msg2 = msg1.Split(str1);
                    for (int j = 0; j < msg2.Length; j++)
                    {
                        listBox1.Items.Add(msg2[j]);
                    }
                }
            }
            else
            {
                listBox1.Items.Add("连接已断开");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
