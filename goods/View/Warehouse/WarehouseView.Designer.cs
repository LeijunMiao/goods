﻿namespace goods
{
    partial class WarehouseView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarehouseView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.新建ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.修改ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.注销ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.打印ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.pagingCom1 = new goods.pagingCom();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(957, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(332, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "搜索";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(149, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "编码/名称";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(972, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.修改ToolStripMenuItem,
            this.注销ToolStripMenuItem,
            this.打印ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(45, 22);
            this.toolStripDropDownButton1.Text = "仓库";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new System.EventHandler(this.修改ToolStripMenuItem_Click);
            // 
            // 注销ToolStripMenuItem
            // 
            this.注销ToolStripMenuItem.Name = "注销ToolStripMenuItem";
            this.注销ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.注销ToolStripMenuItem.Text = "注销/激活";
            this.注销ToolStripMenuItem.Click += new System.EventHandler(this.注销ToolStripMenuItem_Click);
            // 
            // 打印ToolStripMenuItem
            // 
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.打印ToolStripMenuItem.Text = "打印";
            this.打印ToolStripMenuItem.Click += new System.EventHandler(this.打印ToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem1,
            this.修改ToolStripMenuItem1,
            this.注销ToolStripMenuItem1,
            this.打印ToolStripMenuItem1});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(45, 22);
            this.toolStripDropDownButton2.Text = "仓位";
            // 
            // 新建ToolStripMenuItem1
            // 
            this.新建ToolStripMenuItem1.Name = "新建ToolStripMenuItem1";
            this.新建ToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.新建ToolStripMenuItem1.Text = "新建";
            this.新建ToolStripMenuItem1.Click += new System.EventHandler(this.新建ToolStripMenuItem1_Click);
            // 
            // 修改ToolStripMenuItem1
            // 
            this.修改ToolStripMenuItem1.Name = "修改ToolStripMenuItem1";
            this.修改ToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.修改ToolStripMenuItem1.Text = "修改";
            this.修改ToolStripMenuItem1.Click += new System.EventHandler(this.修改ToolStripMenuItem1_Click);
            // 
            // 注销ToolStripMenuItem1
            // 
            this.注销ToolStripMenuItem1.Name = "注销ToolStripMenuItem1";
            this.注销ToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.注销ToolStripMenuItem1.Text = "注销/激活";
            this.注销ToolStripMenuItem1.Click += new System.EventHandler(this.注销ToolStripMenuItem1_Click);
            // 
            // 打印ToolStripMenuItem1
            // 
            this.打印ToolStripMenuItem1.Name = "打印ToolStripMenuItem1";
            this.打印ToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.打印ToolStripMenuItem1.Text = "打印";
            this.打印ToolStripMenuItem1.Click += new System.EventHandler(this.打印ToolStripMenuItem1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(468, 321);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.loadPosition);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(477, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(468, 321);
            this.dataGridView2.TabIndex = 9;
            // 
            // pagingCom1
            // 
            this.pagingCom1.BtnTextNext = "下页";
            this.pagingCom1.BtnTextPrevious = "上页";
            this.pagingCom1.DisplayStyle = goods.pagingCom.DisplayStyleEnum.文字;
            this.pagingCom1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagingCom1.Location = new System.Drawing.Point(0, 448);
            this.pagingCom1.Name = "pagingCom1";
            this.pagingCom1.RecordCount = 5;
            this.pagingCom1.Size = new System.Drawing.Size(972, 41);
            this.pagingCom1.TabIndex = 8;
            this.pagingCom1.PageIndexChanged += new goods.pagingCom.EventHandler(this.pageIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 115);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(948, 327);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // WarehouseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 489);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pagingCom1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WarehouseView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仓库管理";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void DataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private pagingCom pagingCom1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注销ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 注销ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打印ToolStripMenuItem1;
    }
}

