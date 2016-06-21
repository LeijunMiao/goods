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
using System.Data;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace goods
{
    public partial class QRCodeList : Form
    {
        orderCtrl ctrl = new orderCtrl();
        utilCls util = new utilCls();

        private PrintDocument picToPrint = new System.Drawing.Printing.PrintDocument();
        private int count = 0;
        private List<Bitmap> _printBmps = new List<Bitmap>();

        private string pattern = @"(^[1-9]\d{0,2}$)|(^(1000)$)";
        private string param1 = null;
        public QRCodeList(List<string> list_uuids)
        {
            InitializeComponent();
            loadData(list_uuids);
            this.picToPrint.PrintPage += new PrintPageEventHandler(picToPrint_PrintPage);
        }
        private void loadData(List<string> list_uuids)
        {
            DataTable dtData = ctrl.getqrcode(list_uuids);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                imageList1.Images.Add(util.GenByZXingNet(dtData.Rows[i]["num"].ToString()));
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
                short num = Convert.ToInt16(textBox1.Text);
                picToPrint.PrinterSettings.Copies = num;

                PrintPreviewDialog PrintPriview = new PrintPreviewDialog();
                PrintPriview.Document = picToPrint;
                PrintPriview.WindowState = FormWindowState.Maximized;
                PrintPriview.ShowDialog();
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
            e.Graphics.DrawImage(imageList1.Images[count], new Rectangle((int)Math.Ceiling(Chinese_OneWidth), 0, imageList1.Images[count].Width, imageList1.Images[count].Height));

            /********************start--判断是否需要再打印下一页--start*************************/
            count++;
            if (imageList1.Images.Count > count)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

        }
        private void getPic()
        {
            if (_printBmps.Count < 2)
            {
                //Bitmap t = GenByZXingNet("123");
                //Bitmap t1 = GenByZXingNet("1234");
               // _printBmps.Add(t);
                //_printBmps.Add(t1);
            }
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
    }
}
