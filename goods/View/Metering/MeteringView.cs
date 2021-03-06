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
namespace goods
{
    public partial class MeteringView : Form
    {
        meteringCtrl ctrl = new meteringCtrl();
        private DataTable dtData = null;
        public MeteringView()
        {
            InitializeComponent();
            initPage();
        }
        public void initPage()
        {
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = false;

            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.textBox1.KeyDown += button1_KeyDown;


            DataGridViewColumn colIsActive = new DataGridViewLinkColumn();
            colIsActive.DataPropertyName = "isActive";//字段
            colIsActive.Visible = false;
            colIsActive.Name = "isActive";
            dataGridView1.Columns.Add(colIsActive);

            DataGridViewColumn colId = new DataGridViewLinkColumn();
            colId.DataPropertyName = "id";//字段
            colId.Visible = false;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "num";//字段
            colNum.Name = "num";
            colNum.HeaderText = "编号";
            dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";//字段
            colName.Name = "name";
            colName.HeaderText = "名称";
            dataGridView1.Columns.Add(colName);

            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "status";//字段
            colStatus.Name = "status";
            colStatus.HeaderText = "状态";
            dataGridView1.Columns.Add(colStatus);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        public void loadData()
        {
            BindDataWithPage(1);
        }
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text);

            dtData.Columns.Add("status");
            for (int i = 0; i < dtData.Rows.Count; i++)
            {

                if (Convert.ToBoolean(dtData.Rows[i]["isActive"])) dtData.Rows[i]["status"] = "激活";
                else dtData.Rows[i]["status"] = "注销";
            }

            dataGridView1.DataSource = dtData;

            pagingCom1.RecordCount = ctrl.getCount(textBox1.Text);
            pagingCom1.reSet();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MeteringPopup popup = new MeteringPopup(this,null);
            popup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { BindDataWithPage(1); }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                MeteringModel s = new MeteringModel(Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["id"].Value), this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["num"].Value.ToString(), this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["name"].Value.ToString());
                MeteringPopup popup = new MeteringPopup(this, s);
                popup.Show();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                var cellIsActive = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["isActive"].Value;
                var cellId = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["id"].Value;
                MessageModel msg = ctrl.switchStatus(Convert.ToInt32(cellId), Convert.ToBoolean(cellIsActive));
                if (msg.Code != 0)
                {
                    MessageBox.Show(msg.Msg);
                }
                else
                {
                    if (Convert.ToBoolean(cellIsActive))
                    {
                        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["status"].Value = "注销";

                    }

                    else
                    {
                        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["status"].Value = "激活";
                    }
                    this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["isActive"].Value = !Convert.ToBoolean(cellIsActive);

                }
            }
        }
    }
}
