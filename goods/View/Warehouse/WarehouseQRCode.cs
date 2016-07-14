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
        utilCls util = new utilCls();
        public WarehouseQRCode(string type,string warCode,string posCode, string warName, string posName)
        {
            InitializeComponent();
            this.label1.Text = warName;
            if(posCode == "")
            {
                pictureBox1.Image = util.GenByZXingNet(warCode);
                this.label3.Visible = false;
                this.label4.Visible = false;
            }
            else
            {
                pictureBox1.Image = util.GenByZXingNet(posCode);
                this.label4.Text = posName;
            }
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="msg">二维码信息</param>
        /// <returns>图片</returns>
        //private Bitmap GenByZXingNet(string msg)
        //{
        //    BarcodeWriter writer = new BarcodeWriter();
        //    writer.Format = BarcodeFormat.QR_CODE;
        //    writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");//编码问题
        //    writer.Options.Hints.Add(
        //       EncodeHintType.ERROR_CORRECTION,
        //       ZXing.QrCode.Internal.ErrorCorrectionLevel.H

        //    );
        //    const int codeSizeInPixels = 80;   //设置图片长宽
        //    writer.Options.Height = writer.Options.Width = codeSizeInPixels;
        //    writer.Options.Margin = 0;//设置边框
        //    ZXing.Common.BitMatrix bm = writer.Encode(msg);
        //    Bitmap img = writer.Write(bm);
        //    return img;
        //}

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
                    Font f = new Font("宋体", 12);
                    Brush bru = Brushes.Black;
                    Bitmap bpItem = new Bitmap(e.PageBounds.Width, e.PageBounds.Height);
                    Graphics gItem = Graphics.FromImage(bpItem);

                    string ck = this.label2.Text + this.label1.Text;
                    string cw = this.label3.Text + this.label4.Text;
                    
                    decimal ckWidth = Convert.ToDecimal(e.Graphics.MeasureString(ck, f).Width);
                    decimal cwWidth = Convert.ToDecimal(e.Graphics.MeasureString(cw, f).Width);
                    int fHeight = Convert.ToInt32(e.Graphics.MeasureString("测", f).Height);
                    int fWidth = Convert.ToInt32(e.Graphics.MeasureString("测", f).Width);
                    int maxWidth = (int)Math.Ceiling((ckWidth > cwWidth ? ckWidth : cwWidth));
                    


                    int pic_X = (e.PageBounds.Width - pictureBox1.Image.Width - maxWidth) / 2;
                    if (pic_X < 0) pic_X = 0;
                    int pic_Y = (e.PageBounds.Height - pictureBox1.Image.Height) / 2;
                    if (pic_Y < 0) pic_Y = 0;
                    e.Graphics.DrawImage(pictureBox1.Image, pic_X, pic_Y);//e.Graphics.VisibleClipBounds);, pictureBox1.Image.Width, pictureBox1.Image.Height
                    List<string> list_ck = util.GetMultiLineString(ck, e.PageBounds.Width - pic_X - pictureBox1.Image.Width - fWidth, gItem, f);
                    List<string> list_cw = util.GetMultiLineString(cw, e.PageBounds.Width - pic_X - pictureBox1.Image.Width - fWidth, gItem, f);
                    int minHeight = (pictureBox1.Image.Height - (list_ck.Count + list_cw.Count) * fHeight) / 2;
                    if (minHeight < 0) minHeight = 0;
                    int i;
                    for ( i= 0; i < list_ck.Count; i++)
                    {
                        e.Graphics.DrawString(list_ck[i], f, bru, pic_X + pictureBox1.Image.Width, pic_Y + minHeight+ fHeight*i);
                    }
                    
                    if(this.label4.Text != "")
                    {
                        for (int j = 0; j < list_cw.Count; j++)
                        {
                            e.Graphics.DrawString(list_cw[j], f, bru, pic_X + pictureBox1.Image.Width, pic_Y + fHeight * (i+j) + minHeight);
                        }
                    }
                    e.HasMorePages = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("出错！");
            }
        }
        //private void printPreviewDialog1_Load(object sender, EventArgs e)
        //{
        //    if (printPreviewDialog1.Controls.ContainsKey("toolStrip1"))
        //    {
        //        ToolStrip ts = printPreviewDialog1.Controls["toolStrip1"] as ToolStrip;
        //        ts.Items.Add("打印设置");
        //        if (ts.Items.ContainsKey("printToolStripButton")) //打印按钮
        //        {
        //            ts.Items["printToolStripButton"].MouseDown += new MouseEventHandler(click);
        //        }
        //    }
        //}
        private void printSet(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
        void click(object sender, MouseEventArgs e)
        {
            printDocument1.Print();
        }

    }
}
