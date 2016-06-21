using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;
namespace goods
{
    public partial class TestPrint : Form
    {
        private PrintDocument picToPrint = new System.Drawing.Printing.PrintDocument();
        private int count = 0;
        private List<Bitmap> _printBmps = new List<Bitmap>();
        public TestPrint()
        {
            InitializeComponent();
            this.picToPrint.PrintPage += new PrintPageEventHandler(picToPrint_PrintPage);
        }
        private void picToPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 10, FontStyle.Regular);
            decimal Chinese_OneWidth = Convert.ToDecimal(e.Graphics.MeasureString("测", f).Width);
            int pageWidth = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Width, 0)) - 1;//打印机可打印区域的宽度
            int onePageHeight = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Height, 0)) - 1;//打印机可打印区域的高度
            MessageBox.Show(_printBmps.Count+"|" +count);
            getPic();
            e.Graphics.DrawImage(_printBmps[count], new Rectangle((int)Math.Ceiling(Chinese_OneWidth), 0, _printBmps[count].Width, _printBmps[count].Height));
            
            /********************start--判断是否需要再打印下一页--start*************************/
            count++;
            if (_printBmps.Count > count)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
            /**********************end--判断是否需要再打印下一页--end*************************/
        }
        private void getPic()
        {
            
            if (_printBmps.Count < 2)
            {
                Bitmap t = GenByZXingNet("123");
                Bitmap t1 = GenByZXingNet("1234");
                _printBmps.Add(t);
                _printBmps.Add(t1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testModel tm = new testModel("cc","dd");
            
            PrintDataModel<object> m = new PrintDataModel<object>();
            m.pageTitle = "测试";
            m.TitleData = new List<string> { "aa" ,"bb"};
            m.TableData = new List<object> { tm };
            m.ColumnNames = new List<string> { "ff","gg" };
            m.CanResetLine = new List<bool> { true,true};
            m.EndData = new List<string> { "jj","tt" };
            List<PrintDataModel<object>> list = new List<PrintDataModel<object>> { m};

            try
            {
                CommonPrintTools<object> cp = new CommonPrintTools<object>(list);
                cp.PrintPriview();


            }
            catch (Exception)
            {

                throw;
            }
        }
        public class testModel
        {
            public string id { get; set; }
            public string name { get; set; }
            public testModel(string id,string name)
            {
                this.id = id;
                this.name = name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPriview();


            }
            catch (Exception)
            {

                throw;
            }
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
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="msg">二维码信息</param>
        /// <returns>图片</returns>
        private Bitmap GenByZXingNet(string msg)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");//编码问题
            writer.Options.Hints.Add(
               EncodeHintType.ERROR_CORRECTION,
               ZXing.QrCode.Internal.ErrorCorrectionLevel.H

            );
            const int codeSizeInPixels = 80;   //设置图片长宽
            writer.Options.Height = writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 0;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(msg);
            Bitmap img = writer.Write(bm);
            return img;
        }
    }
}
