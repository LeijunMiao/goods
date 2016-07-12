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
    public partial class CallSlipList : Form
    {
        listCtrl ctrl = new listCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        public CallSlipList()
        {
            InitializeComponent();
            initPage();
        }
        private void initPage()
        {
            this.dateTimePicker1.Checked = false;
            this.textBox2.KeyDown += button1_KeyDown;
            this.textBox3.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.dataGridView1.ReadOnly = true;
            this.comboBox1.SelectedIndex = 0;

            DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Format = "yyyy-MM-dd";
            dataGridViewCellStyle1.NullValue = null;

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            this.dataGridView1.Columns.Add(colId);

            DataGridViewColumn colDate = new DataGridViewTextBoxColumn();
            colDate.DataPropertyName = "date";
            colDate.Name = "date";
            colDate.HeaderText = "日期";
            colDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Columns.Add(colDate);

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "num";
            colNum.Name = "num";
            colNum.HeaderText = "单据编码";
            this.dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colWarehouse = new DataGridViewTextBoxColumn();
            colWarehouse.DataPropertyName = "warehouseName";
            colWarehouse.Name = "warehouseName";
            colWarehouse.HeaderText = "发货仓库";
            this.dataGridView1.Columns.Add(colWarehouse);

            DataGridViewColumn colPos = new DataGridViewTextBoxColumn();
            colPos.DataPropertyName = "positionName";
            colPos.Name = "positionName";
            colPos.HeaderText = "仓位";
            this.dataGridView1.Columns.Add(colPos);

            DataGridViewColumn colMNum = new DataGridViewTextBoxColumn();
            colMNum.DataPropertyName = "MNum";
            colMNum.Name = "MNum";
            colMNum.HeaderText = "物料代码";
            this.dataGridView1.Columns.Add(colMNum);

            DataGridViewColumn colMName = new DataGridViewTextBoxColumn();
            colMName.DataPropertyName = "MName";
            colMName.Name = "MName";
            colMName.HeaderText = "物料名称";
            this.dataGridView1.Columns.Add(colMName);

            DataGridViewColumn colSpe = new DataGridViewTextBoxColumn();
            colSpe.DataPropertyName = "specifications";
            colSpe.Name = "specifications";
            colSpe.HeaderText = "规格型号";
            this.dataGridView1.Columns.Add(colSpe);

            DataGridViewColumn colMeter = new DataGridViewTextBoxColumn();
            colMeter.DataPropertyName = "meterName";
            colMeter.Name = "meterName";
            colMeter.HeaderText = "单位";
            this.dataGridView1.Columns.Add(colMeter);

            DataGridViewColumn colQuantity = new DataGridViewTextBoxColumn();
            colQuantity.DataPropertyName = "quantity";
            colQuantity.Name = "quantity";
            colQuantity.HeaderText = "领料数量";
            this.dataGridView1.Columns.Add(colQuantity);

            DataGridViewColumn colSubMeter = new DataGridViewTextBoxColumn();
            colSubMeter.DataPropertyName = "subMeterName";
            colSubMeter.Name = "subMeterName";
            colSubMeter.HeaderText = "辅助单位";
            this.dataGridView1.Columns.Add(colSubMeter);

            DataGridViewColumn colCov = new DataGridViewTextBoxColumn();
            colCov.DataPropertyName = "conversion";
            colCov.Name = "conversion";
            colCov.HeaderText = "转换率";
            this.dataGridView1.Columns.Add(colCov);

            DataGridViewColumn colSubQuantity = new DataGridViewTextBoxColumn();
            colSubQuantity.DataPropertyName = "subquantity";
            colSubQuantity.Name = "subquantity";
            colSubQuantity.HeaderText = "辅助数量";
            this.dataGridView1.Columns.Add(colSubQuantity);

            DataGridViewColumn colOwner = new DataGridViewTextBoxColumn();
            colOwner.DataPropertyName = "fullName";
            colOwner.Name = "fullName";
            colOwner.HeaderText = "制单人";
            this.dataGridView1.Columns.Add(colOwner);

            DataGridViewColumn colIsDef = new DataGridViewTextBoxColumn();
            colIsDef.DataPropertyName = "isDeficit";
            colIsDef.Name = "isDeficit";
            colIsDef.Visible = false;
            this.dataGridView1.Columns.Add(colIsDef);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            loadData(pagingCom1.PageIndex);
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { loadData(1); }
        }
        public void loadData(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getCallSlips(pagingCom1.PageIndex, pagingCom1.PageSize,
                this.dateTimePicker1.Checked, this.dateTimePicker1.Value.Date, textBox2.Text, textBox3.Text, this.comboBox1.Text);
            //dt = new DataTable();
            //DataColumn dcDate = new DataColumn("日期");
            //DataColumn dcNum = new DataColumn("单据编码");
            //DataColumn dcWare = new DataColumn("发货仓库");
            //DataColumn dcPos = new DataColumn("仓位");
            //DataColumn dcMNum = new DataColumn("物料代码");
            //DataColumn dcMName = new DataColumn("物料名称");
            //DataColumn dcSep = new DataColumn("规格型号");
            //DataColumn dcMete = new DataColumn("单位");
            //DataColumn dcQuantity = new DataColumn("领料数量");
            //DataColumn dcSubMete = new DataColumn("辅助单位");
            //DataColumn dcConv = new DataColumn("转换率");
            //DataColumn dcSubQua = new DataColumn("辅助数量");
            //DataColumn dcUser = new DataColumn("制单人");

            //DataColumn[] list_dc = { dcDate,dcNum,dcWare,dcPos, dcMNum, dcMName, dcSep, dcMete,dcQuantity,
            //    dcSubMete,dcConv,dcSubQua, dcUser };
            //dt.Columns.AddRange(list_dc);

            //for (int i = 0; i < dtData.Rows.Count; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr[0] = DateTime.Parse(dtData.Rows[i]["date"].ToString()).ToString("yyyy/M/d");
            //    dr[1] = dtData.Rows[i]["num"].ToString();
            //    dr[2] = dtData.Rows[i]["warehouseName"].ToString();
            //    dr[3] = dtData.Rows[i]["positionName"].ToString();
            //    dr[4] = dtData.Rows[i]["MNum"].ToString();
            //    dr[5] = dtData.Rows[i]["MName"].ToString();
            //    dr[6] = dtData.Rows[i]["specifications"].ToString();
            //    dr[7] = dtData.Rows[i]["meterName"].ToString();
            //    dr[8] = dtData.Rows[i]["quantity"].ToString();
            //    dr[9] = dtData.Rows[i]["subMeterName"].ToString();
            //    dr[10] = dtData.Rows[i]["conversion"].ToString();
            //    dr[11] = dtData.Rows[i]["subquantity"].ToString();
            //    dr[12] = dtData.Rows[i]["fullName"].ToString();

            //    dt.Rows.Add(dr);
            //}
            dataGridView1.DataSource = dtData;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["isDeficit"].Value))
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
            pagingCom1.RecordCount = ctrl.getCallSlipCount( this.dateTimePicker1.Checked, this.dateTimePicker1.Value.Date, textBox2.Text, textBox3.Text, this.comboBox1.Text);
            pagingCom1.reSet();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                CallSlipView view = new CallSlipView(dataGridView1.SelectedRows[0].Cells["单据编码"].Value.ToString());
                view.Show();
            }
            else
            {
                MessageBox.Show("请选择一行！");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            loadData(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Checked = false;
            this.textBox2.Text = "";
            this.textBox3.Text = "";
        }

        private void CallSlipList_Load(object sender, EventArgs e)
        {
            loadData(1);
        }
    }
}
