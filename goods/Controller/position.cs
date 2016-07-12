using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class positionCtrl
    {
        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from position";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        public DataTable getByWId(int wid)
        {
            string sql = "select * from position where warehouse =" + wid;
            sql += " order by id desc ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            PositionModel s = (PositionModel)obj;
            string sql = "";
            if (s.Id > 0) sql = "UPDATE position SET num = '" + s.Num + "',name = '" + s.Name + "' WHERE id =  '" + s.Id + "'";
            else
            {
                string sqlPos = "select count(*) from warehouse where id = '" + s.Warehouse + "' and isActive = 0";
                DataTable dt = h.ExecuteQuery(sqlPos, CommandType.Text);
                if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    return new MessageModel(20001, "更新失败,请先激活仓库。");
                }
                sql = "insert into position (num,name,warehouse) values('" + s.Num + "','" + s.Name + "','" + s.Warehouse + "');";
            }
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
        #region 更新仓位状态
        public MessageModel switchStatus(int id, bool isActive)
        {
            MessageModel msg = new MessageModel();
            if (!isActive)
            {
                string sqlPos = "select count(*) from position p inner join warehouse w on p.warehouse = w.id where p.id = '" + id + "' and w.isActive = 0";
                DataTable dt = h.ExecuteQuery(sqlPos, CommandType.Text);
                if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    return new MessageModel(20001, "更新失败,请先激活仓库。");
                }
            }
            string sql = "UPDATE position SET isActive = @isActive WHERE id = '" + id + "' ";
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
