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
    public partial class MaterielView : Form
    {
        materielCtrl ctrl = new materielCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        public MaterielView()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        public void loadData()
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text);
            dt = new DataTable();
            DataColumn dcId = new DataColumn("ID");
            DataColumn dcNO = new DataColumn("编号");
            DataColumn dcUName = new DataColumn("名称");
            DataColumn dcSep = new DataColumn("规格型号");
            DataColumn dcMete = new DataColumn("计量单位");
            DataColumn dcSme = new DataColumn("辅助计量单位");
            DataColumn dcConv = new DataColumn("转换率");
            DataColumn dcType = new DataColumn("属性");
            DataColumn dcTax = new DataColumn("税率");
            DataColumn dcBatch = new DataColumn("业务批次管理");
            DataColumn[] list_dc = { dcId, dcNO, dcUName, dcSep, dcMete, dcSme, dcConv, dcType, dcTax, dcBatch };
            dt.Columns.AddRange(list_dc);

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["id"].ToString();
                dr[1] = dtData.Rows[i]["num"].ToString();
                dr[2] = dtData.Rows[i]["name"].ToString();
                dr[3] = dtData.Rows[i]["specifications"].ToString();
                dr[4] = dtData.Rows[i]["metering"].ToString();
                dr[5] = dtData.Rows[i]["subMetering"].ToString();
                dr[6] = dtData.Rows[i]["conversion"].ToString();
                dr[7] = dtData.Rows[i]["type"].ToString();
                dr[8] = dtData.Rows[i]["tax"].ToString();
                dr[9] = dtData.Rows[i]["isBatch"].ToString();
                dt.Rows.Add(dr);
            }


            dataGridView1.DataSource = dt;
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { BindDataWithPage(1); }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MaterielPopup popup = new MaterielPopup(this);
            popup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
    }
}
