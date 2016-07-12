using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using goods.Model;
using System.Data;
using goods.Interface;
using System.Data.SqlClient;

namespace goods.Controller
{
    class roleCtrl: main
    {
        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from role";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 按部门获取
        public DataTable getbyDepId(int depId)
        {
            string sql = "select * from role where departmentId = '"+ depId + "' order by id desc";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 查询同部门职位
        public DataTable getSimilarRole(int id)
        {
            string sql = "SELECT * FROM role where departmentId = (select departmentId from role where id = '"+ id + "'); ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        
        #region 新建
        public MessageModel add(object obj)
        {
            string sql = "";
            RoleModel rm = (RoleModel)obj;
            SqlParameter[] para =
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            sql = "insert into role (name,createdAt,departmentId) values('" + rm.Name + "','" + DateTime.Now + "','" + rm.DepartmentId + "');";//select @id=@@IDENTITY
            //int res = DBHelp.GetSQL(sql);
            int res = h.ExecuteNonQuery(sql, CommandType.Text);

            //dep.Id = Convert.ToInt32(para[0].Value.ToString());
            MessageModel msg;
            if (res > 0)
            {
                msg = new MessageModel(0, "新建成功", rm);
            }
            else msg = new MessageModel(10005, "新建失败");
            return msg;
        }
        #endregion
        #region 更新
        public MessageModel set(object obj)
        {
            string sql = "";
            RoleModel rm = (RoleModel)obj;
            sql = "UPDATE role SET name = '" + rm.Name + "' WHERE id = '" + rm.Id + "' ";
            //int res = DBHelp.GetSQL(sql);
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
            RoleModel rm = (RoleModel)obj;
            string sql = "SELECT count(id) FROM user where role = '" + rm.Id + "' ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            if (Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                return msg = new MessageModel(20003, "该角色已被关联，不允许删除!");
            }
            else
            {
                sql = "DELETE FROM role WHERE id = '" + rm.Id + "' ";
                int res = h.ExecuteNonQuery(sql, CommandType.Text);
                if (res > 0)
                {
                    msg = new MessageModel(0, "删除成功");
                }
                else msg = new MessageModel(10005, "删除失败");
                return msg;
            }
            
        }
        #endregion
    }
}
