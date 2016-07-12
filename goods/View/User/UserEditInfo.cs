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
    public partial class UserEditInfo : Form
    {
        User u = new User();
        userCtrl uctrl = new userCtrl();
        departmentCtrl ctrl = new departmentCtrl();//部门控制类
        roleCtrl rctrl = new roleCtrl();
        DataTable depdt = null;
        bool isFirst = true;
        public int depId;
        public UserView parentForm;
        public UserEditInfo(UserView form,object u)
        {
            this.u = (User)u;
            parentForm = form;
            InitializeComponent();
            LoadData();
            isFirst = false;
        }
        private void LoadData()
        {
            DataTable roleDT = rctrl.getSimilarRole(u.Role);
            this.comboBox2.DataSource = roleDT;
            this.comboBox2.DisplayMember = "name";
            this.comboBox2.ValueMember = "id";
            this.comboBox2.SelectedValue = u.Role;
            depdt = ctrl.get();
            this.comboBox1.DataSource = depdt;
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "id";
            this.comboBox1.SelectedValue = roleDT.Rows[0]["departmentId"];
            textBox1.Text = u.UserName;
            textBox2.Text = u.FullName;
            //this.comboBox2.ValueMember = u.Role.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int departant = Convert.ToInt32(this.comboBox1.SelectedValue.ToString());
            int role = Convert.ToInt32(this.comboBox2.SelectedValue.ToString());
            User update = new User(u.Id,textBox1.Text, textBox2.Text, role);
            MessageModel msg = uctrl.set(update);
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
            if (!isFirst)
            {
                string role = this.comboBox1.SelectedValue.ToString();
                if (this.comboBox1.SelectedIndex == 0) role = depdt.Rows[0]["id"].ToString();
                DataTable dt = rctrl.getbyDepId(Convert.ToInt32(role));
                this.comboBox2.DataSource = dt;
                this.comboBox2.DisplayMember = "name";
                this.comboBox2.ValueMember = "id";
                this.comboBox2.SelectedIndex = 0;

            }

        }
    }
}
