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
    public partial class MsgBox : Form
    {
        informationCtrl ctrl = new informationCtrl();
        string key = "order";
        bool first = true;
        public MsgBox()
        {
            InitializeComponent();
            initTable();
            loadDate();
        }
        private void initTable()
        {
            MidModule.EventUser += new UsersDlg(saveDate);
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewColumn colId = new DataGridViewLinkColumn();
            colId.DataPropertyName = "id";//字段
            colId.Visible = false;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);

            DataGridViewColumn colUId = new DataGridViewLinkColumn();
            colUId.DataPropertyName = "uid";//字段
            colUId.Visible = false;
            colUId.Name = "uid";
            dataGridView1.Columns.Add(colUId);

            DataGridViewColumn colUName = new DataGridViewTextBoxColumn();
            colUName.DataPropertyName = "username";//字段
            colUName.HeaderText = "用户名";
            dataGridView1.Columns.Add(colUName);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "fullname";//字段
            colName.HeaderText = "名称";
            dataGridView1.Columns.Add(colName);

            dataGridView2.AutoGenerateColumns = false;

            DataGridViewColumn colId2 = new DataGridViewLinkColumn();
            colId2.DataPropertyName = "id";//字段
            colId2.Visible = false;
            colId2.Name = "id";
            dataGridView2.Columns.Add(colId2);

            DataGridViewColumn colUId2 = new DataGridViewLinkColumn();
            colUId2.DataPropertyName = "uid";//字段
            colUId2.Visible = false;
            colUId2.Name = "uid";
            dataGridView1.Columns.Add(colUId2);

            DataGridViewColumn colUName2 = new DataGridViewTextBoxColumn();
            colUName2.DataPropertyName = "username";//字段
            colUName2.HeaderText = "用户名";
            dataGridView2.Columns.Add(colUName2);

            DataGridViewColumn colName2 = new DataGridViewTextBoxColumn();
            colName2.DataPropertyName = "fullname";//字段
            colName2.HeaderText = "名称";
            dataGridView2.Columns.Add(colName2);

            tabControl1.Selected += TabControl1_Selected;
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage1)
            {
                key = "order";
            }
            else 
            {
                key = "stock";
                if (first)
                {
                    var dtData = ctrl.getbyFilter(key);
                    dataGridView2.DataSource = dtData;
                    first = false;
                }
            }
        }
        
        public void loadDate() {
            var dtData = ctrl.getbyFilter(key);
            if (key == "order") dataGridView1.DataSource = dtData;
            else dataGridView2.DataSource = dtData;
        } 
        
        public void saveDate(object sender, List<object> users)
        {
            if (users.Count > 0)
            {
                MessageModel res = ctrl.addbyids(users, key);
                if(res.Code > 0)
                {
                    MessageBox.Show(res.Msg);
                }
                else
                {
                    loadDate();
                }
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UserSelect view = new UserSelect();
            view.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MessageModel msg;
                    if (key == "order")
                    {
                        msg = ctrl.del(dataGridView1.CurrentRow.Cells["id"].Value);
                        if (msg.Code == 0)
                        {
                            this.dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        }
                        else
                        {
                            MessageBox.Show(msg.Msg);
                        }
                    }
                    else
                    {
                        msg = ctrl.del(dataGridView2.CurrentRow.Cells["id"].Value);
                        if (msg.Code == 0)
                        {
                            this.dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
                        }
                        else
                        {
                            MessageBox.Show(msg.Msg);
                        }
                    }
                    
                }
            }
        }

    }
}
