using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goods
{
    public partial class Setting : Form
    {
        MD5Cls md5cls = new MD5Cls();
        public Setting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入服务器IP地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                this.textBox1.Focus();
                this.textBox1.SelectAll();
            }
            else if (this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库登录名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                this.textBox2.Focus();
                this.textBox2.SelectAll();
            }
            else if (this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                this.textBox3.Focus();
                this.textBox3.SelectAll();
            }

            else
            {
                string key = md5cls.GenerateKey();
                string xmlstr = "";
                xmlstr += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n";
                xmlstr += "<setting>\r\n";
                if (this.textBox1.Text.Trim() != "")
                {
                    xmlstr += "<ServerIP>" + this.textBox1.Text.Trim() + "</ServerIP>\r\n";
                }
                else
                {
                    xmlstr += "<ServerIP>127.0.0.1</ServerIP>\r\n";
                }

                if (this.textBox2.Text.Trim() != "")
                {
                    xmlstr += "<User_ID>" + this.textBox2.Text.Trim() + "</User_ID>\r\n";
                }
                else
                {
                    xmlstr += "<User_ID></User_ID>\r\n";
                }

                if (this.textBox3.Text.Trim() != "")
                {
                    xmlstr += "<User_Pwd>" + md5cls.MD5Encrypt(this.textBox3.Text.Trim(), key) + "</User_Pwd>\r\n";//
                }
                else
                {
                    xmlstr += "<User_Pwd></User_Pwd>\r\n";
                }
                xmlstr += "<Key>" + key + "</Key>\r\n";


                xmlstr += "</setting>";
                //MessageBox.Show(key);
                try
                {
                    xmlDoc xmldoc = new xmlDoc();
                    xmldoc.LoadXmlString(xmlstr);
                    //var b = xmldoc.SaveXmlFile(@"~/ipConfig.xml");//"\\ipConfig.xml"
                    var b = xmldoc.SaveXmlFile("ipConfig.xml");
                    
                    if (this.Text.Trim() == "")
                    {
                        ServerInfo.ServerIP = "127.0.0.1";
                    }
                    else
                    {
                        ServerInfo.ServerIP = this.textBox1.Text.Trim();
                    }
                    if (b)
                    {
                        MessageBox.Show("IP配置成功!");
                    }
                    else
                    {
                        MessageBox.Show("IP配置失败!");
                    }
                    ServerInfo.User_ID = this.textBox2.Text.Trim();
                    ServerInfo.User_Pwd = this.textBox3.Text.Trim();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("IP配置失败\r\n请检查网络或联系管理人员");
                }
            }
        }


        private void Setting_Load(object sender, System.EventArgs e)
        {
            this.textBox1.Text = ServerInfo.GetSettingIP();
            this.textBox2.Text = ServerInfo.GetSettingUser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
