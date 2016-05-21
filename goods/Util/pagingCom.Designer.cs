namespace goods
{
    partial class pagingCom
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pagingCom));
            this.lblPager = new System.Windows.Forms.Label();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnToPageIndex = new System.Windows.Forms.Button();
            this.lbPre = new System.Windows.Forms.Label();
            this.lbEnd = new System.Windows.Forms.Label();
            this.txtToPageIndex = new System.Windows.Forms.TextBox();
            //this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            //this.imglstPager = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lblPager
            // 
            this.lblPager.AutoSize = true;
            this.lblPager.Location = new System.Drawing.Point(56, 14);
            this.lblPager.Name = "lblPager";
            this.lblPager.Size = new System.Drawing.Size(287, 12);
            this.lblPager.TabIndex = 0;
            this.lblPager.Text = "总共{0}条记录,当前第{1}页,共{2}页,每页{3}条记录";
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(371, 9);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(75, 23);
            this.btnFirst.TabIndex = 1;
            this.btnFirst.Text = "首页";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(452, 9);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 2;
            this.btnPrevious.Text = "上一页";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(533, 9);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(614, 9);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 4;
            this.btnLast.Text = "末页";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnToPageIndex
            // 
            this.btnToPageIndex.Location = new System.Drawing.Point(853, 9);
            this.btnToPageIndex.Name = "btnToPageIndex";
            this.btnToPageIndex.Size = new System.Drawing.Size(75, 23);
            this.btnToPageIndex.TabIndex = 5;
            this.btnToPageIndex.Text = "跳转";
            this.btnToPageIndex.UseVisualStyleBackColor = true;
            this.btnToPageIndex.Click += new System.EventHandler(this.btnToPageIndex_Click);
            // 
            // lbPre
            // 
            this.lbPre.AutoSize = true;
            this.lbPre.Location = new System.Drawing.Point(717, 14);
            this.lbPre.Name = "lbPre";
            this.lbPre.Size = new System.Drawing.Size(17, 12);
            this.lbPre.TabIndex = 6;
            this.lbPre.Text = "第";
            // 
            // lbEnd
            // 
            this.lbEnd.AutoSize = true;
            this.lbEnd.Location = new System.Drawing.Point(810, 14);
            this.lbEnd.Name = "lbEnd";
            this.lbEnd.Size = new System.Drawing.Size(17, 12);
            this.lbEnd.TabIndex = 7;
            this.lbEnd.Text = "页";
            // 
            // txtToPageIndex
            // 
            this.txtToPageIndex.Location = new System.Drawing.Point(740, 11);
            this.txtToPageIndex.Name = "txtToPageIndex";
            this.txtToPageIndex.Size = new System.Drawing.Size(64, 21);
            this.txtToPageIndex.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            //this.contextMenuStrip1.Name = "contextMenuStrip1";
            //this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // imglstPager
            // 
            //this.imglstPager.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstPager.ImageStream")));
            //this.imglstPager.TransparentColor = System.Drawing.Color.Transparent;
            //this.imglstPager.Images.SetKeyName(0, "first.gif");
            //this.imglstPager.Images.SetKeyName(1, "prev.gif");
            //this.imglstPager.Images.SetKeyName(2, "next.gif");
            //this.imglstPager.Images.SetKeyName(3, "last.gif");
            // 
            // paging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtToPageIndex);
            this.Controls.Add(this.lbEnd);
            this.Controls.Add(this.lbPre);
            this.Controls.Add(this.btnToPageIndex);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.lblPager);
            this.Name = "paging";
            this.Size = new System.Drawing.Size(981, 41);
            this.Load += new System.EventHandler(this.WinFormPager_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WinFormPager_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblPager;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnToPageIndex;
        private System.Windows.Forms.Label lbPre;
        private System.Windows.Forms.Label lbEnd;
        private System.Windows.Forms.TextBox txtToPageIndex;
        //private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        //  private System.Windows.Forms.ImageList imglstPager;
    }
}
