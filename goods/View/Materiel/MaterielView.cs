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
namespace goods
{
    public partial class MaterielView : Form
    {
        materielCtrl ctrl = new materielCtrl();
        private DataTable dtData = null;
        public MaterielView()
        {
            InitializeComponent();
            initPage();


        }
        public void initPage()
        {
            this.textBox1.KeyDown += button1_KeyDown;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewColumn colIsActive = new DataGridViewLinkColumn();
            colIsActive.DataPropertyName = "isActive";//字段
            colIsActive.Visible = false;
            colIsActive.Name = "isActive";
            dataGridView1.Columns.Add(colIsActive);

            DataGridViewColumn colId = new DataGridViewLinkColumn();
            colId.DataPropertyName = "id";//字段
            colId.Visible = false;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);

            DataGridViewColumn colNum = new DataGridViewTextBoxColumn();
            colNum.DataPropertyName = "num";//字段
            colNum.Name = "num";
            colNum.HeaderText = "编号";
            dataGridView1.Columns.Add(colNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";//字段
            colName.Name = "name";
            colName.HeaderText = "名称";
            dataGridView1.Columns.Add(colName);

            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "status";//字段
            colStatus.Name = "status";
            colStatus.HeaderText = "状态";
            dataGridView1.Columns.Add(colStatus);

            DataGridViewColumn colSep = new DataGridViewTextBoxColumn();
            colSep.DataPropertyName = "specifications";
            colSep.Name = "specifications";
            colSep.HeaderText = "规格参数";
            dataGridView1.Columns.Add(colSep);

            DataGridViewColumn colMetering = new DataGridViewTextBoxColumn();
            colMetering.DataPropertyName = "metering";
            colMetering.Name = "metering";
            colMetering.HeaderText = "计量单位";
            dataGridView1.Columns.Add(colMetering);

            DataGridViewColumn colSubMetering = new DataGridViewTextBoxColumn();
            colSubMetering.DataPropertyName = "subMetering";
            colSubMetering.Name = "subMetering";
            colSubMetering.HeaderText = "辅助单位";
            dataGridView1.Columns.Add(colSubMetering);

            DataGridViewColumn colType = new DataGridViewTextBoxColumn();
            colType.DataPropertyName = "type";
            colType.Name = "type";
            colType.HeaderText = "属性";
            dataGridView1.Columns.Add(colType);

            DataGridViewColumn conversion = new DataGridViewTextBoxColumn();
            conversion.HeaderText = "转换率";
            conversion.Name = "conversion";
            conversion.DataPropertyName = "conversion";
            dataGridView1.Columns.Add(conversion);

            DataGridViewColumn tax = new DataGridViewTextBoxColumn();
            tax.HeaderText = "税率";
            tax.Name = "tax";
            tax.DataPropertyName = "tax";
            dataGridView1.Columns.Add(tax);

            DataGridViewColumn isBatch = new DataGridViewTextBoxColumn();
            isBatch.HeaderText = "业务批次管理";
            isBatch.Name = "isBatch";
            isBatch.DataPropertyName = "isBatch";
            dataGridView1.Columns.Add(isBatch);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        public void loadData()
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pagingCom1.PageIndex);
        }
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = ctrl.getFilterList(pagingCom1.PageIndex, pagingCom1.PageSize, textBox1.Text);

            dtData.Columns.Add("status");
            for (int i = 0; i < dtData.Rows.Count; i++)
            {

                if (Convert.ToBoolean(dtData.Rows[i]["isActive"])) dtData.Rows[i]["status"] = "激活";
                else dtData.Rows[i]["status"] = "注销";
            }
            dataGridView1.DataSource = dtData;
            pagingCom1.RecordCount = ctrl.getCountAll(textBox1.Text);
            pagingCom1.reSet();

        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { BindDataWithPage(1); }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MaterielPopup popup = new MaterielPopup(this,0);
            popup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                List<int> ids = new List<int>();
                
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    //MaterielModel mm = new MaterielModel(Convert.ToInt32(dataGridView1.SelectedRows[i].Cells["id"].Value) , 
                    //    dataGridView1.SelectedRows[i].Cells["编号"].Value.ToString(), 
                    //    dataGridView1.SelectedRows[i].Cells["名称"].Value.ToString(),
                    //    dataGridView1.SelectedRows[i].Cells["计量单位"].Value.ToString());
                    ids.Add(Convert.ToInt32(dataGridView1.SelectedRows[i].Cells["id"].Value));
                }
                SafetyStockSetting view = new SafetyStockSetting(ids);
                view.Show();
            }
            else
            {
                MessageBox.Show("请选择物料！");
            }
            
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                MaterielPopup popup = new MaterielPopup(this, Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["id"].Value));
                popup.Show();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                var cellIsActive = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["isActive"].Value;
                var cellId = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["id"].Value;
                MessageModel msg = ctrl.switchStatus(Convert.ToInt32(cellId), Convert.ToBoolean(cellIsActive));
                if (msg.Code != 0)
                {
                    MessageBox.Show(msg.Msg);
                }
                else
                {
                    if (Convert.ToBoolean(cellIsActive))
                    {
                        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["status"].Value = "注销";

                    }

                    else
                    {
                        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["status"].Value = "激活";
                    }
                    this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["isActive"].Value = !Convert.ToBoolean(cellIsActive);

                }
            }
        }
    }
}