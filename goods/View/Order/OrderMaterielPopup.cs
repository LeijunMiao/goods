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
    public partial class OrderMaterielPopup : Form
    {
        materielCtrl mctrl = new materielCtrl();
        CreateOrderView parentForm;
        public OrderMaterielPopup(CreateOrderView form)
        {
            parentForm = form;
            InitializeComponent();
            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            newColumn.HeaderText = "选择";
            dataGridView1.Columns.Add(newColumn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pagingCom1.PageIndex = 1;
            pagingCom1.PageSize = 10;
            var dtData = mctrl.getFilterListLimit(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text, parentForm.allids);
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
            //MessageBox.Show(e.RowIndex+"");
            this.Hide();
            List<int> ids = new List<int>{ Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value) };
            parentForm.renderMateriel(ids);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.RowIndex + "");
            this.dataGridView1[0, e.RowIndex].Value = !Convert.ToBoolean(this.dataGridView1[0, e.RowIndex].Value) ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> ids = new List<int>(); 
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    ids.Add(Convert.ToInt32(dataGridView1.Rows[i].Cells["id"].Value));
                }
            }
            if (ids.Count == 0)
            {
                MessageBox.Show("请至少选择一条数据！", "提示");
                return;
            }
            else
            {
                this.Hide();
                parentForm.renderMateriel(ids);
            }
        }
    }
}
