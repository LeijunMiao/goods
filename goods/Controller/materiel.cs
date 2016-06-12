using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class materielCtrl
    {
        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from materiel";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        public DataTable getFilterList(int pageIndex, int pageSize, string keyword)
        {
            string sql = "SELECT * FROM materiel ";
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
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            MaterielModel s = (MaterielModel)obj;
            string sql = "";

            sql = "insert into materiel (num,name,specifications,metering,subMetering,conversion,type,tax,isBatch) values('"
                + s.Num + "','" + s.Name + "','" + s.Specifications + "','" + s.Metering + "',@subMetering,'" + s.Conversion + "','" + s.Type + "','" + s.Tax + "',@isBatch);";
            MessageModel msg;
            try
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras.Add("@isBatch", s.IsBatch);
                paras.Add("@subMetering", s.SubMetering);
                int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);


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
                msg = new MessageModel(10005, e.ToString());
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
