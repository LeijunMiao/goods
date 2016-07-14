using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goods.Controller;

namespace goods
{
    public partial class StockList : Form
    {
        stockCtrl ctrl = new stockCtrl();
        public StockList()
        {
            InitializeComponent();
            loadTabel();
            loadData(1);
        }
        private void loadTabel()
        {
            this.dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn numColumn = new DataGridViewTextBoxColumn();
            numColumn.HeaderText = "批次编号";
            numColumn.DataPropertyName = "batchTNum";
            dataGridView1.Columns.Add(numColumn);

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "id";
            idColumn.Visible = false;
            idColumn.Name = "id";
            dataGridView1.Columns.Add(idColumn);

            DataGridViewTextBoxColumn supColumn = new DataGridViewTextBoxColumn();
            supColumn.DataPropertyName = "supplier";
            supColumn.HeaderText = "供应商";
            dataGridView1.Columns.Add(supColumn);

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


            

            this.textBox1.KeyDown += button1_KeyDown;
            this.textBox2.KeyDown += button1_KeyDown;
            this.textBox3.KeyDown += button1_KeyDown;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);

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
            pagingCom1.PageSize = 10;
            var dtData = ctrl.getFilterListLimit(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text, textBox2.Text, textBox3.Text, new List<int>());

            dataGridView1.DataSource = dtData;
            pagingCom1.RecordCount = ctrl.getCount(textBox1.Text, textBox2.Text, textBox3.Text, new List<int>());
            pagingCom1.reSet();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData(1);
        }
    }
}
