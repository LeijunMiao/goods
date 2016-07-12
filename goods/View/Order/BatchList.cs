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
        private List<Bitmap> _printBmps = new List<Bitmap>();

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
            Font f = new Font("宋体", 10, FontStyle.Regular);
            decimal Chinese_OneWidth = Convert.ToDecimal(e.Graphics.MeasureString("测", f).Width);
            int pageWidth = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Width, 0)) - 1;//打印机可打印区域的宽度
            int onePageHeight = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Height, 0)) - 1;//打印机可打印区域的高度
            //getPic();
            e.Graphics.DrawImage(_printBmps[count], new Rectangle((int)Math.Ceiling(Chinese_OneWidth), 0, _printBmps[count].Width, _printBmps[count].Height));

            /********************start--判断是否需要再打印下一页--start*************************/
            count++;
            if (_printBmps.Count > count)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

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
                _printBmps = new List<Bitmap>();
                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    _printBmps.Add(util.GenByZXingNet(this.dataGridView1.SelectedRows[i].Cells["num"].Value.ToString()));
                    //(Bitmap)this.dataGridView1.SelectedRows[i].Cells["image"].Value
                }
                PrintPriview();
            }
            
        }
    }
}
