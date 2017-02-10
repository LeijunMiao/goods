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
    public partial class UserPopup : Form
    {
        userCtrl uctrl = new userCtrl();
        departmentCtrl ctrl = new departmentCtrl();//部门控制类
        roleCtrl rctrl = new roleCtrl();
        DataTable depdt = null;
        public int depId;

        public UserView parentForm;
        public UserPopup(UserView form)
        {
            parentForm = form;
            InitializeComponent();
            LoadData();
        }
        #region 加载部门列表
        private void LoadData()
        {
            depdt = ctrl.get();
            this.comboBox1.DataSource = depdt;
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "id";
            this.comboBox1.SelectedIndex = 0;
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox4.Text.Trim() == "")
            {
                MessageBox.Show("用户名密码不得为空！");
                return;
            }
            if(this.comboBox1.SelectedValue == null || this.comboBox2.SelectedValue == null)
            {
                MessageBox.Show("部门和角色必选！");
                return;
            }
            int departant = Convert.ToInt32(this.comboBox1.SelectedValue.ToString());
            int role = Convert.ToInt32(this.comboBox2.SelectedValue.ToString());
            User u = new User(textBox1.Text, textBox2.Text, role, textBox4.Text);
            MessageModel msg = uctrl.add(u);
            if (msg.Code == 0)
            {
                parentForm.loadData();
                this.Close();
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string role = this.comboBox1.SelectedValue.ToString();
            if (this.comboBox1.SelectedIndex == 0) role = depdt.Rows[0]["id"].ToString();
            DataTable dt = rctrl.getbyDepId(Convert.ToInt32(role));
            this.comboBox2.Text = "";
            this.comboBox2.DataSource = dt;
            this.comboBox2.DisplayMember = "name";
            this.comboBox2.ValueMember = "id";
            if(dt.Rows.Count >0 ) this.comboBox2.SelectedIndex = 0;
        }
    }
}
