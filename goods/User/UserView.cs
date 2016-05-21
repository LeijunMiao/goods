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

namespace goods
{
    public partial class UserView : Form
    {
        //总记录数
        userCtrl uctrl = new userCtrl();
        private DataTable dtData = null;
        private DataTable dt = null;

        public int RecordCount = 0;
        public UserView()
        {
            InitializeComponent();
        }
        public void loadData() {
            BindDataWithPage(1);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }
        private void pageIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(pagingCom1.PageIndex+"");
            BindDataWithPage(pagingCom1.PageIndex);
        }
        /// <summary>
        /// 绑定第Index页的数据
        /// </summary>
        /// <param name="Index"></param>
        private void BindDataWithPage(int Index)
        {
            pagingCom1.PageIndex = Index;
            pagingCom1.PageSize = 10;
            dtData = uctrl.getUserList();
            dt = new DataTable();
            DataColumn dcNO = new DataColumn("编号");
            DataColumn dcUName = new DataColumn("用户名");
            DataColumn dcFName = new DataColumn("姓名");
            DataColumn dcRole = new DataColumn("职位");
            dt.Columns.Add(dcNO);
            dt.Columns.Add(dcUName);
            dt.Columns.Add(dcFName);
            dt.Columns.Add(dcRole);

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtData.Rows[i]["id"].ToString();
                dr[1] = dtData.Rows[i]["userName"].ToString();
                dr[2] = dtData.Rows[i]["fullName"].ToString();
                dr[3] = dtData.Rows[i]["name"].ToString();
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;

            //获取并设置总记录数
            pagingCom1.RecordCount = RecordCount;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UserPopup popup = new UserPopup(this);
            popup.Show();
        }
    }
}
