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
    public partial class GoDownEntryList : Form
    {
        listCtrl ctrl = new listCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        public GoDownEntryList()
        {
            InitializeComponent();
            loadDate(1);
        }
        public void loadDate(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize,this.textBox1.Text);
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
                dr[0] = dtData.Rows[i]["date"].ToString();
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
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
