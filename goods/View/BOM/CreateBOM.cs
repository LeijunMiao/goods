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
using System.Text.RegularExpressions;

namespace goods
{
    public partial class CreateBOM : Form
    {
        bomCtrl ctrl = new bomCtrl();
        materielCtrl mctrl = new materielCtrl();
        int id = -1;
        public List<int> allids = new List<int>();
        //private string pattern = @"(^(\d*\.)?\d*$)"; //@"(^[1-9]\.\d{0,2}$)";
        //private string param1 = null;
        public CreateBOM()
        {
            InitializeComponent();
            initPage();
            loadData();
        }
        private void initPage()
        {
            contextMenuStrip1.Opening += ContextMenuStrip1_Opening;
            numberbox11.style(145, 21);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            this.dataGridView1.Columns.Add(colId);

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "Num";
            colNum.Name = "num";
            colNum.HeaderText = "编号";
            colNum.ReadOnly = true;
            this.dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            colName.Name = "name";
            colName.HeaderText = "名称";
            colName.ReadOnly = true;
            this.dataGridView1.Columns.Add(colName);

            DataGridViewColumn colSep = new DataGridViewTextBoxColumn();
            colSep.DataPropertyName = "specifications";
            colSep.Name = "specifications";
            colSep.HeaderText = "规格参数";
            colSep.ReadOnly = true;
            this.dataGridView1.Columns.Add(colSep);

            DataGridViewColumn colMetering = new DataGridViewTextBoxColumn();
            colMetering.DataPropertyName = "metering";
            colMetering.Name = "metering";
            colMetering.HeaderText = "计量单位";
            colMetering.ReadOnly = true;
            this.dataGridView1.Columns.Add(colMetering);

            DataGridViewColumn colquantity = new DataGridViewTextBoxColumn();
            colquantity.HeaderText = "数量";
            colquantity.Name = "quantity";
            colquantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Columns.Add(colquantity);

            DataGridViewColumn colsummary = new DataGridViewTextBoxColumn();
            colsummary.HeaderText = "备注";
            colsummary.Name = "summary";
            this.dataGridView1.Columns.Add(colsummary);
        }
        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                e.Cancel = true;
            }
        }
        private void loadData()
        {
            comboBox1.SelectedIndex = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MaterielLookUpArgs args = new MaterielLookUpArgs(false, new List<int>());
            args.type = "自制件";
            OrderMaterielPopup popup = new OrderMaterielPopup(args);
            popup.AddMateriel += Popup_AddMateriel;
            popup.Show();
        }
        private void Popup_AddMateriel(object sender, MaterielEventArgs e)
        {
            if (e.ids.Count > 0)
            {
                var dtData = mctrl.getByids4BOM(e.ids);
                id = e.ids[0];
                this.textBox1.Text = dtData.Rows[0]["num"].ToString();
                this.textBox2.Text = dtData.Rows[0]["name"].ToString();
                this.textBox3.Text = dtData.Rows[0]["specifications"].ToString();
                this.textBox4.Text = dtData.Rows[0]["metering"].ToString();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OrderMaterielPopup popup2 = new OrderMaterielPopup(new MaterielLookUpArgs(true, allids));
            popup2.AddMateriel += Popup2_AddMateriel;
            popup2.Show();
        }
        private void Popup2_AddMateriel(object sender, MaterielEventArgs e)
        {
            if (e.ids.Count > 0)
            {
                var dtData = mctrl.getByids4BOM(e.ids);

                allids.AddRange(e.ids);
                this.dataGridView1.Rows.Add(dtData.Rows.Count);
                int j = 0;
                for (int i = this.dataGridView1.RowCount - dtData.Rows.Count; i < this.dataGridView1.RowCount; i++)
                {
                    this.dataGridView1.Rows[i].Cells["id"].Value = dtData.Rows[j]["id"];
                    this.dataGridView1.Rows[i].Cells["num"].Value = dtData.Rows[j]["num"];
                    this.dataGridView1.Rows[i].Cells["name"].Value = dtData.Rows[j]["name"];
                    this.dataGridView1.Rows[i].Cells["specifications"].Value = dtData.Rows[j]["specifications"];
                    this.dataGridView1.Rows[i].Cells["metering"].Value = dtData.Rows[j]["metering"];
                    j++;
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell = null;
            if (this.dataGridView1.RowCount == 0)
            {
                MessageBox.Show("请添加子级！");
                return;
            }
            else if (id == -1)
            {
                MessageBox.Show("请添加父级！");
                return;
            }
            int quantity = Convert.ToInt32(numberbox11.TextNumber);
            if (quantity <= 0)
            {
                MessageBox.Show("父级数量大于0！");
                return;
            }
            BOMMainModel obj = new BOMMainModel();
            if (comboBox1.Text == "否") obj.jump = false;
            else obj.jump = true;
            obj.materiel = id;
            obj.quantity = quantity;
            obj.isActive = checkBox1.Checked;
            obj.list = new List<BOMDetailModel>();
            for (int i = this.dataGridView1.RowCount; i > 0; i--)
            {

                BOMDetailModel lm = new BOMDetailModel();
                lm.materiel = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["id"].Value);
                lm.quantity = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["quantity"].Value);
                if (this.dataGridView1.Rows[i - 1].Cells["summary"].Value != null) lm.remark = this.dataGridView1.Rows[i - 1].Cells["summary"].Value.ToString();
                if (lm.quantity == 0)
                {
                    MessageBox.Show("子级数量大于0！");
                    return;
                }
                obj.list.Add(lm);
            }

            var msg = ctrl.add(obj);
            if (msg.Code == 0)
            {
                MessageBox.Show(msg.Msg);
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
                dataGridView1.Enabled = false;

                DataTable dt = ctrl.getlastinsert(id);
                textBox5.Text = dt.Rows[0]["num"].ToString();
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除行！");
                return;
            }
            for (int i = this.dataGridView1.SelectedRows.Count; i > 0; i--)
            {
                allids.Remove(Convert.ToInt32(dataGridView1.SelectedRows[i - 1].Cells["id"].Value));
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allids.Remove(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["id"].Value));
            dataGridView1.Rows.RemoveAt(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Index);
        }
    }
}
