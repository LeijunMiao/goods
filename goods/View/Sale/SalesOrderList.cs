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
    public partial class SalesOrderList : Form
    {
        salesOrderCtrl ctrl = new salesOrderCtrl();
        public SalesOrderList()
        {
            InitializeComponent();
            initPage();
            BindDataWithPage(1);
        }
        private void initPage()
        {
            this.textBox1.KeyDown += button1_KeyDown;
            this.textBox2.KeyDown += button1_KeyDown;
            this.textBox4.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AutoGenerateColumns = false;

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;

            DataGridViewColumn coldeliveryDate = new DataGridViewTextBoxColumn();
            coldeliveryDate.HeaderText = "交货日期";
            coldeliveryDate.Name = "deliveryDate";
            coldeliveryDate.DataPropertyName = "deliveryDate";
            this.dataGridView1.Columns.Add(coldeliveryDate);

            DataGridViewColumn colsonum = new DataGridViewTextBoxColumn();
            colsonum.HeaderText = "订单编号";
            colsonum.Name = "sonum";
            colsonum.DataPropertyName = "sonum";
            this.dataGridView1.Columns.Add(colsonum);

            DataGridViewColumn colcnum = new DataGridViewTextBoxColumn();
            colcnum.HeaderText = "客户编号";
            colcnum.Name = "cnum";
            colcnum.DataPropertyName = "cnum";
            this.dataGridView1.Columns.Add(colcnum);

            DataGridViewColumn colcname = new DataGridViewTextBoxColumn();
            colcname.HeaderText = "客户名称";
            colcname.Name = "cname";
            colcname.DataPropertyName = "cname";
            this.dataGridView1.Columns.Add(colcname);

            DataGridViewColumn colmnum = new DataGridViewTextBoxColumn();
            colmnum.HeaderText = "物料编号";
            colmnum.Name = "mnum";
            colmnum.DataPropertyName = "mnum";
            this.dataGridView1.Columns.Add(colmnum);

            DataGridViewColumn colmname = new DataGridViewTextBoxColumn();
            colmname.HeaderText = "物料名称";
            colmname.Name = "mname";
            colmname.DataPropertyName = "mname";
            this.dataGridView1.Columns.Add(colmname);

            DataGridViewColumn colmeter = new DataGridViewTextBoxColumn();
            colmeter.HeaderText = "单位";
            colmeter.Name = "metering";
            colmeter.DataPropertyName = "metering";
            this.dataGridView1.Columns.Add(colmeter);

            DataGridViewColumn colquantity = new DataGridViewTextBoxColumn();
            colquantity.HeaderText = "数量";
            colquantity.Name = "quantity";
            colquantity.DataPropertyName = "quantity";
            this.dataGridView1.Columns.Add(colquantity);

            DataGridViewColumn colprice = new DataGridViewTextBoxColumn();
            colprice.HeaderText = "单价";
            colprice.Name = "price";
            colprice.DataPropertyName = "price";
            this.dataGridView1.Columns.Add(colprice);

            DataGridViewColumn coltaxprice = new DataGridViewTextBoxColumn();
            coltaxprice.DefaultCellStyle = dataGridViewCellStyle3;
            coltaxprice.HeaderText = "含税单价";
            coltaxprice.Name = "taxprice";
            coltaxprice.DataPropertyName = "taxprice";
            this.dataGridView1.Columns.Add(coltaxprice);

            DataGridViewColumn colamount = new DataGridViewTextBoxColumn();
            colamount.HeaderText = "金额";
            colamount.Name = "amount";
            colamount.DataPropertyName = "amount";
            this.dataGridView1.Columns.Add(colamount);

            DataGridViewColumn colall = new DataGridViewTextBoxColumn();
            colall.DefaultCellStyle = dataGridViewCellStyle3;
            colall.HeaderText = "价税合计";
            colall.Name = "allamount";
            colall.DataPropertyName = "allamount";
            this.dataGridView1.Columns.Add(colall);

            DataGridViewColumn colstatus = new DataGridViewTextBoxColumn();
            colstatus.HeaderText = "状态";
            colstatus.Name = "status";
            //colstatus.DataPropertyName = "status";
            this.dataGridView1.Columns.Add(colstatus);

            DataGridViewColumn colout = new DataGridViewTextBoxColumn();
            colout.HeaderText = "出库数量";
            colout.Name = "out";
            //colout.DataPropertyName = "out";
            this.dataGridView1.Columns.Add(colout);

            DataGridViewColumn colneout = new DataGridViewTextBoxColumn();
            colneout.HeaderText = "未出库数量";
            colneout.Name = "neout";
            //colneout.DataPropertyName = "neout";
            this.dataGridView1.Columns.Add(colneout);

            DataGridViewColumn colsoid = new DataGridViewTextBoxColumn();
            colsoid.Name = "soid";
            colsoid.Visible = false;
            colsoid.DataPropertyName = "soid";
            this.dataGridView1.Columns.Add(colsoid);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { BindDataWithPage(1); }
        }
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            DataTable dt = ctrl.getFilterOrderMateriel(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text, textBox2.Text,
                textBox4.Text, dateTimePicker1.Checked, dateTimePicker1.Value.Date );
            dataGridView1.DataSource = dt;
            pagingCom1.RecordCount = ctrl.getCount(textBox1.Text, textBox2.Text,
                textBox4.Text, dateTimePicker1.Checked, dateTimePicker1.Value.Date);
            pagingCom1.reSet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                SalesOrderView view = new SalesOrderView(Convert.ToInt32(dataGridView1.CurrentRow.Cells["soid"].Value));
                view.Show();
            }
            else
            {
                MessageBox.Show("请选择一项。");
            }
        }
    }
}
