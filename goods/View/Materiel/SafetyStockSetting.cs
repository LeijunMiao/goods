using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goods.Model;
using goods.Controller;
using System.Data;
namespace goods
{
    public partial class SafetyStockSetting : Form
    {
        materielCtrl ctrl = new materielCtrl();
        public SafetyStockSetting(List<int> ids)
        {
            InitializeComponent();
            initTable();
            loadData(ids);
        }

        private void loadData(List<int> ids)
        {
            DataTable dt =  ctrl.getStock(ids);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void initTable()
        {
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.CurrentCell = null;
                List<MaterielModel> list_m = new List<MaterielModel>();
                for (int i = this.dataGridView1.RowCount; i > 0; i--)
                {
                    MaterielModel model = new MaterielModel(Convert.ToInt32(this.dataGridView1.Rows[i - 1].Cells["id"].Value),
                        Math.Round(Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["safetystock"].Value), 2),
                        Math.Round(Convert.ToDouble(this.dataGridView1.Rows[i - 1].Cells["maxstock"].Value), 2));
                    if (model.safetystock == 0 || model.maxstock == 0)
                    {
                        MessageBox.Show("物料缺失数量！");
                        return;
                    }

                    list_m.Add(model);
                }

                var msg = ctrl.addstock(list_m);
                if (msg.Code == 0)
                {
                    MessageBox.Show(msg.Msg);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
