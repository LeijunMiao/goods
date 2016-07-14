using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using goods.Model;
using System.Data;
namespace goods.Controller
{
    class informationCtrl
    {

        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取
        public DataTable getbyFilter(string type)//int pageIndex, int pageSize, 
        {
            if (type == "")
            {
                type = "order";
            }
            string sql = "SELECT u.id uid,u.fullName,u.userName,mu.id FROM messageuser mu inner join  user u on mu.user = u.id where mu.type = '" + type + "'";
            sql += " order by u.id desc ";
            //if (pageIndex < 1) pageIndex = 1;
            //sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            MessageModel msg = new MessageModel();
            return msg;
        }
        #endregion
        #region 新建一堆
        public MessageModel addbyids(List<object> list,string key)
        {
            List<User> list_u = new List<User>();
            string sqlSelect = "SELECT * FROM messageuser where type = '"+ key + "' and user in (";
            User u;
            for (int i = 0; i < list.Count; i++)
            {
                u = (User)list[i];
                list_u.Add(u);
                if (i != 0) sqlSelect += ",";
                sqlSelect += "'" + u.Id + "'";
            }
            sqlSelect += ")";
            DataTable dt = h.ExecuteQuery(sqlSelect, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list_u.Remove(list_u.Where(p => p.Id == Convert.ToInt32(dt.Rows[i]["user"])).FirstOrDefault());
            }
            int res;
            MessageModel msg;
            if (list_u.Count >0)
            {
                string sql = "insert into messageuser (user,usernum,type) values";
                for (int i = 0; i < list_u.Count; i++)
                {
                    if (i != 0) sql += ",";
                    sql += "('" + list_u[i].Id + "','" + list_u[i].UserName + "','" + key + "')";
                }
                res = h.ExecuteNonQuery(sql, CommandType.Text);
                if (res > 0)
                {
                    msg = new MessageModel(0, "新建成功");
                }
                else msg = new MessageModel(10005, "新建失败");
                return msg;
            }
            else
            {
                return msg = new MessageModel(20003, "用户已添加");
            }
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
            MessageModel msg;
            string sql = "DELETE FROM messageuser WHERE id = '" + obj.ToString() + "' ";
            int res = h.ExecuteNonQuery(sql, CommandType.Text);
            if (res > 0)
            {
                msg = new MessageModel(0, "删除成功");
            }
            else msg = new MessageModel(10005, "删除失败");
            return msg;
        }
        #endregion
        
    }
}
