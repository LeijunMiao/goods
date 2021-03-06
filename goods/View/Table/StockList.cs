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
using Observer;
using goods.Model;
namespace goods
{
    public partial class StockList : Form
    {
        stockCtrl ctrl = new stockCtrl();
        solidbackingCtrl sbCtrl = new solidbackingCtrl();
        public int category;
        public StockList()
        {
            InitializeComponent();
            MidModule.EventSend += new MsgDlg(MidModule_EventSend);
            loadTabel();
            loadData(1);
        }
        private void MidModule_EventSend(object sender, object msg)
        {
            if (sender != null)
            {
                SupplierModel sm = (SupplierModel)msg;
                this.textBox4.Text = sm.Name;
            }
        }
        private void loadTabel()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.CellMouseEnter += DataGridView1_CellMouseEnter;

            DataGridViewTextBoxColumn supColumn = new DataGridViewTextBoxColumn();
            supColumn.DataPropertyName = "supplier";
            supColumn.HeaderText = "供应商";
            dataGridView1.Columns.Add(supColumn);


            DataGridViewTextBoxColumn numColumn = new DataGridViewTextBoxColumn();
            numColumn.HeaderText = "批次编号";
            numColumn.DataPropertyName = "batchTNum";
            dataGridView1.Columns.Add(numColumn);

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "id";
            idColumn.Visible = false;
            idColumn.Name = "id";
            dataGridView1.Columns.Add(idColumn);

            DataGridViewTextBoxColumn mnumColumn = new DataGridViewTextBoxColumn();
            mnumColumn.DataPropertyName = "num";
            mnumColumn.HeaderText = "物料编码";
            dataGridView1.Columns.Add(mnumColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "name";
            nameColumn.HeaderText = "物料名称";
            dataGridView1.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn wnameColumn = new DataGridViewTextBoxColumn();
            wnameColumn.DataPropertyName = "wname";
            wnameColumn.HeaderText = "仓库";
            dataGridView1.Columns.Add(wnameColumn);

            DataGridViewTextBoxColumn pnameColumn = new DataGridViewTextBoxColumn();
            pnameColumn.DataPropertyName = "pname";
            pnameColumn.HeaderText = "仓位";
            dataGridView1.Columns.Add(pnameColumn);

            DataGridViewTextBoxColumn avaColumn = new DataGridViewTextBoxColumn();
            avaColumn.DataPropertyName = "avaquantity";
            avaColumn.HeaderText = "可用库存";
            dataGridView1.Columns.Add(avaColumn);

            DataGridViewTextBoxColumn combColumn = new DataGridViewTextBoxColumn();
            combColumn.DataPropertyName = "combination";
            combColumn.Name = "combination";
            combColumn.Visible = false;
            dataGridView1.Columns.Add(combColumn);

            DataGridViewTextBoxColumn avColumn = new DataGridViewTextBoxColumn();
            avColumn.DataPropertyName = "attrvalue";
            avColumn.HeaderText = "辅助属性";
            avColumn.Name = "attrvalue";
            dataGridView1.Columns.Add(avColumn);

            DataGridViewTextBoxColumn ctgyColumn = new DataGridViewTextBoxColumn();
            ctgyColumn.DataPropertyName = "category";
            ctgyColumn.HeaderText = "分类";
            ctgyColumn.Name = "category";
            dataGridView1.Columns.Add(ctgyColumn);

            this.textBox1.KeyDown += button1_KeyDown;
            this.textBox2.KeyDown += button1_KeyDown;
            this.textBox3.KeyDown += button1_KeyDown;
            this.textBox4.KeyDown += button1_KeyDown;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.pagingCom1.PageIndexChanged += new pagingCom.EventHandler(this.pageIndexChanged);

        } 
        private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            loadData(pagingCom1.PageIndex);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { loadData(1); }
        }
        private void loadData(int index)
        {
            pagingCom1.PageIndex = index;
            pagingCom1.PageSize = 20;
            var dts = ctrl.getFilterListLimit(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text, textBox2.Text, textBox3.Text, this.textBox4.Text, category, new List<int>());
            var dtData = dts[0];
            dtData.Columns.Add("attrvalue");
            List<int> ids = new List<int>();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (dtData.Rows[i]["combination"] != DBNull.Value) ids.Add(Convert.ToInt32(dtData.Rows[i]["combination"]));
            }

            dataGridView1.DataSource = dtData;
            Dictionary<int, string> map = sbCtrl.getbyCombIds(ids);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells["combination"].Value != DBNull.Value)
                {
                    var comb = Convert.ToInt32(dataGridView1.Rows[i].Cells["combination"].Value);

                    if (map.Keys.Contains(comb))
                    {
                        dataGridView1.Rows[i].Cells["attrvalue"].Value = map[comb];
                    }
                }
            }
            if (dts[1].Rows[0][0] != DBNull.Value) pagingCom1.RecordCount = Convert.ToInt32(dts[1].Rows[0][0]);
            else pagingCom1.RecordCount = 0;
            pagingCom1.reSet();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SupplierSelect ss = new SupplierSelect();
            ss.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CategorySelect view = new CategorySelect();
            view.CategorySet += View_CategorySet;
            view.Show();
        }
        private void View_CategorySet(object sender, CategoryEventArgs e)
        {
            this.textBox5.Text = e.name;
            this.category = e.id;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            this.category = -1;
        }
    }
}
