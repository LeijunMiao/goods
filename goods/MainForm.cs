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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dateLoad();
        }
        private void dateLoad()
        {
            if(PropertyClass.Role > 1)
            {
                policyCtrl pctrl = new policyCtrl();
                var dt = pctrl.getFeatureByRole(PropertyClass.Role);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["key"].ToString())
                    {
                        case "department":
                            this.departmentItem.Enabled = true;
                            break;
                        case "role":
                            this.roleItem.Enabled = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            this.入库管理ToolStripMenuItem.Enabled = true;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.statusTime.Text = "当前时间：" + DateTime.Now.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
            this.statusUser.Text = "系统操作员：" + PropertyClass.SendNameValue;

            //if (PropertyClass.SendPopedomValue == "超级管理员")
            //{
            //    this.基础信息管理ToolStripMenuItem.Enabled = true;
            //    this.入库管理ToolStripMenuItem.Enabled = true;
            //    this.出库管理ToolStripMenuItem.Enabled = true;
            //    this.盘点管理ToolStripMenuItem.Enabled = true;
            //    this.数据查询ToolStripMenuItem.Enabled = true;
            //    this.权限管理ToolStripMenuItem.Enabled = true;
            //    return;
            //}
            //if (PropertyClass.SendPopedomValue.IndexOf("入库管理") > -1)
            //{
            //    this.入库管理ToolStripMenuItem.Enabled = true;
            //}
            //if (PropertyClass.SendPopedomValue.IndexOf("出库管理") > -1)
            //{
            //    this.出库管理ToolStripMenuItem.Enabled = true;
            //}
            //if (PropertyClass.SendPopedomValue.IndexOf("盘点管理") > -1)
            //{
            //    this.盘点管理ToolStripMenuItem.Enabled = true;
            //}
            //if (PropertyClass.SendPopedomValue.IndexOf("数据查询") > -1)
            //{
            //    this.数据查询ToolStripMenuItem.Enabled = true;
            //}
            //if (PropertyClass.SendPopedomValue.IndexOf("基础信息管理") > -1)
            //{
            //    this.基础信息管理ToolStripMenuItem.Enabled = true;
            //}
            //if (PropertyClass.SendPopedomValue.IndexOf("设置权限") > -1)
            //{
            //    this.设置权限ToolStripMenuItem.Enabled = true;
            //}
        }

        private void 设置权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SetCompetence sc = new SetCompetence();
            //sc.MdiParent = this;
            //sc.Show();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdatePassword up = new UpdatePassword();
            //up.MdiParent = this;
            //up.Show();
        }


        private void 入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Storage sr = new Storage();
            //sr.MdiParent = this;
            //sr.Show();
        }

        private void 出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TheLibrary tl = new TheLibrary();
            //tl.MdiParent = this;
            //tl.Show();
        }

        private void 盘点库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Inventory it = new Inventory();
            //it.MdiParent = this;
            //it.Show();
        }

        private void 产品查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DataQuery dq = new DataQuery();
            //dq.MdiParent = this;
            //dq.Show();
        }

        private void 出库查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SellQuery sq = new SellQuery();
            //sq.MdiParent = this;
            //sq.Show();
        }

        private void 库存查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //StockQuery sq = new StockQuery();
            //sq.MdiParent = this;
            //sq.Show();
        }

        private void departmentMenuItem_Click(object sender, EventArgs e)
        {
            Department dep = new Department();
            //dep.MdiParent = this;
            dep.Show();
        }

        private void roleMenuItem_Click(object sender, EventArgs e)
        {
            RoleView rv = new RoleView();
            //rv.MdiParent = this;
            rv.Show();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserView rv = new UserView();
            //rv.MdiParent = this;
            rv.Show();
        }

        private void 客户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerView view = new CustomerView();
            view.Show();
        }

        private void 供应商管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplierView rv = new SupplierView();
            //rv.MdiParent = this;
            rv.Show();
        }

        private void 仓库管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseView rv = new WarehouseView();
            //rv.MdiParent = this;
            rv.Show();
        }

        private void 计量单位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeteringView rv = new MeteringView();
           // rv.MdiParent = this;
            rv.Show();
        }

        private void 物料管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterielView rv = new MaterielView();
            //rv.MdiParent = this;
            rv.Show();
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateOrderView rv = new CreateOrderView();
            //rv.MdiParent = this;
            rv.Show();
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderSearch view = new OrderSearch();
            view.Show();
        }

        private void 查询ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GoDownEntryList view = new GoDownEntryList();
            view.Show();
        }
    }
}
