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
            initTable();
            searchSupplier(1);

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                SupplierModel sm = new SupplierModel(Convert.ToInt32(this.dataGridView1[0, e.RowIndex].Value), this.dataGridView1[2, e.RowIndex].Value.ToString());
                MidModule.SendMessage(this, sm);//发送参数值
                this.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchSupplier(1);
        }
        private void initTable()
        {
            dataGridView1.AutoGenerateColumns = false;
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
        }
        private void searchSupplier(int index)
        {
            pagingCom1.PageIndex = index;
            pagingCom1.PageSize = 10;
            SupplierModel s = new SupplierModel();
            s.Name = textBox1.Text;
            s.IsActive = 1;
            var dtData = sctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, s);

            //获取并设置总记录数
            pagingCom1.RecordCount = sctrl.getCount(s);//Convert.ToInt32(dsData.Tables[1].Rows[0][0]);

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
            pagingCom1.reSet();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { searchSupplier(1); }
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            searchSupplier(pagingCom1.PageIndex);
        }
    }
}
