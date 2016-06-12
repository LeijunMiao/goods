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

namespace goods
{
    public partial class UserView : Form
    {
        
        userCtrl uctrl = new userCtrl();
        roleCtrl rctrl = new roleCtrl();
        //private DataSet dsData = null;
        private DataTable dtData = null;
        private DataTable dt = null;
        //总记录数
        public int RecordCount = 0;
        public UserView()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void loadData() {
            BindDataWithPage(1);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = rctrl.get();
            DataRow dr = dt.NewRow();
            dr["id"] = "-1";
            dr["name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            this.comboBox1.DataSource = dt;
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "id";
            this.comboBox1.SelectedIndex = 0;
            BindDataWithPage(1);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }
        /// <summary>
        /// 绑定第Index页的数据
        /// </summary>
        /// <param name="Index"></param>
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            int roleId = Convert.ToInt32(this.comboBox1.SelectedValue.ToString());
            innerParmas parmas = new innerParmas(pagingCom1.PageIndex, pagingCom1.PageSize, roleId, textBox1.Text);//textBox1.Text
            dtData = uctrl.getUserList(parmas.pageIndex, parmas.pageSize, parmas.role, parmas.fullName);
            //获取并设置总记录数
            pagingCom1.RecordCount = uctrl.getUserListCount(roleId, textBox1.Text);//Convert.ToInt32(dsData.Tables[1].Rows[0][0]);
            //dtData = dsData.Tables[0];
            dt = new DataTable();
            DataColumn dcNO = new DataColumn("编号");
            DataColumn dcUName = new DataColumn("用户名");
            DataColumn dcFName = new DataColumn("姓名");
            DataColumn dcRole = new DataColumn("职位");
            DataColumn dcStatus = new DataColumn("状态");
            dt.Columns.Add(dcNO);
            dt.Columns.Add(dcUName);
            dt.Columns.Add(dcFName);
            dt.Columns.Add(dcRole);
            dt.Columns.Add(dcStatus);

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["id"].ToString();
                dr[1] = dtData.Rows[i]["userName"].ToString();
                dr[2] = dtData.Rows[i]["fullName"].ToString();
                dr[3] = dtData.Rows[i]["name"].ToString();
                if (dtData.Rows[i]["isActive"].ToString() == "1") dr[4] = "激活";
                else dr[4] = "注销";
                //dr[4] = dtData.Rows[i]["isActive"].ToString();
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;
            pagingCom1.reSet();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UserPopup popup = new UserPopup(this);
            popup.Show();
        }

        /// <summary>
        /// 内部类：搜索信息
        /// </summary>
        public class innerParmas
        {
            public int pageIndex;
            public int pageSize;
            public int role;
            public string fullName;
            public innerParmas(int pageIndex, int pageSize, int role, string fullName)
            {
                this.pageIndex = pageIndex;
                this.pageSize = pageSize;
                this.role = role;
                this.fullName = fullName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) MessageBox.Show("请选中一行");
            else
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string status = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                UserEditPopup popup = new UserEditPopup(this, id, status);
                popup.Show();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) MessageBox.Show("请选中一行");
            else
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string status = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                MessageModel msg = uctrl.updateStatus(id, status);
                if (msg.Code != 0)
                {
                    MessageBox.Show(msg.Msg);
                }
                else
                {
                    if(status == "激活")
                        dataGridView1.SelectedRows[0].Cells[4].Value = "注销";
                    else
                        dataGridView1.SelectedRows[0].Cells[4].Value = "激活";
                }
            }
        }
    }
}
