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
namespace goods
{
    public partial class SupplierSelect : Form
    {
        supplierCtrl sctrl = new supplierCtrl();
        public SupplierSelect()
        {
            InitializeComponent();
            searchSupplier();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SupplierModel sm = new SupplierModel(Convert.ToInt32(this.dataGridView1[0, e.RowIndex].Value) , this.dataGridView1[2, e.RowIndex].Value.ToString());
            MidModule.SendMessage(this, sm);//发送参数值
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchSupplier();
        }
        private void searchSupplier()
        {
            pagingCom1.PageIndex = 1;
            pagingCom1.PageSize = 10;
            var dtData = sctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, "", textBox1.Text);

            DataGridViewColumn colId = new DataGridViewLinkColumn();
            colId.DataPropertyName = "id";//字段
            colId.Visible = false;
            dataGridView1.Columns.Add(colId);

            DataGridViewColumn colNum = new DataGridViewLinkColumn();
            colNum.DataPropertyName = "num";//字段
            colNum.Visible = false;
            dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewLinkColumn();
            colName.DataPropertyName = "name";//字段
            colName.HeaderText = "名称";
            dataGridView1.Columns.Add(colName);
            //var dt = new DataTable();
            //DataColumn dcId = new DataColumn("ID");
            //DataColumn dcNO = new DataColumn("编号");
            //DataColumn dcUName = new DataColumn("名称");
            //DataColumn[] list_dc = { dcId, dcNO, dcUName };
            //dt.Columns.AddRange(list_dc);
            /*
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["id"].ToString();
                //dr[1] = dtData.Rows[i]["num"].ToString();
                dr[2] = dtData.Rows[i]["name"].ToString();
                dt.Rows.Add(dr);
            }*/


            dataGridView1.DataSource = dtData;
            dataGridView1.ClearSelection();
        }
    }
}
