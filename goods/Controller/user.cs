using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using goods.Interface;
using System.Data;
using goods.Model;
using System.Data.SqlClient;
using System.Security.Cryptography;
namespace goods.Controller
{
    class userCtrl
    {
        MySqlHelper h = new MySqlHelper();
        DBHelp db = new DBHelp();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from user";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        public int getUserListCount(int role, string fullName)
        {
            string sql = "SELECT  count(*) FROM db_goodsmanage.user ";
            string select = "";
            if (role != -1)
            {
                select += " where role =" + role;
            }
            if (fullName != "")
            {
                if (select == "") select += " where ";
                else select += " and ";
                select += " fullName like '%" + fullName + "%'";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        public DataTable getUserList(int pageIndex, int pageSize, int role, string fullName)
        {
            innerParmas parmas = new innerParmas(pageIndex, pageSize, role, fullName) ;
            string sql = "SELECT u.id,u.fullName,u.userName,r.name FROM db_goodsmanage.user u , db_goodsmanage.role r " +
                "where u.role = r.id  ";
            if (parmas.role != -1)
            {
                sql += " and u.role = " + parmas.role;
            }
            if(parmas.fullName != "")
            {
                sql += " and u.fullName like '%" + parmas.fullName + "%'";
            }
            if (parmas.pageIndex < 1) parmas.pageIndex = 1;
            sql +=" LIMIT " + (parmas.pageIndex-1) * parmas.pageSize + "," + parmas.pageSize;
            //sql += ";SELECT FOUND_ROWS();";
            
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            //DataSet ds = h.ExecutePagingQuery(sql, CommandType.Text);
            return dt;
            
        }

        #region 筛选获取

        #endregion
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            User u = (User)obj;
            string sql = "";
            MD5 md5 = new MD5CryptoServiceProvider();
            string hash = GetOf(u.Hashed_password);

            sql = "insert into user (userName,fullName,role,hashed_password,createdAt) values('"
                + u.UserName + "','" + u.FullName + "','" + u.Role + "','" 
                + hash + "','" + DateTime.Now + "');";
            int res = h.ExecuteNonQuery(sql, CommandType.Text);

            MessageModel msg;
            if (res > 0)
            {
                msg = new MessageModel(0, "新建成功", u);
            }
            else msg = new MessageModel(10005, "新建失败");
            return msg;
        }
        /// <summary>返回 MD5 值</summary>
        /// <param name="myString">要转换的 MD5 值的字符串</param>
        public string GetOf(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            return byte2String;
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

        /// <summary>
        /// 内部类：搜索信息
        /// </summary>
        public class innerParmas
        {
            public int pageIndex;
            public int pageSize;
            public int role;
            public string fullName;

            public innerParmas(int pageIndex, int pageSize, int role, string fullName)
            {
                this.pageIndex = pageIndex;
                this.pageSize = pageSize;
                this.role = role;
                this.fullName = fullName;
            }
        }
    }
}
