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
using System.Drawing.Printing;
namespace goods
{
    public partial class GoDownEntryEdit : Form
    {
        listCtrl ctrl = new listCtrl();
        GoDownEntryModel gm = new GoDownEntryModel();
        DataTable dt = new DataTable();
        CommonPrintTools<object> cp;
        PrintDataModel<object> m;
        List<string> list_tableTitle = new List<string> { "编号", "名称", "规格参数", "单位", "辅助单位",  "实收数量", "转换率", "辅助数量", "单价", "金额"};
        public GoDownEntryEdit(string num)
        {
            InitializeComponent();
            loadTable();
            initDate(num);
            initPrintData();
        }
        private void loadTable()
        {
            this.dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            this.dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;

            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;

            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            dataGridViewCellStyle5.Format = "N4";
            dataGridViewCellStyle5.NullValue = null;

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataError += DataGridView1_DataError;

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);

            DataGridViewColumn colNo = new DataGridViewTextBoxColumn();
            colNo.DataPropertyName = "no";
            colNo.Name = "no";
            colNo.HeaderText = "序号";
            colNo.ReadOnly = true;
            dataGridView1.Columns.Add(colNo);

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "MNum";
            colNum.Name = "num";
            colNum.HeaderText = "编号";
            colNum.ReadOnly = true;
            dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "MName";
            colName.Name = "name";
            colName.HeaderText = "名称";
            colName.ReadOnly = true;
            dataGridView1.Columns.Add(colName);

            DataGridViewColumn colSep = new DataGridViewTextBoxColumn();
            colSep.DataPropertyName = "specifications";
            colSep.Name = "specifications";
            colSep.HeaderText = "规格参数";
            colSep.ReadOnly = true;
            dataGridView1.Columns.Add(colSep);

            DataGridViewColumn price = new DataGridViewTextBoxColumn();
            price.DefaultCellStyle = dataGridViewCellStyle5;
            price.DataPropertyName = "price";
            price.HeaderText = "单价";
            price.Name = "price";

            DataGridViewColumn quantity = new DataGridViewTextBoxColumn();
            quantity.HeaderText = "实收数量";
            quantity.Name = "quantity";
            quantity.DataPropertyName = "quantity";

            DataGridViewColumn conversion = new DataGridViewTextBoxColumn();
            conversion.DefaultCellStyle = dataGridViewCellStyle3;
            conversion.HeaderText = "转换率";
            conversion.Name = "conversion";
            conversion.DataPropertyName = "conversion";
            conversion.ReadOnly = true;

            DataGridViewColumn subquantity = new DataGridViewTextBoxColumn();
            subquantity.DefaultCellStyle = dataGridViewCellStyle3;
            subquantity.HeaderText = "辅助数量";
            subquantity.Name = "subquantity";
            subquantity.DataPropertyName = "subquantity";
            subquantity.ReadOnly = true;

            DataGridViewColumn amount = new DataGridViewTextBoxColumn();
            amount.DefaultCellStyle = dataGridViewCellStyle5;
            amount.HeaderText = "金额";
            amount.Name = "amount";
            amount.DataPropertyName = "amount";

            DataGridViewColumn metering = new DataGridViewTextBoxColumn();
            metering.HeaderText = "单位";
            metering.Name = "metering";
            metering.DataPropertyName = "meterName";
            metering.ReadOnly = true;

            DataGridViewColumn submetering = new DataGridViewTextBoxColumn();
            submetering.HeaderText = "辅助单位";
            submetering.Name = "submetering";
            submetering.DataPropertyName = "subMeterName";
            submetering.ReadOnly = true;

            DataGridViewColumn dcIsBatch = new DataGridViewTextBoxColumn();
            dcIsBatch.Name = "isBatch";
            dcIsBatch.DataPropertyName = "isBatch";
            dcIsBatch.Visible = false;

            DataGridViewColumn dcCombination = new DataGridViewTextBoxColumn();
            dcCombination.Name = "combination";
            dcCombination.DataPropertyName = "combination";
            dcCombination.Visible = false;

            DataGridViewColumn dcBatch = new DataGridViewTextBoxColumn();
            dcBatch.Name = "batch";
            dcBatch.DataPropertyName = "batch";
            dcBatch.Visible = false;


            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            metering,
            submetering,
            quantity,
            conversion,
            subquantity,
            price,
            amount,
            dcIsBatch,
            dcCombination,
            dcBatch});
        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["no"].Value = row.Index + 1;
            }
        }
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("数据格式错误！");
            e.Cancel = false;
        }

        private void initDate(string num)
        {
            dt = ctrl.getbyNum(num);
            if (dt.Rows.Count == 0)
            {
                this.Hide();
                return;
            }
            this.label9.Text = DateTime.Parse(dt.Rows[0]["date"].ToString()).ToString("yyyy/M/d");
            this.label10.Text = dt.Rows[0]["num"].ToString();
            this.label11.Text = dt.Rows[0]["supplierName"].ToString();
            this.label12.Text = dt.Rows[0]["fullName"].ToString();
            this.label13.Text = dt.Rows[0]["warehouseName"].ToString();
            this.label15.Text = dt.Rows[0]["positionName"].ToString();

            gm.warehouse = Convert.ToInt32(dt.Rows[0]["warehouse"]) ;
            if (dt.Rows[0]["position"] == DBNull.Value) gm.position = null;
            else gm.position = Convert.ToInt32(dt.Rows[0]["position"]);
            gm.isDeficit = Convert.ToBoolean(dt.Rows[0]["isDeficit"]);

            gm.id = Convert.ToInt32(dt.Rows[0]["gid"]);
            gm.supplier = Convert.ToInt32(dt.Rows[0]["supplier"]);

            dt.Columns.Add("amount");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["amount"] = Convert.ToDouble(dt.Rows[i]["quantity"]) * Convert.ToDouble(dt.Rows[i]["price"]);
            }
            dataGridView1.DataSource = dt;

            if (Convert.ToUInt32(dt.Rows[0]["user"]) != PropertyClass.UserId)
            {
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                dataGridView1.ReadOnly = true;
            }
            else
            {
                int can = ctrl.stockChanged(gm.id);
                if (can != 0)
                {
                    toolStripButton1.Enabled = false;
                    toolStripButton2.Enabled = false;
                    dataGridView1.ReadOnly = true;
                }
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell = null;

            var listM = new List<ListModel>();
            for (int i = this.dataGridView1.RowCount; i > 0; i--)
            {
                ListModel lm = new ListModel();
                if(this.dataGridView1.Rows[i - 1].Cells["conversion"].Value != DBNull.Value) lm.conversion = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["conversion"].Value), 2);
                lm.id = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["id"].Value);
                if(this.dataGridView1.Rows[i - 1].Cells["quantity"].Value == DBNull.Value || this.dataGridView1.Rows[i - 1].Cells["price"].Value == DBNull.Value)
                {
                    MessageBox.Show("物料缺失数量,价格！");
                    return;
                }
                lm.quantity = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["quantity"].Value);
                lm.price = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["price"].Value);
                lm.isBatch = Convert.ToBoolean(this.dataGridView1.Rows[i - 1].Cells["isBatch"].Value);
                if(this.dataGridView1.Rows[i - 1].Cells["combination"].Value != DBNull.Value) lm.combination = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["combination"].Value); 
                if (lm.id == 0)
                {
                    MessageBox.Show("错误，请重新加载窗口！");
                    return;
                }
                listM.Add(lm);
            }

            var msg = ctrl.setList(gm,listM);
            if (msg.Code == 0)
            {
                //this.Hide();
                MessageBox.Show(msg.Msg);
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var listM = new List<ListModel>();
                for (int i = this.dt.Rows.Count; i > 0; i--)
                {
                    ListModel lm = new ListModel();
                    lm.materiel = Convert.ToInt32(dt.Rows[i - 1]["materiel"]);
                    lm.quantity = Convert.ToDouble(dt.Rows[i - 1]["quantity"]);
                    lm.isBatch = Convert.ToBoolean(dt.Rows[i - 1]["isBatch"]);
                    lm.batch = Convert.ToInt32(dt.Rows[i - 1]["batch"]);
                    if (dt.Rows[i - 1]["combination"] != DBNull.Value) lm.combination = Convert.ToInt32(dt.Rows[i - 1]["combination"]);
                    listM.Add(lm);
                }

                var msg = ctrl.delorder(gm, listM);
                if (msg.Code == 0)
                {
                    MessageBox.Show(msg.Msg);
                    //this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            cp.PrintSetup();
        }
        private void initPrintData()
        {
            m = new PrintDataModel<object>();
            m.TableData = new List<object>();
            m.pageTitle = "入库单";
            m.TitleData = new List<string> {
                "日期：" + this.label9.Text ,
                "编号" + this.label10.Text,
                "供应商：" + this.label11.Text,
                "仓库："+ this.label13.Text,
                "仓位："+ this.label15.Text,
                "制单人：" +this.label12.Text
            };

            m.ColumnNames = list_tableTitle;
            m.CanResetLine = new List<bool> { true, true, true, true, true, true, true, true, true, true };
            List<PrintDataModel<object>> list = new List<PrintDataModel<object>> { m };
            cp = new CommonPrintTools<object>(list);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cp.reset();
            printListModel tm;
            m.TableData = new List<object>();
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                tm = new printListModel(this.dataGridView1.Rows[i]);
                m.TableData.Add(tm);
            }
            //m.EndData = new List<string> { "jj", "tt" };
            try
            {
                //cp = new CommonPrintTools<object>(list);
                cp.PrintPriview();
                //cp.Print();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void DataGridView1_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if(this.dataGridView1.Columns[e.ColumnIndex].Name == "quantity")
            {
                var quantity = this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value;
                if(quantity != DBNull.Value)
                {
                    var conversion = this.dataGridView1.Rows[e.RowIndex].Cells["conversion"].Value;
                    if (conversion != DBNull.Value)
                        this.dataGridView1.Rows[e.RowIndex].Cells["subquantity"].Value
                            = Convert.ToDouble(conversion) * Convert.ToDouble(quantity);
                    var price = this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value;
                    if(price  != DBNull.Value)
                        this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value = Convert.ToDouble(price) *Convert.ToDouble(quantity);
                }
            }
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "price")
            {
                var price = this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value;
                if (price != DBNull.Value)
                {
                    var quantity = this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value;
                    if (quantity != DBNull.Value)
                        this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value = Convert.ToDouble(price) * Convert.ToDouble(quantity);
                }
            }
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "amount")
            {
                var amount = this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value;
                if (amount != DBNull.Value)
                {
                    var quantity = this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value;
                    if (quantity != DBNull.Value)
                        this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value = Convert.ToDouble(amount) / Convert.ToDouble(quantity);
                }
            }
            //switch (this.dataGridView1.Columns[e.ColumnIndex].Name)
            //{
            //    case "quantity":
            //        this.dataGridView1.Rows[e.RowIndex].Cells["subquantity"].Value
            //            = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value) *
            //            Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["conversion"].Value);
            //        this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value
            //            = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
            //            Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
            //        break;
            //}
        }
    }
}
