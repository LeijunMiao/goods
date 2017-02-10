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
    public partial class QRCodeList : Form
    {
        orderCtrl ctrl = new orderCtrl();
        utilCls util = new utilCls();

        private PrintDocument picToPrint = new System.Drawing.Printing.PrintDocument();
        private PrintPreviewDialog printPriview = new System.Windows.Forms.PrintPreviewDialog();
        private int count = 0;
        private List<picModel> _printBmps = new List<picModel>();

        private string pattern = @"(^[1-9]\d{0,2}$)|(^(1000)$)";
        private string param1 = null;
        public QRCodeList(List<string> list_uuids)
        {
            InitializeComponent();
            loadData(list_uuids);
            this.picToPrint.PrintPage += new PrintPageEventHandler(picToPrint_PrintPage);
            this.printPriview.Load += new System.EventHandler(this.printPreviewDialog1_Load);
        }
        private void loadData(List<string> list_uuids)
        {
            DataTable dtData = ctrl.getqrcode(list_uuids);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                Bitmap bm = util.GenByZXingNet(dtData.Rows[i]["num"].ToString());
                picModel pm = new picModel();
                pm.printBmp = bm;
                pm.batch = "批次号：" + dtData.Rows[i]["num"].ToString();
                pm.num = "物料编码：" + dtData.Rows[i]["mnum"].ToString();
                pm.name = "物料名称：" + dtData.Rows[i]["name"].ToString();
                if (dtData.Rows[i]["spe"] != DBNull.Value) pm.spe = "规格型号：" + dtData.Rows[i]["spe"].ToString();
                if (dtData.Rows[i]["attribute"] != DBNull.Value) pm.attribute = "物料属性：" + dtData.Rows[i]["attribute"].ToString();
                pm.supplier = "供应商：" + dtData.Rows[i]["supplier"].ToString();
                _printBmps.Add(pm);

                imageList1.Images.Add(bm);
                ListViewItem li = new ListViewItem();
                li.Text = dtData.Rows[i]["num"].ToString();
                li.ImageIndex = i;         //对应指定即可
                listView1.Items.Add(li);
            }

            imageList1.ImageSize = new Size(112, 112);
            this.listView1.LargeImageList = imageList1;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PrintPriview();
        }
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="dt">要打印的DataTable</param>
        /// <param name="Title">打印文件的标题</param>
        public void PrintPriview()
        {
            try
            {
                count = 0;
                short num = Convert.ToInt16(textBox1.Text);
                picToPrint.PrinterSettings.Copies = num;

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
            Brush bru = Brushes.Black;
            Bitmap bpItem = new Bitmap(e.PageBounds.Width, e.PageBounds.Height);
            Graphics gItem = Graphics.FromImage(bpItem);

            int fHeight = Convert.ToInt32(e.Graphics.MeasureString("测", f).Height);
            int fWidth = Convert.ToInt32(e.Graphics.MeasureString("测", f).Width);
            int maxWidth = (int)Math.Ceiling(Convert.ToDecimal(e.Graphics.MeasureString(getmax(_printBmps[count]), f).Width));

            int pic_X = (e.PageBounds.Width - _printBmps[count].printBmp.Width) / 2;
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
                    var attr = p.GetValue(pm,null).ToString();
                    decimal pWidth = Convert.ToDecimal(e.Graphics.MeasureString(attr, f).Width);
                    List<string> list = util.GetMultiLineString(attr, e.PageBounds.Width - text_X - fWidth, gItem, f);
                    int i;
                    for (i = 0; i < list.Count; i++)
                    {
                        e.Graphics.DrawString(list[i], f, bru, text_X, fHeight * (line + i) + fHeight/2);
                    }
                    line += i;
                }
            }

            e.Graphics.DrawImage(_printBmps[count].printBmp, pic_X, fHeight * line + fHeight / 2);//new Rectangle((int)Math.Ceiling(Chinese_OneWidth), 0 _printBmps[count].printBmp.Width, _printBmps[count].printBmp.Height)

            //e.Graphics.DrawImage(imageList1.Images[count], new Rectangle((int)Math.Ceiling(Chinese_OneWidth), 0, imageList1.Images[count].Width, imageList1.Images[count].Height));

            /********************start--判断是否需要再打印下一页--start*************************/
            count++;
            if (imageList1.Images.Count > count)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

        }
        private string getmax(picModel pm)
        {
            string max = "";
            foreach (System.Reflection.PropertyInfo p in pm.GetType().GetProperties())
            {
                if (p.Name != "printBmp")
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == "")
            {
                textBox1.Text = "";
            }
            else
            {
                Match m = Regex.Match(this.textBox1.Text, pattern);   // 匹配正则表达式

                if (!m.Success)   // 输入的不是数字
                {
                    this.textBox1.Text = param1;   // textBox内容不变

                    // 将光标定位到文本框的最后
                    this.textBox1.SelectionStart = this.textBox1.Text.Length;
                }
                else   // 输入的是数字
                {
                    param1 = this.textBox1.Text;   // 将现在textBox的值保存下来
                }
            }
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
