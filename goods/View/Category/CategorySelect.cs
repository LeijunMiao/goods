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
    public partial class CategorySelect : Form
    {
        
        public delegate void CategoryEventHandler(object sender, CategoryEventArgs e);
        public event CategoryEventHandler CategorySet;

        categoryCtrl ctrl = new categoryCtrl();
        Dictionary<string, TreeNode> map_id_tn = new Dictionary<string, TreeNode>();//存树map
        public CategorySelect()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            DataTable dt = ctrl.getActive();
            //dataGridView1.DataSource = dt;

            map_id_tn = new Dictionary<string, TreeNode>();
            treeView1.Nodes.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["parent"] == DBNull.Value)
                {
                    TreeNode node = treeView1.Nodes.Add(dt.Rows[i]["name"].ToString());
                    node.Tag = new innerTag(dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), "");
                    map_id_tn.Add(dt.Rows[i]["id"].ToString(), node);
                }
                else
                {
                    if (map_id_tn.ContainsKey(dt.Rows[i]["parent"].ToString()))
                    {
                        TreeNode childNode = map_id_tn[dt.Rows[i]["parent"].ToString()].Nodes.Add(dt.Rows[i]["name"].ToString());
                        childNode.Tag = new innerTag(dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), dt.Rows[i]["parent"].ToString());
                        map_id_tn.Add(dt.Rows[i]["id"].ToString(), childNode);
                    }
                }

            }
            this.treeView1.ExpandAll();
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
            public innerTag(string id, string name, string parent)
            {
                this.id = Convert.ToInt32(id); ;
                this.name = name;
                if (parent != "") this.parent = Convert.ToInt32(parent);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (treeView1.SelectedNode != null)
            {
                innerTag it = (innerTag) treeView1.SelectedNode.Tag;
                CategorySet(this, new CategoryEventArgs(it.id, it.name));
            }
            this.Close();
        }
    }
}
