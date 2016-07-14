﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goods.Controller;
using goods.Model;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
namespace goods
{
    public partial class CheckListView : Form
    {
        stockCtrl ctrl = new stockCtrl();
        System.Data.DataTable dt = new System.Data.DataTable();
        utilCls utilcls = new utilCls();
        public CheckListView(string num)
        {
            InitializeComponent();
            initTable();
            loadData(num);
        }
        private void initTable()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn numColumn = new DataGridViewTextBoxColumn();
            numColumn.HeaderText = "批次编号";
            numColumn.DataPropertyName = "batchTNum";
            numColumn.Name = "batchTNum";

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "id";
            idColumn.Visible = false;
            idColumn.Name = "id";

            DataGridViewTextBoxColumn materielColumn = new DataGridViewTextBoxColumn();
            materielColumn.DataPropertyName = "materiel";
            materielColumn.Visible = false;
            materielColumn.Name = "materiel";

            DataGridViewTextBoxColumn warehouseColumn = new DataGridViewTextBoxColumn();
            warehouseColumn.DataPropertyName = "warehouse";
            warehouseColumn.Visible = false;
            warehouseColumn.Name = "warehouse";

            DataGridViewTextBoxColumn positionColumn = new DataGridViewTextBoxColumn();
            positionColumn.DataPropertyName = "position";
            positionColumn.Visible = false;
            positionColumn.Name = "position";

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "name";
            nameColumn.HeaderText = "物料名称";
            nameColumn.Name = "name";

            DataGridViewTextBoxColumn wnameColumn = new DataGridViewTextBoxColumn();
            wnameColumn.DataPropertyName = "wname";
            wnameColumn.HeaderText = "仓库";
            wnameColumn.Name = "wname";

            DataGridViewTextBoxColumn pnameColumn = new DataGridViewTextBoxColumn();
            pnameColumn.DataPropertyName = "pname";
            pnameColumn.HeaderText = "仓位";
            pnameColumn.Name = "pname";

            DataGridViewTextBoxColumn zvaColumn = new DataGridViewTextBoxColumn();
            zvaColumn.DataPropertyName = "truequantity";
            zvaColumn.HeaderText = "库存";
            zvaColumn.Name = "truequantity";

            DataGridViewTextBoxColumn avaColumn = new DataGridViewTextBoxColumn();
            avaColumn.DataPropertyName = "avaquantity";
            avaColumn.HeaderText = "盘点库存";
            avaColumn.Name = "avaquantity";

            DataGridViewTextBoxColumn lossColumn = new DataGridViewTextBoxColumn();
            lossColumn.DataPropertyName = "loss";
            lossColumn.HeaderText = "盈亏";
            lossColumn.Name = "loss";

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
           idColumn,numColumn,nameColumn,wnameColumn,pnameColumn,zvaColumn,avaColumn,lossColumn});

        }

        private void loadData(string num){
            dt = ctrl.getByNum(num);
            this.label3.Text = num;
            this.label5.Text = dt.Rows[0]["date"].ToString();
            this.label6.Text = dt.Rows[0]["id"].ToString();
            dt.Columns.Add("loss");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["avaquantity"] == DBNull.Value) dt.Rows[i]["avaquantity"] = 0;
                dt.Rows[i]["loss"] = Convert.ToDouble(dt.Rows[i]["avaquantity"]) - Convert.ToDouble(dt.Rows[i]["truequantity"]);
            }
            dataGridView1.DataSource = dt;
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (label3.Text == "")
            {
                MessageBox.Show("请先保存！");
                return;
            }
            this.pictureBox1.Image = utilcls.GenByZXingNet(label6.Text);
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + "\\image\\";
            if (!Directory.Exists(path))
            {
                //Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }

            string filepath = path + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
            this.pictureBox1.Image.Save(filepath);
            //int result = this.ExportExcel("盘点单", this.dataGridView1); //this.dataGridView1:DataGridView控件
            bool result = OutToExcelFromDataGridView("盘点单", filepath, path, label6.Text, label5.Text, this.dataGridView1, true);
        }

        public bool OutToExcelFromDataGridView(string title, string filepath, string path, string num, string date, DataGridView dgv, bool isShowExcel)
        {
            int titleColumnSpan = 0;//标题的跨列数
            string fileName = "";//保存的excel文件名
            int columnIndex = 1;//列索引
            if (dgv.Rows.Count == 0)
                return false;
            /*保存对话框*/
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出Excel(*.xls)|*.xls";
            sfd.FileName = title + DateTime.Now.ToString("yyyyMMddhhmmss");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fileName = sfd.FileName;
                /*建立Excel对象*/
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                if (excel == null)
                {
                    MessageBox.Show("无法创建Excel对象,可能您的计算机未安装Excel!");
                    return false;
                }
                try
                {
                    excel.Application.Workbooks.Add(true);
                    excel.Visible = isShowExcel;
                    /*分析标题的跨列数*/
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        if (column.Visible == true)
                            titleColumnSpan++;
                    }
                    /*合并标题单元格*/
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                    //worksheet.get_Range("A1", "C10").Merge();            
                    worksheet.get_Range(worksheet.Cells[1, 1] as Range, worksheet.Cells[1, titleColumnSpan] as Range).Merge();
                    /*生成标题*/
                    excel.Cells[1, 1] = title;
                    (excel.Cells[1, 1] as Range).HorizontalAlignment = XlHAlign.xlHAlignCenter;//标题居中
                                                                                               //生成字段名称
                                                                                               /*生成表头数据*/
                    excel.Cells[2, 1] = "编码";
                    excel.Cells[3, 1] = "日期";
                    excel.Cells[2, 2] = num;
                    excel.Cells[3, 2] = date;

                    Range m_objRange = worksheet.get_Range("G2", m_objOpt);
                    m_objRange.Select();
                    float PicLeft, PicTop;
                    PicLeft = Convert.ToSingle(m_objRange.Left);
                    PicTop = Convert.ToSingle(m_objRange.Top);
                    filepath = filepath.Replace(@"\\", @"/");
                    worksheet.Shapes.AddPicture(filepath, Microsoft.Office.Core.MsoTriState.msoFalse,
                      Microsoft.Office.Core.MsoTriState.msoTrue, PicLeft, PicTop, 50, 50);
                    //@"C:/Users/Administrator/Documents/Visual Studio 2015/Projects/goods/goods/bin/Debug/image/20160624045711.jpg"
                    columnIndex = 1;
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (dgv.Columns[i].Visible == true)
                        {
                            excel.Cells[4, columnIndex] = dgv.Columns[i].HeaderText;
                            (excel.Cells[4, columnIndex] as Range).HorizontalAlignment = XlHAlign.xlHAlignCenter;//字段居中
                            columnIndex++;
                        }
                    }
                    //填充数据              
                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        columnIndex = 1;
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            if (dgv.Columns[j].Visible == true)
                            {
                                if (dgv[j, i].ValueType == typeof(string))
                                {
                                    excel.Cells[i + 5, columnIndex] = "'" + dgv[j, i].Value.ToString();
                                }
                                else
                                {
                                    excel.Cells[i + 5, columnIndex] = dgv[j, i].Value.ToString();
                                }
                                (excel.Cells[i + 5, columnIndex] as Range).HorizontalAlignment = XlHAlign.xlHAlignLeft;//字段居中
                                columnIndex++;
                            }
                        }
                    }
                    worksheet.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                catch { }
                finally
                {
                    excel.Quit();
                    excel = null;
                    GC.Collect();
                    Directory.Delete(path, true);
                }
                //KillProcess("Excel");
                return true;
            }
            else
            {
                return false;
            }
        }
        private object m_objOpt = System.Reflection.Missing.Value;
    }
}
