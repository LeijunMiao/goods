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
    public partial class SupplierView : Form
    {
        supplierCtrl ctrl = new supplierCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        //总记录数
        public int RecordCount = 0;
        public SupplierView()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        public void loadData()
        {
            BindDataWithPage(1);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }

        /// <summary>
        /// 绑定第Index页的数据
        /// </summary>
        /// <param name="Index"></param>
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text, textBox2.Text);
            //获取并设置总记录数
            pagingCom1.RecordCount = ctrl.getCount(textBox1.Text, textBox2.Text);
            dt = new DataTable();
            DataColumn dcNO = new DataColumn("编号");
            DataColumn dcUName = new DataColumn("名称");
            dt.Columns.Add(dcNO);
            dt.Columns.Add(dcUName);

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["num"].ToString();
                dr[1] = dtData.Rows[i]["Name"].ToString();
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;
            pagingCom1.reSet();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SupplierPopup popup = new SupplierPopup(this);
            popup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
    }
}
