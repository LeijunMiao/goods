using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goods.Controller;
using goods.Model;
using Observer;
using Microsoft.VisualBasic;
namespace goods
{
    public partial class CreateOrderView : Form
    {
        materielCtrl mctrl = new materielCtrl();
        orderCtrl octrl = new orderCtrl();
        DataTable dt = new DataTable();
        int supplier = -1;
        int user = -1;
        bool isSave = false;
        public List<int> allids = new List<int>();

        CommonPrintTools<object> cp;
        PrintDataModel<object> m;
        List<string> list_tableTitle = new List<string> { "物料编码", "名称", "规格参数", "计量单位", "数量", "价税合计", "交货日期" };
        public CreateOrderView()
        {
            InitializeComponent();
            loadTable();
            initData();
            initPrintData();
            MidModule.EventSend += new MsgDlg(MidModule_EventSend);
            MidModule.EventSendIds += new IdsDlg(renderMateriel);
        }
        private void MidModule_EventSend(object sender, object msg)
        {
            if (sender != null)
            {
                SupplierModel sm = (SupplierModel)msg;
                this.textBox2.Text = sm.Name;
                this.supplier = sm.Id;
            }
        }

        private void loadTable()
        {
            this.dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Rows.Clear();
            loadColumns();
        }
        private void loadColumns()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

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

            DataGridViewColumn colSubMetering = new DataGridViewTextBoxColumn();
            colSubMetering.DataPropertyName = "subMetering";
            colSubMetering.Name = "subMetering";
            colSubMetering.HeaderText = "辅助单位";
            colSubMetering.ReadOnly = true;
            this.dataGridView1.Columns.Add(colSubMetering);

            DataGridViewColumn colType = new DataGridViewTextBoxColumn();
            colType.DataPropertyName = "type";
            colType.Name = "type";
            colType.HeaderText = "属性";
            colType.ReadOnly = true;
            this.dataGridView1.Columns.Add(colType);

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

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.price,
            this.tax,
            this.taxprice,
            this.quantity,
            this.conversion,
            this.subquantity,
            this.amount,
            coltaxamount,
            colallamount,
            this.deliveryDate,
            this.summary});
            // 
            // price
            // 
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.price.DefaultCellStyle = dataGridViewCellStyle3;
            this.price.HeaderText = "单价";
            this.price.Name = "price";
            // 
            // taxprice
            // 
            this.taxprice.DefaultCellStyle = dataGridViewCellStyle3;
            this.taxprice.HeaderText = "含税单价";
            this.taxprice.Name = "taxprice";
            // 
            // amount
            // 
            this.amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.amount.HeaderText = "金额";
            this.amount.Name = "amount";
            // 
            // tax
            // 
            this.tax.DefaultCellStyle = dataGridViewCellStyle3;
            this.tax.HeaderText = "税率";
            this.tax.DataPropertyName = "tax";
            this.tax.Name = "tax";
            // 
            // deliveryDate
            // 
            dataGridViewCellStyle4.Format = "yyyy-MM-dd";
            dataGridViewCellStyle4.NullValue = null;
            this.deliveryDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.deliveryDate.HeaderText = "交货日期";
            this.deliveryDate.Name = "deliveryDate";
            this.deliveryDate.DataPropertyName = "deliveryDate";
            this.deliveryDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deliveryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // quantity
            // 
            this.quantity.HeaderText = "数量";
            this.quantity.Name = "quantity";
            this.subquantity.DefaultCellStyle = dataGridViewCellStyle3;
            // 
            // subquantity
            // 
            this.subquantity.HeaderText = "辅助数量";
            this.subquantity.Name = "subquantity";
            this.subquantity.DefaultCellStyle = dataGridViewCellStyle3;
            // 
            // conversion
            // 
            this.conversion.DefaultCellStyle = dataGridViewCellStyle3;
            this.conversion.HeaderText = "转换率";
            this.conversion.Name = "conversion";
            this.conversion.DataPropertyName = "conversion";
            this.conversion.ReadOnly = true;
            // 
            // summary
            // 
            this.summary.HeaderText = "备注";
            this.summary.Name = "summary";

        }
        private void initData()
        {
            this.label7.Text = PropertyClass.SendNameValue;
            this.user = PropertyClass.UserId;

            //DataColumn dcId = new DataColumn("ID");
            //DataColumn dcNO = new DataColumn("编号");
            //DataColumn dcUName = new DataColumn("名称");
            //DataColumn dcSep = new DataColumn("规格型号");
            //DataColumn dcMete = new DataColumn("计量单位");
            //DataColumn dcSme = new DataColumn("辅助计量单位");
            //DataColumn dcConv = new DataColumn("转换率");
            //DataColumn dcType = new DataColumn("属性");
            //DataColumn dcTax = new DataColumn("税率");
            //DataColumn[] list_dc = { dcId, dcNO, dcUName, dcSep, dcMete, dcSme, dcConv, dcType, dcTax };
            //dt.Columns.AddRange(list_dc);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OrderMaterielPopup popup = new OrderMaterielPopup(allids);
            popup.Show();
        }
        public void renderMateriel(object sender, List<int> ids)
        {
            if(this.dataGridView1.ColumnCount == 0) loadColumns(); //Columns有时会丢失

            List<DataGridViewRow> list_row = new List<DataGridViewRow>();
            if (ids.Count > 0){
                allids.AddRange(ids);
                var dtData = mctrl.getByids(ids);
                this.dataGridView1.Rows.Add(dtData.Rows.Count);
                int j = 0;
                for (int i = this.dataGridView1.RowCount - dtData.Rows.Count; i < this.dataGridView1.RowCount; i++)
                {
                    this.dataGridView1.Rows[i].Cells["id"].Value = dtData.Rows[j]["id"];
                    this.dataGridView1.Rows[i].Cells["num"].Value = dtData.Rows[j]["num"];
                    this.dataGridView1.Rows[i].Cells["name"].Value = dtData.Rows[j]["name"];
                    this.dataGridView1.Rows[i].Cells["specifications"].Value = dtData.Rows[j]["specifications"];
                    this.dataGridView1.Rows[i].Cells["metering"].Value = dtData.Rows[j]["metering"];
                    this.dataGridView1.Rows[i].Cells["subMetering"].Value = dtData.Rows[j]["subMetering"];
                    if (dtData.Rows[j]["subMetering"] == DBNull.Value)
                    {
                        this.dataGridView1.Rows[i].Cells["subquantity"].ReadOnly = true;
                    }
                    this.dataGridView1.Rows[i].Cells["conversion"].Value = dtData.Rows[j]["conversion"];
                    this.dataGridView1.Rows[i].Cells["type"].Value = dtData.Rows[j]["type"];
                    this.dataGridView1.Rows[i].Cells["tax"].Value = dtData.Rows[j]["tax"];
                    this.dataGridView1.Rows[i].Cells["deliveryDate"].Value = dateTimePicker2.Value.Date.ToString("yyyy/M/d");
                    j++;
                }


                //dt.Merge(dtData);
                //dataGridView1.DataSource = dt;
                //for (int i = 0; i < dataGridView1.RowCount; i++)
                //{
                //    if (dataGridView1.Rows[i].Cells["subMetering"].Value.ToString() == "")
                //    {
                //        dataGridView1.Rows[i].Cells["subquantity"].ReadOnly = true;
                //    } 
                //}


                //dataGridView1.Rows.Add(dtData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierSelect ss = new SupplierSelect();
            ss.Show();
        }
        

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.SelectedRows.Count == 0 )
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell = null;
            if (this.dataGridView1.RowCount == 0)
            {
                MessageBox.Show("请添加物料！");
                return;
            }
            else if (supplier == -1)
            {
                MessageBox.Show("请添加供应商！");
                return;
            }
            orderParmas obj = new orderParmas(supplier, user, textBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            obj.listM = new List<ListModel>();
            for (int i = this.dataGridView1.RowCount; i > 0; i--)
            {
                ListModel lm = new ListModel();
                if(this.dataGridView1.Rows[i - 1].Cells["conversion"].Value != DBNull.Value) lm.conversion = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["conversion"].Value),2);
                lm.price = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["price"].Value),2);
                lm.line = i;
                lm.materiel = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["id"].Value);
                lm.quantity = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["quantity"].Value);
                if (this.dataGridView1.Rows[i - 1].Cells["summary"].Value != null)  lm.summary = this.dataGridView1.Rows[i - 1].Cells["summary"].Value.ToString();
                lm.tax = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["tax"].Value);
                lm.deliveryDate = Convert.ToDateTime(this.dataGridView1.Rows[i - 1].Cells["deliveryDate"].Value);
                if(lm.price == 0 || lm.quantity == 0)
                {
                    MessageBox.Show("物料缺失价格或数量！");
                    return;
                }

                obj.listM.Add(lm);
            }
            
            var msg = octrl.add(obj);
            if (msg.Code == 0)
            {
                //this.Hide();
                MessageBox.Show(msg.Msg);
                isSave = true;
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                button1.Enabled = false;
                textBox1.Enabled = false;
                dataGridView1.Enabled = false;

                DataTable dtorder = octrl.getlastinsert(PropertyClass.UserId);
                label9.Visible = true;
                label10.Text = dtorder.Rows[0]["num"].ToString();

                octrl.addMsg(label10.Text, textBox2.Text, dateTimePicker2.Value.ToString());
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cp.PrintSetup();
        }
        private void initPrintData()
        {
            m = new PrintDataModel<object>();
            m.TableData = new List<object>();
            m.pageTitle = "采购订单";
            m.ColumnNames = list_tableTitle;
            m.CanResetLine = new List<bool> { true, true, true, true, true, true, true };
            List<PrintDataModel<object>> list = new List<PrintDataModel<object>> { m };
            cp = new CommonPrintTools<object>(list);
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (!isSave)
            {
                MessageBox.Show("请先保存！");
                return;
            }
            string num = Interaction.InputBox("请输入打印份数", "输入", "1");
            if (num == "") return;
            if (Convert.ToInt16(num) > 0)
            {
                cp.setCopies(Convert.ToInt16(num));
                printModel tm;
                m.TableData = new List<object>();

                for (int i = 0; i < this.dataGridView1.RowCount; i++)
                {
                    tm = new printModel(this.dataGridView1.Rows[i]);
                    m.TableData.Add(tm);
                }

                m.TitleData = new List<string> {
                    "编号："+ label10.Text,
                    "日期：" + dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    "供应商：" + textBox2.Text,
                    "交货日期："+ dateTimePicker2.Value.ToString("yyyy-MM-dd"),
                    "制单人：" +label7.Text,
                    "备注："+textBox1.Text
                };

                try
                {
                    cp.reset();
                    cp.PrintPriview();

                }
                catch (Exception)
                {

                    throw;
                }
            }
            
            //this.printPreviewDialog1.ShowDialog();
        }
       

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

            //MessageBox.Show(e.RowIndex +"|"+e.ColumnIndex);
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
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value)  *
                        (1+Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value));
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
                        (1+Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value));
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
                    if (dataGridView1.Rows[e.RowIndex].Cells["subMetering"].Value.ToString() != "")
                    {
                        this.dataGridView1.Rows[e.RowIndex].Cells["subquantity"].Value
                            = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value) *
                            Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["conversion"].Value);
                    }

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
                case "conversion":
                    this.dataGridView1.Rows[e.RowIndex].Cells["subquantity"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["conversion"].Value);
                    break;
                case "subquantity":
                    if(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value != null)
                    {
                        this.dataGridView1.Rows[e.RowIndex].Cells["conversion"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["subquantity"].Value) /
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                    }
                    
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
    }
}
