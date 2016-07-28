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
    public partial class CategoryList : Form
    {
        categoryCtrl ctrl = new categoryCtrl();
        Dictionary<string, TreeNode> map_id_tn = new Dictionary<string, TreeNode>();//存树map
        int currentId = -1;
        int parent = -1;
        public CategoryList()
        {
            InitializeComponent();
            initPage();
            loadData();
        }
        private void initPage()
        {
            //this.dataGridView1.AutoGenerateColumns = false;
            //DataGridViewColumn colId = new DataGridViewLinkColumn();
            //colId.DataPropertyName = "id";//字段
            //colId.Visible = false;
            //colId.Name = "id";
            //dataGridView1.Columns.Add(colId);

            //DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            //colName.DataPropertyName = "name";//字段
            //colName.HeaderText = "名称";
            //colName.Name = "name";
            //dataGridView1.Columns.Add(colName);

            //DataGridViewColumn colStatus = new DataGridViewTextBoxColumn();
            //colStatus.DataPropertyName = "status";//字段
            //colStatus.HeaderText = "状态";
            //colStatus.Name = "status";
            //dataGridView1.Columns.Add(colStatus);

            //DataGridViewColumn colIsActive = new DataGridViewLinkColumn();
            //colIsActive.DataPropertyName = "isActive";//字段
            //colIsActive.Visible = false;
            //colIsActive.Name = "isActive";
            //dataGridView1.Columns.Add(colIsActive);
            
        }
        private void loadData()
        {
            DataTable dt = ctrl.get();
            //dataGridView1.DataSource = dt;

            map_id_tn = new Dictionary<string, TreeNode>();
            treeView1.Nodes.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["parent"] == DBNull.Value)
                {
                    TreeNode node = treeView1.Nodes.Add(dt.Rows[i]["name"].ToString());
                    if (!Convert.ToBoolean(dt.Rows[i]["isActive"]))
                    {
                        node.ForeColor = Color.Red;
                    }
                    node.Tag = new innerTag(dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), "",Convert.ToBoolean(dt.Rows[i]["isActive"]));
                    map_id_tn.Add(dt.Rows[i]["id"].ToString(), node);
                }
                else
                {
                    if (map_id_tn.ContainsKey(dt.Rows[i]["parent"].ToString()))
                    {
                        TreeNode childNode = map_id_tn[dt.Rows[i]["parent"].ToString()].Nodes.Add(dt.Rows[i]["name"].ToString());
                        if (!Convert.ToBoolean(dt.Rows[i]["isActive"]))
                        {
                            childNode.ForeColor = Color.Red;
                        }
                        childNode.Tag = new innerTag(dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), dt.Rows[i]["parent"].ToString(), Convert.ToBoolean(dt.Rows[i]["isActive"]));
                        map_id_tn.Add(dt.Rows[i]["id"].ToString(), childNode);
                    }
                }

            }
            this.treeView1.ExpandAll();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //if(this.dataGridView1.CurrentCell != null)
            //{
            //    bool isActive = Convert.ToBoolean(this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["isActive"].Value);
            //    MessageModel msg = ctrl.switchStatus(Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["id"].Value), isActive);
            //    if(msg.Code == 0)
            //    {
            //        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["isActive"].Value = !isActive;
            //        if (isActive) this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["status"].Value = "注销";
            //        else this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["status"].Value = "活动";
            //    }
            //    MessageBox.Show(msg.Msg);
            //}
            if (treeView1.SelectedNode != null)
            {
                innerTag it = (innerTag)treeView1.SelectedNode.Tag;
                MessageModel msg = ctrl.switchStatus(it.id, it.isActive);
                if (msg.Code == 0)
                {
                    if(it.isActive) treeView1.SelectedNode.ForeColor = Color.Red;
                    else treeView1.SelectedNode.ForeColor = Color.Black;

                    it.isActive = !it.isActive;
                }
                MessageBox.Show(msg.Msg);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = true;
            currentId = -1;
            if (treeView1.SelectedNode != null)
            {
                innerTag it = (innerTag)treeView1.SelectedNode.Tag;
                parent = it.parent;
            }
            else parent = -1;
            this.textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.save();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                this.panel2.Visible = true;
                //currentId = Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["id"].Value);
                //this.textBox1.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["name"].Value.ToString();
                innerTag it = (innerTag)treeView1.SelectedNode.Tag;
                currentId = it.id;
                this.textBox1.Text = it.name;
            }
            else
            {
                MessageBox.Show("请选择一项。");
            }
        }
        private void save()
        {
            MessageModel msg;
            if(this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("名称不能为空！");
                return;
            }
            if (currentId > 0)
            {
                CategoryModel model = new CategoryModel();
                model.id = currentId;
                model.name = this.textBox1.Text.Trim();
                msg = ctrl.set(model);
            }
            else
            {
                CategoryModel model = new CategoryModel();
                model.name = this.textBox1.Text.Trim();
                if (parent > 0) model.parent = parent;
                msg = ctrl.add(model);
            }
            if (msg.Code == 0)
            {
                loadData();
                this.panel2.Visible = false;
            }
            MessageBox.Show(msg.Msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = true;
            currentId = -1;
            if (treeView1.SelectedNode != null)
            {
                innerTag it = (innerTag)treeView1.SelectedNode.Tag;
                parent = it.id;
            }
            else parent = -1;
            this.textBox1.Text = "";
        }
        /// <summary>
        /// 内部类：节点信息
        /// </summary>
        public class innerTag
        {
            public int id;
            public string name;
            public int parent;
            public bool isActive;
            public innerTag(string id, string name, string parent,bool isActive)
            {
                this.id = Convert.ToInt32(id); ;
                this.name = name;
                if(parent != "") this.parent = Convert.ToInt32(parent);
                this.isActive = isActive;
            }
        }
    }
}
