﻿namespace goods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基础信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.departmentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.供应商管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仓库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计量单位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.物料管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.消息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.采购订单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.入库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.移入查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.交货分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.移出查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.盘点管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.盘点库存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.即时库存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.安全库存预警ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(62)))), ((int)(((byte)(110)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基础信息管理ToolStripMenuItem,
            this.采购订单ToolStripMenuItem,
            this.入库管理ToolStripMenuItem,
            this.出库管理ToolStripMenuItem,
            this.盘点管理ToolStripMenuItem,
            this.报表ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(940, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 基础信息管理ToolStripMenuItem
            // 
            this.基础信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.departmentItem,
            this.roleItem,
            this.用户管理ToolStripMenuItem,
            this.客户管理ToolStripMenuItem,
            this.供应商管理ToolStripMenuItem,
            this.仓库管理ToolStripMenuItem,
            this.计量单位ToolStripMenuItem,
            this.物料管理ToolStripMenuItem,
            this.消息管理ToolStripMenuItem});
            this.基础信息管理ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.基础信息管理ToolStripMenuItem.Name = "基础信息管理ToolStripMenuItem";
            this.基础信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.基础信息管理ToolStripMenuItem.Text = "基础信息管理";
            // 
            // departmentItem
            // 
            this.departmentItem.Name = "departmentItem";
            this.departmentItem.Size = new System.Drawing.Size(136, 22);
            this.departmentItem.Text = "组织架构";
            this.departmentItem.Visible = false;
            this.departmentItem.Click += new System.EventHandler(this.departmentMenuItem_Click);
            // 
            // roleItem
            // 
            this.roleItem.Name = "roleItem";
            this.roleItem.Size = new System.Drawing.Size(136, 22);
            this.roleItem.Text = "角色管理";
            this.roleItem.Visible = false;
            this.roleItem.Click += new System.EventHandler(this.roleMenuItem_Click);
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            this.用户管理ToolStripMenuItem.Visible = false;
            this.用户管理ToolStripMenuItem.Click += new System.EventHandler(this.用户管理ToolStripMenuItem_Click);
            // 
            // 客户管理ToolStripMenuItem
            // 
            this.客户管理ToolStripMenuItem.Name = "客户管理ToolStripMenuItem";
            this.客户管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.客户管理ToolStripMenuItem.Text = "客户管理";
            this.客户管理ToolStripMenuItem.Visible = false;
            this.客户管理ToolStripMenuItem.Click += new System.EventHandler(this.客户管理ToolStripMenuItem_Click);
            // 
            // 供应商管理ToolStripMenuItem
            // 
            this.供应商管理ToolStripMenuItem.Name = "供应商管理ToolStripMenuItem";
            this.供应商管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.供应商管理ToolStripMenuItem.Text = "供应商管理";
            this.供应商管理ToolStripMenuItem.Visible = false;
            this.供应商管理ToolStripMenuItem.Click += new System.EventHandler(this.供应商管理ToolStripMenuItem_Click);
            // 
            // 仓库管理ToolStripMenuItem
            // 
            this.仓库管理ToolStripMenuItem.Name = "仓库管理ToolStripMenuItem";
            this.仓库管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.仓库管理ToolStripMenuItem.Text = "仓库管理";
            this.仓库管理ToolStripMenuItem.Visible = false;
            this.仓库管理ToolStripMenuItem.Click += new System.EventHandler(this.仓库管理ToolStripMenuItem_Click);
            // 
            // 计量单位ToolStripMenuItem
            // 
            this.计量单位ToolStripMenuItem.Name = "计量单位ToolStripMenuItem";
            this.计量单位ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.计量单位ToolStripMenuItem.Text = "计量单位";
            this.计量单位ToolStripMenuItem.Visible = false;
            this.计量单位ToolStripMenuItem.Click += new System.EventHandler(this.计量单位ToolStripMenuItem_Click);
            // 
            // 物料管理ToolStripMenuItem
            // 
            this.物料管理ToolStripMenuItem.Name = "物料管理ToolStripMenuItem";
            this.物料管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.物料管理ToolStripMenuItem.Text = "物料管理";
            this.物料管理ToolStripMenuItem.Visible = false;
            this.物料管理ToolStripMenuItem.Click += new System.EventHandler(this.物料管理ToolStripMenuItem_Click);
            // 
            // 消息管理ToolStripMenuItem
            // 
            this.消息管理ToolStripMenuItem.Name = "消息管理ToolStripMenuItem";
            this.消息管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.消息管理ToolStripMenuItem.Text = "消息管理";
            this.消息管理ToolStripMenuItem.Visible = false;
            this.消息管理ToolStripMenuItem.Click += new System.EventHandler(this.消息管理ToolStripMenuItem_Click);
            // 
            // 采购订单ToolStripMenuItem
            // 
            this.采购订单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增ToolStripMenuItem,
            this.查询ToolStripMenuItem});
            this.采购订单ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.采购订单ToolStripMenuItem.Name = "采购订单ToolStripMenuItem";
            this.采购订单ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.采购订单ToolStripMenuItem.Text = "采购订单";
            this.采购订单ToolStripMenuItem.Visible = false;
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新增ToolStripMenuItem.Text = "新增";
            this.新增ToolStripMenuItem.Click += new System.EventHandler(this.新增ToolStripMenuItem_Click);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.查询ToolStripMenuItem.Text = "查询";
            this.查询ToolStripMenuItem.Click += new System.EventHandler(this.查询ToolStripMenuItem_Click);
            // 
            // 入库管理ToolStripMenuItem
            // 
            this.入库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询ToolStripMenuItem1,
            this.移入查询ToolStripMenuItem,
            this.交货分析ToolStripMenuItem});
            this.入库管理ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.入库管理ToolStripMenuItem.Name = "入库管理ToolStripMenuItem";
            this.入库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.入库管理ToolStripMenuItem.Text = "入库管理";
            this.入库管理ToolStripMenuItem.Visible = false;
            // 
            // 查询ToolStripMenuItem1
            // 
            this.查询ToolStripMenuItem1.Name = "查询ToolStripMenuItem1";
            this.查询ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.查询ToolStripMenuItem1.Text = "外购入库查询";
            this.查询ToolStripMenuItem1.Click += new System.EventHandler(this.查询ToolStripMenuItem1_Click);
            // 
            // 移入查询ToolStripMenuItem
            // 
            this.移入查询ToolStripMenuItem.Name = "移入查询ToolStripMenuItem";
            this.移入查询ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.移入查询ToolStripMenuItem.Text = "移入查询";
            this.移入查询ToolStripMenuItem.Click += new System.EventHandler(this.移入查询ToolStripMenuItem_Click);
            // 
            // 交货分析ToolStripMenuItem
            // 
            this.交货分析ToolStripMenuItem.Name = "交货分析ToolStripMenuItem";
            this.交货分析ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.交货分析ToolStripMenuItem.Text = "交货分析";
            this.交货分析ToolStripMenuItem.Click += new System.EventHandler(this.交货分析ToolStripMenuItem_Click);
            // 
            // 出库管理ToolStripMenuItem
            // 
            this.出库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询ToolStripMenuItem2,
            this.移出查询ToolStripMenuItem});
            this.出库管理ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.出库管理ToolStripMenuItem.Name = "出库管理ToolStripMenuItem";
            this.出库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.出库管理ToolStripMenuItem.Text = "出库管理";
            this.出库管理ToolStripMenuItem.Visible = false;
            // 
            // 查询ToolStripMenuItem2
            // 
            this.查询ToolStripMenuItem2.Name = "查询ToolStripMenuItem2";
            this.查询ToolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.查询ToolStripMenuItem2.Text = "领料查询";
            this.查询ToolStripMenuItem2.Click += new System.EventHandler(this.查询ToolStripMenuItem2_Click);
            // 
            // 移出查询ToolStripMenuItem
            // 
            this.移出查询ToolStripMenuItem.Name = "移出查询ToolStripMenuItem";
            this.移出查询ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.移出查询ToolStripMenuItem.Text = "移出查询";
            this.移出查询ToolStripMenuItem.Click += new System.EventHandler(this.移出查询ToolStripMenuItem_Click);
            // 
            // 盘点管理ToolStripMenuItem
            // 
            this.盘点管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.盘点库存ToolStripMenuItem});
            this.盘点管理ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.盘点管理ToolStripMenuItem.Name = "盘点管理ToolStripMenuItem";
            this.盘点管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.盘点管理ToolStripMenuItem.Text = "盘点管理";
            this.盘点管理ToolStripMenuItem.Visible = false;
            // 
            // 盘点库存ToolStripMenuItem
            // 
            this.盘点库存ToolStripMenuItem.Name = "盘点库存ToolStripMenuItem";
            this.盘点库存ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.盘点库存ToolStripMenuItem.Text = "盘点库存";
            this.盘点库存ToolStripMenuItem.Click += new System.EventHandler(this.盘点库存ToolStripMenuItem_Click);
            // 
            // 报表ToolStripMenuItem
            // 
            this.报表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.即时库存ToolStripMenuItem1,
            this.安全库存预警ToolStripMenuItem1});
            this.报表ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.报表ToolStripMenuItem.Name = "报表ToolStripMenuItem";
            this.报表ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.报表ToolStripMenuItem.Text = "报表";
            this.报表ToolStripMenuItem.Visible = false;
            // 
            // 即时库存ToolStripMenuItem1
            // 
            this.即时库存ToolStripMenuItem1.Name = "即时库存ToolStripMenuItem1";
            this.即时库存ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.即时库存ToolStripMenuItem1.Text = "即时库存";
            this.即时库存ToolStripMenuItem1.Click += new System.EventHandler(this.即时库存ToolStripMenuItem1_Click);
            // 
            // 安全库存预警ToolStripMenuItem1
            // 
            this.安全库存预警ToolStripMenuItem1.Name = "安全库存预警ToolStripMenuItem1";
            this.安全库存预警ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.安全库存预警ToolStripMenuItem1.Text = "安全库存预警";
            this.安全库存预警ToolStripMenuItem1.Click += new System.EventHandler(this.安全库存预警ToolStripMenuItem1_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改密码ToolStripMenuItem});
            this.帮助ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Visible = false;
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
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(62)))), ((int)(((byte)(110)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusUser,
            this.toolStripStatusLabel2,
            this.statusTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(940, 29);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusUser
            // 
            this.statusUser.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.statusUser.ForeColor = System.Drawing.Color.White;
            this.statusUser.Name = "statusUser";
            this.statusUser.Size = new System.Drawing.Size(454, 24);
            this.statusUser.Spring = true;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 24);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // statusTime
            // 
            this.statusTime.ForeColor = System.Drawing.Color.White;
            this.statusTime.Name = "statusTime";
            this.statusTime.Size = new System.Drawing.Size(454, 24);
            this.statusTime.Spring = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(665, 475);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 38);
            this.label1.TabIndex = 4;
            this.label1.Text = "数字化产品通路";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(718, 527);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "  Version 1.0  ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(709, 337);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 135);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(709, 564);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "云御科技提供技术支持";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(689, 563);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(940, 668);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基础信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 入库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 盘点管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 盘点库存ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 交货分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 移出查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移入查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 即时库存ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 安全库存预警ToolStripMenuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}



