using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goods.Controller;
using goods.Model;
using Observer;

namespace goods
{
    public partial class CreateOrderView : Form
    {
        materielCtrl mctrl = new materielCtrl();
        orderCtrl octrl = new orderCtrl();
        DataTable dt = new DataTable();
        int supplier = -1;
        int user = -1;
        
        public List<int> allids = new List<int>();
        public CreateOrderView()
        {
            InitializeComponent();
            MidModule.EventSend += new MsgDlg(MidModule_EventSend);

            initData();

            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
        }
        private void MidModule_EventSend(object sender, object msg)
        {
            if (sender != null)
            {
                SupplierModel sm = (SupplierModel)msg;
                this.label8.Text = sm.Name;
                this.supplier = sm.Id;
            }
        }
        private void initData()
        {
            this.label7.Text = PropertyClass.SendNameValue;
            this.user = PropertyClass.UserId;

            DataColumn dcId = new DataColumn("ID");
            DataColumn dcNO = new DataColumn("编号");
            DataColumn dcUName = new DataColumn("名称");
            DataColumn dcSep = new DataColumn("规格型号");
            DataColumn dcMete = new DataColumn("计量单位");
            DataColumn dcSme = new DataColumn("辅助计量单位");
            DataColumn dcConv = new DataColumn("转换率");
            DataColumn dcType = new DataColumn("属性");
            DataColumn dcTax = new DataColumn("税率");
            DataColumn[] list_dc = { dcId, dcNO, dcUName, dcSep, dcMete, dcSme, dcConv, dcType, dcTax };
            dt.Columns.AddRange(list_dc);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OrderMaterielPopup popup = new OrderMaterielPopup(this);
            popup.Show();
        }
        public void renderMateriel(List<int> ids)
        {
            if (ids.Count > 0){
                allids.AddRange(ids);
                var dtData = mctrl.getByids(ids);
                    
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
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierSelect ss = new SupplierSelect();
            ss.Show();
        }
        

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.SelectedRows.Count == 0 )
            {
                MessageBox.Show("请选择要删除行！");
                return;
            }
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = this.dataGridView1.SelectedRows.Count; i > 0; i--)
                {
                    allids.Remove(Convert.ToInt32(dataGridView1.SelectedRows[i - 1].Cells[0].Value));
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell = null;
            if (this.dataGridView1.RowCount == 0)
            {
                MessageBox.Show("请添加物料！");
                return;
            }
            orderParmas obj = new orderParmas(supplier, user, textBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            obj.listM = new List<ListModel>();
            for (int i = this.dataGridView1.RowCount; i > 0; i--)
            {
                ListModel lm = new ListModel();
                lm.conversion = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["conversion"].Value);
                lm.price = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["price"].Value);
                lm.line = i;
                lm.materiel = Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["id"].Value);
                lm.quantity = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["quantity"].Value);
                if (this.dataGridView1.Rows[i - 1].Cells["summary"].Value != null)  lm.summary = this.dataGridView1.Rows[i - 1].Cells["summary"].Value.ToString();
                lm.tax = Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["tax"].Value);
                obj.listM.Add(lm);
            }
            
            var msg = octrl.add(obj);
            if (msg.Code == 0)
            {
                //this.Hide();
                MessageBox.Show(msg.Msg);
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.ShowDialog();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //打印内容 为 局部的 this.panel1
                Bitmap _NewBitmap = new Bitmap(panel1.Width, panel1.Height);
                panel1.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
                e.Graphics.DrawImage(_NewBitmap, 0, 0, _NewBitmap.Width, _NewBitmap.Height);
            }
            catch (Exception exception)
            {
                MessageBox.Show("出错！");
            }
        }
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            if (printPreviewDialog1.Controls.ContainsKey("toolStrip1"))
            {
                ToolStrip ts = printPreviewDialog1.Controls["toolStrip1"] as ToolStrip;
                //ts.Items.Add("打印设置");
                if (ts.Items.ContainsKey("printToolStripButton")) //打印按钮
                {
                    ts.Items["printToolStripButton"].MouseDown += new MouseEventHandler(click);
                }
            }
        }
        void click(object sender, MouseEventArgs e)
        {
            printDocument1.Print();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }
    }
}
