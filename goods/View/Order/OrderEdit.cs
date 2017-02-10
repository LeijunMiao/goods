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
using Observer;
using Microsoft.VisualBasic;
namespace goods
{
    public partial class OrderEdit : Form
    {
        materielCtrl mctrl = new materielCtrl();
        orderCtrl ctrl = new orderCtrl();
        solidbackingCtrl sbctrl = new solidbackingCtrl();
        string posummary = "";
        DataTable dt = new DataTable();
        int supplier = -1;
        int orderid;
        List<int> allids = new List<int>();

        CommonPrintTools<object> cp;
        PrintDataModel<object> m;
        List<string> list_tableTitle = new List<string> { "物料编码", "名称", "规格参数", "计量单位", "数量","价税合计", "交货日期" };

        Dictionary<int, Dictionary<int, attrClass>> mapAttr = new Dictionary<int, Dictionary<int, attrClass>>();
        public OrderEdit(int poid)
        {
            InitializeComponent();
            MidModule.EventSendIds += new IdsDlg(renderMateriel);
            loadTable();
            orderid = poid;
            loadData();
            initPrintData();
        }

        private void renderMateriel(object sender, List<int> ids)
        {
            if (ids.Count > 0)
            {
                allids.AddRange(ids);
                var dtData = mctrl.getByids(ids, supplier);
                DataColumn dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.DateTime");
                dc.ColumnName = "deliveryDate";
                dtData.Columns.Add(dc);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["deliveryDate"] = DateTime.Now.Date;
                }
                dt.Merge(dtData);
                dataGridView1.DataSource = dt;
            }
        }

        private void loadTable()
        {
            this.dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            dataGridViewCellStyle5.Format = "N4";
            dataGridViewCellStyle5.NullValue = null;

            this.dataGridView1.DataError += DataGridView1_DataError;
            this.dataGridView1.AutoGenerateColumns = false;

            this.dataGridView1.ShowCellToolTips = true;
            this.dataGridView1.CellMouseEnter += DataGridView1_CellMouseEnter;
            this.dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            this.dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";

            DataGridViewColumn colOMId = new DataGridViewTextBoxColumn();
            colOMId.DataPropertyName = "omid";
            colOMId.Visible = false;
            colOMId.Name = "omid";

            DataGridViewColumn colNo = new DataGridViewTextBoxColumn();
            colNo.DataPropertyName = "no";
            colNo.Name = "no";
            colNo.HeaderText = "序号";
            colNo.ReadOnly = true;

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "num";
            colNum.Name = "num";
            colNum.HeaderText = "编号";
            colNum.ReadOnly = true;

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            colName.Name = "name";
            colName.HeaderText = "名称";
            colName.ReadOnly = true;

            DataGridViewColumn colSep = new DataGridViewTextBoxColumn();
            colSep.DataPropertyName = "specifications";
            colSep.Name = "specifications";
            colSep.HeaderText = "规格参数";
            colSep.ReadOnly = true;

            DataGridViewColumn price = new DataGridViewTextBoxColumn();
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            price.DefaultCellStyle = dataGridViewCellStyle5;
            price.DataPropertyName = "price";
            price.HeaderText = "单价";
            price.Name = "price";

            DataGridViewColumn quantity = new DataGridViewTextBoxColumn();
            quantity.HeaderText = "数量";
            quantity.Name = "quantity";
            quantity.DataPropertyName = "quantity";


            DataGridViewColumn amount = new DataGridViewTextBoxColumn();
            amount.DefaultCellStyle = dataGridViewCellStyle5;
            amount.HeaderText = "金额";
            amount.Name = "amount";
            amount.DataPropertyName = "amount";

            DataGridViewColumn metering = new DataGridViewTextBoxColumn();
            metering.HeaderText = "单位";
            metering.Name = "metering";
            metering.DataPropertyName = "metering";
            metering.ReadOnly = true;

            DataGridViewColumn conversion = new DataGridViewTextBoxColumn();
            conversion.HeaderText = "转换率";
            conversion.Name = "conversion";
            conversion.DataPropertyName = "conversion";
            conversion.ReadOnly = true;

            DataGridViewColumn tax = new DataGridViewTextBoxColumn();
            tax.HeaderText = "税率";
            tax.Name = "tax";
            tax.DataPropertyName = "tax";

            DataGridViewColumn summary = new DataGridViewTextBoxColumn();
            summary.HeaderText = "备注";
            summary.Name = "summary";
            summary.DataPropertyName = "summary";

            DataGridViewColumn colallamount = new DataGridViewTextBoxColumn();
            colallamount.DefaultCellStyle = dataGridViewCellStyle5;
            colallamount.Name = "allamount";
            colallamount.HeaderText = "价税合计";
            colallamount.DataPropertyName = "allamount";

            dataGridViewCellStyle4.Format = "yyyy-MM-dd";
            dataGridViewCellStyle4.NullValue = null;
            this.deliveryDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.deliveryDate.HeaderText = "交货日期";
            this.deliveryDate.Name = "deliveryDate";
            this.deliveryDate.DataPropertyName = "deliveryDate";
            this.deliveryDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deliveryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;

            //DataGridViewColumn colattbtn = new DataGridViewTextBoxColumn();
            //colattbtn.DataPropertyName = "solidbacking";
            //colattbtn.Name = "solidbacking";
            //colattbtn.HeaderText = "辅助属性";
            //colattbtn.DefaultCellStyle.NullValue = "空";
            //colattbtn.ReadOnly = true;

            DataGridViewButtonColumn colattbtn = new DataGridViewButtonColumn();
            colattbtn.Name = "solidbacking";
            colattbtn.HeaderText = "辅助属性";
            colattbtn.DataPropertyName = "solidbacking";
            colattbtn.DefaultCellStyle.NullValue = "空";

            DataGridViewColumn colAttrNum = new DataGridViewTextBoxColumn();
            colAttrNum.DataPropertyName = "attrnum";
            colAttrNum.Name = "attrnum";
            colAttrNum.Visible = false;

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colId,colNo,colNum,colName,colSep,metering,quantity,conversion,price,tax, amount,summary,colallamount,this.deliveryDate,colOMId,colattbtn,colAttrNum });

            
        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["no"].Value = row.Index + 1;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "solidbacking" && Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["attrnum"].Value) > 0)
            {
                var id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                Dictionary<int, attrClass> map = new Dictionary<int, attrClass>();
                if (mapAttr.Keys.Contains(id))
                {
                    map = mapAttr[id];
                }
                OrderSolidBacking view = new OrderSolidBacking(id, e.RowIndex, dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString(), map);
                view.SolidBackingSet += View_SolidBackingSet;
                view.Show();
            }
        }

        private void View_SolidBackingSet(object sender, SolidBackingEventArgs e)
        {
            if (!mapAttr.Keys.Contains(e.materiel))
            {
                mapAttr.Add(e.materiel, new Dictionary<int, attrClass>());
                mapAttr[e.materiel].Add(e.id, e.ac);
            }
            else if (!mapAttr[e.materiel].Keys.Contains(e.id))
            {
                mapAttr[e.materiel].Add(e.id, e.ac);
            }
            else
            {
                mapAttr[e.materiel][e.id] = e.ac;

            }

            dataGridView1.CurrentCell.Value = "";
            for (int i = 0; i < mapAttr[e.materiel].Values.Count; i++)
            {
                if (i != 0) dataGridView1.CurrentCell.Value += ",";
                dataGridView1.CurrentCell.Value += mapAttr[e.materiel].Values.ToList()[i].valueName;
            }
        }
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("数据格式不正确！");
            e.Cancel = false;
        }

        private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();
        }

        private void loadData()
        {
            dt = ctrl.getbyId(orderid);
            if (dt.Rows.Count == 0)
            {
                this.Close();
                return;
            }
            allids = new List<int>();
            List<int> lits_omid = new List<int>();
            dt.Columns.Add("allamount");
            dt.Columns.Add("solidbacking");
            DataColumn dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int64");
            dc.ColumnName = "attrnum";
            dt.Columns.Add(dc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                allids.Add(Convert.ToInt32(dt.Rows[i]["id"]));
                lits_omid.Add(Convert.ToInt32(dt.Rows[i]["omid"]));
                dt.Rows[i]["allamount"] = Math.Round(Convert.ToDouble(dt.Rows[i]["quantity"]) * Convert.ToDouble(dt.Rows[i]["price"])*(1 + Convert.ToDouble(dt.Rows[i]["tax"])),4);
            }
            this.label7.Text = DateTime.Parse(dt.Rows[0]["date"].ToString()).ToString("yyyy/M/d");
            this.label13.Text = dt.Rows[0]["ponum"].ToString();
            this.label10.Text = dt.Rows[0]["supplier"].ToString();
            supplier = Convert.ToInt32(dt.Rows[0]["supplierId"]);
            this.label11.Text = dt.Rows[0]["user"].ToString();
            this.label8.Text = DateTime.Parse(dt.Rows[0]["podeliveryDate"].ToString()).ToString("yyyy/M/d"); 
            this.textBox1.Text = dt.Rows[0]["posummary"].ToString();
            posummary = dt.Rows[0]["posummary"].ToString(); 


            Dictionary<int, attrNumModel> map = ctrl.getbyOMIds(lits_omid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var omid = Convert.ToInt32(dt.Rows[i]["omid"]);
                var mid = Convert.ToInt32(dt.Rows[i]["id"]);
                if (map.Keys.Contains(omid))
                {
                    dt.Rows[i]["solidbacking"] = map[omid].value;
                    dt.Rows[i]["attrnum"] = map[omid].num;
                    if (!mapAttr.Keys.Contains(mid))
                    {
                        mapAttr.Add(mid, new Dictionary<int, attrClass>());
                    }
                    for (int j = 0; j < map[omid].list.Count; j++)
                    {
                        mapAttr[mid].Add(map[omid].list[j].solidbacking,new attrClass(map[omid].list[j].solidbacking,map[omid].list[j].key,map[omid].list[j].value));
                    }
                }
                else
                {
                    dt.Rows[i]["attrnum"] = 0;
                    dt.Rows[i]["solidbacking"] = "无";
                }
            }
            dataGridView1.DataSource = dt;
            if (Convert.ToUInt32(dt.Rows[0]["pouser"]) != PropertyClass.UserId)
            {
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
                toolStripButton6.Enabled = false;
                dataGridView1.ReadOnly = true;
            }
            else
            {
                int can = ctrl.hasGoDownEntry(orderid);
                if (can != 0)
                {
                    toolStripButton1.Enabled = false;
                    toolStripButton2.Enabled = false;
                    toolStripButton3.Enabled = false;
                    toolStripButton6.Enabled = false;
                    dataGridView1.ReadOnly = true;
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
            List < ListModel > list = new List<ListModel>();
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (this.dataGridView1.Rows[i].Cells["price"].Value == DBNull.Value || this.dataGridView1.Rows[i].Cells["quantity"].Value == DBNull.Value)
                {
                    MessageBox.Show("物料缺失价格或数量！");
                    return;
                }
                ListModel lm = new ListModel();
                lm.name = this.dataGridView1.Rows[i].Cells["name"].Value.ToString();
                lm.price = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[i].Cells["price"].Value), 4);
                lm.line = i;
                lm.materiel = Convert.ToInt32(this.dataGridView1.Rows[i].Cells["id"].Value);
                lm.quantity = Convert.ToDouble(this.dataGridView1.Rows[i].Cells["quantity"].Value);
                lm.summary = this.dataGridView1.Rows[i].Cells["summary"].Value.ToString();
                lm.tax = Convert.ToDouble(this.dataGridView1.Rows[i].Cells["tax"].Value);
                if(this.dataGridView1.Rows[i].Cells["conversion"].Value != DBNull.Value) lm.conversion = Convert.ToDouble(this.dataGridView1.Rows[i].Cells["conversion"].Value);
                lm.deliveryDate = Convert.ToDateTime(this.dataGridView1.Rows[i].Cells["deliveryDate"].Value);
                var num = 0;
                var attrnum = Convert.ToInt32(this.dataGridView1.Rows[i].Cells["attrnum"].Value);
                if (mapAttr.Keys.Contains(lm.materiel)) num = mapAttr[lm.materiel].Keys.Count;
                if (num < attrnum)
                {
                    MessageBox.Show("请添加辅助属性！");
                    return;
                }
                if (attrnum > 0) lm.combination = sbctrl.getCombin(lm.materiel, mapAttr[lm.materiel]);
                list.Add(lm);
            }
            OrderModel order = new OrderModel();
            order.SupName = label10.Text;
            order.Num = label13.Text;
            order.Id = orderid;
            if (posummary != this.textBox1.Text) order.Summary = this.textBox1.Text;
            var msg = ctrl.setList(order, list);
            MessageBox.Show(msg.Msg);
        }
        private void DataGridView1_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //if ((this.dataGridView1.Columns[e.ColumnIndex].Name == "quantity" || this.dataGridView1.Columns[e.ColumnIndex].Name == "price") 
            //    && this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value != DBNull.Value 
            //    && this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value != DBNull.Value)
            //{
            //    this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value
            //        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
            //        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
            //    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) * (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value)),2);
            //}
            if(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value != DBNull.Value && this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value != DBNull.Value)
            switch (this.dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "price":
                    this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                        = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) * (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value)), 4);
                    break;
                case "tax":
                    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                        = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) * (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value)), 4);
                    break;
              
                case "quantity":
                    this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value
                        = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value) *
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                    this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                        = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) * (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value)), 4);

                    break;
                case "amount":
                        if (this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value != DBNull.Value)
                        {
                            if (this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value != null)
                                this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value
                                = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) /
                                Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                            this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value
                                = Math.Round(Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) * (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value)), 4);
                        }
                    break;
                case "allamount":
                    if (this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value != DBNull.Value && this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value != DBNull.Value)
                        this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["allamount"].Value) / (1 + Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["tax"].Value));
                    this.dataGridView1.Rows[e.RowIndex].Cells["price"].Value = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["amount"].Value) /
                        Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["quantity"].Value);
                    break;
                default:
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MaterielLookUpArgs args = new MaterielLookUpArgs();
            args.ids = allids;
            OrderMaterielPopup popup = new OrderMaterielPopup(args);
            popup.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
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

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var msg = ctrl.del(orderid, label13.Text, label10.Text);
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
        private void initPrintData()
        {
            m = new PrintDataModel<object>();
            m.TableData = new List<object>();
            m.pageTitle = "采购订单";
            m.ColumnNames = list_tableTitle;
            m.CanResetLine = new List<bool> { true, true, true, true, true, true, true};
            List<PrintDataModel<object>> list = new List<PrintDataModel<object>> { m };
            cp = new CommonPrintTools<object>(list);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cp.PrintSetup();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string num = Interaction.InputBox("请输入打印份数", "输入", "1");
            if(num != "")
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
                    "订单编号："+label13.Text,
                    "日期：" + label7.Text,
                    "供应商：" + label10.Text,
                    "交货日期："+ label8.Text,
                    "制单人：" +label11.Text,
                    "",
                    "摘要："+textBox1.Text
                    //"制单人：" +label11.Text
                    
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
            
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择行！");
                return;
            }
            else
            {
                BatchList view = new BatchList(Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["omid"].Value));
                view.Show();
            }
            
        }
    }
}
