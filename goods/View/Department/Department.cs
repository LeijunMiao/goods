using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using goods.Controller;
using goods.Model;

namespace goods
{
    public partial class Department : Form
    {
        departmentCtrl ctrl = new departmentCtrl();//部门控制类
        Dictionary<string, TreeNode> map_id_tn = new Dictionary<string, TreeNode>();//存树map
        public Department()
        {
            InitializeComponent();
        }

        private void Department_Load(object sender, EventArgs e)
        {
            this.LoadData();

        }

        private void LoadData()
        {
            map_id_tn = new Dictionary<string, TreeNode>();
            DataTable dt = ctrl.get();
            treeView1.Nodes.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["parentId"] == DBNull.Value)
                {
                    TreeNode node = treeView1.Nodes.Add(dt.Rows[i]["name"].ToString());
                    node.Tag = new innerTag(dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), "");
                    map_id_tn.Add(dt.Rows[i]["id"].ToString(), node);
                }
                else
                {
                    if (map_id_tn.ContainsKey(dt.Rows[i]["parentId"].ToString()))
                    {
                        TreeNode childNode = map_id_tn[dt.Rows[i]["parentId"].ToString()].Nodes.Add(dt.Rows[i]["name"].ToString());
                        childNode.Tag = new innerTag(dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), dt.Rows[i]["parentId"].ToString());
                        map_id_tn.Add(dt.Rows[i]["id"].ToString(), childNode);
                    }
                }

            }
            this.treeView1.ExpandAll();
        }
        /// <summary>
        /// 新建同级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //this.createDepartment("");
            if (treeView1.SelectedNode != null)
            {
                innerTag it = (innerTag)treeView1.SelectedNode.Tag;
                if (it.parent == "")
                {
                    MessageBox.Show("禁止创建同级最高级别组织！");
                    return;
                }
                this.createDepartment(it.parent);//, treeView1.SelectedNode.Parent
            }
            else
            {
                this.createDepartment("");
            }
        }
        /// <summary>
        /// 新建下一级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                innerTag it = (innerTag)treeView1.SelectedNode.Tag;
                this.createDepartment(it.id);//, treeView1.SelectedNode
            }
            else
            {
                this.createDepartment("");
            }
                
        }


        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="parent">父级id</param>
        /// <param name="ptn">父级节点</param>
        private void createDepartment(string parent) //TreeNode ptn
        {
            string str = Interaction.InputBox("请输入名称", "输入名称", "");
            if (str.Count() != 0)
            {
                DepartmentModel dep;
                if(parent == "") dep = new DepartmentModel(str, null);
                else dep = new DepartmentModel(str, Convert.ToInt32(parent));
                MessageModel msg = ctrl.add(dep);
                if (msg.Code == 0)
                {
                    //TreeNode node = ptn.Nodes.Add(str);
                    //node.Tag = new innerTag(dep.Id.ToString(), str, parent);
                    this.LoadData();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string text = treeView1.SelectedNode.Text != null ? treeView1.SelectedNode.Text : "";
            string str = Interaction.InputBox("请输入名称", "输入名称", text);
            if (str.Count() != 0)
            {
                innerTag it = (innerTag)treeView1.SelectedNode.Tag;
                DepartmentModel dep = new DepartmentModel(Convert.ToInt32(it.id),str);
                MessageModel msg = ctrl.set(dep);
                if (msg.Code == 0)
                {
                    this.treeView1.SelectedNode.Text = str;
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            innerTag it = (innerTag)treeView1.SelectedNode.Tag;
            if (it.parent == "")
            {
                MessageBox.Show("禁止删除最高级别组织！");
                return;
            }
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DepartmentModel dep = new DepartmentModel(Convert.ToInt32(it.id));
                MessageModel msg = ctrl.del(dep);

                if (msg.Code == 0)
                {
                    //treeView1.SelectedNode.Parent.Nodes.Remove(treeView1.SelectedNode);
                    treeView1.SelectedNode.Remove();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }

        /// <summary>
        /// 内部类：节点信息
        /// </summary>
        public class innerTag
        {
            public string id;
            public string name;
            public string parent;
            public innerTag(string id, string name, string parent)
            {
                this.id = id;
                this.name = name;
                this.parent = parent;
            }
        }

    }
}
