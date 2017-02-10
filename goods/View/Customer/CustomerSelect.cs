using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using goods.Controller;
using goods.Model;

namespace goods
{
    public partial class CustomerSelect : Form
    {
        customerCtrl ctrl = new customerCtrl();
        public delegate void CustomerEventHandler(object sender, CustomerEventArgs e);
        public event CustomerEventHandler CustomerSet;
        public CustomerSelect()
        {
            InitializeComponent();
            initTable();
            search(1);
        }
        private void initTable()
        {
            pagingCom1.PageIndexChanged += PagingCom1_PageIndexChanged;
            textBox1.KeyDown += TextBox1_KeyDown;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.CellClick += DataGridView1_CellClick;

            DataGridViewColumn colNum = new DataGridViewLinkColumn();
            colNum.DataPropertyName = "num";
            colNum.Visible = false;
            colNum.Name = "num";
            dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewLinkColumn();
            colName.DataPropertyName = "name";
            colName.HeaderText = "名称";
            colName.Name = "name";
            dataGridView1.Columns.Add(colName);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                CustomerSet(this, new CustomerEventArgs(this.dataGridView1.Rows[e.RowIndex].Cells["num"].Value.ToString(), this.dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString()));
                this.Close();
            }
        }

        private void PagingCom1_PageIndexChanged(object sender, EventArgs e)
        {
            search(pagingCom1.PageIndex);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { search(1); }
        }
        private void search(int index)
        {
            pagingCom1.PageIndex = index;
            pagingCom1.PageSize = 10;
            var dtData = ctrl.getSelect(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text);

            //获取并设置总记录数
            pagingCom1.RecordCount = ctrl.getSelectCount(textBox1.Text);

            dataGridView1.DataSource = dtData;
            dataGridView1.ClearSelection();
            pagingCom1.reSet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            search(1);
        }
    }
}
