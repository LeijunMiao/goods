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
            if(PropertyClass.Role > 0)
            {
                policyCtrl pctrl = new policyCtrl();
                var dt = pctrl.getFeatureByRole(PropertyClass.Role);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["key"].ToString())
                    {
                        case "department":
                            this.departmentItem.Visible = true;
                            break;
                        case "role":
                            this.roleItem.Visible = true;
                            break;
                        case "user":
                            this.用户管理ToolStripMenuItem.Visible = true;
                            break;
                        case "customer":
                            this.客户管理ToolStripMenuItem.Visible = true;
                            break;
                        case "supplier":
                            this.供应商管理ToolStripMenuItem.Visible = true;
                            break;
                        case "warehouse":
                            this.仓库管理ToolStripMenuItem.Visible = true;
                            break;
                        case "metering":
                            this.计量单位ToolStripMenuItem.Visible = true;
                            break;
                        case "materiel":
                            this.物料管理ToolStripMenuItem.Visible = true;
                            break;
                        case "order":
                            this.采购订单ToolStripMenuItem.Visible = true;
                            break;
                        case "inorder":
                            this.入库管理ToolStripMenuItem.Visible = true;
                            break;
                        case "outorder":
                            this.出库管理ToolStripMenuItem.Visible = true;
                            break;
                        case "checkorder":
                            this.盘点管理ToolStripMenuItem.Visible = true;
                            break;
                        case "message":
                            this.消息管理ToolStripMenuItem.Visible = true;
                            break;
                        default:
                            break;
                    }
                }
            }
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
        }


        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdatePassword up = new UpdatePassword();
            //up.MdiParent = this;
            //up.Show();
        }

        private void 盘点库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckList view = new CheckList();
            view.Show();
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


        private void 交货分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderTrace view = new OrderTrace();
            view.Show();
        }

        private void 查询ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CallSlipList view = new CallSlipList();
            view.Show();
        }

        private void 移出查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OutOrderList view = new OutOrderList();
            view.Show();
        }

        private void 移入查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InOrderList view = new InOrderList();
            view.Show();
        }


        private void 消息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MsgBox view = new MsgBox();
            view.Show();
        }

        private void 即时库存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StockList view = new StockList();
            view.Show();
        }

        private void 安全库存预警ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SafetyStockList view = new SafetyStockList();
            view.Show();
        }
    }
}
