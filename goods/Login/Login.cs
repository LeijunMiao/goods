using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goods.Controller;
namespace goods
{
    public partial class Login : Form
    {
        MD5Cls md5Cls = new MD5Cls();
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlHelper h = new MySqlHelper();
            try
            {
                if (this.textBox1.Text.Length == 0)
                {
                    MessageBox.Show("用户名不能为空！");
                }
                else if (this.textBox2.Text.Length == 0)
                {
                    MessageBox.Show("密码不能为空！");
                }
                else
                {
                    userCtrl ctrl = new userCtrl();
                    string sql = "select * from user where userName='" + this.textBox1.Text + "' and hashed_password='" + ctrl.GetOf(this.textBox2.Text) + "'";
                    DataTable dtuser = h.ExecuteQuery(sql, CommandType.Text);//DBHelp.GetDataSet(sql);

                    if (dtuser.Rows.Count > 0)
                    {
                        if(dtuser.Rows[0]["isActive"].ToString() == "1")
                        {
                            saveLocal();
                            PropertyClass.SendNameValue = dtuser.Rows[0]["fullName"].ToString();
                            PropertyClass.UserId = Convert.ToInt32(dtuser.Rows[0]["id"]);
                            //PropertyClass.SendPopedomValue = dtuser.Rows[0]["user_competence"].ToString();
                            //PropertyClass.Password = dtuser.Rows[0]["user_password"].ToString();
                            PropertyClass.Role = Convert.ToInt32(dtuser.Rows[0]["role"]);
                            MainForm mainform = new MainForm();
                            this.Hide();
                            mainform.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("用户已被注销！");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误！");
                        this.textBox2.Focus();
                        this.textBox2.SelectAll();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("服务器登陆失败！");
                return;
            }
            
        }

        private void saveLocal(){
            string key = md5Cls.GenerateKey();
            string xmlstr = "";
            xmlstr += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n";
            xmlstr += "<setting>\r\n";

            if (this.textBox1.Text.Trim() != "")
            {
                xmlstr += "<User_Num>" + this.textBox1.Text.Trim() + "</User_Num>\r\n";
            }
            //else
            //{
            //    xmlstr += "<User_Num></User_Num>\r\n";
            //}

            if (this.textBox2.Text.Trim() != "" && checkBox1.Checked)
            {
                xmlstr += "<User_Pwd>" + md5Cls.MD5Encrypt(this.textBox2.Text.Trim(), key) + "</User_Pwd>\r\n";
            }
            else
            {
                xmlstr += "<User_Pwd></User_Pwd>\r\n";
            }
            //else
            //{
            //    xmlstr += "<User_Pwd></User_Pwd>\r\n";
            //}
            xmlstr += "<Key>" + key + "</Key>\r\n"; 
            xmlstr += "</setting>";
            try
            {
                xmlDoc xmldoc2 = new xmlDoc();
                xmldoc2.LoadXmlString(xmlstr);
               var b = xmldoc2.SaveXmlFile("user.xml");
            }
            catch
            {
                
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Setting view = new Setting();
            view.Show();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            ServerInfo.GetServerInfo();
            string fileName = "user.xml";
            if (!System.IO.File.Exists(fileName))
            {
                return;
            }
            else
            {
                xmlDoc xmldoc = new xmlDoc();
                if (xmldoc.LoadXmlFile(fileName))
                {
                    this.textBox1.Text = xmldoc.readByNodeName("User_Num");
                    if (xmldoc.readByNodeName("User_Pwd") != "")
                    {
                        this.textBox2.Text = md5Cls.MD5Decrypt(xmldoc.readByNodeName("User_Pwd"), xmldoc.readByNodeName("Key"));
                        this.checkBox1.Checked = true;
                    }
                    
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
