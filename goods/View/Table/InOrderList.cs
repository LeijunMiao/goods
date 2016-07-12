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
namespace goods
{
    public partial class InOrderList : Form
    {
        listCtrl ctrl = new listCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        public InOrderList()
        {
            InitializeComponent();
            initPage();
            loadData(1);
        }
        private void initPage()
        {
            this.dateTimePicker1.Checked = false;
            this.textBox2.KeyDown += button1_KeyDown;
            this.textBox3.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.dataGridView1.ReadOnly = true;
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
            dtData = ctrl.getInOrders(pagingCom1.PageIndex, pagingCom1.PageSize,
                this.dateTimePicker1.Checked, this.dateTimePicker1.Value.Date, textBox2.Text, textBox3.Text);
            dt = new DataTable();
            DataColumn dcDate = new DataColumn("日期");
            DataColumn dcNum = new DataColumn("单据编码");
            DataColumn dcWare = new DataColumn("发货仓库");
            DataColumn dcPos = new DataColumn("仓位");
            DataColumn dcMNum = new DataColumn("物料代码");
            DataColumn dcMName = new DataColumn("物料名称");
            DataColumn dcSep = new DataColumn("规格型号");
            DataColumn dcMete = new DataColumn("单位");
            DataColumn dcQuantity = new DataColumn("领料数量");
            DataColumn dcSubMete = new DataColumn("辅助单位");
            DataColumn dcConv = new DataColumn("转换率");
            DataColumn dcSubQua = new DataColumn("辅助数量");
            DataColumn dcUser = new DataColumn("制单人");

            DataColumn[] list_dc = { dcDate,dcNum,dcWare,dcPos, dcMNum, dcMName, dcSep, dcMete,dcQuantity,
                dcSubMete,dcConv,dcSubQua, dcUser };
            dt.Columns.AddRange(list_dc);

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = DateTime.Parse(dtData.Rows[i]["date"].ToString()).ToString("yyyy/M/d");
                dr[1] = dtData.Rows[i]["num"].ToString();
                dr[2] = dtData.Rows[i]["warehouseName"].ToString();
                dr[3] = dtData.Rows[i]["positionName"].ToString();
                dr[4] = dtData.Rows[i]["MNum"].ToString();
                dr[5] = dtData.Rows[i]["MName"].ToString();
                dr[6] = dtData.Rows[i]["specifications"].ToString();
                dr[7] = dtData.Rows[i]["meterName"].ToString();
                dr[8] = dtData.Rows[i]["quantity"].ToString();
                dr[9] = dtData.Rows[i]["subMeterName"].ToString();
                dr[10] = dtData.Rows[i]["conversion"].ToString();
                dr[11] = dtData.Rows[i]["subquantity"].ToString();
                dr[12] = dtData.Rows[i]["fullName"].ToString();

                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;

            pagingCom1.RecordCount = ctrl.getInOrderCount(this.dateTimePicker1.Checked, this.dateTimePicker1.Value.Date, textBox2.Text, textBox3.Text);
            pagingCom1.reSet();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                InOrderView view = new InOrderView(dataGridView1.SelectedRows[0].Cells["单据编码"].Value.ToString());
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
    }
}
