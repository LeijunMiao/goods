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
    public partial class MeteringView : Form
    {
        meteringCtrl ctrl = new meteringCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        public MeteringView()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        public void loadData()
        {
            BindDataWithPage(1);
        }
        private void BindDataWithPage(int Index)
        {
            dtData = ctrl.getFilterList(textBox1.Text);
            dt = new DataTable();
            DataColumn dcId = new DataColumn("ID");
            DataColumn dcNO = new DataColumn("编号");
            DataColumn dcUName = new DataColumn("名称");
            dt.Columns.Add(dcId);
            dt.Columns.Add(dcNO);
            dt.Columns.Add(dcUName);

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MeteringPopup popup = new MeteringPopup(this);
            popup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { BindDataWithPage(1); }
        }
    }
}
