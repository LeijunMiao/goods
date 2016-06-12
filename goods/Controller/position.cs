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
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            PositionModel s = (PositionModel)obj;
            string sql = "";

            sql = "insert into position (num,name,warehouse) values('"
                + s.Num + "','" + s.Name + "','" + s.Warehouse + "');";
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
