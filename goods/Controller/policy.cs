using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class policyCtrl
    {
        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from policy";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取功能
        public DataTable getFeature()
        {
            string sql = "select * from feature";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取功能
        public DataTable getFeatureByRole(int roleId)
        {
            string sql = "select f.name,f.id,f.key,p.id pid from policy p, feature f where p.feature = f.id and  p.role =" + roleId;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 新建
        public MessageModel add(object obj)
        {
            PolicyModel s = (PolicyModel)obj;


            string sql = "insert into policy (role,feature) select '" + s.Role + "','" + s.Feature + "' from DUAL WHERE NOT EXISTS(select id from policy where role = '" + s.Role + "' and feature = '" + s.Feature + "') limit 1 ;";
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, CommandType.Text);  //ExecuteNonQuery

                if (res > 0)
                {
                    msg = new MessageModel(0, "新建成功", s);
                }
                else msg = new MessageModel(10005, "新建失败或已存在该权限");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！" + e.ToString();
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
            MessageModel msg;
            PolicyModel pm = (PolicyModel)obj;
            string sql = "DELETE FROM policy WHERE id = '" + pm.Id + "' ";
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
