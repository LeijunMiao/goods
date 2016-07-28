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
    public partial class OrderSolidBacking : Form
    {
        solidbackingCtrl ctrl = new solidbackingCtrl();
        int mid = -1;
        // 定义下拉列表框
        private ComboBox cmb_Temp = new ComboBox();
        Dictionary<int, DataTable> map = new Dictionary<int, DataTable>();
        Dictionary<int, int> map_attr_sb = new Dictionary<int, int>();
        Dictionary<int, attrClass> mapAttr;
        CreateOrderView parentForm;
        public OrderSolidBacking(CreateOrderView form,int id, string name, Dictionary<int, attrClass> map)
        {
            InitializeComponent();
            parentForm = form;
            mid = id;
            this.label1.Text = name;
            mapAttr = map;
            initPage();
            loadData();
        }
        private void initPage()
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.FillWeight = 20;
            colId.Name = "id";
            dataGridView1.Columns.Add(colId);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            dataGridView1.Columns.Add(colName);

            DataGridViewTextBoxColumn colValue = new DataGridViewTextBoxColumn();
            colValue.DataPropertyName = "value";
            colValue.Name = "attrValue";
            colValue.HeaderText = "";
            dataGridView1.Columns.Add(colValue);

            cmb_Temp.ValueMember = "id";
            cmb_Temp.DisplayMember = "name";
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Temp.Visible = false;
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);
            dataGridView1.Controls.Add(cmb_Temp);

            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView1.Scroll += DataGridView1_Scroll;
            dataGridView1.ColumnWidthChanged += DataGridView1_ColumnWidthChanged;
            //dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
        }

        private void DataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cmb_Temp.Visible = false;
        }

        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            this.cmb_Temp.Visible = false;
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == "attrValue")
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    BindAttrValue(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["id"].Value));
                    cmb_Temp.Text = dataGridView1.CurrentCell.Value.ToString(); 
                    cmb_Temp.Left = rect.Left;
                    cmb_Temp.Top = rect.Top;
                    cmb_Temp.Width = rect.Width;
                    cmb_Temp.Height = rect.Height;
                    cmb_Temp.Visible = true;
                }
                else
                {
                    cmb_Temp.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(((ComboBox)sender).SelectedValue);
            var name = ((ComboBox)sender).Text;
            dataGridView1.CurrentCell.Value = name;
            dataGridView1.CurrentCell.Tag = id;

            parentForm.setSolidBacking(mid, map_attr_sb[id],new attrClass(map_attr_sb[id],id, name));
        }

        private void loadData()
        {
            DataTable dt = ctrl.getMaterielAttrValue(mid);
            DataTable dtSB = new DataTable();
            DataColumn dcId = new DataColumn("id");
            DataColumn dcName = new DataColumn("name");
            DataColumn dcValue = new DataColumn("value");
            dtSB.Columns.AddRange(new DataColumn[] { dcId, dcName, dcValue });
            
            Dictionary<int, string> map_sb = new Dictionary<int, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var sId = Convert.ToInt32(dt.Rows[i]["sId"]);
                map_attr_sb.Add(Convert.ToInt32(dt.Rows[i]["id"]), Convert.ToInt32(dt.Rows[i]["sId"]));
                if (!map_sb.Keys.Contains(sId))
                {
                    map_sb.Add(sId, dt.Rows[i]["sName"].ToString());
                    map.Add(sId, new DataTable());
                    map[sId].Columns.Add("id");
                    map[sId].Columns.Add("name");

                    DataRow dr = dtSB.NewRow();
                    var sid = Convert.ToInt32(dt.Rows[i]["sId"]);
                    dr["id"] = dt.Rows[i]["sId"];
                    dr["name"] = dt.Rows[i]["sName"];
                    if(mapAttr.Keys.Contains(sid)) dr["value"] = mapAttr[sid].valueName;
                    dtSB.Rows.Add(dr);
                }
                DataRow drAttr = map[sId].NewRow();
                drAttr["id"] = Convert.ToInt32(dt.Rows[i]["id"]);
                drAttr["name"] = dt.Rows[i]["name"].ToString();
                map[sId].Rows.Add(drAttr);
            }
            dataGridView1.DataSource = dtSB;
        }
        /// <summary>
        /// 绑定性别下拉列表框
                /// </summary>
        private void BindAttrValue(int id)
        {
            cmb_Temp.DataSource = map[id];
        }

    }
}
