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
using goods.Model;
using Microsoft.VisualBasic;
namespace goods
{
    public partial class CustomerView : Form
    {
        customerCtrl ctrl = new customerCtrl();
        DataTable dt;
        public CustomerView()
        {
            InitializeComponent();
            initTable();
            loadData();
        }
        public void loadData()
        {
            BindDataWithPage(1);
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
            BindDataWithPage(1);
        }
        private void initTable()
        {
            this.textBox1.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "num";//字段
            colNum.HeaderText = "编号";
            colNum.Name = "num";
            dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";//字段
            colName.HeaderText = "名称";
            colName.Name = "name";
            dataGridView1.Columns.Add(colName);

        }
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            edit();
        }
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dt = ctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, this.textBox1.Text);
            dataGridView1.DataSource = dt;

            //获取并设置总记录数
            pagingCom1.RecordCount = ctrl.getCount(this.textBox1.Text);//Convert.ToInt32(dsData.Tables[1].Rows[0][0]);
            pagingCom1.reSet();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CustomerPopup view = new CustomerPopup(this);
            view.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) MessageBox.Show("请选中一行");
            else
            {
                edit();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageModel msg = ctrl.del(dataGridView1.SelectedRows[0].Cells["num"].Value);
                if (msg.Code == 0)
                {
                    DataRow[] dr = dt.Select("num='" + dataGridView1.SelectedRows[0].Cells["num"].Value.ToString() + "'");
                    dt.Rows.Remove(dr[0]);
                    this.dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }
        private void edit()
        {
            string str = Interaction.InputBox("请输入名称", "输入名称", dataGridView1.SelectedRows[0].Cells["name"].Value.ToString());
            if (str.Count() != 0)
            {
                CustomerModel cm = new CustomerModel(dataGridView1.SelectedRows[0].Cells["num"].Value.ToString(), str);
                MessageModel msg = ctrl.set(cm);
                if (msg.Code == 0)
                {
                    dataGridView1.SelectedRows[0].Cells["name"].Value = str;
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }
    }
}
