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
        public DataTable getFilterList(int pageIndex, int pageSize, SupplierModel s)
        {
            string sql = "SELECT id,num,name,isActive FROM supplier ";
            string select = "";
            if (s.Num != "")
            {
                select += " where num like '%" + s.Num + "%'";
            }
            if (s.Name != "")
            {
                if (select == "") select += " where ";
                else select += " and ";
                select += " name like '%" + s.Name + "%'";
            }
            if(s.IsActive != null)
            {
                select += " and isActive = @isActive ";
            }
            sql += select;
            if (pageIndex < 1) pageIndex = 1;
            sql += " order by id desc,isActive desc  ";
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@isActive", s.IsActive);
            DataTable dt = h.ExecuteQuery(sql, paras, CommandType.Text);
            return dt;

        }
        public int getCount(SupplierModel s)
        {
            string sql = "SELECT  count(*) FROM supplier ";
            string select = "";
            if (s.Num != "")
            {
                select += " where num like '%" + s.Num + "%'";
            }
            if (s.Name != "")
            {
                if (select == "") select += " where ";
                else select += " and ";
                select += " name like '%" + s.Name + "%'";
            }
            if (s.IsActive != null)
            {
                select += " and isActive = @isActive ";
            }
            sql += select;
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@isActive", s.IsActive);
            DataTable dt = h.ExecuteQuery(sql, paras, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        } 
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            SupplierModel s = (SupplierModel)obj;
            string sql = "";
            if (s.Id > 0) sql = "UPDATE supplier SET num = @num,name = @name WHERE id = @id ";
            else sql = "insert into supplier (num,name,createdAt) values(@num,@name,'" + DateTime.Now + "');";
            MessageModel msg;
            try
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras.Add("@num", s.Num);
                paras.Add("@name", s.Name);
                paras.Add("@id", s.Id);
                int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);

                
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

        #region 更新供应商状态
        public MessageModel switchStatus(int id, bool isActive)
        {
            MessageModel msg = new MessageModel();
            string sql = "UPDATE supplier SET isActive = @isActive WHERE id = '" + id + "' ";
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
