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
using Observer;
using goods.Model;
namespace goods
{
    public partial class GoDownEntryList : Form
    {
        listCtrl ctrl = new listCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        public GoDownEntryList()
        {
            InitializeComponent();
            MidModule.EventSend += new MsgDlg(MidModule_EventSend);
            initPage();
            loadDate(1);
        }
        private void MidModule_EventSend(object sender, object msg)
        {
            if (sender != null)
            {
                SupplierModel sm = (SupplierModel)msg;
                this.textBox1.Text = sm.Name;
            }
        }
        private void initPage()
        {
            this.dateTimePicker1.Checked = false;
            this.textBox1.KeyDown += button1_KeyDown;
            this.textBox2.KeyDown += button1_KeyDown;
            this.textBox3.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.dataGridView1.ReadOnly = true;
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            loadDate(pagingCom1.PageIndex);
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { loadDate(1); }
        }
        public void loadDate(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, this.textBox1.Text, this.dateTimePicker1.Checked,this.dateTimePicker1.Value.Date, textBox2.Text, textBox3.Text);
            dt = new DataTable();
            DataColumn dcDate = new DataColumn("日期");
            DataColumn dcNum = new DataColumn("单据编码");
            DataColumn dcSup = new DataColumn("供应商");
            DataColumn dcWare = new DataColumn("收料仓库");
            DataColumn dcPos = new DataColumn("仓位");
            DataColumn dcMNum = new DataColumn("物料代码");
            DataColumn dcMName = new DataColumn("物料名称");
            DataColumn dcSep = new DataColumn("规格型号");
            DataColumn dcMete = new DataColumn("单位");
            DataColumn dcQuantity = new DataColumn("实收数量");
            DataColumn dcSubMete = new DataColumn("辅助单位");
            DataColumn dcConv = new DataColumn("转换率");
            DataColumn dcSubQua = new DataColumn("辅助数量");
            DataColumn dcPrice = new DataColumn("单价");
            DataColumn dcAmount = new DataColumn("金额");
            DataColumn dcUser = new DataColumn("制单人");
            
            DataColumn[] list_dc = { dcDate,dcNum,  dcSup,dcWare,dcPos, dcMNum, dcMName, dcSep, dcMete,dcQuantity,
                dcSubMete,dcConv,dcSubQua, dcPrice,dcAmount, dcUser };
            dt.Columns.AddRange(list_dc);

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = DateTime.Parse(dtData.Rows[i]["date"].ToString()).ToString("yyyy/M/d");
                dr[1] = dtData.Rows[i]["num"].ToString();
                dr[2] = dtData.Rows[i]["supplierName"].ToString();
                dr[3] = dtData.Rows[i]["warehouseName"].ToString();
                dr[4] = dtData.Rows[i]["positionName"].ToString();
                dr[5] = dtData.Rows[i]["MNum"].ToString();
                dr[6] = dtData.Rows[i]["MName"].ToString();
                dr[7] = dtData.Rows[i]["specifications"].ToString();
                dr[8] = dtData.Rows[i]["meterName"].ToString();
                dr[9] = dtData.Rows[i]["quantity"].ToString();
                dr[10] = dtData.Rows[i]["subMeterName"].ToString();
                dr[11] = dtData.Rows[i]["conversion"].ToString();
                dr[12] = dtData.Rows[i]["subquantity"].ToString();
                dr[13] = dtData.Rows[i]["price"].ToString();
                dr[14] = Convert.ToDouble(dtData.Rows[i]["quantity"]) * Convert.ToDouble(dtData.Rows[i]["price"]);
                dr[15] = dtData.Rows[i]["fullName"].ToString();

                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;

            pagingCom1.RecordCount = ctrl.getCount(this.textBox1.Text, this.dateTimePicker1.Checked,this.dateTimePicker1.Value.Date, textBox2.Text, textBox3.Text);
            pagingCom1.reSet();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                GoDownEntryEdit view = new GoDownEntryEdit(dataGridView1.SelectedRows[0].Cells["单据编码"].Value.ToString());
                view.Show();
            }
            else
            {
                MessageBox.Show("请选择一行！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadDate(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Checked = false;
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierSelect ss = new SupplierSelect();
            ss.Show();
        }
    }
}
