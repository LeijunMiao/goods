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
    public partial class CallSlipView : Form
    {
        listCtrl ctrl = new listCtrl();
        CallSlipModel cm = new CallSlipModel();
        DataTable dt = new DataTable();
        CommonPrintTools<object> cp;
        PrintDataModel<object> m;
        List<string> list_tableTitle = new List<string> { "编号", "名称", "规格参数", "单位", "辅助单位", "领料数量", "转换率", "辅助数量"};

        public CallSlipView(string num)
        {
            InitializeComponent();
            loadTable();
            initDate(num);
            initPrintData();
        }
        private void loadTable()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1.AutoGenerateColumns = false;

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);

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


            DataGridViewColumn quantity = new DataGridViewTextBoxColumn();
            quantity.HeaderText = "领料数量";
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

            DataGridViewColumn warehouse = new DataGridViewTextBoxColumn();
            warehouse.HeaderText = "仓库";
            warehouse.Name = "warehouse";
            warehouse.DataPropertyName = "warehouseName";
            warehouse.ReadOnly = true;

            DataGridViewColumn position = new DataGridViewTextBoxColumn();
            position.HeaderText = "仓位";
            position.Name = "position";
            position.DataPropertyName = "positionName";
            position.ReadOnly = true;

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            metering,
            submetering,
            quantity,
            conversion,
            subquantity,
            warehouse,
            position});
        }
        private void initDate(string num)
        {
            dt = ctrl.getCallSlipbyNum(num);
            if (dt.Rows.Count == 0)
            {
                this.Hide();
                return;
            }
            this.label9.Text = DateTime.Parse(dt.Rows[0]["date"].ToString()).ToString("yyyy/M/d");
            this.label10.Text = dt.Rows[0]["num"].ToString();
            this.label12.Text = dt.Rows[0]["fullName"].ToString();
            //this.label13.Text = dt.Rows[0]["warehouseName"].ToString();
            //this.label15.Text = dt.Rows[0]["positionName"].ToString();
            dataGridView1.DataSource = dt;

            //cm.warehouse = Convert.ToInt32(dt.Rows[0]["warehouse"]);
            //if (dt.Rows[0]["position"] == DBNull.Value) cm.position = null;
            //else cm.position = Convert.ToInt32(dt.Rows[0]["position"]);
            cm.isDeficit = Convert.ToBoolean(dt.Rows[0]["isDeficit"]);

            cm.id = Convert.ToInt32(dt.Rows[0]["gid"]);

            if (Convert.ToUInt32(dt.Rows[0]["user"]) != PropertyClass.UserId)
            {
                toolStripButton2.Enabled = false;
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
                    lm.warehouse = Convert.ToInt32(dt.Rows[i - 1]["warehouse"]);
                    if (dt.Rows[i - 1]["position"] != DBNull.Value) lm.position = Convert.ToInt32(dt.Rows[i - 1]["position"]);
                    lm.supplier = Convert.ToInt32(dt.Rows[i - 1]["supplier"]);
                    if (dt.Rows[i - 1]["batch"] != DBNull.Value)  lm.batch = Convert.ToInt32(dt.Rows[i - 1]["batch"]);
                    listM.Add(lm);
                }

                var msg = ctrl.delCallSlip(cm, listM);
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
            m.pageTitle = "领料单";
            m.TitleData = new List<string> {
                "日期：" + this.label9.Text ,
                "编号：" + this.label10.Text,
             //   "仓库："+ this.label13.Text,
             //   "仓位："+ this.label15.Text,
                "制单人：" +this.label12.Text
            };

            m.ColumnNames = list_tableTitle;
            m.CanResetLine = new List<bool> { true, true, true, true, true, true, true, true };
            List<PrintDataModel<object>> list = new List<PrintDataModel<object>> { m };
            cp = new CommonPrintTools<object>(list);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cp.reset();
            printCallSlipModel tm;
            m.TableData = new List<object>();
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                tm = new printCallSlipModel(this.dataGridView1.Rows[i]);
                m.TableData.Add(tm);
            }
            //m.EndData = new List<string> { "jj", "tt" };
            try
            {
                //cp = new CommonPrintTools<object>(list);
                cp.PrintPriview();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
