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
using Observer;
namespace goods
{
    public partial class OrderMaterielPopup : Form
    {
        materielCtrl mctrl = new materielCtrl();
        //List<int> parentIds;
        MaterielLookUpArgs args;
        public int category;
        List<int> list_selected = new List<int>();

        public delegate void MaterielEventHandler(object sender, MaterielEventArgs e);
        public event MaterielEventHandler AddMateriel;

        public OrderMaterielPopup(MaterielLookUpArgs args)
        {
            this.args = args;
            //parentIds = args.ids;
            InitializeComponent();
            loadTabel();
            loadData(1);
        }

        private void loadTabel()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
            newColumn.HeaderText = "选择";
            newColumn.FillWeight = 20;
            newColumn.Name = "ck";
            dataGridView1.Columns.Add(newColumn);

            DataGridViewTextBoxColumn numColumn = new DataGridViewTextBoxColumn();
            numColumn.HeaderText = "编号";
            numColumn.DataPropertyName = "num";
            dataGridView1.Columns.Add(numColumn);

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "id";
            idColumn.Visible = false;
            idColumn.Name = "id";
            dataGridView1.Columns.Add(idColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "name";
            nameColumn.HeaderText = "名称";
            dataGridView1.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn speColumn = new DataGridViewTextBoxColumn();
            speColumn.DataPropertyName = "specifications";
            speColumn.HeaderText = "规格参数";
            dataGridView1.Columns.Add(speColumn);
            if (!args.multi)
            {
                this.dataGridView1.MultiSelect = args.multi;
                newColumn.Visible = false;
            }
        }
        private void loadData(int index)
        {
            pagingCom1.PageIndex = index;
            pagingCom1.PageSize = 10;
            var dtData = mctrl.getFilterListLimit(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text, category, args);
            dataGridView1.DataSource = dtData;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if(list_selected.Contains(Convert.ToInt32(dataGridView1.Rows[i].Cells["id"].Value)))
                    dataGridView1.Rows[i].Cells["ck"].Value = true;
            }
            pagingCom1.RecordCount = mctrl.getCount(textBox1.Text, args);
            pagingCom1.reSet();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadData(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //List<int> ids = new List<int> { Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value) };
                //parentForm.renderMateriel(ids);
                if (!args.multi)
                {
                    AddMateriel(this, new MaterielEventArgs(new List<int> { Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value)}));
                }
                else
                {
                    list_selected.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value));
                    MidModule.SendIds(this, list_selected);//发送参数值
                    if (AddMateriel != null) AddMateriel(this, new MaterielEventArgs(list_selected));
                }
                this.Close();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var ischeck = !Convert.ToBoolean(this.dataGridView1[0, e.RowIndex].Value);
                this.dataGridView1[0, e.RowIndex].Value = ischeck;
                if(ischeck == true)
                {
                    list_selected.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value));
                }
                else
                {
                    list_selected.Remove(list_selected.Where(p => p == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value)).FirstOrDefault());
                }
                //MessageBox.Show(e.RowIndex+"" + this.dataGridView1[0, e.RowIndex].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //List<int> ids = new List<int>(); 
            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    if (dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
            //    {
            //        ids.Add(Convert.ToInt32(dataGridView1.Rows[i].Cells["id"].Value));
            //    }
            //}
            if (list_selected.Count == 0)
            {
                MessageBox.Show("请至少选择一条数据！", "提示");
                return;
            }
            else
            {
                //parentForm.renderMateriel(ids);
                MidModule.SendIds(this, list_selected);//发送参数值
                if (AddMateriel != null) AddMateriel(this, new MaterielEventArgs(list_selected));
                this.Close();
            }
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { loadData(1); }   
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            loadData(pagingCom1.PageIndex);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CategorySelect view = new CategorySelect();
            view.CategorySet += View_CategorySet;
            view.Show();
        }
        private void View_CategorySet(object sender, CategoryEventArgs e)
        {
            this.textBox5.Text = e.name;
            this.category = e.id;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox5.Text = "";
            this.category = -1;
        }
    }

    public class MaterielLookUpArgs
    {
        public bool multi { get; set; }
        public List<int> ids { get; set; }
        public string type { get; set; }
        public bool? bom { get; set; }
        public MaterielLookUpArgs()
        {
            this.multi = true;
            this.ids = new List<int>();

        }
        public MaterielLookUpArgs(bool multi, List<int> ids)
        {
            this.multi = multi;
            this.ids = ids;
        }
    }
}
