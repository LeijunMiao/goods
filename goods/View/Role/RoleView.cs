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
using goods.Role;
using Microsoft.VisualBasic;

namespace goods
{
    public partial class RoleView : Form
    {
        departmentCtrl ctrl = new departmentCtrl();//部门控制类
        roleCtrl rc = new roleCtrl(); //角色控制类
        policyCtrl pctrl = new policyCtrl();
        Dictionary<string, TreeNode> map_id_tn = new Dictionary<string, TreeNode>();//存树map
        private DataTable dtData = null;
        private DataTable dt = null;
        private DataTable dtPolicyView = null; 

        int roleId = -1;
        int loaded = -1; //判断窗体是否加载完成

        public RoleView()
        {
            InitializeComponent();
            this.LoadData();
        }
        private void Form1_Shown(Object sender, EventArgs e)
        {
            loaded = 1;
            loadPolicy(sender, e);

        }
        public void LoadRoleData(int depid){
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            if (depid == -1)
            {
                innerTag it = (innerTag)treeView1.SelectedNode.Tag;
                depid = Convert.ToInt32(it.id);
            }
            else treeView1.SelectedNode = map_id_tn[depid.ToString()];
            dtData = rc.getbyDepId(depid);
            dt = new DataTable();
            DataColumn dcNO = new DataColumn("no");
            DataColumn dcName = new DataColumn("name");
            dt.Columns.Add(dcNO);
            dt.Columns.Add(dcName);
            

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["id"].ToString();
                dr[1] = dtData.Rows[i]["name"].ToString();
                dt.Rows.Add(dr);
                ////if(data.Rows[i]["isActive"].ToString() == "1") dr[1] = "激活";
                ////else dr[1] = "注销";
                //dt.Rows.Add(dr);data.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), dt.Rows[i]["departmentId"].ToString()
            }
            dataGridView1.DataSource = dt;

        }
        #region 加载部门树，功能列表
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
                    node.Tag = new innerTag(dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), dt.Rows[i]["parentId"].ToString());
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

            this.comboBox1.DataSource = pctrl.getFeature();
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "id";
            this.comboBox1.SelectedIndex = 0;
        }
        #endregion

        
        public void loadPolicy(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && loaded == 1)
            {
                DataGridViewColumn colId = new DataGridViewTextBoxColumn();
                colId.DataPropertyName = "id";
                colId.Visible = false;
                colId.Name = "id";
                dataGridView2.Columns.Add(colId);

                roleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                var dtPolicyData = pctrl.getFeatureByRole(roleId);
                dtPolicyView = new DataTable();
                DataColumn dcName = new DataColumn("名称");
                DataColumn dcId = new DataColumn("id");
                dtPolicyView.Columns.Add(dcName);
                dtPolicyView.Columns.Add(dcId);


                for (int i = 0; i < dtPolicyData.Rows.Count; i++)
                {
                    DataRow dr = dtPolicyView.NewRow();
                    dr[0] = dtPolicyData.Rows[i]["name"].ToString();
                    dr[1] = dtPolicyData.Rows[i]["pid"].ToString();
                    dtPolicyView.Rows.Add(dr);
                }
                dataGridView2.DataSource = dtPolicyView;
            }

        }

        #region 新建角色
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            innerTag it = (innerTag)treeView1.SelectedNode.Tag;
            RolePopup popup = new RolePopup(this, it.id);
            popup.Show();
        }
        #endregion



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadRoleData(-1);
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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) MessageBox.Show("请选中一行");
            else
            {
                string str = Interaction.InputBox("请输入名称", "输入名称", dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                if (str.Count() != 0)
                {
                    RoleModel rm = new RoleModel(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value), str);
                    MessageModel msg = rc.set(rm);
                    if (msg.Code == 0)
                    {
                        dataGridView1.SelectedRows[0].Cells[1].Value = str;
                    }
                    else
                    {
                        MessageBox.Show(msg.Msg);
                    }
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RoleModel rm = new RoleModel(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                MessageModel msg = rc.del(rm);
                if (msg.Code == 0)
                {
                    DataRow[] dr = dt.Select("no='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    dt.Rows.Remove(dr[0]);
                    this.dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int feature = Convert.ToInt32(this.comboBox1.SelectedValue.ToString());
            if (feature >0 && roleId > 0)
            {
                PolicyModel pm = new PolicyModel(roleId, feature);
                MessageModel msg = pctrl.add(pm);
                if (msg.Code == 0)
                {
                    loadPolicy(sender,e);
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一行！");
                return;
            }
            else if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PolicyModel rm = new PolicyModel();
                rm.Id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["id"].Value);
                MessageModel msg = pctrl.del(rm);
                if (msg.Code == 0)
                {
                    DataRow[] dr = dtPolicyView.Select("id='" + dataGridView2.SelectedRows[0].Cells["id"].Value.ToString() + "'");
                    dtPolicyView.Rows.Remove(dr[0]);
                    this.dataGridView2.DataSource = dtPolicyView;
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }
    }
}
