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

using System.Drawing.Printing;
using System.Text.RegularExpressions;
namespace goods
{
    public partial class BatchList : Form
    {
        orderCtrl ctrl = new orderCtrl();
        utilCls util = new utilCls();

        private PrintDocument picToPrint = new System.Drawing.Printing.PrintDocument();
        private PrintPreviewDialog printPriview = new System.Windows.Forms.PrintPreviewDialog();
        
        private int count = 0;
        private List<picModel> _printBmps = new List<picModel>();

        public BatchList(int omid)
        {
            InitializeComponent();
            initTable();
            loadData(omid);
            this.picToPrint.PrintPage += new PrintPageEventHandler(picToPrint_PrintPage);
            this.printPriview.Load += new System.EventHandler(this.printPreviewDialog1_Load);
        }
        private void initTable()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.CellFormatting += dataGridview1_CellFormatting;

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "num";
            colNum.Name = "num";
            colNum.HeaderText = "批次编号";
            colNum.ReadOnly = true;
            this.dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colMNum = new DataGridViewTextBoxColumn();
            colMNum.DataPropertyName = "mnum";
            colMNum.Name = "mnum";
            colMNum.Visible = false;
            this.dataGridView1.Columns.Add(colMNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            colName.Name = "name";
            colName.HeaderText = "名称";
            this.dataGridView1.Columns.Add(colName);

            DataGridViewColumn colSpe = new DataGridViewTextBoxColumn();
            colSpe.DataPropertyName = "spe";
            colSpe.Name = "spe";
            colSpe.Visible = false;
            this.dataGridView1.Columns.Add(colSpe);

            DataGridViewColumn colAttrbute = new DataGridViewTextBoxColumn();
            colAttrbute.DataPropertyName = "attribute";
            colAttrbute.Name = "attribute";
            colAttrbute.HeaderText = "属性";
            this.dataGridView1.Columns.Add(colAttrbute);

            DataGridViewColumn colSupplier = new DataGridViewTextBoxColumn();
            colSupplier.DataPropertyName = "supplier";
            colSupplier.Name = "supplier";
            colSupplier.Visible = false;
            this.dataGridView1.Columns.Add(colSupplier);

            DataGridViewColumn colDate = new DataGridViewTextBoxColumn();
            colDate.DataPropertyName = "date";
            colDate.Name = "date";
            colDate.HeaderText = "日期";
            colDate.ReadOnly = true;
            this.dataGridView1.Columns.Add(colDate);

            DataGridViewImageColumn column = new DataGridViewImageColumn();
            column.Name = "Image";
            column.HeaderText = "批次二维码";
            //column.Image = System.Drawing.Image.FromFile("路径");
            dataGridView1.Columns.Add(column);
        }
        private void loadData(int omid)
        {
            DataTable dt = ctrl.getbyOMId(omid);
            if (dt.Rows.Count == 0)
            {
                this.Hide();
                return;
            }

            dataGridView1.DataSource = dt;

        }

        private void dataGridview1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name.Equals("Image"))
            {
                e.Value = util.GenByZXingNet(this.dataGridView1.Rows[e.RowIndex].Cells["num"].Value.ToString());
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                PageSetupDialog PageSetup = new PageSetupDialog();
                PageSetup.Document = picToPrint;
                PageSetup.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印设置错误，请检查打印设置！消息：" + ex.Message);

            }
        }
        public void PrintPriview()
        {
            try
            {
                count = 0;
                //short num = Convert.ToInt16(textBox1.Text);
                //picToPrint.PrinterSettings.Copies = num;

                //PrintPreviewDialog PrintPriview = new PrintPreviewDialog();
                printPriview.Document = picToPrint;
                printPriview.WindowState = FormWindowState.Maximized;
                printPriview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印错误，请检查打印设置！消息：" + ex.Message);

            }
        }
        private void picToPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12, FontStyle.Regular);
            decimal Chinese_OneWidth = Convert.ToDecimal(e.Graphics.MeasureString("测", f).Width);
            int pageWidth = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Width, 0)) - 1;//打印机可打印区域的宽度
            int onePageHeight = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Height, 0)) - 1;//打印机可打印区域的高度
            //getPic();

            Brush bru = Brushes.Black;
            Bitmap bpItem = new Bitmap(e.PageBounds.Width, e.PageBounds.Height);
            Graphics gItem = Graphics.FromImage(bpItem);
            
            int fHeight = Convert.ToInt32(e.Graphics.MeasureString("测", f).Height);
            int fWidth = Convert.ToInt32(e.Graphics.MeasureString("测", f).Width);
            int maxWidth = (int)Math.Ceiling(Convert.ToDecimal(e.Graphics.MeasureString(getmax(_printBmps[count]), f).Width));

            int pic_X = (e.PageBounds.Width - _printBmps[count].printBmp.Width ) / 2;
            if (pic_X < 0) pic_X = 0;
            int pic_Y = (e.PageBounds.Height - _printBmps[count].printBmp.Height) / 2;
            if (pic_Y < 0) pic_Y = 0;
            int text_X = (e.PageBounds.Width - maxWidth) / 2;
            if (text_X < 0) text_X = 0;

            var pm = _printBmps[count];
            int line = 0;
            foreach (System.Reflection.PropertyInfo p in pm.GetType().GetProperties())
            {
                if (p.Name != "printBmp")
                {
                    var attr = p.GetValue(pm, null).ToString();
                    decimal pWidth = Convert.ToDecimal(e.Graphics.MeasureString(attr, f).Width);
                    List<string> list = util.GetMultiLineString(attr, e.PageBounds.Width - text_X - fWidth, gItem, f);
                    int i;
                    for (i = 0; i < list.Count; i++)
                    {
                        e.Graphics.DrawString(list[i], f, bru, text_X, fHeight * (line + i) + fHeight / 2);
                    }
                    line += i;
                }
            }

            e.Graphics.DrawImage(_printBmps[count].printBmp, pic_X, fHeight * line + fHeight / 2);//new Rectangle((int)Math.Ceiling(Chinese_OneWidth), 0 _printBmps[count].printBmp.Width, _printBmps[count].printBmp.Height)

            //decimal ckWidth = Convert.ToDecimal(e.Graphics.MeasureString(ck, f).Width);
            //decimal cwWidth = Convert.ToDecimal(e.Graphics.MeasureString(cw, f).Width);

            //e.Graphics.DrawImage(pictureBox1.Image, pic_X, pic_Y);//e.Graphics.VisibleClipBounds);, pictureBox1.Image.Width, pictureBox1.Image.Height
            //List<string> list_ck = util.GetMultiLineString(ck, e.PageBounds.Width - pic_X - pictureBox1.Image.Width - fWidth, gItem, f);
            //List<string> list_cw = util.GetMultiLineString(cw, e.PageBounds.Width - pic_X - pictureBox1.Image.Width - fWidth, gItem, f);
            //int minHeight = (pictureBox1.Image.Height - (list_ck.Count + list_cw.Count) * fHeight) / 2;
            //if (minHeight < 0) minHeight = 0;
            //int i;
            //for (i = 0; i < list_ck.Count; i++)
            //{
            //    e.Graphics.DrawString(list_ck[i], f, bru, pic_X + pictureBox1.Image.Width, pic_Y + minHeight + fHeight * i);
            //}

            //if (this.label4.Text != "")
            //{
            //    for (int j = 0; j < list_cw.Count; j++)
            //    {
            //        e.Graphics.DrawString(list_cw[j], f, bru, pic_X + pictureBox1.Image.Width, pic_Y + fHeight * (i + j) + minHeight);
            //    }
            //}
            //e.Graphics.DrawString("名称：" + _printBmps[count].batch, f, bru, 100, 20);
            /********************start--判断是否需要再打印下一页--start*************************/
            count++;
            if (_printBmps.Count > count)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

        }

        private string getmax(picModel pm)
        {
            string max = "";
            foreach(System.Reflection.PropertyInfo p in pm.GetType().GetProperties())
            {
                if(p.Name != "printBmp")
                {
                    if (p.GetValue(pm,null).ToString().Length > max.Length) max = p.GetValue(pm,null).ToString();
                }
            }
            return max;
        }
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            if (printPriview.Controls.ContainsKey("toolStrip1"))
            {
                ToolStrip ts = printPriview.Controls["toolStrip1"] as ToolStrip;
                //ts.Items.Add("打印设置");
                if (ts.Items.ContainsKey("printToolStripButton")) //打印按钮
                {
                    ts.Items["printToolStripButton"].MouseDown += new MouseEventHandler(click); 
                }
            }
        }

        void click(object sender, MouseEventArgs e)
        {
            count = 0;
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择行！");
                return;
            }
            else
            {
                _printBmps = new List<picModel>();
                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    picModel pm = new picModel();
                    pm.printBmp = util.GenByZXingNet(this.dataGridView1.SelectedRows[i].Cells["num"].Value.ToString());
                    pm.batch = "批次号：" + this.dataGridView1.SelectedRows[i].Cells["num"].Value.ToString();
                    pm.num = "物料编码：" + this.dataGridView1.SelectedRows[i].Cells["mnum"].Value.ToString();
                    pm.name = "物料名称：" + this.dataGridView1.SelectedRows[i].Cells["name"].Value.ToString();
                    if(this.dataGridView1.SelectedRows[i].Cells["spe"].Value != DBNull.Value) pm.spe = "规格型号：" + this.dataGridView1.SelectedRows[i].Cells["spe"].Value.ToString();
                    pm.attribute = "物料属性：" + this.dataGridView1.SelectedRows[i].Cells["attribute"].Value.ToString();
                    pm.supplier = "供应商：" + this.dataGridView1.SelectedRows[i].Cells["supplier"].Value.ToString();
                    _printBmps.Add(pm);
                    //(Bitmap)this.dataGridView1.SelectedRows[i].Cells["image"].Value
                }
                PrintPriview();
            }
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            dlg.Document = picToPrint;
            dlg.ShowDialog();
        }

        private class picModel
        {
            public Bitmap printBmp { get; set; }
            public string num { get; set; }
            public string name { get; set; }
            public string spe { get; set; }
            public string supplier { get; set; }
            public string attribute { get; set; }
            public string batch { get; set; }
        }
    }
}
