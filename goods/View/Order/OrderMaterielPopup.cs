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
    public partial class OrderMaterielPopup : Form
    {
        materielCtrl mctrl = new materielCtrl();

        public OrderMaterielPopup()
        {
            InitializeComponent();
            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            newColumn.HeaderText = "选择";
            dataGridView1.Columns.Add(newColumn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pagingCom1.PageIndex = 1;
            pagingCom1.PageSize = 10;
            var dtData = mctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text);
            var dt = new DataTable();
            DataColumn dcId = new DataColumn("ID");
            DataColumn dcNO = new DataColumn("编号");
            DataColumn dcUName = new DataColumn("名称");
            DataColumn[] list_dc = { dcId, dcNO, dcUName};
            dt.Columns.AddRange(list_dc);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["id"].ToString();
                dr[1] = dtData.Rows[i]["num"].ToString();
                dr[2] = dtData.Rows[i]["name"].ToString();
                dt.Rows.Add(dr);
            }


            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(e.RowIndex+"");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.RowIndex + "");
            this.dataGridView1[0, e.RowIndex].Value = !Convert.ToBoolean(this.dataGridView1[0, e.RowIndex].Value) ;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
