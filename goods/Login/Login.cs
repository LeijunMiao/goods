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
        MySqlHelper h = new MySqlHelper();
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
                userCtrl ctrl = new userCtrl();
                string sql = "select * from user where userName='" + this.textBox1.Text + "' and hashed_password='" + ctrl.GetOf(this.textBox2.Text) + "'";
                DataTable dtuser = h.ExecuteQuery(sql, CommandType.Text);//DBHelp.GetDataSet(sql);

                if (dtuser.Rows.Count > 0)
                {
                    PropertyClass.SendNameValue = dtuser.Rows[0]["userName"].ToString();
                    PropertyClass.UserId = Convert.ToInt32(dtuser.Rows[0]["id"]);
                    //PropertyClass.SendPopedomValue = dtuser.Rows[0]["user_competence"].ToString();
                    //PropertyClass.Password = dtuser.Rows[0]["user_password"].ToString();
                    PropertyClass.Role = Convert.ToInt32(dtuser.Rows[0]["role"].ToString());
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
