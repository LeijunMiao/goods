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
    public partial class BOMEdit : Form
    {
        bomCtrl ctrl = new bomCtrl();
        materielCtrl mctrl = new materielCtrl();
        int id = -1;
        int materiel;
        bool isActive;
        List<int> allids = new List<int>();
        public BOMEdit(int id)
        {
            InitializeComponent();
            initPage();
            this.id = id;
            loadData();
        }
        private void initPage()
        {
            contextMenuStrip1.Opening += ContextMenuStrip1_Opening;
            numberbox11.style(145, 21);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            dataGridView1.AutoGenerateColumns = false;
            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            this.dataGridView1.Columns.Add(colId);

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "cmnum";
            colNum.Name = "cmnum";
            colNum.HeaderText = "编号";
            colNum.ReadOnly = true;
            this.dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "cmname";
            colName.Name = "cmname";
            colName.HeaderText = "名称";
            colName.ReadOnly = true;
            this.dataGridView1.Columns.Add(colName);

            DataGridViewColumn colSep = new DataGridViewTextBoxColumn();
            colSep.DataPropertyName = "cmspe";
            colSep.Name = "cmspe";
            colSep.HeaderText = "规格参数";
            colSep.ReadOnly = true;
            this.dataGridView1.Columns.Add(colSep);

            DataGridViewColumn colMetering = new DataGridViewTextBoxColumn();
            colMetering.DataPropertyName = "cmetering";
            colMetering.Name = "cmetering";
            colMetering.HeaderText = "计量单位";
            colMetering.ReadOnly = true;
            this.dataGridView1.Columns.Add(colMetering);

            DataGridViewColumn colquantity = new DataGridViewTextBoxColumn();
            colquantity.HeaderText = "数量";
            colquantity.DataPropertyName = "dtlquantity";
            colquantity.Name = "dtlquantity";
            colquantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Columns.Add(colquantity);

            DataGridViewColumn colsummary = new DataGridViewTextBoxColumn();
            colsummary.HeaderText = "备注";
            colsummary.DataPropertyName = "remark";
            colsummary.Name = "remark";
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

            DataTable dt = ctrl.getbyMianId(id);
            materiel = Convert.ToInt32(dt.Rows[0]["mmid"]);
            allids.Add(materiel);
            label8.Text = dt.Rows[0]["mnum"].ToString();
            label9.Text = dt.Rows[0]["mname"].ToString();
            label13.Text = dt.Rows[0]["specifications"].ToString();
            label10.Text = dt.Rows[0]["metering"].ToString();
            label11.Text = dt.Rows[0]["num"].ToString();
            numberbox11.TextNumber = dt.Rows[0]["quantity"].ToString();
            if (Convert.ToBoolean(dt.Rows[0]["jump"])) comboBox1.SelectedItem = "是";
            else comboBox1.SelectedItem = "否";
            isActive = Convert.ToBoolean(dt.Rows[0]["isActive"]);
            setStatus();
            this.dataGridView1.Rows.Add(dt.Rows.Count);
            int j = 0;
            for (int i = this.dataGridView1.RowCount - dt.Rows.Count; i < this.dataGridView1.RowCount; i++)
            {
                this.dataGridView1.Rows[i].Cells["id"].Value = dt.Rows[j]["id"];
                this.dataGridView1.Rows[i].Cells["cmnum"].Value = dt.Rows[j]["cmnum"];
                this.dataGridView1.Rows[i].Cells["cmname"].Value = dt.Rows[j]["cmname"];
                this.dataGridView1.Rows[i].Cells["cmspe"].Value = dt.Rows[j]["cmspe"];
                this.dataGridView1.Rows[i].Cells["cmetering"].Value = dt.Rows[j]["cmetering"];
                this.dataGridView1.Rows[i].Cells["dtlquantity"].Value = dt.Rows[j]["dtlquantity"];
                this.dataGridView1.Rows[i].Cells["remark"].Value = dt.Rows[j]["remark"];
                allids.Add(Convert.ToInt32(dt.Rows[j]["id"]));
                j++;
            }
            
            //dataGridView1.DataSource = dt;
        }

        private void setStatus()
        {
            if (isActive) {
                label14.Text = "活动";
                label14.ForeColor = Color.Green;
                toolStripButton2.Text = "注销";
            }
            else {
                label14.Text = "注销";
                label14.ForeColor = Color.Red;
                toolStripButton2.Text = "激活";
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认更新?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageModel res = ctrl.switchStatus(id, materiel, isActive);
                if (res.Code == 0)
                {
                    isActive = !isActive;
                    setStatus();
                }
                else MessageBox.Show(res.Msg);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
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
                    this.dataGridView1.Rows[i].Cells["cmnum"].Value = dtData.Rows[j]["num"];
                    this.dataGridView1.Rows[i].Cells["cmname"].Value = dtData.Rows[j]["name"];
                    this.dataGridView1.Rows[i].Cells["cmspe"].Value = dtData.Rows[j]["specifications"];
                    this.dataGridView1.Rows[i].Cells["cmetering"].Value = dtData.Rows[j]["metering"];
                    j++;
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除行！");
                return;
            }
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = this.dataGridView1.SelectedRows.Count; i > 0; i--)
                {
                    allids.Remove(Convert.ToInt32(dataGridView1.SelectedRows[i - 1].Cells["id"].Value));
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
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
            obj.id = id;
            obj.materiel = materiel;
            obj.quantity = quantity;
            obj.list = new List<BOMDetailModel>();
            for (int i = this.dataGridView1.RowCount; i > 0; i--)
            {

                BOMDetailModel lm = new BOMDetailModel();
                lm.materiel = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["id"].Value);
                lm.quantity = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["dtlquantity"].Value);
                if (this.dataGridView1.Rows[i - 1].Cells["remark"].Value != null) lm.remark = this.dataGridView1.Rows[i - 1].Cells["remark"].Value.ToString();
                if (lm.quantity == 0)
                {
                    MessageBox.Show("子级数量大于0！");
                    return;
                }
                obj.list.Add(lm);
            }

            var msg = ctrl.set(obj);
            MessageBox.Show(msg.Msg);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                allids.Remove(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["id"].Value));
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Index);
            }
        }
    }
}
