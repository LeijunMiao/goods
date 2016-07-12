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
    public partial class SafetyStockList : Form
    {
        materielCtrl ctrl = new materielCtrl();
        public SafetyStockList()
        {
            InitializeComponent();
            initTable();
            loadData(1);
        }

        private void loadData(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            DataTable dt = ctrl.getStockWarning(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text);
            dt.Columns.Add("diff");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["stock"] == DBNull.Value) dt.Rows[i]["stock"] = 0;
                dt.Rows[i]["diff"] = Convert.ToDouble(dt.Rows[i]["stock"])  - Convert.ToDouble(dt.Rows[i]["safetystock"]);
            }
            dataGridView1.DataSource = dt;
            pagingCom1.RecordCount = ctrl.getStockWarningCount(textBox1.Text);
            pagingCom1.reSet();

        }
        private void initTable()
        {
            this.textBox1.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewColumn colId = new DataGridViewLinkColumn();
            colId.Visible = false;
            colId.Name = "Id";
            colId.DataPropertyName = "id";
            dataGridView1.Columns.Add(colId);

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.Name = "Num";
            colNum.HeaderText = "物料代码";
            colNum.ReadOnly = true;
            colNum.DataPropertyName = "num";
            dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.Name = "Name";
            colName.HeaderText = "物料名称";
            colName.ReadOnly = true;
            colName.DataPropertyName = "name";
            dataGridView1.Columns.Add(colName);

            DataGridViewColumn colMeter = new DataGridViewTextBoxColumn();
            colMeter.Name = "MeteringName";
            colMeter.HeaderText = "单位";
            colMeter.ReadOnly = true;
            colMeter.DataPropertyName = "metering";
            dataGridView1.Columns.Add(colMeter);

            DataGridViewColumn colSafety = new DataGridViewTextBoxColumn();
            colSafety.Name = "safetystock";
            colSafety.HeaderText = "安全库存";
            colSafety.DataPropertyName = "safetystock";
            dataGridView1.Columns.Add(colSafety);

            DataGridViewColumn colMax = new DataGridViewTextBoxColumn();
            colMax.Name = "maxstock";
            colMax.HeaderText = "最大库存量";
            colMax.DataPropertyName = "maxstock";
            dataGridView1.Columns.Add(colMax);

            DataGridViewColumn colStock = new DataGridViewTextBoxColumn();
            colStock.Name = "stock";
            colStock.HeaderText = "实时库存";
            colStock.DataPropertyName = "stock";
            dataGridView1.Columns.Add(colStock);

            DataGridViewColumn colDiff = new DataGridViewTextBoxColumn();
            colDiff.Name = "diff";
            colDiff.HeaderText = "差额";
            colDiff.DataPropertyName = "diff";
            dataGridView1.Columns.Add(colDiff);

        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { loadData(1); }
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            loadData(pagingCom1.PageIndex);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadData(1);
        }

    }
}
