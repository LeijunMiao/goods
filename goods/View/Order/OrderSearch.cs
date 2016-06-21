﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Observer;
using goods.Controller;
using goods.Model;
namespace goods
{
    public partial class OrderSearch : Form
    {
        orderCtrl ctrl = new orderCtrl();
        string supplier = "";
        private DataTable dtData = null;
        private DataTable dt = null;
        public OrderSearch()
        {
            InitializeComponent();
            MidModule.EventSend += new MsgDlg(MidModule_EventSend);
            initPage();
        }
        private void initPage()
        {
            this.textBox1.KeyDown += button1_KeyDown;
            this.textBox2.KeyDown += button1_KeyDown;
            this.textBox3.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.dataGridView1.ReadOnly = true;
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { BindDataWithPage(1); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SupplierSelect ss = new SupplierSelect();
            ss.Show();
        }

        private void MidModule_EventSend(object sender, object msg)
        {
            if (sender != null)
            {
                SupplierModel sm = (SupplierModel)msg;
                this.textBox3.Text = sm.Name;
                this.supplier = sm.Name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getFilterOrderMateriel(pagingCom1.PageIndex, pagingCom1.PageSize, this.textBox3.Text, this.dateTimePicker1.Checked,dateTimePicker1.Value.Date, textBox1.Text, textBox2.Text);
            dt = new DataTable();
            DataColumn dcNum = new DataColumn("单据编码");
            DataColumn dcDate = new DataColumn("日期");
            DataColumn dcSup = new DataColumn("供应商");
            DataColumn dcMNum = new DataColumn("物料代码");
            DataColumn dcMName = new DataColumn("物料名称");
            DataColumn dcSep = new DataColumn("规格型号");
            DataColumn dcMete = new DataColumn("单位");
            DataColumn dcPrice = new DataColumn("单价");
            DataColumn dcQuantity = new DataColumn("数量");
            DataColumn dcTaxPrice = new DataColumn("含税单价");
            DataColumn dcAmount = new DataColumn("金额");
            DataColumn dcTax = new DataColumn("税率");
            DataColumn dcTaxAmount = new DataColumn("税额");
            DataColumn dcAll = new DataColumn("价税合计");
            DataColumn dcDeliveryDate = new DataColumn("交货日期");
            DataColumn dcStatus = new DataColumn("状态");
            DataColumn dcId = new DataColumn("ID");
            DataColumn dcmId = new DataColumn("mid");
            DataColumn dcInGoods = new DataColumn("入库数量");
            DataColumn dcNotInGoods = new DataColumn("未入库数量");
            DataColumn[] list_dc = { dcNum, dcDate, dcSup, dcMNum, dcMName, dcSep, dcMete, dcPrice,dcQuantity, dcTaxPrice,
                dcAmount, dcTax, dcTaxAmount, dcAll, dcDeliveryDate, dcStatus,dcId,dcmId,dcInGoods,dcNotInGoods };
            dt.Columns.AddRange(list_dc);

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["ponum"].ToString();
                dr[1] = DateTime.Parse(dtData.Rows[i]["date"].ToString()).ToString("yyyy/M/d"); 
                dr[2] = dtData.Rows[i]["sname"].ToString();
                dr[3] = dtData.Rows[i]["mnum"].ToString();
                dr[4] = dtData.Rows[i]["mname"].ToString();
                dr[5] = dtData.Rows[i]["specifications"].ToString();
                dr[6] = dtData.Rows[i]["metering"].ToString();
                dr[7] = dtData.Rows[i]["price"].ToString();
                dr[8] = dtData.Rows[i]["quantity"].ToString();
                dr[9] = Convert.ToDouble(dtData.Rows[i]["price"]) * (1+Convert.ToDouble(dtData.Rows[i]["tax"]));
                dr[10] = dtData.Rows[i]["amount"].ToString();
                dr[11] = dtData.Rows[i]["tax"].ToString();
                dr[12] = Convert.ToDouble(dtData.Rows[i]["amount"])* Convert.ToDouble(dtData.Rows[i]["tax"]);
                dr[13] = Convert.ToDouble(dtData.Rows[i]["amount"]) * (1 + Convert.ToDouble(dtData.Rows[i]["tax"]));
                dr[14] = DateTime.Parse( dtData.Rows[i]["deliveryDate"].ToString()).ToString("yyyy/M/d");
                if (dtData.Rows[i]["quantityAll"] == DBNull.Value) dtData.Rows[i]["quantityAll"] = 0;
                var diff = Convert.ToDouble(dtData.Rows[i]["quantity"]) - Convert.ToDouble(dtData.Rows[i]["quantityAll"]);
                if (diff <= 0) dr[15] = "关闭";
                else dr[15] = "激活";
                dr[16] = dtData.Rows[i]["id"].ToString();
                dr[17] = dtData.Rows[i]["mid"].ToString();
                dr[18] = dtData.Rows[i]["quantityAll"].ToString();
                dr[19] = diff;

                dt.Rows.Add(dr);
            }


            dataGridView1.DataSource = dt;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;

            pagingCom1.RecordCount = ctrl.getCount(this.textBox3.Text, this.dateTimePicker1.Checked, dateTimePicker1.Value.Date, textBox1.Text, textBox2.Text);
            pagingCom1.reSet();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            List<parmas> list_p = new List<parmas>();
            List<string> uuids = new List<string>();
            if(this.dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    list_p.Add(new parmas(Convert.ToInt32( dataGridView1.SelectedRows[i].Cells["id"].Value),
                        Convert.ToInt32(dataGridView1.SelectedRows[i].Cells["mid"].Value),
                        dataGridView1.SelectedRows[i].Cells["物料代码"].Value.ToString()));
                }
                uuids = ctrl.updateBatch(list_p);

            }
            QRCodeList pop = new QRCodeList(uuids);
            pop.Show();


        }
    }
}
