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
using goods.Model;
namespace goods.Role
{
    public partial class RolePopup : Form
    {
        departmentCtrl ctrl = new departmentCtrl();//部门控制类
        roleCtrl rctrl = new roleCtrl();
        public string roleId;
        public RoleView parentForm;
        public RolePopup(RoleView form, string roleId)
        {
            InitializeComponent();
            this.parentForm = form;
            this.roleId = roleId;
            LoadData();
        }

        #region 加载部门列表
        private void LoadData()
        {
            DataTable dt = ctrl.get();
            this.comboBox1.DataSource = dt;
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "id";
            this.comboBox1.SelectedIndex = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["id"].ToString() == this.roleId) this.comboBox1.SelectedIndex = i;
            }
            
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(this.comboBox1.SelectedValue.ToString());
            RoleModel rm = new RoleModel(textBox1.Text, value);
            MessageModel msg = rctrl.add(rm);
            if (msg.Code == 0)
            {
                this.Hide();
                parentForm.LoadRoleDate(value);
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
            
        }
    }
}
