
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using System.IO;

namespace goods
    {
        /// <summary>
        /// 通用打印模型(T-->列表数据的模型，该模型必须为自定义的对象模型，严禁使用linq或其他工具生成的数据库表模型，其中的各个字段均需要使用string类型)
        /// </summary>
        /// <typeparam name="T">列表数据的模型，该模型必须为自定义的对象模型，严禁使用linq或其他工具生成的数据库表模型，其中的各个字段均需要使用string类型</typeparam>
        public class PrintDataModel<T>
        {
            /// <summary>
            /// 文档标题
            /// </summary>
            public string pageTitle { get; set; }
            /// <summary>
            /// 图形数据
            /// </summary>
            public Bitmap extData { get; set; }
            /// <summary>
            /// 头部的表头数据（不包括文档标题，如果没有，应传入 null；每个元素为一行，如果一行无法容纳，会换行处理。）
            /// </summary>
            public List<string> TitleData { get; set; }
            /// <summary>
            /// 页面中间的列表数据（如果没有，应传入 null）
            /// </summary>
            public List<T> TableData { get; set; }
            /// <summary>
            /// 页面中间的列表数据的表头（与列表数据中的列一一对应，如果没有(仅限列表数据传入null的情况)，应传入 null）
            /// </summary>
            public List<string> ColumnNames { get; set; }
            /// <summary>
            /// 列表数据的各个列是否允许折行显示，允许为true，不允许为false（仅当自动计算的列宽不足以显示内容时生效，如果传入null则表示所有列均允许折行显示）
            /// </summary>
            public List<bool> CanResetLine { get; set; }
            /// <summary>
            /// 底部的结尾数据（不包括页码，如果没有，应传入 null；每个元素为一行，如果一行无法容纳，会换行处理。）
            /// </summary>
            public List<string> EndData { get; set; }
        }
        /// <summary>
        /// 通用打印类(调用本类的方法是，请使用try-catch语句块，内部错误消息将以异常的形式抛出)
        /// </summary>
        /// <typeparam name="T">列表数据的模型，该模型必须为自定义的对象模型，严禁使用linq或其他工具生成的数据库表模型，其中的各个字段均需要使用string类型</typeparam>
        public class CommonPrintTools<T>
        {
            private PrintDocument docToPrint = new System.Drawing.Printing.PrintDocument();//创建一个PrintDocument的实例
                                                                                           /// <summary>
                                                                                           /// 需打印的文档数据
                                                                                           /// </summary>
            private List<PrintDataModel<T>> _printDataModels;
            /// <summary>
            /// 数据打印时的页码
            /// </summary>
            private int pageIndex = 1;
            /// <summary>
            /// 需要打印的文档绘制出的图片数据
            /// </summary>
            private List<Bitmap> _printBmps = new List<Bitmap>();
            private int count = 0;
            /// <summary>
            /// 初始化打印类的各项参数
            /// </summary>
            /// <param name="PrintDataModels">需打印的文档数据</param>
            public CommonPrintTools(List<PrintDataModel<T>> PrintDataModels)
            {
                this.docToPrint.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);//将事件处理函数添加到PrintDocument的PrintPage中
                _printDataModels = PrintDataModels;
            }
            /// <summary>
            /// 打印机开始打印的事件处理函数
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void docToPrint_PrintPage(object sender, PrintPageEventArgs e)
            {
                Font f = new Font("宋体", 10, FontStyle.Regular);
                decimal Chinese_OneWidth = Convert.ToDecimal(e.Graphics.MeasureString("测", f).Width);
                int pageWidth = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Width, 0)) - 1;//打印机可打印区域的宽度
                int onePageHeight = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Height, 0)) - 1;//打印机可打印区域的高度
                if (_printBmps == null || _printBmps.Count <= 0)
                    _printBmps = DrawPrintPic(e.Graphics, pageWidth, onePageHeight);

                e.Graphics.DrawImage(_printBmps[count], new Rectangle((int)Math.Ceiling(Chinese_OneWidth), 0, _printBmps[count].Width, _printBmps[count].Height));

                /********************start--判断是否需要再打印下一页--start*************************/
                count++;
                if (_printBmps.Count > count)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
                /**********************end--判断是否需要再打印下一页--end*************************/
            }
            /// <summary>
            /// 绘制需打印的内容
            /// </summary>
            /// <param name="eventG"></param>
            /// <param name="pageWidth"></param>
            /// <param name="allPageHeight"></param>
            /// <param name="msg"></param>
            /// <returns></returns>
            private List<Bitmap> DrawPrintPic(Graphics eventG, int pageWidth, int pageHeight)
            {
                Font f = new Font("宋体", 10, FontStyle.Regular);
                decimal Chinese_OneWidth = Convert.ToDecimal(eventG.MeasureString("测", f).Width);
                decimal Chinese_OneHeight = Convert.ToDecimal(eventG.MeasureString("测", f).Height);
                decimal English_OneWidth = Convert.ToDecimal(eventG.MeasureString("c", f).Width);
                decimal English_OneHeight = Convert.ToDecimal(eventG.MeasureString("c", f).Height);
                List<Bitmap> bmpList = new List<Bitmap>();
                //循环需打印的文档集合
                foreach (var pdm in _printDataModels)
                {
                    Bitmap bp = new Bitmap(pageWidth, pageHeight);
                    Graphics g = Graphics.FromImage(bp);
                    //填上底色
                    g.FillRectangle(Brushes.White, 0, 0, bp.Width, bp.Height);
                    /************************start初始化每个文档的数据start************************/
                    //页面中间的列表数据
                    List<T> middleData = pdm.TableData;
                    //页面中间的列表数据的表头
                    List<string> columnNames = pdm.ColumnNames;
                    //头部的表头数据（不包括文档标题）
                    List<string> topData = pdm.TitleData;
                    //底部的结尾数据（不包括页码）
                    List<string> bottomData = pdm.EndData;
                    //文档标题
                    string pageTitle = pdm.pageTitle;
                    //各个列是否允许折行
                    List<bool> CanResetLine = pdm.CanResetLine;
                    //图形数据
                    Bitmap codeBP = pdm.extData;
                    //检查数据列表的列与列明是否对应
                    if (middleData != null && middleData.Count > 0)
                    {
                        System.Reflection.PropertyInfo[] pInfo = middleData[0].GetType().GetProperties();
                    if (pInfo.Length != columnNames.Count)
                        {
                            throw new Exception("列表数据的列数与表头数据的列数不相符！");
                        }
                    }
                    /**************************end初始化每个文档的数据end**************************/
                    /*********************start计算各部分高度以及页面数据start*********************/
                    //计算表头高度
                    decimal headEndHeight = ComputeHeadEndHeight(pageTitle, topData, Chinese_OneHeight, pageWidth, g, f);
                    //计算结尾高度
                    headEndHeight += ComputeHeadEndHeight(null, bottomData, Chinese_OneHeight, pageWidth, g, f);
                    //如果列表中有数据，并且表头部分+一行空行的高度大于等于页面高度（页面中没有剩余空间绘制列表数据），抛出异常信息
                    if ((headEndHeight + Chinese_OneHeight >= pageHeight) && middleData != null && middleData.Count > 0)
                    {
                        throw new Exception("表头或表尾数据太多，页面中将没有绘制数据行的空间！");
                    }
                    //数据列表的各列宽度
                    decimal[] columnWidths = null;
                    //数据列表的各行行高
                    decimal[] rowHeights = null;
                    DataTable dt = new DataTable();
                    if (middleData != null && middleData.Count > 0)
                    {
                        string msg = "";
                        dt = ReflactionToDataTable(middleData, columnNames, ref msg);
                        if (dt == null && !string.IsNullOrEmpty(msg))
                        {
                            throw new Exception(msg);
                        }
                        columnWidths = GetColumnWidths(dt, CanResetLine, Chinese_OneWidth, g, f);
                        columnWidths = GetColumnWidthToPage(columnWidths, pageWidth, Chinese_OneWidth, CanResetLine, g, f);
                        rowHeights = GetRowHeights(dt, columnWidths, g, f);
                    }
                    /***********************end计算各部分高度以及页面数据end***********************/
                    /****start绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制start****/
                    //中间数据列表存在的情况
                    if (middleData != null && middleData.Count > 0 && dt != null && dt.Rows.Count > 0)
                    {
                        bmpList.AddRange(DrawPage(pageWidth - (int)Math.Ceiling(Chinese_OneWidth * 2), pageHeight, f, Chinese_OneWidth, Chinese_OneHeight, topData, bottomData, pageTitle, codeBP, CanResetLine, rowHeights, dt, headEndHeight));
                    }
                    else//没有中间数据列表的情况
                    {
                        bmpList.AddRange(DrawPageWithOutDataTable(pageWidth - (int)Math.Ceiling(Chinese_OneWidth * 2), pageHeight, f, Chinese_OneHeight, topData, bottomData, pageTitle));
                    }
                    /******end绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制绘制end******/
                }
                return bmpList;
            }
            /// <summary>
            /// 绘制打印页面
            /// </summary>
            /// <param name="pageWidth">页面宽度</param>
            /// <param name="pageHeight">页面高度</param>
            /// <param name="f">字体</param>
            /// <param name="Chinese_OneWidth">单字宽度</param>
            /// <param name="Chinese_OneHeight">单字高度</param>
            /// <param name="topData">头部的表头数据（不包括文档标题）</param>
            /// <param name="bottomData">底部的结尾数据（不包括页码）</param>
            /// <param name="pageTitle">文档标题</param>
            /// <param name="columnWidths">列宽度集合</param>
            /// <param name="rowHeights">行高集合</param>
            /// <param name="dt">需打印的所有列表数据</param>
            /// <returns>所有需要打印的页面</returns>
            private List<Bitmap> DrawPage(int pageWidth, int pageHeight, Font f, decimal Chinese_OneWidth, decimal Chinese_OneHeight, List<string> topData, List<string> bottomData, string pageTitle, Bitmap codeBP, List<bool> CanResetLine, decimal[] rowHeights, DataTable dt, decimal headEndHeight)
            {
                List<Bitmap> bmpList = new List<Bitmap>();
                //获取每页中需要打印的列表数据
                List<DataTable> dts = GetDataTableToPages(dt, rowHeights, pageHeight - headEndHeight - Chinese_OneHeight);
                //未分割DataTable时DataTable的行索引
                int sourceRowIndex = 1;
                //未分割DataTable时DataTable的行索引(用于计算)
                int sourceRowIndexForCompute = 1;
                //页索引
                int pageIndex = 1;
                //循环绘制每一页
                foreach (DataTable drawDt in dts)
                {
                    Bitmap bpItem = new Bitmap(pageWidth, pageHeight);
                    Graphics gItem = Graphics.FromImage(bpItem);
                    //当前显示的数据列表的各列宽度
                    decimal[] columnWidths = GetColumnWidths(drawDt, CanResetLine, Chinese_OneWidth, gItem, f);
                    columnWidths = GetColumnWidthToPage(columnWidths, pageWidth, Chinese_OneWidth, CanResetLine, gItem, f);
                    //填上底色
                    gItem.FillRectangle(Brushes.White, 0, 0, bpItem.Width, bpItem.Height);
                    //当前绘制行距离页面最顶部的距离
                    decimal currentMarginTop = 0;
                    /******************************start表头部分start******************************/
                    //标题
                    if (!string.IsNullOrEmpty(pageTitle))
                    {
                        currentMarginTop += Chinese_OneHeight;
                        List<string> drawValues = GetMultiLineString(pageTitle, pageWidth, gItem, f);
                        foreach (string dv in drawValues)
                        {
                            gItem.DrawString(dv, f, Brushes.Black, (float)(pageWidth - (decimal)gItem.MeasureString(dv, f).Width) / 2, (float)currentMarginTop);
                            currentMarginTop += Chinese_OneHeight;
                        }
                    }
                    //图形
                    if (codeBP != null && codeBP.Height > 0 && codeBP.Width > 0)
                    {
                        gItem.DrawImage(codeBP, 0, (float)currentMarginTop);
                        currentMarginTop += codeBP.Height;
                    }
                    //表头数据
                    if (topData != null && topData.Count > 0)
                    {
                        int n = 0;//分左右
                        foreach (string tdTitle in topData)
                        {
                            
                            List<string> drawValues = GetMultiLineString(tdTitle, pageWidth, gItem, f);
                            
                            foreach (string dv in drawValues)
                            {
                                //gItem.DrawString(dv, f, Brushes.Black, 0, (float)currentMarginTop);
                                //gItem.DrawString(dv, f, Brushes.Black, (float)(pageWidth - (decimal)gItem.MeasureString(dv, f).Width - Chinese_OneWidth), (float)currentMarginTop);
                                if (n % 2 != 0)
                                {
                                    gItem.DrawString(dv, f, Brushes.Black, (float)(pageWidth - (decimal)gItem.MeasureString(dv, f).Width - Chinese_OneWidth), (float)currentMarginTop);
                                    currentMarginTop += Chinese_OneHeight;
                                }
                                else
                                {
                                    currentMarginTop += Chinese_OneHeight / 2;
                                    gItem.DrawString(dv, f, Brushes.Black, 0, (float)currentMarginTop);
                                    if(n == topData.Count -1) currentMarginTop += Chinese_OneHeight;
                                }
                            }
                            n++;
                        }
                    }
                    /********************************end表头部分end********************************/
                    /****************************start中间列表部分start****************************/
                    //绘制标题行
                    //每个单元格距离左侧的距离
                    decimal colLeft = 0;
                    decimal drawDtHeight = rowHeights[0];//GetRowHeights(drawDt, Chinese_OneHeight, columnWidths, gItem, f).Sum();
                    for (int rowIndex = 0; rowIndex < drawDt.Rows.Count; rowIndex++)
                    {
                        drawDtHeight += rowHeights[sourceRowIndexForCompute];
                        sourceRowIndexForCompute++;
                    }
                    currentMarginTop += Chinese_OneHeight / 2;
                    //绘制各个列的标题
                    for (int colIndex = 0; colIndex < drawDt.Columns.Count; colIndex++)
                    {
                        //列名
                        string colName = drawDt.Columns[colIndex].ColumnName;
                        //计算需要绘制的文字需要在几行中显示
                        List<string> drawValues = GetMultiLineString(colName, (float)columnWidths[colIndex], gItem, f);
                        decimal rowMarginTop = currentMarginTop;
                        foreach (string dv in drawValues)
                        {
                            gItem.DrawString(dv, f, Brushes.Black, (float)colLeft, (float)rowMarginTop);
                            rowMarginTop += Chinese_OneHeight;
                        }
                        //绘制表格的列
                        gItem.DrawLine(new Pen(Brushes.Black), new Point((int)Math.Ceiling(colLeft), (int)Math.Floor(currentMarginTop) - 1), new Point((int)Math.Ceiling(colLeft), (int)Math.Ceiling(currentMarginTop + drawDtHeight) - 2));
                        //每个单元格的数据需要向右移动一个当前单元格的宽度
                        colLeft += columnWidths[colIndex];
                    }
                    //绘制表格的第一行
                    gItem.DrawLine(new Pen(Brushes.Black), new Point(0, (int)Math.Floor(currentMarginTop) - 1), new Point((int)Math.Ceiling(columnWidths.Sum() - 2), (int)Math.Floor(currentMarginTop) - 1));
                    //绘制表格的最后一列
                    gItem.DrawLine(new Pen(Brushes.Black), new Point((int)Math.Ceiling(columnWidths.Sum() - 2), (int)Math.Floor(currentMarginTop) - 1), new Point((int)Math.Ceiling(columnWidths.Sum() - 2), (int)Math.Ceiling(currentMarginTop + drawDtHeight) - 2));
                    currentMarginTop += rowHeights[0];
                    //绘制列表中的数据
                    for (int rowIndex = 0; rowIndex < drawDt.Rows.Count; rowIndex++)
                    {
                        //每个单元格距离左侧的距离
                        decimal rowMarginLeft = 0;
                        for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                        {
                            //需要绘制的当前单元格的文字
                            string drawValue = drawDt.Rows[rowIndex][colIndex].ToString();
                            //计算需要绘制的文字需要在几行中显示
                            List<string> drawValues = GetMultiLineString(drawValue, (float)columnWidths[colIndex], gItem, f);
                            //当前行的各个列均是从同一个高度（距离顶部的距离）开始绘制的
                            decimal rowMarginTop = currentMarginTop;
                            foreach (string dv in drawValues)
                            {
                                gItem.DrawString(dv, f, Brushes.Black, (float)rowMarginLeft, (float)rowMarginTop);
                                rowMarginTop += Chinese_OneHeight;
                            }
                            //每个单元格的数据需要向右移动一个当前单元格的宽度
                            rowMarginLeft += columnWidths[colIndex];
                        }
                        //绘制表格的行
                        gItem.DrawLine(new Pen(Brushes.Black), new Point(0, (int)Math.Floor(currentMarginTop) - 1), new Point((int)Math.Ceiling(columnWidths.Sum() - 2), (int)Math.Floor(currentMarginTop) - 1));
                        //绘制完一行的数据后，将高度（距离顶部的距离）累加当前行的高度
                        currentMarginTop += rowHeights[sourceRowIndex];
                        sourceRowIndex++;
                    }
                    //绘制表格的行
                    gItem.DrawLine(new Pen(Brushes.Black), new Point(0, (int)Math.Floor(currentMarginTop) - 1), new Point((int)Math.Ceiling(columnWidths.Sum() - 2), (int)Math.Floor(currentMarginTop) - 1));
                    /******************************end中间列表部分end******************************/
                    /******************************start结尾部分start******************************/
                    if (bottomData != null && bottomData.Count > 0)
                    {
                        currentMarginTop += Chinese_OneHeight / 2;
                        foreach (string bdData in bottomData)
                        {
                            //当前行的各个列均是从同一个高度（距离顶部的距离）开始绘制的
                            decimal rowMarginTop = currentMarginTop;
                            //计算需要绘制的文字需要在几行中显示
                            List<string> drawValues = GetMultiLineString(bdData, (float)pageWidth, gItem, f);
                            foreach (string dv in drawValues)
                            {
                                gItem.DrawString(dv, f, Brushes.Black, 0, (float)rowMarginTop);
                                rowMarginTop += Chinese_OneHeight;
                            }
                            currentMarginTop += Chinese_OneHeight;
                        }
                    }
                    string pageInfo = string.Format("当前第{0}页，共{1}页", pageIndex.ToString(), dts.Count.ToString());
                    gItem.DrawString(pageInfo, f, Brushes.Black, (float)(pageWidth - (decimal)gItem.MeasureString(pageInfo, f).Width - Chinese_OneWidth), (float)currentMarginTop);
                    /********************************end结尾部分end********************************/
                    bmpList.Add(bpItem);
                    pageIndex++;
                }
                return bmpList;
            }
            /// <summary>
            /// 绘制打印页面（无数据列表）
            /// </summary>
            /// <param name="pageWidth"></param>
            /// <param name="pageHeight"></param>
            /// <param name="f"></param>
            /// <param name="Chinese_OneWidth"></param>
            /// <param name="Chinese_OneHeight"></param>
            /// <param name="topData"></param>
            /// <param name="bottomData"></param>
            /// <param name="pageTitle"></param>
            /// <returns></returns>
            private List<Bitmap> DrawPageWithOutDataTable(int pageWidth, int pageHeight, Font f, decimal Chinese_OneHeight, List<string> topData, List<string> bottomData, string pageTitle)
            {
                List<Bitmap> bmpList = new List<Bitmap>();
                //所有需要绘制的数据（不包括标题）
                List<string> drawList = new List<string>();
                if (topData != null && topData.Count > 0)
                    drawList.AddRange(topData);
                if (bottomData != null && bottomData.Count > 0)
                    drawList.AddRange(bottomData);
                Bitmap bp = new Bitmap(pageWidth, pageHeight);
                Graphics g = Graphics.FromImage(bp);
                //可用绘制数据的高度
                decimal dataHeight = pageHeight - Chinese_OneHeight;//默认为页面高度减去一个空行高度
                if (!string.IsNullOrEmpty(pageTitle))
                {
                    //标题的高度
                    decimal titleHeight = 0;
                    //标题切割成的多行
                    List<string> titles = GetMultiLineString(pageTitle, pageWidth, g, f);
                    //计算标题的总高度
                    foreach (string t in titles)
                    {
                        titleHeight += (decimal)g.MeasureString(t, f).Width;
                    }
                    //计算可用绘制数据的高度（页面高度减去标题高度再减去两个空行的高度）
                    dataHeight = (decimal)pageHeight - titleHeight - Chinese_OneHeight;
                }
                //每页需要绘制的数据集合
                List<List<string>> drawData = GetLinesToPages(drawList, Chinese_OneHeight, dataHeight, pageWidth, g, f);
                //循环绘制每页的数据
                foreach (List<string> drawD in drawData)
                {
                    Bitmap bpItem = new Bitmap(pageWidth, pageHeight);
                    Graphics gItem = Graphics.FromImage(bpItem);
                    //填上底色
                    gItem.FillRectangle(Brushes.White, 0, 0, bpItem.Width, bpItem.Height);
                    //当前绘制行距离页面最顶部的距离
                    decimal currentMarginTop = 0;
                    //标题
                    if (!string.IsNullOrEmpty(pageTitle))
                    {
                        currentMarginTop += Chinese_OneHeight;
                        List<string> drawValues = GetMultiLineString(pageTitle, pageWidth, gItem, f);
                        foreach (string dv in drawValues)
                        {
                            gItem.DrawString(dv, f, Brushes.Black, (float)(pageWidth - (decimal)gItem.MeasureString(dv, f).Width) / 2, (float)currentMarginTop);
                            currentMarginTop += Chinese_OneHeight;
                        }
                    }
                    foreach (string dv in drawD)
                    {
                        gItem.DrawString(dv, f, Brushes.Black, 0, (float)currentMarginTop);
                        currentMarginTop += Chinese_OneHeight;
                    }
                    string pageInfo = string.Format("当前第{0}页，共{1}页", pageIndex.ToString(), drawData.Count.ToString());
                    gItem.DrawString(pageInfo, f, Brushes.Black, 0, (float)currentMarginTop);
                    bmpList.Add(bpItem);
                    pageIndex++;
                }
                return bmpList;
            }
            /// <summary>
            /// 开始打印
            /// </summary>
            /// <param name="printname">计算机上已安装的打印机的名称</param>
            public void StartPrint(string printname)
            {
                System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();//创建一个PrintDialog的实例。
                PrintDialog1.AllowSomePages = true;
                PrintDialog1.ShowHelp = true;
                PrintDialog1.Document = docToPrint;//把PrintDialog的Document属性设为上面配置好的PrintDocument的实例
                docToPrint.PrinterSettings.PrinterName = printname; //_366KF.Manage.Common.PickOrderPrinter; //设置打印机，填写计算机上已安装的打印机的名称
                docToPrint.Print();//开始打印
            }

        public void Print()
        {
            System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();//创建一个PrintDialog的实例。
            PrintDialog1.AllowSomePages = true;
            PrintDialog1.ShowHelp = true;
            PrintDialog1.Document = docToPrint;//把PrintDialog的Document属性设为上面配置好的PrintDocument的实例
            docToPrint.Print();//开始打印
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
                if (PrintPriview.Controls.ContainsKey("toolStrip1"))
                {
                    ToolStrip ts = PrintPriview.Controls["toolStrip1"] as ToolStrip;
                    //ts.Items.Add("打印设置");
                    if (ts.Items.ContainsKey("printToolStripButton")) //打印按钮
                    {
                        ts.Items["printToolStripButton"].MouseDown += new MouseEventHandler(click);
                    }
                }
                PrintPriview.Document = docToPrint;
                PrintPriview.WindowState = FormWindowState.Maximized;
                PrintPriview.ShowDialog();
                this.reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印错误，请检查打印设置！消息：" + ex.Message);

            }
        }
        public void setCopies(short num)
        {
            docToPrint.PrinterSettings.Copies = num;
        }
        private void click(object sender, MouseEventArgs e)
        {
            this.reset();
            //docToPrint.Print();
        }

        private void changePrinter()
        {
            PrintDialog dlg = new PrintDialog();
            dlg.Document = docToPrint;
            dlg.ShowDialog();
        }
        public void PrintSetup()
        {
            try
            {
                PageSetupDialog PageSetup = new PageSetupDialog();
                PageSetup.Document = docToPrint;
                PageSetup.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印设置错误，请检查打印设置！消息：" + ex.Message);

            }
        }
        public void reset()
        {
            this.count = 0;
        }
        public void TestPrint()
            {
                int pageWidth = 300; //Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Width, 0)) - 1;//打印机可打印区域的宽度
                int onePageHeight = 1000;// Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Height, 0)) - 1;//打印机可打印区域的高度
                Bitmap bp = new Bitmap(pageWidth, onePageHeight);
                Graphics g = Graphics.FromImage(bp);
                List<Bitmap> bmps = DrawPrintPic(g, pageWidth, onePageHeight);
                int bmpName = 1;
                string path = Application.StartupPath + "/pic";
                List<string> paths = Directory.GetFiles(path).ToList();
                foreach (string p in paths)
                {
                    File.Delete(p);
                }
                foreach (Bitmap b in bmps)
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    b.Save(path + "/" + bmpName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    bmpName++;
                }
            }
            /// <summary>
            /// 利用反射将数据对象集合转换为DataTable
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="dataModel"></param>
            /// <param name="columnNames"></param>
            /// <returns></returns>
            private DataTable ReflactionToDataTable(List<T> dataModel, List<string> columnNames, ref string msg)
            {
                try
                {
                    DataTable dt = new DataTable();
                    if (typeof(T).Equals(typeof(String)))
                    {
                        msg = "数据集合必须为自定义对象集合！";
                        return new DataTable();
                    }
                    if (dataModel == null || dataModel.Count <= 0)
                    {
                        msg = "传入的数据集合为空！";
                        return new DataTable();
                    }
                    if (columnNames == null || columnNames.Count <= 0)
                    {
                        msg = "传入的列名数据集合为空！";
                        return new DataTable();
                    }
                    PropertyInfo[] pInfos = ((object)dataModel[0]).GetType().GetProperties();
                    if (pInfos.Length != columnNames.Count)
                    {
                        msg = "数据列数与列名数量不一致！";
                        return new DataTable();
                    }
                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        dt.Columns.Add(columnNames[i], pInfos[i].PropertyType);
                    }
                    for (int i = 0; i < dataModel.Count; i++)
                    {
                        object[] objArray = new object[pInfos.Length];
                        for (int j = 0; j < pInfos.Length; j++)
                        {
                            object ob = pInfos[j].GetValue(dataModel[i], null);
                            objArray.SetValue(ob, j);
                        }
                        dt.LoadDataRow(objArray, true);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    msg = "请检查传入的数据类型，是否为List<自定义对象>类型，自定义对象必须有属性值！(异常信息：" + ex.Message + ")";
                    return null;
                }
            }
            /// <summary>
            /// 讲一个字符串按照固定的长度切分为多个适合长度的字符串
            /// </summary>
            /// <param name="dataLine">需要切分的字符串</param>
            /// <param name="width">固定的长度</param>
            /// <param name="g">绘制对象</param>
            /// <param name="f">字体</param>
            /// <returns>切分后得到的字符串集合</returns>
            private List<string> GetMultiLineString(string dataLine, float width, Graphics g, Font f)
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
                    if (i >= chars.Length || i== -1)
                        break;
                    i++;
                }
                return dataLines;
            }
            /// <summary>
            /// 获取各个列的最大宽度值
            /// </summary>
            /// <param name="DataTablePrint">数据列表</param>
            /// <param name="CanResetLine">各列是否允许折行显示</param>
            /// <param name="Chinese_OneWidth">单字宽度</param>
            /// <param name="g">绘制对象</param>
            /// <param name="f">字体</param>
            /// <returns></returns>
            private decimal[] GetColumnWidths(DataTable DataTablePrint, List<bool> CanResetLine, decimal Chinese_OneWidth, Graphics g, Font f)
            {
                decimal[] res = new decimal[DataTablePrint.Columns.Count]; ;
                foreach (DataRow dr in DataTablePrint.Rows)
                {
                    for (int i = 0; i < DataTablePrint.Columns.Count; i++)
                    {
                        //后面加的半个单字宽度用来抵消decimal类型尾数被舍弃的情形
                        decimal colwidth = Convert.ToDecimal(g.MeasureString(dr[i].ToString().Trim(), f).Width) + (CanResetLine[i] ? 0 : Chinese_OneWidth / 2);
                        if (colwidth > res[i])
                        {
                            res[i] = colwidth;
                        }
                    }
                }
                for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
                {
                    string ColumnText = DataTablePrint.Columns[Cols].ColumnName.ToString();
                    //后面加的半个单字宽度用来抵消decimal类型尾数被舍弃的情形
                    decimal colwidth = Convert.ToInt32(g.MeasureString(ColumnText, f).Width) + (CanResetLine[Cols] ? 0 : Chinese_OneWidth / 2);
                    if (colwidth > res[Cols])
                    {
                        res[Cols] = colwidth;
                    }
                }
                if (res[1] > 300) res[1] = 300;
                return res;
            }
            /// <summary>
            /// 计算表头部分高度
            /// </summary>
            /// <param name="pageTitle">页面标题</param>
            /// <param name="topData">表头数据</param>
            /// <param name="Chinese_OneHeight">单行高度</param>
            /// <param name="pageWidth">页面宽度</param>
            /// <param name="g">绘制对象</param>
            /// <param name="f">字体</param>
            /// <returns></returns>
            private decimal ComputeHeadEndHeight(string pageTitle, List<string> topData, decimal Chinese_OneHeight, float pageWidth, Graphics g, Font f)
            {
                decimal headHeight = 0;
                if (!string.IsNullOrEmpty(pageTitle))
                {
                    List<string> pts = GetMultiLineString(pageTitle, pageWidth, g, f);
                    headHeight += Chinese_OneHeight;
                    headHeight += GetRowHeight(pts, g, f);
                    headHeight += Chinese_OneHeight;
                }
                if (topData != null && topData.Count > 0)
                {
                    foreach (string tds in topData)
                    {
                        List<string> tdss = GetMultiLineString(tds, pageWidth, g, f);
                        headHeight += GetRowHeight(tdss, g, f);
                    }
                }
                return headHeight;
            }
            /// <summary>
            /// 获取各个行的最大高度值(索引为0的行高为列标题的最大行高)
            /// </summary>
            /// <param name="dt">数据列表</param>
            /// <param name="columnWidths">各个列的最大宽度值</param>
            /// <param name="g">绘制对象</param>
            /// <param name="f">字体</param>
            /// <returns>返回行最大高度值集合</returns>
            private decimal[] GetRowHeights(DataTable dt, decimal[] columnWidths, Graphics g, Font f)
            {
                decimal[] rowHeights = new decimal[dt.Rows.Count + 1];
                int columnNameIndex = 0;
                //计算标题行的高度
                foreach (DataColumn dc in dt.Columns)
                {
                    string dValue = dc.ColumnName;
                    decimal cwidth = columnWidths[columnNameIndex];
                    List<string> mLines = GetMultiLineString(dValue, (float)cwidth, g, f);
                    decimal h = GetRowHeight(mLines, g, f);
                    if (h > rowHeights[0])
                    {
                        rowHeights[0] = h;
                    }
                    columnNameIndex++;
                }
                int columnIndex = 0;
                //计算各个数据行的高度
                foreach (DataColumn dc in dt.Columns)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string dValue = dt.Rows[i][dc.ColumnName].ToString();
                        decimal cwidth = columnWidths[columnIndex];
                        List<string> mLines = GetMultiLineString(dValue, (float)cwidth, g, f);
                        decimal h = GetRowHeight(mLines, g, f);
                        if (h > rowHeights[i + 1])
                        {
                            rowHeights[i + 1] = h;
                        }
                    }
                    columnIndex++;
                }
                return rowHeights;
            }
            /// <summary>
            /// 计算行高
            /// </summary>
            /// <param name="lines">一个DataRow需要显示的行集合</param>
            /// <param name="g">绘制对象</param>
            /// <param name="f">字体</param>
            /// <returns>返回DataRow行高</returns>
            private decimal GetRowHeight(List<string> lines, Graphics g, Font f)
            {
                decimal h = 0;
                foreach (string line in lines)
                {
                    h += (decimal)g.MeasureString(line, f).Height;
                }
                return h;
            }
            /// <summary>
            /// 根据实际的列宽计算适应页面所需的列宽度
            /// </summary>
            /// <param name="columnWidths">实际列宽</param>
            /// <param name="pageWidth">页面宽度</param>
            /// <param name="Chinese_OneWidth">单字宽度</param>
            /// <param name="CanResetLine">各列是否允许折行显示</param>
            /// <param name="g">绘制对象</param>
            /// <param name="f">字体</param>
            /// <returns>按比例计算之后的列宽</returns>
            private decimal[] GetColumnWidthToPage(decimal[] columnWidths, decimal pageWidth, decimal Chinese_OneWidth, List<bool> CanResetLine, Graphics g, Font f)
            {
                string cWidthString = "";
                for (int i = 0; i < columnWidths.Length; i++)
                {
                    cWidthString += "测";
                }
                if (pageWidth < (decimal)g.MeasureString(cWidthString, f).Width)
                {
                    throw new Exception("列数太多，当前纸张无法呈现（以每列一个汉字算，所有列的宽度之和，大于纸张宽度）！");
                }
                //允许折行的列数
                int canResetCount = 0;
                //不允许折行的列的总宽度
                decimal solidWidth = 0;
                //允许折行的列的总宽度
                decimal columnSumWidth = columnWidths.Sum();
                //是否存在不允许折行的列
                if (CanResetLine != null && CanResetLine.Where(c => !c).Count() > 0)
                {
                    columnSumWidth = 0;
                    for (int i = 0; i < columnWidths.Length; i++)
                    {
                        //如果当前列不允许折行，累加不允许折行的列的总宽度
                        if (!CanResetLine[i])
                        {
                            solidWidth += columnWidths[i];
                        }
                        else//累加允许折行的列的总宽度，累加允许折行的列数
                        {
                            columnSumWidth += columnWidths[i];
                            canResetCount++;
                        }
                    }
                    //计算可以进行折行处理的剩余总宽度
                    pageWidth -= solidWidth;
                }
                //如果 按单字宽度计算，允许折行的总宽度 大于（>） 可以进行折行处理的剩余总宽度，则抛出异常
                string crWidthString = "";
                for (int i = 0; i < canResetCount; i++)
                {
                    crWidthString += "测";
                }
                if ((decimal)g.MeasureString(crWidthString, f).Width > pageWidth)
                {
                    throw new Exception("当前纸张无法呈现所有内容（以每列一个汉字算，所有可折行列的宽度之和，大于可以进行折行处理的剩余总宽度）！");
                }
                decimal[] res = new decimal[columnWidths.Length];
                columnWidths.CopyTo(res, 0);
                for (int i = 0; i < columnWidths.Length; i++)
                {
                    if (CanResetLine == null || CanResetLine[i])
                        res[i] = (res[i] / columnSumWidth) * pageWidth;
                }
                return res;
            }
            /// <summary>
            /// 获取每页的数据列表
            /// </summary>
            /// <param name="dt">所有列表数据</param>
            /// <param name="rowHeights">行高集合</param>
            /// <param name="dataHeight">显示数据的可用高度</param>
            /// <returns></returns>
            private List<DataTable> GetDataTableToPages(DataTable dt, decimal[] rowHeights, decimal dataHeight)
            {
                dataHeight -= rowHeights[0];
                List<DataTable> dts = new List<DataTable>();
                int rowIndex = 0;
                decimal currentHeight = 0;
                decimal[] rowHs = new decimal[rowHeights.Length - 1];
                for (int i = 0; i < rowHs.Length; i++)
                {
                    rowHs[i] = rowHeights[i + 1];
                }
                List<DataRow> drs = new List<DataRow>();
                while (rowIndex <= dt.Rows.Count)
                {
                    if (rowIndex == rowHs.Length)
                    {
                        DataTable ndt = dt.Clone();
                        foreach (DataRow dr in drs)
                        {
                            DataRow ndtDr = ndt.NewRow();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                ndtDr[dc.ColumnName] = dr[dc.ColumnName];
                            }
                            ndt.Rows.Add(ndtDr);
                        }
                        dts.Add(ndt);
                        break;
                    }
                    if ((currentHeight + rowHs[rowIndex] > dataHeight))
                    {
                        DataTable ndt = dt.Clone();
                        foreach (DataRow dr in drs)
                        {
                            DataRow ndtDr = ndt.NewRow();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                ndtDr[dc.ColumnName] = dr[dc.ColumnName];
                            }
                            ndt.Rows.Add(ndtDr);
                        }
                        dts.Add(ndt);
                        rowIndex--;
                        currentHeight = 0;
                        drs.Clear();
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = dt.Rows[rowIndex][i].ToString();
                        }
                        drs.Add(dr);
                        currentHeight += rowHs[rowIndex];
                    }
                    rowIndex++;
                }
                return dts;
            }
            /// <summary>
            /// 获取每页的非数据列表的数据行
            /// </summary>
            /// <param name="data">所有数据行</param>
            /// <param name="Chinese_OneHeight">单行高度</param>
            /// <param name="dataHeight">显示数据的可用高度</param>
            /// <param name="pageWidth">页面宽度</param>
            /// <param name="g">绘制对象</param>
            /// <param name="f">字体</param>
            /// <returns></returns>
            private List<List<string>> GetLinesToPages(List<string> data, decimal Chinese_OneHeight, decimal dataHeight, decimal pageWidth, Graphics g, Font f)
            {
                List<List<string>> res = new List<List<string>>();
                List<string> resData = new List<string>();
                decimal currentHeight = 0;
                foreach (string dLine in data)
                {
                    List<string> dvs = GetMultiLineString(dLine, (float)pageWidth, g, f);
                    foreach (string dv in dvs)
                    {
                        if (currentHeight + Chinese_OneHeight > dataHeight)
                        {
                            res.Add(resData);
                            resData.Clear();
                            currentHeight = 0;
                            resData.Add(dv);
                            currentHeight += Chinese_OneHeight;
                        }
                        else
                        {
                            resData.Add(dv);
                            currentHeight += Chinese_OneHeight;
                        }
                    }
                }
                return res;
            }
        }
    }

