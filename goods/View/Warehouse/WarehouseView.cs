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
    public partial class WarehouseView : Form
    {
        warehouseCtrl ctrl = new warehouseCtrl();
        positionCtrl pctrl = new positionCtrl();
        private DataTable dtData = null;
        int wId = -1;
        string num;
        int loaded = -1; //判断窗体是否加载完成
        //总记录数
        public int RecordCount = 0;
        public WarehouseView()
        {
            InitializeComponent();
            initPage();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            //newColumn.HeaderText = "选择";
            //dataGridView1.Columns.Add(newColumn);
        }
        private void initPage()
        {
            this.textBox1.KeyDown += button1_KeyDown;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "num";
            colNum.Name = "num";
            colNum.HeaderText = "编号";

            DataGridViewColumn colNum2 = new DataGridViewTextBoxColumn();
            colNum2.DataPropertyName = "num";
            colNum2.Name = "num";
            colNum2.HeaderText = "编号";
            dataGridView1.Columns.Add(colNum);
            dataGridView2.Columns.Add(colNum2);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            colName.Name = "name";
            colName.HeaderText = "名称";
            DataGridViewColumn colName2 = new DataGridViewTextBoxColumn();
            colName2.DataPropertyName = "name";
            colName2.Name = "name";
            colName2.HeaderText = "名称";
            dataGridView1.Columns.Add(colName);
            dataGridView2.Columns.Add(colName2);

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            DataGridViewColumn colId2 = new DataGridViewTextBoxColumn();
            colId2.DataPropertyName = "id";
            colId2.Visible = false;
            colId2.Name = "id";
            dataGridView1.Columns.Add(colId);
            dataGridView2.Columns.Add(colId2);

            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "status";//字段
            colStatus.Name = "status";
            colStatus.HeaderText = "状态";
            DataGridViewColumn colStatus2 = new DataGridViewTextBoxColumn();
            colStatus2.DataPropertyName = "status";//字段
            colStatus2.Name = "status";
            colStatus2.HeaderText = "状态";
            dataGridView1.Columns.Add(colStatus);
            dataGridView2.Columns.Add(colStatus2);

            DataGridViewColumn colIsActive = new DataGridViewLinkColumn();
            colIsActive.DataPropertyName = "isActive";//字段
            colIsActive.Visible = false;
            colIsActive.Name = "isActive";

            DataGridViewColumn colIsActive2 = new DataGridViewLinkColumn();
            colIsActive2.DataPropertyName = "isActive";//字段
            colIsActive2.Visible = false;
            colIsActive2.Name = "isActive";

            dataGridView1.Columns.Add(colIsActive);
            dataGridView2.Columns.Add(colIsActive2);

        }
        private void Form1_Shown(Object sender, EventArgs e)
        {
            loaded = 1;
            loadPosition(sender,e);

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        public void loadData()
        {
            BindDataWithPage(1);
        }
        public void loadPosition(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count >0 && loaded == 1)
            {
                wId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                num = dataGridView1.SelectedRows[0].Cells["num"].Value.ToString();
                DataTable dtPos = pctrl.getByWId(wId);
                dtPos.Columns.Add("status");
                for (int i = 0; i < dtPos.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtPos.Rows[i]["isActive"])) dtPos.Rows[i]["status"] = "激活";
                    else dtPos.Rows[i]["status"] = "注销";
                }
                dataGridView2.DataSource = dtPos;
            }

        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { BindDataWithPage(1); }
        }

        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text);
            dtData.Columns.Add("status");
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtData.Rows[i]["isActive"])) dtData.Rows[i]["status"] = "激活";
                else dtData.Rows[i]["status"] = "注销";
            }
            dataGridView1.DataSource = dtData;
            //获取并设置总记录数
            pagingCom1.RecordCount = ctrl.getCount(textBox1.Text);
            pagingCom1.reSet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }


        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehousePopup popup = new WarehousePopup(this, "ck", wId,null);
            popup.Show();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                WarehouseModel s = new WarehouseModel(Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["id"].Value), this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["num"].Value.ToString(), this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["name"].Value.ToString());
                WarehousePopup popup = new WarehousePopup(this, "ck", wId,s);
                popup.Show();
            }
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                var cellIsActive = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["isActive"].Value;
                var cellId = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["id"].Value;
                MessageModel msg = ctrl.switchStatus(Convert.ToInt32(cellId), Convert.ToBoolean(cellIsActive));
                if (msg.Code != 0)
                {
                    MessageBox.Show(msg.Msg);
                }
                else
                {
                    if (Convert.ToBoolean(cellIsActive))
                    {
                        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["status"].Value = "注销";

                    }

                    else
                    {
                        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["status"].Value = "激活";
                    }
                    this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["isActive"].Value = !Convert.ToBoolean(cellIsActive);

                }
            }

        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (num != null)
            {
                WarehouseQRCode popup = new WarehouseQRCode("ck", num,"");
                popup.Show();
            }
            else
            {
                MessageBox.Show("请选择一行。");
            }
        }

        private void 新建ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WarehousePopup popup = new WarehousePopup(this, "cw", wId,null);
            popup.Show();
        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PositionModel p = new PositionModel(Convert.ToInt32(this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells["id"].Value), this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells["num"].Value.ToString(), this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells["name"].Value.ToString());
            WarehousePopup popup = new WarehousePopup(this, "cw", wId,p);
            popup.Show();
        }

        private void 注销ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.CurrentCell != null)
            {
                var cellIsActive = this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells["isActive"].Value;
                var cellId = this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells["id"].Value;
                MessageModel msg = pctrl.switchStatus(Convert.ToInt32(cellId), Convert.ToBoolean(cellIsActive));
                if (msg.Code != 0)
                {
                    MessageBox.Show(msg.Msg);
                }
                else
                {
                    if (Convert.ToBoolean(cellIsActive))
                    {
                        this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells["status"].Value = "注销";

                    }

                    else
                    {
                        this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells["status"].Value = "激活";
                    }
                    this.dataGridView2.Rows[this.dataGridView2.CurrentCell.RowIndex].Cells["isActive"].Value = !Convert.ToBoolean(cellIsActive);

                }
            }
        }

        private void 打印ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0) MessageBox.Show("请选中一行");
            else
            {
                string positionNum = dataGridView2.SelectedRows[0].Cells["num"].Value.ToString();
                string warNum = dataGridView1.SelectedRows[0].Cells["num"].Value.ToString();
                WarehouseQRCode popup = new WarehouseQRCode("cw", warNum, positionNum);
                popup.Show();
            }
        }
    }
}
