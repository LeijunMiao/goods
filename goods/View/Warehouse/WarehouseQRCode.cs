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
using ZXing;
namespace goods
{
    public partial class WarehouseQRCode : Form
    {
        public WarehouseQRCode(string type,string code)
        {
            InitializeComponent();
            this.label1.Text = code;
            pictureBox1.Image = GenByZXingNet(code);
            //this.printDocument1.OriginAtMargins = true;//启用页边距
            //this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
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
            const int codeSizeInPixels = 250;   //设置图片长宽
            writer.Options.Height = writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 0;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(msg);
            Bitmap img = writer.Write(bm);
            return img;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.ShowDialog();
        }
        //打印内容的设置
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ////打印内容 为 整个Form
            //Image myFormImage;
            //myFormImage = new Bitmap(this.Width, this.Height);
            //Graphics g = Graphics.FromImage(myFormImage);
            //g.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            //e.Graphics.DrawImage(myFormImage, 0, 0);

            ////打印内容 为 局部的 this.groupBox1
            //Bitmap _NewBitmap = new Bitmap(groupBox1.Width, groupBox1.Height);
            //groupBox1.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
            //e.Graphics.DrawImage(_NewBitmap, 0, 0, _NewBitmap.Width, _NewBitmap.Height); 

            //打印内容 为 自定义文本内容 
            //e.Graphics.DrawImage(pictureBox1.Image, 0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height);
            /*
            Font font = new Font("宋体", 12);
            Brush bru = Brushes.Blue;
            for (int i = 1; i <= 5; i++)
            {
                e.Graphics.DrawString("hello world ", font, bru, i * 20, i * 20);
            }
            */
            //MessageBox.Show("Test");
            try
            {
                if (pictureBox1.Image != null)
                {
                    e.Graphics.DrawImage(pictureBox1.Image, 20, 20);//e.Graphics.VisibleClipBounds);
                    e.HasMorePages = false;
                }
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
                ts.Items.Add("打印设置");
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

    }
}
