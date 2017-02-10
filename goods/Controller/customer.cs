using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using goods.Model;
using System.Data;
namespace goods.Controller
{
    class customerCtrl
    {

        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from customer";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        public int getCount(string key)
        {
            string sql = "SELECT Count(*) FROM customer ";
            string select = "";
            if (key != "")
            {
                select += " where num like '%" + key + "%' or name like '%" + key + "%'";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        public DataTable getFilterList(int pageIndex, int pageSize, string keyword)
        {
            string sql = "SELECT * FROM customer ";
            string select = "";
            if (keyword != "")
            {
                select += " where num like '%" + keyword + "%' or name like '%" + keyword + "%'";
            }
            sql += select;
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }

        /// <summary>
        /// 筛选活动的客户
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable getSelect(int pageIndex, int pageSize, string keyword)
        {
            string sql = "SELECT * FROM customer where isActive = 1";
            string select = "";
            if (keyword != "")
            {
                select += " and num like '%" + keyword + "%' or name like '%" + keyword + "%'";
            }
            sql += select;
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        /// <summary>
        /// 筛选活动的客户数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int getSelectCount(string key)
        {
            string sql = "SELECT Count(*) FROM customer where isActive = 1";
            string select = "";
            if (key != "")
            {
                select += " and num like '%" + key + "%' or name like '%" + key + "%'";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }

        #region 新建
        public MessageModel add(object obj)
        {
            CustomerModel model = (CustomerModel)obj;
            var dt = getCustombyName(model.Name, "");
            if (dt.Rows.Count > 0)
            {
                return new MessageModel(20003, "名称已被注册！");
            }
            string sql = "";

            sql = "insert into customer (num,name) values('"
                + model.Num + "','" + model.Name + "');";
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, CommandType.Text);
                if (res > 0)
                {
                    msg = new MessageModel(0, "新建成功", model);
                }
                else msg = new MessageModel(10005, "新建失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！";
                if (e.ToString().IndexOf("PRIMARY") != -1) err = "编码重复！";
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion
        #region 更新
        public MessageModel set(object obj)
        {
            string sql = "";
            CustomerModel model = (CustomerModel)obj;
            var dt = getCustombyName(model.Name, model.Num);
            if (dt.Rows.Count > 0)
            {
                return new MessageModel(20003, "名称已被注册！");
            }
            sql = "UPDATE customer SET name = '" + model.Name + "' WHERE num = '" + model.Num + "' ";
            int res = h.ExecuteNonQuery(sql, CommandType.Text);
            MessageModel msg;
            if (res > 0)
            {
                msg = new MessageModel(0, "更新成功");
            }
            else msg = new MessageModel(10005, "更新失败");
            return msg;
        }
        #endregion
        #region 删除
        public MessageModel del(object obj)
        {
            MessageModel msg;
            string sql = "DELETE FROM customer WHERE num = '" + obj.ToString() + "' ";
            int res = h.ExecuteNonQuery(sql, CommandType.Text);
            if (res > 0)
            {
                msg = new MessageModel(0, "删除成功");
            }
            else msg = new MessageModel(10005, "删除失败");
            return msg;
        }
        #endregion

        #region 获取客户byName
        public DataTable getCustombyName(string name, string num)
        {
            string sql = "select * from customer where name = '" + name + "' ";
            if (num != "") sql += "and num != '" + num + "'";
            sql += "limit 1 ";
            return h.ExecuteQuery(sql, CommandType.Text);
        }
        #endregion
    }
}
