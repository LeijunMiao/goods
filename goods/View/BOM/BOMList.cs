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
    public partial class BOMList : Form
    {
        bomCtrl ctrl = new bomCtrl();
        public BOMList()
        {
            InitializeComponent();
            initPage();
            loadData(1);

        }
        private void initPage()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.CellMouseDoubleClick += DataGridView1_CellMouseDoubleClick;
            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            this.dataGridView1.Columns.Add(colId);
            DataGridViewColumn colbomid = new DataGridViewTextBoxColumn();
            colbomid.DataPropertyName = "bomid";
            colbomid.Visible = false;
            colbomid.Name = "bomid";
            this.dataGridView1.Columns.Add(colbomid);
            

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "num";
            colNum.Name = "num";
            colNum.HeaderText = "BOM编号";
            this.dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colMNum = new DataGridViewTextBoxColumn();
            colMNum.DataPropertyName = "mnum";
            colMNum.Name = "mnum";
            colMNum.HeaderText = "物料编号";
            this.dataGridView1.Columns.Add(colMNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            colName.Name = "name";
            colName.HeaderText = "名称";
            this.dataGridView1.Columns.Add(colName);

            DataGridViewColumn colSep = new DataGridViewTextBoxColumn();
            colSep.DataPropertyName = "specifications";
            colSep.Name = "specifications";
            colSep.HeaderText = "规格参数";
            this.dataGridView1.Columns.Add(colSep);

            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "status";
            colStatus.Name = "status";
            colStatus.HeaderText = "状态";
            this.dataGridView1.Columns.Add(colStatus);

            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.textBox1.KeyDown += TextBox1_KeyDown;
            //DataGridViewColumn colMetering = new DataGridViewTextBoxColumn();
            //colMetering.DataPropertyName = "metering";
            //colMetering.Name = "metering";
            //colMetering.HeaderText = "计量单位";
            //this.dataGridView1.Columns.Add(colMetering);
        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BOMView view = new BOMView(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["bomid"].Value));
                if(!view.IsDisposed)view.Show();
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
        private void loadData(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            DataTable dt =  ctrl.get(pagingCom1.PageIndex, pagingCom1.PageSize, this.textBox1.Text);
            dataGridView1.DataSource = dt;
            pagingCom1.RecordCount = ctrl.getCount(this.textBox1.Text);
            pagingCom1.reSet();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CreateBOM view = new CreateBOM();
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
                BOMEdit view = new BOMEdit(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bomid"].Value));
                view.Show();

            }
        }
    }
}
