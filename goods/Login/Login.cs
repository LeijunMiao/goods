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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                string sql = "select * from sys_user where user_no='" + this.textBox1.Text + "' and user_password='" + this.textBox2.Text + "'";
                DataTable dtuser = DBHelp.GetDataSet(sql);

                if (dtuser.Rows.Count > 0)
                {
                    PropertyClass.SendNameValue = dtuser.Rows[0]["user_no"].ToString();
                    PropertyClass.SendPopedomValue = dtuser.Rows[0]["user_competence"].ToString();
                    PropertyClass.Password = dtuser.Rows[0]["user_password"].ToString();

                    MainForm mainform = new MainForm();
                    this.Hide();
                    mainform.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或者密码错误！");
                    this.textBox2.Focus();
                    this.textBox2.SelectAll();
                }
            }
        }
    }
}
