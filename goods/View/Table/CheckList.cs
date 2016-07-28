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
    public partial class CheckList : Form
    {
        stockCtrl ctrl = new stockCtrl();
        public CheckList()
        {
            InitializeComponent();
            initTable();
            loadData(1);
        }
        private void initTable()
        {
            this.textBox1.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn numColumn = new DataGridViewTextBoxColumn();
            numColumn.HeaderText = "编号";
            numColumn.DataPropertyName = "num";
            numColumn.Name = "num";
            dataGridView1.Columns.Add(numColumn);

            DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.HeaderText = "日期";
            dateColumn.DataPropertyName = "date";
            dataGridView1.Columns.Add(dateColumn);

            DataGridViewTextBoxColumn userColumn = new DataGridViewTextBoxColumn();
            userColumn.HeaderText = "制单人";
            userColumn.DataPropertyName = "user";
            dataGridView1.Columns.Add(userColumn);

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.HeaderText = "状态";
            statusColumn.DataPropertyName = "status";
            statusColumn.Name = "status";
            dataGridView1.Columns.Add(statusColumn);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            loadData(pagingCom1.PageIndex);
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { loadData(1); }
        }
        public void loadData(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            DataTable dt = ctrl.getCheckList(pagingCom1.PageIndex, pagingCom1.PageSize,textBox1.Text, dateTimePicker1.Value.Date
                , dateTimePicker1.Checked);
            dataGridView1.DataSource = dt;

            pagingCom1.RecordCount = ctrl.getCheckListCount(textBox1.Text, dateTimePicker1.Value.Date
                , dateTimePicker1.Checked);
            pagingCom1.reSet();

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CreateCheckList view = new CreateCheckList();
            view.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData(1);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                CheckListView view = new CheckListView(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["num"].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["status"].Value.ToString());
                view.Show();
            }
            else
            {
                MessageBox.Show("请选择一行！");
            }
            

        }
    }
}
