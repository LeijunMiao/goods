using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Observer;
using goods.Model;
using goods.Controller;
namespace goods
{
    public partial class OrderTrace : Form
    {
        orderCtrl ctrl = new orderCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        int supplier = -1;
        public OrderTrace()
        {
            InitializeComponent();
            MidModule.EventSend += new MsgDlg(MidModule_EventSend);
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }

        private void MidModule_EventSend(object sender, object msg)
        {
            if (sender != null)
            {
                SupplierModel sm = (SupplierModel)msg;
                this.textBox1.Text = sm.Name;
                this.supplier = sm.Id;
            }
        }


        private void OrderTrace_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierSelect ss = new SupplierSelect();
            ss.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getSupplierOrder(pagingCom1.PageIndex, pagingCom1.PageSize, 
                dateTimePicker1.Checked, dateTimePicker1.Value.Date, dateTimePicker2.Checked, dateTimePicker2.Value.Date, this.textBox1.Text);
            dt = new DataTable();
            DataColumn dcNum = new DataColumn("供应商代码");
            DataColumn dcName = new DataColumn("供应商名称");
            DataColumn dcAll = new DataColumn("到期订单数量");
            DataColumn dcRight = new DataColumn("延期交货数量");
            DataColumn dcOver = new DataColumn("准时交货数量");
            DataColumn dcScale = new DataColumn("准时交货率（%）");
            DataColumn[] list_dc = { dcNum, dcName, dcAll, dcRight, dcOver, dcScale };
            dt.Columns.AddRange(list_dc);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["num"].ToString();
                dr[1] = dtData.Rows[i]["name"].ToString();
                if (dtData.Rows[i]["rightquantity"] == DBNull.Value)
                {
                    dtData.Rows[i]["rightquantity"] = 0;
                }
                if (dtData.Rows[i]["allquantity"] == DBNull.Value)
                {
                    dtData.Rows[i]["allquantity"] = 0;
                    dr[5] = 0;
                }
                else
                {
                    dr[5] = Math.Round(Convert.ToDouble(dtData.Rows[i]["rightquantity"]) / Convert.ToDouble(dtData.Rows[i]["allquantity"]) * 100, 2); 
                }
                dr[2] = dtData.Rows[i]["allquantity"].ToString();
                dr[3] = Convert.ToDouble(dtData.Rows[i]["allquantity"]) - Convert.ToDouble(dtData.Rows[i]["rightquantity"]);
                dr[4] = dtData.Rows[i]["rightquantity"].ToString();
                
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;

            pagingCom1.RecordCount = ctrl.getSupplierOrderCount(this.textBox1.Text);
            pagingCom1.reSet();
        }
    }
}
