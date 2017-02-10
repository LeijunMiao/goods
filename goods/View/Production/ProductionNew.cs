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
namespace goods { 
    public partial class ProductionNew : Form
    {
        materielCtrl mctrl = new materielCtrl();
        salesOrderCtrl sctrl = new salesOrderCtrl();
        int materal = -1;
        public ProductionNew()
        {
            InitializeComponent();
            initPage();
        }

        private void initPage()
        {
            dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
            dateTimePicker2.ValueChanged += DateTimePicker2_ValueChanged;

            dataGridView1.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "id";
            idColumn.Visible = false;
            idColumn.Name = "id";
            dataGridView1.Columns.Add(idColumn);

            DataGridViewTextBoxColumn numColumn = new DataGridViewTextBoxColumn();
            numColumn.HeaderText = "物料编号";
            numColumn.DataPropertyName = "dtlnum";
            numColumn.Name = "dtlnum";
            dataGridView1.Columns.Add(numColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.HeaderText = "物料名称";
            nameColumn.DataPropertyName = "dtlname";
            nameColumn.Name = "dtlname";
            dataGridView1.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn speColumn = new DataGridViewTextBoxColumn();
            speColumn.HeaderText = "规格型号";
            speColumn.DataPropertyName = "specifications";
            speColumn.Name = "specifications";
            dataGridView1.Columns.Add(speColumn);

            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn();
            typeColumn.HeaderText = "物料属性";
            typeColumn.DataPropertyName = "type";
            typeColumn.Name = "type";
            dataGridView1.Columns.Add(typeColumn);

            DataGridViewTextBoxColumn meterColumn = new DataGridViewTextBoxColumn();
            meterColumn.HeaderText = "计量单位";
            meterColumn.DataPropertyName = "dtlme";
            meterColumn.Name = "dtlme";
            dataGridView1.Columns.Add(meterColumn);

            DataGridViewTextBoxColumn issuedqtyColumn = new DataGridViewTextBoxColumn();
            issuedqtyColumn.HeaderText = "应发数量";
            issuedqtyColumn.DataPropertyName = "issuedqty";
            issuedqtyColumn.Name = "issuedqty";
            dataGridView1.Columns.Add(issuedqtyColumn);

            DataGridViewTextBoxColumn actualqtyColumn = new DataGridViewTextBoxColumn();
            actualqtyColumn.HeaderText = "实发数量";
            actualqtyColumn.DataPropertyName = "actualqty";
            actualqtyColumn.Name = "actualqty";
            dataGridView1.Columns.Add(actualqtyColumn);

            DataGridViewButtonColumn supColumn = new DataGridViewButtonColumn();
            supColumn.HeaderText = "供应商";
            supColumn.Name = "supplier";
            supColumn.DefaultCellStyle.NullValue = "空";
            dataGridView1.Columns.Add(supColumn);

            DataGridViewButtonColumn colattbtn = new DataGridViewButtonColumn();
            colattbtn.Name = "solidbacking";
            colattbtn.HeaderText = "辅助属性";
            colattbtn.DefaultCellStyle.NullValue = "空";
            dataGridView1.Columns.Add(colattbtn);

            DataGridViewButtonColumn nextColumn = new DataGridViewButtonColumn();
            nextColumn.HeaderText = "生成下级计划";
            nextColumn.DefaultCellStyle.NullValue = "生成";
            dataGridView1.Columns.Add(nextColumn);
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("“计划完工时间”不能早于“计划开工时间”!");
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value < DateTime.Now)
            {
                MessageBox.Show("计划开工时间不能早于当前服务器时间!");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var args = new MaterielLookUpArgs(false, new List<int>());
            args.type = "自制件";
            OrderMaterielPopup popup = new OrderMaterielPopup(args);
            popup.AddMateriel += Popup_AddMateriel;
            popup.Show();
        }
        private void Popup_AddMateriel(object sender, MaterielEventArgs e)
        {
            if (e.ids.Count > 0)
            {
                var dtData = mctrl.getByids4BOM(e.ids);
                materal = e.ids[0];
                this.textBox1.Text = dtData.Rows[0]["num"].ToString();
                this.textBox2.Text = dtData.Rows[0]["name"].ToString();
                this.textBox3.Text = dtData.Rows[0]["metering"].ToString();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SalesMaterielSelect view = new SalesMaterielSelect();
            view.AddMateriel += View_AddMateriel;
            view.Show();
        }

        private void View_AddMateriel(object sender, SalesMaterielEventArgs e)
        {
            if(e.id > 0)
            {
                DataTable dt = sctrl.getbySalesMaterielId(e.id);
                this.textBox1.Text = dt.Rows[0]["num"].ToString();
                this.textBox2.Text = dt.Rows[0]["name"].ToString();
                this.textBox3.Text = dt.Rows[0]["metering"].ToString();
                this.textBox4.Text = dt.Rows[0]["quantity"].ToString();
                this.textBox5.Text = dt.Rows[0]["customer"].ToString();
                this.textBox6.Text = dt.Rows[0]["line"].ToString();
                this.textBox7.Text = dt.Rows[0]["sonum"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                }
                if (dt.Rows[0]["bomid"] != DBNull.Value) dataGridView1.DataSource = dt;
            }
        }
    }
}
