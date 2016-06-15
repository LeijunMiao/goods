namespace goods
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基础信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.产品信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客户信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.供应商ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.departmentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.入库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.盘点管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.盘点库存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.库存查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.产品查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出库查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.权限管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置权限ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.供应商管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仓库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计量单位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.物料管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.采购订单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基础信息管理ToolStripMenuItem,
            this.采购订单ToolStripMenuItem,
            this.入库管理ToolStripMenuItem,
            this.出库管理ToolStripMenuItem,
            this.盘点管理ToolStripMenuItem,
            this.数据查询ToolStripMenuItem,
            this.权限管理ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1040, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 基础信息管理ToolStripMenuItem
            // 
            this.基础信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.产品信息ToolStripMenuItem,
            this.客户信息ToolStripMenuItem,
            this.供应商ToolStripMenuItem,
            this.departmentItem,
            this.roleItem,
            this.用户管理ToolStripMenuItem,
            this.客户管理ToolStripMenuItem,
            this.供应商管理ToolStripMenuItem,
            this.仓库管理ToolStripMenuItem,
            this.计量单位ToolStripMenuItem,
            this.物料管理ToolStripMenuItem});
            this.基础信息管理ToolStripMenuItem.Name = "基础信息管理ToolStripMenuItem";
            this.基础信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.基础信息管理ToolStripMenuItem.Text = "基础信息管理";
            // 
            // 产品信息ToolStripMenuItem
            // 
            this.产品信息ToolStripMenuItem.Enabled = false;
            this.产品信息ToolStripMenuItem.Name = "产品信息ToolStripMenuItem";
            this.产品信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.产品信息ToolStripMenuItem.Text = "产品信息";
            this.产品信息ToolStripMenuItem.Click += new System.EventHandler(this.产品信息ToolStripMenuItem_Click);
            // 
            // 客户信息ToolStripMenuItem
            // 
            this.客户信息ToolStripMenuItem.Name = "客户信息ToolStripMenuItem";
            this.客户信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.客户信息ToolStripMenuItem.Text = "客户信息";
            this.客户信息ToolStripMenuItem.Click += new System.EventHandler(this.客户信息ToolStripMenuItem_Click);
            // 
            // 供应商ToolStripMenuItem
            // 
            this.供应商ToolStripMenuItem.Name = "供应商ToolStripMenuItem";
            this.供应商ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.供应商ToolStripMenuItem.Text = "供应商信息";
            this.供应商ToolStripMenuItem.Click += new System.EventHandler(this.供应商ToolStripMenuItem_Click);
            // 
            // departmentItem
            // 
            this.departmentItem.Name = "departmentItem";
            this.departmentItem.Size = new System.Drawing.Size(152, 22);
            this.departmentItem.Text = "组织架构";
            this.departmentItem.Click += new System.EventHandler(this.departmentMenuItem_Click);
            // 
            // roleItem
            // 
            this.roleItem.Name = "roleItem";
            this.roleItem.Size = new System.Drawing.Size(152, 22);
            this.roleItem.Text = "角色管理";
            this.roleItem.Click += new System.EventHandler(this.roleMenuItem_Click);
            // 
            // 入库管理ToolStripMenuItem
            // 
            this.入库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.入库ToolStripMenuItem});
            this.入库管理ToolStripMenuItem.Enabled = false;
            this.入库管理ToolStripMenuItem.Name = "入库管理ToolStripMenuItem";
            this.入库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.入库管理ToolStripMenuItem.Text = "入库管理";
            // 
            // 入库ToolStripMenuItem
            // 
            this.入库ToolStripMenuItem.Name = "入库ToolStripMenuItem";
            this.入库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.入库ToolStripMenuItem.Text = "入库";
            this.入库ToolStripMenuItem.Click += new System.EventHandler(this.入库ToolStripMenuItem_Click);
            // 
            // 出库管理ToolStripMenuItem
            // 
            this.出库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.出库ToolStripMenuItem});
            this.出库管理ToolStripMenuItem.Enabled = false;
            this.出库管理ToolStripMenuItem.Name = "出库管理ToolStripMenuItem";
            this.出库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.出库管理ToolStripMenuItem.Text = "出库管理";
            // 
            // 出库ToolStripMenuItem
            // 
            this.出库ToolStripMenuItem.Name = "出库ToolStripMenuItem";
            this.出库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.出库ToolStripMenuItem.Text = "出库";
            this.出库ToolStripMenuItem.Click += new System.EventHandler(this.出库ToolStripMenuItem_Click);
            // 
            // 盘点管理ToolStripMenuItem
            // 
            this.盘点管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.盘点库存ToolStripMenuItem});
            this.盘点管理ToolStripMenuItem.Enabled = false;
            this.盘点管理ToolStripMenuItem.Name = "盘点管理ToolStripMenuItem";
            this.盘点管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.盘点管理ToolStripMenuItem.Text = "盘点管理";
            // 
            // 盘点库存ToolStripMenuItem
            // 
            this.盘点库存ToolStripMenuItem.Name = "盘点库存ToolStripMenuItem";
            this.盘点库存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.盘点库存ToolStripMenuItem.Text = "盘点库存";
            this.盘点库存ToolStripMenuItem.Click += new System.EventHandler(this.盘点库存ToolStripMenuItem_Click);
            // 
            // 数据查询ToolStripMenuItem
            // 
            this.数据查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.库存查询ToolStripMenuItem,
            this.产品查询ToolStripMenuItem,
            this.出库查询ToolStripMenuItem});
            this.数据查询ToolStripMenuItem.Enabled = false;
            this.数据查询ToolStripMenuItem.Name = "数据查询ToolStripMenuItem";
            this.数据查询ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.数据查询ToolStripMenuItem.Text = "数据查询";
            // 
            // 库存查询ToolStripMenuItem
            // 
            this.库存查询ToolStripMenuItem.Name = "库存查询ToolStripMenuItem";
            this.库存查询ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.库存查询ToolStripMenuItem.Text = "库存查询";
            this.库存查询ToolStripMenuItem.Click += new System.EventHandler(this.库存查询ToolStripMenuItem_Click);
            // 
            // 产品查询ToolStripMenuItem
            // 
            this.产品查询ToolStripMenuItem.Name = "产品查询ToolStripMenuItem";
            this.产品查询ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.产品查询ToolStripMenuItem.Text = "入库查询";
            this.产品查询ToolStripMenuItem.Click += new System.EventHandler(this.产品查询ToolStripMenuItem_Click);
            // 
            // 出库查询ToolStripMenuItem
            // 
            this.出库查询ToolStripMenuItem.Name = "出库查询ToolStripMenuItem";
            this.出库查询ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.出库查询ToolStripMenuItem.Text = "出库查询";
            this.出库查询ToolStripMenuItem.Click += new System.EventHandler(this.出库查询ToolStripMenuItem_Click);
            // 
            // 权限管理ToolStripMenuItem
            // 
            this.权限管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置权限ToolStripMenuItem});
            this.权限管理ToolStripMenuItem.Enabled = false;
            this.权限管理ToolStripMenuItem.Name = "权限管理ToolStripMenuItem";
            this.权限管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.权限管理ToolStripMenuItem.Text = "权限管理";
            // 
            // 设置权限ToolStripMenuItem
            // 
            this.设置权限ToolStripMenuItem.Name = "设置权限ToolStripMenuItem";
            this.设置权限ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置权限ToolStripMenuItem.Text = "设置权限";
            this.设置权限ToolStripMenuItem.Click += new System.EventHandler(this.设置权限ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改密码ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusUser,
            this.toolStripStatusLabel2,
            this.statusTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 643);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1040, 29);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusUser
            // 
            this.statusUser.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.statusUser.Name = "statusUser";
            this.statusUser.Size = new System.Drawing.Size(504, 24);
            this.statusUser.Spring = true;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 24);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // statusTime
            // 
            this.statusTime.Name = "statusTime";
            this.statusTime.Size = new System.Drawing.Size(504, 24);
            this.statusTime.Spring = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            this.用户管理ToolStripMenuItem.Click += new System.EventHandler(this.用户管理ToolStripMenuItem_Click);
            // 
            // 客户管理ToolStripMenuItem
            // 
            this.客户管理ToolStripMenuItem.Name = "客户管理ToolStripMenuItem";
            this.客户管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.客户管理ToolStripMenuItem.Text = "客户管理";
            this.客户管理ToolStripMenuItem.Click += new System.EventHandler(this.客户管理ToolStripMenuItem_Click);
            // 
            // 供应商管理ToolStripMenuItem
            // 
            this.供应商管理ToolStripMenuItem.Name = "供应商管理ToolStripMenuItem";
            this.供应商管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.供应商管理ToolStripMenuItem.Text = "供应商管理";
            this.供应商管理ToolStripMenuItem.Click += new System.EventHandler(this.供应商管理ToolStripMenuItem_Click);
            // 
            // 仓库管理ToolStripMenuItem
            // 
            this.仓库管理ToolStripMenuItem.Name = "仓库管理ToolStripMenuItem";
            this.仓库管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.仓库管理ToolStripMenuItem.Text = "仓库管理";
            this.仓库管理ToolStripMenuItem.Click += new System.EventHandler(this.仓库管理ToolStripMenuItem_Click);
            // 
            // 计量单位ToolStripMenuItem
            // 
            this.计量单位ToolStripMenuItem.Name = "计量单位ToolStripMenuItem";
            this.计量单位ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.计量单位ToolStripMenuItem.Text = "计量单位";
            this.计量单位ToolStripMenuItem.Click += new System.EventHandler(this.计量单位ToolStripMenuItem_Click);
            // 
            // 物料管理ToolStripMenuItem
            // 
            this.物料管理ToolStripMenuItem.Name = "物料管理ToolStripMenuItem";
            this.物料管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.物料管理ToolStripMenuItem.Text = "物料管理";
            this.物料管理ToolStripMenuItem.Click += new System.EventHandler(this.物料管理ToolStripMenuItem_Click);
            // 
            // 采购订单ToolStripMenuItem
            // 
            this.采购订单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增ToolStripMenuItem});
            this.采购订单ToolStripMenuItem.Name = "采购订单ToolStripMenuItem";
            this.采购订单ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.采购订单ToolStripMenuItem.Text = "采购订单";
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新增ToolStripMenuItem.Text = "新增";
            this.新增ToolStripMenuItem.Click += new System.EventHandler(this.新增ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1040, 672);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "十方数字化系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基础信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 产品信息ToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem 产品信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客户信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 入库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 盘点管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 权限管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置权限ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 盘点库存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 产品查询ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 供应商ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出库查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 库存查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem departmentItem;
        private System.Windows.Forms.ToolStripMenuItem roleItem;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 供应商管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仓库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计量单位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 物料管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 采购订单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增ToolStripMenuItem;
    }
}



