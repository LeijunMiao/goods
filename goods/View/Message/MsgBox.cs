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
    public partial class MsgBox : Form
    {
        informationCtrl ctrl = new informationCtrl();
        string key = "order";
        bool first = true;
        bool firstTab3 = true;
        public MsgBox()
        {
            InitializeComponent();
            initTable();
            loadDate();
        }
        private void initTable()
        {
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
            dataGridView2.Columns.Add(colUId2);

            DataGridViewColumn colUName2 = new DataGridViewTextBoxColumn();
            colUName2.DataPropertyName = "username";//字段
            colUName2.HeaderText = "用户名";
            dataGridView2.Columns.Add(colUName2);

            DataGridViewColumn colName2 = new DataGridViewTextBoxColumn();
            colName2.DataPropertyName = "fullname";//字段
            colName2.HeaderText = "名称";
            dataGridView2.Columns.Add(colName2);


            dataGridView3.AutoGenerateColumns = false;

            DataGridViewColumn colId3 = new DataGridViewLinkColumn();
            colId3.DataPropertyName = "id";//字段
            colId3.Visible = false;
            colId3.Name = "id";
            dataGridView3.Columns.Add(colId3);

            DataGridViewColumn colUId3 = new DataGridViewLinkColumn();
            colUId3.DataPropertyName = "uid";//字段
            colUId3.Visible = false;
            colUId3.Name = "uid";
            dataGridView3.Columns.Add(colUId3);

            DataGridViewColumn colUName3 = new DataGridViewTextBoxColumn();
            colUName3.DataPropertyName = "username";//字段
            colUName3.HeaderText = "用户名";
            dataGridView3.Columns.Add(colUName3);

            DataGridViewColumn colName3 = new DataGridViewTextBoxColumn();
            colName3.DataPropertyName = "fullname";//字段
            colName3.HeaderText = "名称";
            dataGridView3.Columns.Add(colName3);

            tabControl1.Selected += TabControl1_Selected;
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage1)
            {
                key = "order";
            }
            else if(e.TabPage == tabPage2)
            {
                key = "stock";
                if (first)
                {
                    var dtData = ctrl.getbyFilter(key);
                    dataGridView2.DataSource = dtData;
                    first = false;
                }
            }
            else
            {
                key = "salesorder";
                if (firstTab3)
                {
                    var dtData = ctrl.getbyFilter(key);
                    dataGridView3.DataSource = dtData;
                    firstTab3 = false;
                }
                
            }
        }
        
        public void loadDate() {
            var dtData = ctrl.getbyFilter(key);
            if (key == "order") dataGridView1.DataSource = dtData;
            else if(key == "stock") dataGridView2.DataSource = dtData;
            else dataGridView3.DataSource = dtData;
        } 
       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UserSelect view = new UserSelect();
            view.UserSet += View_UserSet;
            view.Show();
        }

        private void View_UserSet(object sender, UserEventArgs e)
        {
            if (e.list_uids.Count > 0)
            {
                MessageModel res = ctrl.addbyids(e.list_uids, key);
                if (res.Code > 0)
                {
                    MessageBox.Show(res.Msg);
                }
                else
                {
                    loadDate();
                }
            }
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
                    else if (key == "stock")
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
                    else
                    {
                        msg = ctrl.del(dataGridView3.CurrentRow.Cells["id"].Value);
                        if (msg.Code == 0)
                        {
                            this.dataGridView3.Rows.Remove(dataGridView3.CurrentRow);
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
