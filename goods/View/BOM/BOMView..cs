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
    public partial class BOMView : Form
    {
        bomCtrl ctrl = new bomCtrl();
        int id;//bommain id
        int level = 1;
        bool ISBREAK = false;
        DataTable dt_tree = new DataTable();
        Dictionary<int, TreeNode> map_id_tn = new Dictionary<int, TreeNode>();//存树map
        public BOMView( int id )
        {
            InitializeComponent();
            this.id = id;
            initPage();
            loadTreeData();
            if (!ISBREAK) loadTable(this.id);
        }
        /// <summary>
        /// bom表
        /// </summary>
        private void loadTable(int id)
        {
            dt_tree.Clear();
            setTable(id);
            dataGridView1.DataSource = dt_tree;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["bottom"].Value))
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }
        private void initPage()
        {
            dataGridView1.AutoGenerateColumns = false;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;

            DataGridViewColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "id";
            colId.Visible = false;
            colId.Name = "id";
            this.dataGridView1.Columns.Add(colId);

            DataGridViewColumn colBottom = new DataGridViewTextBoxColumn();
            colBottom.DataPropertyName = "bottom";
            colBottom.Visible = false;
            colBottom.Name = "bottom";
            this.dataGridView1.Columns.Add(colBottom);

            DataGridViewColumn colLevel = new DataGridViewTextBoxColumn();
            colLevel.DataPropertyName = "level";
            colLevel.Name = "level";
            colLevel.HeaderText = "层次";
            this.dataGridView1.Columns.Add(colLevel);

            DataGridViewColumn colMNum = new DataGridViewTextBoxColumn();
            colMNum.DataPropertyName = "mnum";
            colMNum.Name = "mnum";
            colMNum.HeaderText = "物料编号";
            this.dataGridView1.Columns.Add(colMNum);

            DataGridViewColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "name";
            colName.Name = "name";
            colName.HeaderText = "物料名称";
            this.dataGridView1.Columns.Add(colName);

            DataGridViewColumn colSep = new DataGridViewTextBoxColumn();
            colSep.DataPropertyName = "specifications";
            colSep.Name = "specifications";
            colSep.HeaderText = "规格参数";
            this.dataGridView1.Columns.Add(colSep);

            DataGridViewColumn colType = new DataGridViewTextBoxColumn();
            colType.DataPropertyName = "type";
            colType.Name = "type";
            colType.HeaderText = "物料属性";
            this.dataGridView1.Columns.Add(colType);

            DataGridViewColumn colMetering = new DataGridViewTextBoxColumn();
            colMetering.DataPropertyName = "metering";
            colMetering.Name = "metering";
            colMetering.HeaderText = "计量单位";
            this.dataGridView1.Columns.Add(colMetering);

            DataGridViewColumn colQuantity = new DataGridViewTextBoxColumn();
            colQuantity.DataPropertyName = "quantity";
            colQuantity.Name = "quantity";
            colQuantity.HeaderText = "数量";
            colQuantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Columns.Add(colQuantity);

            DataGridViewColumn colPrice = new DataGridViewTextBoxColumn();
            colPrice.DataPropertyName = "price";
            colPrice.Name = "price";
            colPrice.HeaderText = "单价";
            this.dataGridView1.Columns.Add(colPrice);

            DataGridViewColumn colAmount = new DataGridViewTextBoxColumn();
            colAmount.DataPropertyName = "amount";
            colAmount.Name = "amount";
            colAmount.HeaderText = "金额";
            this.dataGridView1.Columns.Add(colAmount);

            DataGridViewColumn colRemark = new DataGridViewTextBoxColumn();
            colRemark.DataPropertyName = "remark";
            colRemark.Name = "remark";
            colRemark.HeaderText = "备注";
            this.dataGridView1.Columns.Add(colRemark);

            DataColumn dcId = new DataColumn("id");
            DataColumn dcNum = new DataColumn("num");
            DataColumn dcMNum = new DataColumn("mnum");
            DataColumn dcMName = new DataColumn("name");
            DataColumn dcSep = new DataColumn("specifications");
            DataColumn dcMete = new DataColumn("metering");
            DataColumn dcPrice = new DataColumn("price");
            DataColumn dcQuantity = new DataColumn("quantity");
            DataColumn dcAmount = new DataColumn("amount");
            DataColumn dcLevel = new DataColumn("level");
            DataColumn dcRemark = new DataColumn("remark");
            DataColumn dcType = new DataColumn("type");
            DataColumn dcBottom = new DataColumn("bottom");
            DataColumn[] list_dc = { dcId,dcNum, dcMNum, dcMName, dcSep, dcMete, dcPrice,dcQuantity,
                dcAmount,dcLevel,dcRemark,dcType,dcBottom };
            dt_tree.Columns.AddRange(list_dc);

            treeView1.AfterSelect += TreeView1_AfterSelect;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                loadTable(((innerTag)treeView1.SelectedNode.Tag).did);
            }
        }

        /// <summary>
        /// bom树
        /// </summary>
        private void loadTreeData()
        {
            treeView1.Nodes.Clear();
            DataTable dt = ctrl.getbyid(new List<int> { id });
            TreeNode node = treeView1.Nodes.Add(dt.Rows[0]["name"].ToString());
            int mainid = Convert.ToInt32(dt.Rows[0]["id"]);
            var tag = new innerTag(mainid, mainid, dt.Rows[0]["mnum"].ToString(),dt.Rows[0]["name"].ToString(), null,level, dt.Rows[0]["specifications"].ToString(), dt.Rows[0]["metering"].ToString(), "", dt.Rows[0]["type"].ToString());
            tag.quantity = 1;
            tag.scale = tag.quantity / Convert.ToDouble(dt.Rows[0]["mainqty"]);
            node.Tag = tag;
            level++;
            map_id_tn.Add(mainid, node);
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode childNode = node.Nodes.Add(dt.Rows[i]["mcname"].ToString());
                int children;
                if (dt.Rows[i]["children"] != DBNull.Value)
                {
                    children = Convert.ToInt32(dt.Rows[i]["children"]);
                }
                else
                {
                    children = Convert.ToInt32(dt.Rows[i]["materiel"]);
                }
                
                int parent = Convert.ToInt32(dt.Rows[i]["id"]);
                int dtl = Convert.ToInt32(dt.Rows[i]["did"]);
                list2.Add(dtl);
                var ctag = new innerTag(children, dtl, dt.Rows[i]["mcnum"].ToString(), dt.Rows[i]["mcname"].ToString(), parent, level, dt.Rows[i]["msspe"].ToString(), dt.Rows[i]["mcmetering"].ToString(), dt.Rows[i]["remark"].ToString(), dt.Rows[i]["mctype"].ToString());
                ctag.quantity = ((innerTag)map_id_tn[parent].Tag).scale * Convert.ToDouble(dt.Rows[i]["dtlqty"]);
                //ctag.scale = ctag.quantity / Convert.ToDouble(dt.Rows[i]["mainqty"]);
                childNode.Tag = ctag;
                if(!map_id_tn.ContainsKey(dtl)) map_id_tn.Add(dtl, childNode);
                if (dt.Rows[i]["children"] != DBNull.Value)
                {
                    list.Add(dtl);
                }
                else if (dt.Rows[i]["mctype"].ToString() == "外购件")
                {
                    ctag.price = Convert.ToDouble(dt.Rows[i]["normprice"]);
                    ctag.amount = ctag.quantity * ctag.price;
                    ctag.bottom = true;
                }
                else
                {
                    ctag.price = 0;
                    ctag.bottom = true;

                }
            }
            level++;
            if (list.Count > 0) {
                List<int> list_r = recursionGet(list);
                for (int i = 0; i < list_r.Count; i++)
                {
                    var tagtemp = ((innerTag)map_id_tn[list_r[i]].Tag);
                    if (tagtemp.parent != null) ((innerTag)map_id_tn[Convert.ToInt32(tagtemp.parent)].Tag).amount += tagtemp.amount;
                }

            }
            if (ISBREAK)
            {
                this.Close();
            }
            for (int i = 0; i < list2.Count; i++)
            {
                var tagtemp = ((innerTag)map_id_tn[list2[i]].Tag);
                ((innerTag)map_id_tn[Convert.ToInt32(tagtemp.parent)].Tag).amount += tagtemp.amount;
            }
            foreach (var item in map_id_tn.Values)
            {
                if (((innerTag)item.Tag).price == 0) ((innerTag)item.Tag).price = ((innerTag)item.Tag).amount / ((innerTag)item.Tag).quantity;
            }
            this.treeView1.ExpandAll();
        }

        private List<int> recursionGet(List<int> ids) {
            DataTable dt = ctrl.getbyid2(ids);
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int children;
                if (dt.Rows[i]["children"] != DBNull.Value)
                {
                    children = Convert.ToInt32(dt.Rows[i]["children"]);
                }
                else children = Convert.ToInt32(dt.Rows[i]["materiel"]);
                int parent = Convert.ToInt32(dt.Rows[i]["id"]);
                int dtl = Convert.ToInt32(dt.Rows[i]["did"]);
                int dparent = Convert.ToInt32(dt.Rows[i]["dparent"]);
                list2.Add(dtl);
                TreeNode childNode = map_id_tn[dparent].Nodes.Add(dt.Rows[i]["mcname"].ToString());
                var ctag = new innerTag(children, dtl, dt.Rows[i]["mcnum"].ToString(), dt.Rows[i]["mcname"].ToString(), dparent, level, dt.Rows[i]["msspe"].ToString(), dt.Rows[i]["mcmetering"].ToString(), dt.Rows[i]["remark"].ToString(), dt.Rows[i]["mctype"].ToString());
                ((innerTag)map_id_tn[dparent].Tag).scale = ((innerTag)map_id_tn[dparent].Tag).quantity / Convert.ToDouble(dt.Rows[i]["mainqty"]);
                ctag.quantity = ((innerTag)map_id_tn[dparent].Tag).scale * Convert.ToDouble(dt.Rows[i]["dtlqty"]);
                //ctag.scale = ctag.quantity / Convert.ToDouble(dt.Rows[i]["mainqty"]);
                if (dt.Rows[i]["children"] != DBNull.Value)
                {
                    list.Add(dtl);
                }
                else if (dt.Rows[i]["mctype"].ToString() == "外购件")
                {
                    ctag.price = Convert.ToDouble(dt.Rows[i]["normprice"]);
                    ctag.amount = ctag.quantity * ctag.price;
                    ctag.bottom = true;
                }
                else
                {
                    ctag.price = 0;
                    ctag.amount = 0;
                    ctag.bottom = true;
                }
                childNode.Tag = ctag;
                if (!map_id_tn.ContainsKey(dtl)) map_id_tn.Add(dtl, childNode);
            }
            level++;
            if (level > 10)
            {
                MessageBox.Show("层级超过10层，不能显示！");
                ISBREAK = true;
                return new List<int>();
            }
            if (list.Count > 0)
            {
                List<int>  list_r = recursionGet(list);
                for (int i = 0; i < list_r.Count; i++)
                {
                    var tagtemp = ((innerTag)map_id_tn[list_r[i]].Tag);
                    if(tagtemp.parent != null) ((innerTag)map_id_tn[Convert.ToInt32(tagtemp.parent)].Tag).amount += tagtemp.amount;
                    //MessageBox.Show(tagtemp.name + "|"+ );
                }
            }
            return list2;
        }

        private void setTable(int materiel)
        {
            DataRow dr = tag2dr((innerTag)map_id_tn[materiel].Tag); 
            dt_tree.Rows.Add(dr);
            if(map_id_tn[materiel].Nodes.Count > 0) recursionSetTable(map_id_tn[materiel].Nodes);
        }
        private DataRow tag2dr(innerTag obj)
        {
            var dr = dt_tree.NewRow();
            dr["id"] = obj.id;
            dr["name"] = obj.name;
            dr["mnum"] = obj.mnum;
            dr["level"] = obj.level;
            dr["specifications"] = obj.specifications;
            dr["metering"] = obj.metering;
            dr["remark"] = obj.remark;
            dr["type"] = obj.type;
            dr["quantity"] = Math.Round(obj.quantity,2) ;
            dr["price"] = Math.Round(obj.price, 2);
            dr["amount"] = Math.Round(obj.amount, 2);
            dr["bottom"] = obj.bottom;
            return dr;

        }
        private void recursionSetTable(TreeNodeCollection nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                setTable(((innerTag)nodes[i].Tag).did);
            }
        }
        /// <summary>
        /// 内部类：节点信息
        /// </summary>
        public class innerTag
        {
            public int id;
            public int did;
            public string mnum;
            public string name;
            public string specifications;
            public string metering;
            public double price;
            public double quantity;
            public double amount;
            public string remark;
            public int? parent;
            public int level;
            public string type;
            public double scale;
            public bool bottom;
            public innerTag(int id, int did, string mnum, string name, int? parent,int level, string specifications, string metering, string remark, string type)
            {
                this.id = id;
                this.did = did;
                this.name = name;
                this.parent = parent;
                this.level = level;
                this.mnum = mnum;
                this.specifications = specifications;
                this.metering = metering;
                this.remark = remark;
                this.type = type;
            }
        }
    }

}
