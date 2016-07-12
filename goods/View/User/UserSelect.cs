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
using Observer;
namespace goods
{
    public partial class UserSelect : Form
    {
        userCtrl ctrl = new userCtrl();
        List<User> list_uids = new List<User>();
        public UserSelect()
        {
            InitializeComponent();
            initTable();
            searchUser(1);
        }
        private void initTable()
        {
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            newColumn.HeaderText = "选择";
            newColumn.FillWeight = 20;
            newColumn.Name = "ck";
            dataGridView1.Columns.Add(newColumn);

            DataGridViewColumn colId = new DataGridViewLinkColumn();
            colId.DataPropertyName = "id";//字段
            colId.Visible = false;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);

            DataGridViewColumn colUName = new DataGridViewTextBoxColumn();
            colUName.DataPropertyName = "username";//字段
            colUName.HeaderText = "用户名";
            colUName.Name = "username";
            dataGridView1.Columns.Add(colUName);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "fullname";//字段
            colName.HeaderText = "名称";
            dataGridView1.Columns.Add(colName);
        }

        private void searchUser(int index)
        {
            pagingCom1.PageIndex = index;
            pagingCom1.PageSize = 10;
            var dtData = ctrl.getUserList(pagingCom1.PageIndex, pagingCom1.PageSize,-1, textBox1.Text);

            //获取并设置总记录数
            pagingCom1.RecordCount = ctrl.getUserListCount(-1, textBox1.Text);

            dataGridView1.DataSource = dtData;
            dataGridView1.ClearSelection();
            pagingCom1.reSet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchUser(1);
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { searchUser(1); }
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            searchUser(pagingCom1.PageIndex);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var ischeck = !Convert.ToBoolean(this.dataGridView1[0, e.RowIndex].Value);
                this.dataGridView1[0, e.RowIndex].Value = ischeck;
                if (ischeck == true)
                {
                    list_uids.Add(new User(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value), dataGridView1.Rows[e.RowIndex].Cells["username"].Value.ToString()));
                }
                else
                {
                    list_uids.Remove(list_uids.Where(p => p.Id == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value)).FirstOrDefault());
                }
                //MessageBox.Show(e.RowIndex+"" + this.dataGridView1[0, e.RowIndex].Value.ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MidModule.SendUsers(this, new List<object>(list_uids));//发送参数值
            this.Close();
        }
    }
}
