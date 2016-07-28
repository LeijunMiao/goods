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
    public partial class MaterielSolidBacking : Form
    {
        solidbackingCtrl ctrl = new solidbackingCtrl();
        int mid = -1;
        public MaterielSolidBacking(int id,string name)
        {
            InitializeComponent();
            mid = id;
            this.label1.Text = name;
            initPage();
            loadData();
        }

        private void initPage()
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;

            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnHeadersVisible = false;


            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;

            
            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.FillWeight = 20;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);
            DataGridViewColumn colId2 = new DataGridViewTextBoxColumn();
            colId2.DataPropertyName = "id";
            colId2.Visible = false;
            colId2.FillWeight = 20;
            colId2.Name = "id";
            dataGridView2.Columns.Add(colId2);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            dataGridView1.Columns.Add(colName);
            DataGridViewColumn colName2 = new DataGridViewTextBoxColumn();
            colName2.DataPropertyName = "name";
            dataGridView2.Columns.Add(colName2);

        }
        private void loadData()
        {
            DataTable dt = ctrl.getMaterielSolidBacking(mid);
            DataTable dtM = dt.Clone();
            DataTable dtO = dt.Clone();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["msid"] != DBNull.Value)
                {
                    DataRow newrow = dtM.NewRow();
                    newrow.ItemArray = dt.Rows[i].ItemArray;
                    dtM.Rows.Add(newrow);
                }
                else
                {
                    DataRow newrow = dtO.NewRow();
                    newrow.ItemArray = dt.Rows[i].ItemArray;
                    dtO.Rows.Add(newrow);
                }
            }
            dataGridView1.DataSource = dtM;
            dataGridView2.DataSource = dtO;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                MaterSolidBackingM msb = new MaterSolidBackingM(mid, Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value));
                MessageModel m = ctrl.delMaterielSolidBacking(msb);
                if (m.Code == 0)
                {
                    loadData();
                }
                else MessageBox.Show(m.Msg);
            }
            else
            {
                MessageBox.Show("请选择属性。");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                MaterSolidBackingM msb = new MaterSolidBackingM(mid, Convert.ToInt32(dataGridView2.CurrentRow.Cells["id"].Value));
                MessageModel m = ctrl.addMaterielSolidBacking(msb);
                if (m.Code == 0)
                {
                    loadData();
                }
                else MessageBox.Show(m.Msg);
            }
            else
            {
                MessageBox.Show("请选择属性。");
            }

        }
    }
}
