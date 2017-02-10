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
    public partial class CreateSalesOrder : Form
    {
        materielCtrl mctrl = new materielCtrl();
        salesOrderCtrl sctrl = new salesOrderCtrl();
        string customer = "";
        int user = -1;
        int lineIndex = 1;
        solidbackingCtrl sbctrl = new solidbackingCtrl();
        Dictionary<int, Dictionary<int, attrClass>> mapAttr = new Dictionary<int, Dictionary<int, attrClass>>();
        public CreateSalesOrder()
        {
            InitializeComponent();
            loadTable();
        }
        private void loadTable()
        {
            this.dataGridView1.CellEndEdit += DataGridView1_CellEndEdit; 
            this.dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Rows.Clear();

            this.label5.Text = PropertyClass.SendNameValue;
            this.user = PropertyClass.UserId;
            loadColumns();
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "price":
                    this.dataGridView1.Rows[e.RowIndex].Cells["taxprice"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                        (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value));
                    this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) +
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value);
                    break;
                case "tax":
                    this.dataGridView1.Rows[e.RowIndex].Cells["taxprice"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                        (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value));
                    this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) +
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value);
                    break;
                case "taxprice":
                    if (this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value != null)
                        this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["taxprice"].Value) /
                        (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value));
                    this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) +
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value);
                    break;
                case "quantity":

                    this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) +
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value);

                    break;
                case "amount":
                    if (this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value != null)
                        this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) /
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["taxprice"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                        (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value));
                    this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) +
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["taxamount"].Value);
                    break;
                default:
                    break;
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
            colquantity.DefaultCellStyle = dataGridViewCellStyle3;

            DataGridViewColumn colstockqty = new DataGridViewTextBoxColumn();
            colstockqty.DataPropertyName = "stockqty";
            colstockqty.HeaderText = "库存数量";
            colstockqty.Name = "stockqty";
            colstockqty.ReadOnly = true;
            colstockqty.DefaultCellStyle = dataGridViewCellStyle3;


            DataGridViewColumn colprice = new DataGridViewTextBoxColumn();
            colprice.DefaultCellStyle = dataGridViewCellStyle3;
            colprice.HeaderText = "单价";
            colprice.Name = "price";

            DataGridViewColumn coltaxprice = new DataGridViewTextBoxColumn();
            coltaxprice.DefaultCellStyle = dataGridViewCellStyle3;
            coltaxprice.HeaderText = "含税单价";
            coltaxprice.Name = "taxprice";

            DataGridViewColumn colamount = new DataGridViewTextBoxColumn();
            colamount.DefaultCellStyle = dataGridViewCellStyle3;
            colamount.HeaderText = "金额";
            colamount.Name = "amount";

            DataGridViewColumn coltax = new DataGridViewTextBoxColumn();
            coltax.DefaultCellStyle = dataGridViewCellStyle3;
            coltax.HeaderText = "税率";
            coltax.DataPropertyName = "tax";
            coltax.Name = "tax";

            DataGridViewColumn coltaxamount = new DataGridViewTextBoxColumn();
            coltaxamount.Name = "taxamount";
            coltaxamount.HeaderText = "税额";
            coltaxamount.ReadOnly = true;
            coltaxamount.DefaultCellStyle = dataGridViewCellStyle3;

            DataGridViewColumn colallamount = new DataGridViewTextBoxColumn();
            colallamount.Name = "allamount";
            colallamount.HeaderText = "价税合计";
            colallamount.ReadOnly = true;
            colallamount.DefaultCellStyle = dataGridViewCellStyle3;

            CalendarColumn coldeliveryDate = new CalendarColumn();
            coldeliveryDate.DefaultCellStyle = dataGridViewCellStyle4;
            coldeliveryDate.HeaderText = "交货日期";
            coldeliveryDate.Name = "deliveryDate";
            coldeliveryDate.DataPropertyName = "deliveryDate";
            coldeliveryDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            coldeliveryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            DataGridViewButtonColumn colattbtn = new DataGridViewButtonColumn();
            colattbtn.Name = "solidbacking";
            colattbtn.HeaderText = "辅助属性";
            colattbtn.DefaultCellStyle.NullValue = "空";

            DataGridViewColumn colAttrNum = new DataGridViewTextBoxColumn();
            colAttrNum.DataPropertyName = "attrnum";
            colAttrNum.Name = "attrnum";
            colAttrNum.Visible = false;

            DataGridViewColumn colsummary = new DataGridViewTextBoxColumn();
            colsummary.HeaderText = "备注";
            colsummary.Name = "summary";

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colId, colLine,colNum, colName, colMetering, colquantity, colstockqty, colprice, coltaxprice, colamount, coltax, coltaxamount, colallamount, coldeliveryDate, colattbtn, colAttrNum, colsummary });
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CustomerSelect view = new CustomerSelect();
            view.CustomerSet += View_CustomerSet;
            view.Show();
        }

        private void View_CustomerSet(object sender, Model.CustomerEventArgs e)
        {
            textBox1.Text =  e.name;
            customer = e.num;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MaterielLookUpArgs args = new MaterielLookUpArgs();
            OrderMaterielPopup popup = new OrderMaterielPopup(args);
            popup.AddMateriel += Popup_AddMateriel;
            popup.Show();
        }

        private void Popup_AddMateriel(object sender, Model.MaterielEventArgs e)
        {
            if (this.dataGridView1.ColumnCount == 0) loadColumns(); //Columns有时会丢失

            List<DataGridViewRow> list_row = new List<DataGridViewRow>();
            if (e.ids.Count > 0)
            {
                var dtData = mctrl.getByids4sales(e.ids);
                this.dataGridView1.Rows.Add(dtData.Rows.Count);
                int j = 0;
                for (int i = this.dataGridView1.RowCount - dtData.Rows.Count; i < this.dataGridView1.RowCount; i++)
                {
                    this.dataGridView1.Rows[i].Cells["id"].Value = dtData.Rows[j]["id"];
                    this.dataGridView1.Rows[i].Cells["num"].Value = dtData.Rows[j]["num"];
                    this.dataGridView1.Rows[i].Cells["name"].Value = dtData.Rows[j]["name"];
                    this.dataGridView1.Rows[i].Cells["metering"].Value = dtData.Rows[j]["metering"];
                    this.dataGridView1.Rows[i].Cells["tax"].Value = dtData.Rows[j]["tax"];
                    this.dataGridView1.Rows[i].Cells["stockqty"].Value = dtData.Rows[j]["stockqty"];
                    this.dataGridView1.Rows[i].Cells["deliveryDate"].Value = dateTimePicker2.Value.Date.ToString("yyyy/M/d");
                    
                    this.dataGridView1.Rows[i].Cells["attrnum"].Value = dtData.Rows[j]["attrnum"];
                    if (Convert.ToInt32(dtData.Rows[j]["attrnum"]) == 0) this.dataGridView1.Rows[i].Cells["solidbacking"].Value = "无";

                    this.dataGridView1.Rows[i].Cells["line"].Value = lineIndex;
                    lineIndex++;
                    j++;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "solidbacking" && Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["attrnum"].Value) > 0)
            {
                var id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                var line = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["line"].Value);
                Dictionary<int, attrClass> map = new Dictionary<int, attrClass>();
                if (mapAttr.Keys.Contains(line))
                {
                    map = mapAttr[line];
                }
                OrderSolidBacking view = new OrderSolidBacking(id, line, dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString(), map);
                view.SolidBackingSet += View_SolidBackingSet;
                view.Show();
            }
        }

        private void View_SolidBackingSet(object sender, SolidBackingEventArgs e)
        {
            if (!mapAttr.Keys.Contains(e.line))
            {
                mapAttr.Add(e.line, new Dictionary<int, attrClass>());
                mapAttr[e.line].Add(e.id, e.ac);
            }
            else if (!mapAttr[e.line].Keys.Contains(e.id))
            {
                mapAttr[e.line].Add(e.id, e.ac);
            }
            else
            {
                mapAttr[e.line][e.id] = e.ac;
            }

            dataGridView1.CurrentCell.Value = "";
            for (int i = 0; i < mapAttr[e.line].Values.Count; i++)
            {
                if (i != 0) dataGridView1.CurrentCell.Value += ",";
                dataGridView1.CurrentCell.Value += mapAttr[e.line].Values.ToList()[i].valueName;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
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
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell = null;
            if (this.dataGridView1.RowCount == 0)
            {
                MessageBox.Show("请添加物料！");
                return;
            }
            else if (this.customer == "")
            {
                MessageBox.Show("请添加客户！");
                return;
            }

            SalesOrder obj = new SalesOrder(customer, user, richTextBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            obj.listM = new List<SalesOrderMateriel>();
            for (int i = this.dataGridView1.RowCount; i > 0; i--)
            {
                var line = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["line"].Value);
                SalesOrderMateriel lm = new SalesOrderMateriel();
                lm.line = line;
                lm.price = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["price"].Value), 2);
                lm.materiel = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["id"].Value);
                lm.quantity = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["quantity"].Value);
                if (this.dataGridView1.Rows[i - 1].Cells["summary"].Value != null) lm.summary = this.dataGridView1.Rows[i - 1].Cells["summary"].Value.ToString();
                lm.tax = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["tax"].Value);
                lm.deliveryDate = Convert.ToDateTime(this.dataGridView1.Rows[i - 1].Cells["deliveryDate"].Value);
                if (lm.price == 0 || lm.quantity == 0)
                {
                    MessageBox.Show("物料缺失价格或数量！");
                    return;
                }
                var num = 0;
                var attrnum = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["attrnum"].Value);
                if (mapAttr.Keys.Contains(line)) num = mapAttr[line].Keys.Count;
                if (num < attrnum)
                {
                    MessageBox.Show("请添加辅助属性！");
                    return;
                }
                if (attrnum > 0) lm.combination = sbctrl.getCombin(lm.materiel, mapAttr[line]);
                obj.listM.Add(lm);
            }

            var msg = sctrl.add(obj);
            if (msg.Code == 0)
            {
                //this.Hide();
                MessageBox.Show(msg.Msg);
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                button1.Enabled = false;
                textBox1.Enabled = false;
                richTextBox1.Enabled = false;
                dataGridView1.ReadOnly = true;

                DataTable dtorder = sctrl.getlastinsert(PropertyClass.UserId);
                label7.Visible = true;
                label9.Text = dtorder.Rows[0]["num"].ToString();

                double amount = 0;
                for (int i = 0; i < this.dataGridView1.RowCount; i++)
                {
                    amount += Convert.ToDouble(this.dataGridView1.Rows[i].Cells["amount"].Value);
                }
                sctrl.addMsg(label5.Text,label9.Text, textBox1.Text, dateTimePicker2.Value.ToString(), amount);
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
        }
    }
}
