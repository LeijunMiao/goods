using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using goods.Interface;
using System.Data;
using goods.Model;
using System.Data.SqlClient;

namespace goods.Controller
{
    class departmentCtrl : main
    {
        MySqlHelper h = new MySqlHelper();
        public DataTable get()
        {
            //string sql = "select * from department order by parentId,sortby";
            //DataTable dt  = DBHelp.GetDataSet(sql);
            //return dt;
            string sql = "select * from department order by parentId,sortby";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
            
        }
        public DataTable getChildren()
        {
            string sql = "select * from department where parentId !=0 order by parentId,sortby";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }
        
        public MessageModel add(object obj)
        {
            string sql = "";
            DepartmentModel dep =  (DepartmentModel)obj;
            SqlParameter[] para =
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            if (dep.ParentId != null)
            {
                sql = "insert into department (name,createdAt,parentId) values('" + dep.Name + "','" + DateTime.Now + "','" + dep.ParentId + "');";//select @id=@@IDENTITY

            }
            else
            {
                sql = "insert into department (name,createdAt) values('" + dep.Name + "','" + DateTime.Now + "');";//select @id=@@IDENTITY

            }
            //int res = DBHelp.GetSQL(sql);
            int res = h.ExecuteNonQuery(sql, CommandType.Text);
            
            //dep.Id = Convert.ToInt32(para[0].Value.ToString());
            MessageModel msg;
            if (res > 0)
            {
                msg = new MessageModel(0, "新建成功", dep);
            }
            else msg = new MessageModel(10005, "新建失败");
            return msg;
        }
        public MessageModel set(object obj)
        {
            string sql = "";
            DepartmentModel dep = (DepartmentModel)obj;
            sql = "UPDATE department SET name = '" + dep.Name + "' WHERE id = '" + dep.Id + "' ";
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

        public MessageModel del(object obj)
        {
            MessageModel msg;
            DepartmentModel dep = (DepartmentModel)obj;
            string sql = "SELECT count(id) FROM role where departmentId = '" + dep.Id + "' ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            if(Convert.ToInt32(dt.Rows[0][0]) >0)
            {
                return msg = new MessageModel(20003, "该组织已被关联，不允许删除!");
            }
            else
            {
                sql = "SELECT count(id) FROM department where parentId = '" + dep.Id + "' ";
                dt = h.ExecuteQuery(sql, CommandType.Text);
                if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    return msg = new MessageModel(20003, "该组织有子分类，请先删除!");
                }
                else
                {
                    sql = "DELETE FROM department WHERE id = '" + dep.Id + "' ";
                    int res = h.ExecuteNonQuery(sql, CommandType.Text);

                    if (res > 0)
                    {
                        msg = new MessageModel(0, "删除成功");
                    }
                    else msg = new MessageModel(10005, "删除失败");
                    return msg;
                }
            }
            
        }
    }
}
