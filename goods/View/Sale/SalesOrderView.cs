using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using goods.Model;
using goods.Controller;
namespace goods
{
    public partial class SalesOrderView : Form
    {
        int order = -1;
        salesOrderCtrl ctrl = new salesOrderCtrl();
        public SalesOrderView(int orderId)
        {
            InitializeComponent();
            if(orderId < 1)
            {
                MessageBox.Show("加载错误，请重试！");
                this.Close();
            }
            order = orderId;
            initPage();
            loadData();
        }

        private void initPage()
        {
            this.dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Rows.Clear();

            this.label5.Text = PropertyClass.SendNameValue;
            loadColumns();
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((this.dataGridView1.Columns[e.ColumnIndex].Name == "quantity" || this.dataGridView1.Columns[e.ColumnIndex].Name == "price")
                && this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value != DBNull.Value
                && this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value != DBNull.Value)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value
                    = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                    Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) * (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value)), 2);
                this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) * (Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value)), 2);
                this.dataGridView1.Rows[e.RowIndex].Cells["taxprice"].Value = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) * (Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value)), 2);
                
            }
        }

        private void loadColumns()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;

            dataGridViewCellStyle4.Format = "yyyy-MM-dd";
            dataGridViewCellStyle4.NullValue = null;

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";

            DataGridViewColumn colOMId = new DataGridViewTextBoxColumn();
            colOMId.DataPropertyName = "omid";
            colOMId.Visible = false;
            colOMId.Name = "omid";

            DataGridViewColumn colcomb = new DataGridViewTextBoxColumn();
            colcomb.DataPropertyName = "combination";
            colcomb.Visible = false;
            colcomb.Name = "combination";

            DataGridViewColumn colLine = new DataGridViewTextBoxColumn();
            colLine.Name = "line";
            colLine.DataPropertyName = "line";
            colLine.HeaderText = "序号";
            colLine.ReadOnly = true;

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "Num";
            colNum.Name = "num";
            colNum.HeaderText = "编号";
            colNum.ReadOnly = true;

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            colName.Name = "name";
            colName.HeaderText = "名称";
            colName.ReadOnly = true;

            DataGridViewColumn colMetering = new DataGridViewTextBoxColumn();
            colMetering.DataPropertyName = "metering";
            colMetering.Name = "metering";
            colMetering.HeaderText = "单位";
            colMetering.ReadOnly = true;

            DataGridViewColumn colquantity = new DataGridViewTextBoxColumn();
            colquantity.HeaderText = "数量";
            colquantity.Name = "quantity";
            colquantity.DataPropertyName = "quantity";
            colquantity.DefaultCellStyle = dataGridViewCellStyle3;

            DataGridViewColumn colprice = new DataGridViewTextBoxColumn();
            colprice.DefaultCellStyle = dataGridViewCellStyle3;
            colprice.HeaderText = "单价";
            colprice.Name = "price";
            colprice.DataPropertyName = "price";

            DataGridViewColumn coltaxprice = new DataGridViewTextBoxColumn();
            coltaxprice.DefaultCellStyle = dataGridViewCellStyle3;
            coltaxprice.HeaderText = "含税单价";
            coltaxprice.Name = "taxprice";
            coltaxprice.DataPropertyName = "taxprice";
            coltaxprice.ReadOnly = true;

            DataGridViewColumn colamount = new DataGridViewTextBoxColumn();
            colamount.DefaultCellStyle = dataGridViewCellStyle3;
            colamount.HeaderText = "金额";
            colamount.Name = "amount";
            colamount.DataPropertyName = "amount";
            colamount.ReadOnly = true;

            DataGridViewColumn coltax = new DataGridViewTextBoxColumn();
            coltax.DefaultCellStyle = dataGridViewCellStyle3;
            coltax.HeaderText = "税率";
            coltax.DataPropertyName = "tax";
            coltax.Name = "tax";
            coltax.ReadOnly = true;
            coltax.DataPropertyName = "tax";

            DataGridViewColumn coltaxamount = new DataGridViewTextBoxColumn();
            coltaxamount.Name = "taxamount";
            coltaxamount.HeaderText = "税额";
            coltaxamount.ReadOnly = true;
            coltaxamount.DefaultCellStyle = dataGridViewCellStyle3;
            coltaxamount.DataPropertyName = "taxamount";

            DataGridViewColumn colallamount = new DataGridViewTextBoxColumn();
            colallamount.Name = "allamount";
            colallamount.HeaderText = "价税合计";
            colallamount.ReadOnly = true;
            colallamount.DefaultCellStyle = dataGridViewCellStyle3;
            colallamount.DataPropertyName = "allamount";

            CalendarColumn coldeliveryDate = new CalendarColumn();
            coldeliveryDate.DefaultCellStyle = dataGridViewCellStyle4;
            coldeliveryDate.HeaderText = "交货日期";
            coldeliveryDate.Name = "deliveryDate";
            coldeliveryDate.DataPropertyName = "deliveryDate";
            coldeliveryDate.ReadOnly = true;
            coldeliveryDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            coldeliveryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            DataGridViewColumn colattbtn = new DataGridViewTextBoxColumn();
            colattbtn.Name = "solidbacking";
            colattbtn.DataPropertyName = "solidbacking";
            colattbtn.HeaderText = "辅助属性";
            colattbtn.ReadOnly = true;
            colattbtn.DefaultCellStyle.NullValue = "空";

            DataGridViewColumn colAttrNum = new DataGridViewTextBoxColumn();
            colAttrNum.DataPropertyName = "attrnum";
            colAttrNum.Name = "attrnum";
            colAttrNum.Visible = false;

            DataGridViewColumn colsummary = new DataGridViewTextBoxColumn();
            colsummary.HeaderText = "备注";
            colsummary.Name = "summary";
            colsummary.ReadOnly = true;
            colsummary.DataPropertyName = "summary";

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colId, colOMId, colLine, colNum, colName, colMetering, colquantity, colprice, coltaxprice, colamount, coltax, coltaxamount, colallamount, coldeliveryDate, colattbtn, colAttrNum, colsummary, colcomb });
        }

        public void loadData()
        {
            DataTable dt = ctrl.getbyId(order);
            dt.Columns.Add("taxamount");
            dt.Columns.Add("allamount");
            dt.Columns.Add("solidbacking");
            List<int> lits_omid = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lits_omid.Add(Convert.ToInt32(dt.Rows[i]["omid"]));
                dt.Rows[i]["taxamount"] = Math.Round(Convert.ToDouble(dt.Rows[i]["amount"])  * Convert.ToDouble(dt.Rows[i]["tax"]), 2);
                dt.Rows[i]["allamount"] = Math.Round(Convert.ToDouble(dt.Rows[i]["amount"]) * (1 + Convert.ToDouble(dt.Rows[i]["tax"])), 2);
            }
            this.label7.Text = dt.Rows[0]["customer"].ToString(); 
            this.label8.Text = DateTime.Parse(dt.Rows[0]["deliveryDate"].ToString()).ToString("yyyy/M/d");
            this.label9.Text = DateTime.Parse(dt.Rows[0]["date"].ToString()).ToString("yyyy/M/d");
            this.label5.Text = dt.Rows[0]["user"].ToString();
            this.label10.Text = dt.Rows[0]["sosummary"].ToString();
            this.label12.Text = dt.Rows[0]["sonum"].ToString();
            dataGridView1.DataSource = dt;

            Dictionary<int, string> map = ctrl.getbyOMIds(lits_omid);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                var omid = Convert.ToInt32(dataGridView1.Rows[i].Cells["omid"].Value);

                if (map.Keys.Contains(omid))
                {
                    dataGridView1.Rows[i].Cells["solidbacking"].Value = map[omid];
                }

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell = null;
            List<SalesOrderMateriel> list = new List<SalesOrderMateriel>();
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (this.dataGridView1.Rows[i].Cells["price"].Value == DBNull.Value || this.dataGridView1.Rows[i].Cells["quantity"].Value == DBNull.Value)
                {
                    MessageBox.Show("物料缺失价格或数量！");
                    return;
                }
                SalesOrderMateriel lm = new SalesOrderMateriel();
                lm.price = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[i].Cells["price"].Value), 2);
                lm.materiel = Convert.ToInt32(this.dataGridView1.Rows[i].Cells["id"].Value);
                lm.quantity = Convert.ToDouble(this.dataGridView1.Rows[i].Cells["quantity"].Value);
                if(this.dataGridView1.Rows[i].Cells["combination"].Value != DBNull.Value) lm.combination = Convert.ToInt32(this.dataGridView1.Rows[i].Cells["combination"].Value);
                lm.attrs = this.dataGridView1.Rows[i].Cells["solidbacking"].Value.ToString();
                list.Add(lm);
            }
            var msg = ctrl.setList(order, label12.Text, label5.Text, label9.Text, list);
            MessageBox.Show(msg.Msg);
        }
    }
}
