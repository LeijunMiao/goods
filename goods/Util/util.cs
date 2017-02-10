using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using ZXing;
using System.IO;
using System.Net;
namespace goods
{
    class utilCls
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="msg">二维码信息</param>
        /// <returns>图片</returns>
        public Bitmap GenByZXingNet(string msg)
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

        /// <summary>
        /// 讲一个字符串按照固定的长度切分为多个适合长度的字符串
        /// </summary>
        /// <param name="dataLine">需要切分的字符串</param>
        /// <param name="width">固定的长度</param>
        /// <param name="g">绘制对象</param>
        /// <param name="f">字体</param>
        /// <returns>切分后得到的字符串集合</returns>
        public List<string> GetMultiLineString(string dataLine, float width, Graphics g, Font f)
        {
            List<string> dataLines = new List<string>();
            char[] chars = dataLine.ToCharArray();
            decimal widthT = Convert.ToDecimal(width);
            decimal charWidth = Convert.ToDecimal(g.MeasureString("c".ToString(), f).Width);
            if (widthT < charWidth)
            {
                throw new Exception("数据无法正常显示，经优化计算后的列宽不足以存放一个字符！");
            }
            decimal widthC = 0;
            int i = 0;
            string dLine = "";
            string tmpLine = "";
            while (true)
            {
                if (i == chars.Length)
                    widthC = decimal.MaxValue;
                else
                {
                    tmpLine += chars[i].ToString();
                    widthC = Convert.ToDecimal(g.MeasureString(tmpLine, f).Width);
                }
                if (widthC < widthT)
                {
                    dLine = tmpLine;
                }
                else
                {
                    dataLines.Add(dLine);
                    widthC = 0;
                    dLine = "";
                    tmpLine = "";
                    if (i < chars.Length)
                        i--;
                }
                if (i >= chars.Length || i == -1)
                    break;
                i++;
            }
            return dataLines;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpUrl">FTP地址</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public Stream Info(string ftpUrl, string fileName)
        {
            try
            {
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + "" + fileName));
                reqFtp.UseBinary = true;
                FtpWebResponse respFtp = (FtpWebResponse)reqFtp.GetResponse();
                Stream stream = respFtp.GetResponseStream();
                return stream;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
