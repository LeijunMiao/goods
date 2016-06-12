using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goods.Model;
using goods.Controller;
namespace goods
{
    public partial class UserEditPopup : Form
    {
        userCtrl uctrl = new userCtrl();
        public UserView parentForm;
        int _id = -1;
        public UserEditPopup(UserView form,int id, string status)
        {
            parentForm = form;
            _id = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("密码不能为空");
            }
            else if(textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("二次密码不同");
                textBox3.Text = "";
            }
            else
            {
                MessageModel msg = uctrl.updatePwd(_id,textBox1.Text, textBox2.Text);
                if (msg.Code == 0)
                {
                    this.Hide();
                    parentForm.loadData();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
