using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using goods.Model;
using goods.Controller;
namespace goods
{
    public partial class SalesMaterielSelect : Form
    {
        salesOrderCtrl ctrl = new salesOrderCtrl();

        public delegate void SalesMaterielEventHandler(object sender, SalesMaterielEventArgs e);
        public event SalesMaterielEventHandler AddMateriel;
        public SalesMaterielSelect()
        {
            InitializeComponent();
            loadTabel();
            loadData(1);
        }
        private void loadTabel()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.CellContentDoubleClick += DataGridView1_CellContentDoubleClick;

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "id";
            idColumn.Visible = false;
            idColumn.Name = "id";
            dataGridView1.Columns.Add(idColumn);

            DataGridViewTextBoxColumn orderNumColumn = new DataGridViewTextBoxColumn();
            orderNumColumn.HeaderText = "订单编号";
            orderNumColumn.DataPropertyName = "sonum";
            dataGridView1.Columns.Add(orderNumColumn);

            DataGridViewTextBoxColumn lineColumn = new DataGridViewTextBoxColumn();
            lineColumn.HeaderText = "序号";
            lineColumn.DataPropertyName = "line";
            dataGridView1.Columns.Add(lineColumn);

            DataGridViewTextBoxColumn numColumn = new DataGridViewTextBoxColumn();
            numColumn.HeaderText = "编号";
            numColumn.DataPropertyName = "num";
            dataGridView1.Columns.Add(numColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "name";
            nameColumn.HeaderText = "名称";
            dataGridView1.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn customerColumn = new DataGridViewTextBoxColumn();
            customerColumn.DataPropertyName = "customer";
            customerColumn.HeaderText = "客户";
            dataGridView1.Columns.Add(customerColumn);

            pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.textBox1.KeyDown += TextBox1_KeyDown; ;
            this.textBox2.KeyDown += TextBox1_KeyDown;
            this.textBox3.KeyDown += TextBox1_KeyDown;
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                AddMateriel(this, new SalesMaterielEventArgs(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value)));
                this.Close();
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { loadData(1); }
        }

        private void pageIndexChanged(object sender, EventArgs e)
        {
            loadData(pagingCom1.PageIndex);
        }

        private void loadData(int index)
        {
            pagingCom1.PageIndex = index;
            pagingCom1.PageSize = 10;
            var dtData = ctrl.get4Select(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text, textBox2.Text, textBox3.Text);
            dataGridView1.DataSource = dtData;
            pagingCom1.RecordCount = ctrl.getCount(textBox1.Text, textBox2.Text, textBox3.Text, false, new DateTime());
            pagingCom1.reSet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("请至少选择一条数据！", "提示");
                return;
            }
            else
            {
                AddMateriel(this, new SalesMaterielEventArgs(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value)));
                this.Close();
            }
        }
    }
}
