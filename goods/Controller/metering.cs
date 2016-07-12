using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class meteringCtrl
    {
        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from metering where isActive = 1";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }

        public DataTable getFilterList(int pageIndex, int pageSize, string keyword)
        {
            string sql = "SELECT id,num,name,isActive FROM metering ";
            string select = "";
            if (keyword != "")
            {
                select += " where num like '%" + keyword + "%' or name like '%" + keyword + "%'";
            }
            sql += select + " order by id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }

        #endregion
        public int getCount(string key)
        {
            string sql = "SELECT Count(id) FROM metering ";
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
        #region 新建
        public MessageModel add(object obj)
        {
            MeteringModel s = (MeteringModel)obj;
            string sql = "";
            if (s.Id > 0) sql = "UPDATE metering SET num = '" + s.Num + "',name = '" + s.Name + "' WHERE id = '" + s.Id + "' ";
            else sql = "insert into metering (num,name) values('"+ s.Num + "','" + s.Name + "');";
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, CommandType.Text);


                if (res > 0)
                {
                    msg = new MessageModel(0, "保存成功", s);
                }
                else msg = new MessageModel(10005, "保存失败");
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
        #region 更新计量单位状态
        public MessageModel switchStatus(int id, bool isActive)
        {
            MessageModel msg = new MessageModel();
            string sql = "UPDATE metering SET isActive = @isActive WHERE id = '" + id + "' ";
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@isActive", !isActive);
            int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);

            if (res > 0)
            {
                msg = new MessageModel(0, "更新成功");
            }
            else msg = new MessageModel(10005, "更新失败");

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
