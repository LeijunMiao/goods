using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using goods.Controller;
using goods.Model;
namespace goods
{
    public partial class SolidBackingList : Form
    {
        solidbackingCtrl ctrl = new solidbackingCtrl();
        int current1Id = -1,current2Id = -1;
        bool modeNew1 = true, modeNew2 = true;
        public SolidBackingList()
        {
            InitializeComponent();
            initPage();
            loadData();
        }
        private void initPage()
        {
            this.Load += SolidBackingList_Load;
            this.dataGridView1.CellClick += DataGridView1_CellClick; ;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;
            DataGridViewColumn colId = new DataGridViewLinkColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);
            DataGridViewColumn colId2 = new DataGridViewLinkColumn();
            colId2.DataPropertyName = "id";
            colId2.Visible = false;
            colId2.Name = "id";
            dataGridView2.Columns.Add(colId2);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            colName.HeaderText = "名称";
            colName.Name = "name";
            dataGridView1.Columns.Add(colName);
            DataGridViewColumn colName2 = new DataGridViewTextBoxColumn();
            colName2.DataPropertyName = "name";
            colName2.HeaderText = "名称";
            colName2.Name = "name";
            dataGridView2.Columns.Add(colName2);

            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "status";
            colStatus.HeaderText = "状态";
            colStatus.Name = "status";
            dataGridView1.Columns.Add(colStatus);
            DataGridViewColumn colStatus2 = new DataGridViewTextBoxColumn();
            colStatus2.DataPropertyName = "status";
            colStatus2.HeaderText = "状态";
            colStatus2.Name = "status";
            dataGridView2.Columns.Add(colStatus2);

            DataGridViewColumn colIsActive = new DataGridViewLinkColumn();
            colIsActive.DataPropertyName = "isActive";
            colIsActive.Visible = false;
            colIsActive.Name = "isActive";
            dataGridView1.Columns.Add(colIsActive);
            DataGridViewColumn colIsActive2 = new DataGridViewLinkColumn();
            colIsActive2.DataPropertyName = "isActive";
            colIsActive2.Visible = false;
            colIsActive2.Name = "isActive";
            dataGridView2.Columns.Add(colIsActive2);

        }

        private void SolidBackingList_Load(object sender, System.EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                current1Id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["id"].Value);
                loadData2();
            }
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                current1Id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["id"].Value);
                loadData2();
            }
        }

        private void loadData()
        {
            DataTable dt = ctrl.get();
            dataGridView1.DataSource = dt;
            current1Id = -1;
            loadData2();
        }

        private void loadData2()
        {
            if (current1Id == -1)
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    current1Id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["id"].Value);
                    DataTable dt = ctrl.getAttrValue(current1Id);
                    dataGridView2.DataSource = dt;
                }
            }
            else
            {
                DataTable dt = ctrl.getAttrValue(current1Id);
                dataGridView2.DataSource = dt;
            }
        }
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = true;
            modeNew1 = true;
            this.textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.save1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
        }

        private void 注销激活ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count >0)
            {
                bool isActive = Convert.ToBoolean(this.dataGridView1.SelectedRows[0].Cells["isActive"].Value);
                MessageModel msg = ctrl.switchStatus(Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["id"].Value), isActive);
                if (msg.Code == 0)
                {
                    this.dataGridView1.SelectedRows[0].Cells["isActive"].Value = !isActive;
                    if (isActive) this.dataGridView1.SelectedRows[0].Cells["status"].Value = "注销";
                    else this.dataGridView1.SelectedRows[0].Cells["status"].Value = "活动";
                }
                else MessageBox.Show(msg.Msg);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeNew1 = false;
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                this.panel2.Visible = true;
                this.textBox1.Text = this.dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeNew2 = true;
            this.panel4.Visible = true;
            this.textBox2.Text = "";
        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            modeNew2 = false;
            if (this.dataGridView2.SelectedRows.Count > 0)
            {
                this.panel4.Visible = true;
                current2Id = Convert.ToInt32(this.dataGridView2.SelectedRows[0].Cells["id"].Value);
                this.textBox2.Text = this.dataGridView2.SelectedRows[0].Cells["name"].Value.ToString();
            }
        }

        private void 注销激活ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (this.dataGridView2.SelectedRows.Count > 0)
            {
                bool isActive = Convert.ToBoolean(this.dataGridView2.SelectedRows[0].Cells["isActive"].Value);
                MessageModel msg = ctrl.switchAttrValueStatus(Convert.ToInt32(this.dataGridView2.SelectedRows[0].Cells["id"].Value), isActive);
                if (msg.Code == 0)
                {
                    this.dataGridView2.SelectedRows[0].Cells["isActive"].Value = !isActive;
                    if (isActive) this.dataGridView2.SelectedRows[0].Cells["status"].Value = "注销";
                    else this.dataGridView2.SelectedRows[0].Cells["status"].Value = "活动";
                }
                else MessageBox.Show(msg.Msg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            save2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel4.Visible = false;
        }


        private void save1()
        {
            MessageModel msg;
            if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("名称不能为空！");
                return;
            }
            if (!modeNew1)
            {
                SolidBackingModel model = new SolidBackingModel();
                model.id = current1Id;
                model.name = this.textBox1.Text.Trim();
                msg = ctrl.set(model);
            }
            else
            {
                SolidBackingModel model = new SolidBackingModel();
                model.name = this.textBox1.Text.Trim();
                msg = ctrl.add(model);
            }
            if (msg.Code == 0)
            {
                loadData();
                this.panel2.Visible = false;
            }
            else MessageBox.Show(msg.Msg);
        }

        private void save2()
        {
            MessageModel msg;
            if (this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("名称不能为空！");
                return;
            }
            if (!modeNew2)
            {
                AttrValueModel model = new AttrValueModel();
                model.id = current2Id;
                model.name = this.textBox2.Text.Trim();
                msg = ctrl.setAttrValue(model);
            }
            else
            {
                if (current1Id != -1)
                {
                    AttrValueModel model = new AttrValueModel();
                    model.name = this.textBox2.Text.Trim();
                    model.solidbacking = current1Id;
                    msg = ctrl.addAttrValue(model);
                }
                else return;
            }
            if (msg.Code == 0)
            {
                loadData2();
                this.panel4.Visible = false;
            }
            else MessageBox.Show(msg.Msg);
        }
    }
}
