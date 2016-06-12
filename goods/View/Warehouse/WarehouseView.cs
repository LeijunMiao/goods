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
    public partial class WarehouseView : Form
    {
        warehouseCtrl ctrl = new warehouseCtrl();
        positionCtrl pctrl = new positionCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;
        int wId = -1;
        string num;
        int loaded = -1; //判断窗体是否加载完成
        //总记录数
        public int RecordCount = 0;
        public WarehouseView()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            //newColumn.HeaderText = "选择";
            //dataGridView1.Columns.Add(newColumn);

        }
        private void Form1_Shown(Object sender, EventArgs e)
        {
            loaded = 1;
            loadPosition(sender,e);

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        public void loadData()
        {
            BindDataWithPage(1);
        }
        public void loadPosition(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count >0 && loaded == 1)
            {
                wId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                num = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                dataGridView2.DataSource =  pctrl.getByWId(wId);
            }
            
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
            //获取并设置总记录数
            pagingCom1.RecordCount = ctrl.getCount(textBox1.Text);
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
            pagingCom1.reSet();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            WarehousePopup popup = new WarehousePopup(this,"ck", wId);
            popup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        private void button1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) { BindDataWithPage(1); }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(num != null)
            {
                WarehouseQRCode popup = new WarehouseQRCode("ck", num);
                popup.Show();
            }
            else
            {
                MessageBox.Show("请选择一行。");
            }
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            WarehousePopup popup = new WarehousePopup(this,"cw", wId);
            popup.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0) MessageBox.Show("请选中一行");
            else
            {
                string positionNum = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                WarehouseQRCode popup = new WarehouseQRCode("cw", positionNum);
                popup.Show();
            }
        }
    }
}
