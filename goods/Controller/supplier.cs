using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using goods.Model;
using System.Data;
namespace goods.Controller
{
    class supplierCtrl
    {
        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from supplier";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        public DataTable getFilterList(int pageIndex, int pageSize, string num, string name)
        {
            string sql = "SELECT id,num,name FROM supplier ";
            string select = "";
            if (num != "")
            {
                select += " where num like '%" + num + "%'";
            }
            if (name != "")
            {
                if (select == "") select += " where ";
                else select += " and ";
                select += " name like '%" + name + "%'";
            }
            sql += select;
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }
        public int getCount(string num, string name)
        {
            string sql = "SELECT  count(*) FROM supplier ";
            string select = "";
            if (num != "")
            {
                select += " where num like '%" + num + "%'";
            }
            if (name != "")
            {
                if (select == "") select += " where ";
                else select += " and ";
                select += " name like '%" + name + "%'";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        } 
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            SupplierModel s = (SupplierModel)obj;
            string sql = "";

            sql = "insert into supplier (num,name,createdAt) values('"
                + s.Num + "','" + s.Name + "','" + DateTime.Now + "');";
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, CommandType.Text);

                
                if (res > 0)
                {
                    msg = new MessageModel(0, "新建成功", s);
                }
                else msg = new MessageModel(10005, "新建失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！";
                if (e.ToString().IndexOf("num_UNIQUE") != -1) err = "编码重复！";
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion
        #region 更新
        public MessageModel set(object obj)
        {
            MessageModel msg = new MessageModel();
            return msg;
        }
        #endregion
        #region 删除
        public MessageModel del(object obj)
        {
            MessageModel msg = new MessageModel();
            return msg;
        }
        #endregion
    }
}
