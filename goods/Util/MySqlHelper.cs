using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

namespace goods
{
    public class MySqlHelper
    {
        private string connStr = string.Empty;

        public MySqlHelper()
        {
            //connStr = ConfigurationManager.AppSettings["connStr"].ToString();
            connStr = "Server=" + ServerInfo.ServerIP + ";Database=db_GoodsManage;Uid=sa;Pwd=killer123;charset=utf8";
        }
        public MySqlHelper(string connStr)
        {
            if (!string.IsNullOrEmpty(connStr))
            {
                this.connStr = connStr;
            }
        }

        #region 1、打开数据库
        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <returns>一个打开的数据库连接</returns>
        private MySqlConnection GetConn(MySqlConnection conn)
        {
            conn.ConnectionString = connStr;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }
        #endregion

        #region 2、执行不带参数的增删改SQL语句或存储过程
        /// <summary>
        ///  执行不带参数的增删改SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">增删改SQL语句或存储过程</param>
        /// <param name="ct">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlStr, CommandType ct = CommandType.Text)
        {
            MySqlCommand cmd = null;
            int res;
            using (cmd = new MySqlCommand(sqlStr, GetConn(new MySqlConnection())))
            {
                cmd.CommandType = ct;
                res = cmd.ExecuteNonQuery();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
            }
            return res;
        }
        #endregion

        #region 3、执行带参数的增删改SQL语句或存储过程
        /// <summary>
        ///  执行带参数的增删改SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">增删改SQL语句或存储过程</param>
        /// <param name="ct">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlStr, Dictionary<string, object> dictParas, CommandType ct = CommandType.Text)
        {
            MySqlCommand cmd = null;
            int res;
            using (cmd = new MySqlCommand(sqlStr, GetConn(new MySqlConnection())))
            {
                cmd.CommandType = ct;
                cmd.Parameters.AddRange(ConvertToParas(dictParas));
                res = cmd.ExecuteNonQuery();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
            }
            return res;
        }
        #endregion

        #region 4、执行带参数的增删改SQL语句或存储过程
        /// <summary>
        ///  执行带参数的增删改SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">增删改SQL语句或存储过程</param>
        /// <param name="ct">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlStr, MySqlParameter[] paras, CommandType ct = CommandType.Text)
        {
            MySqlCommand cmd = null;
            int res;
            using (cmd = new MySqlCommand(sqlStr, GetConn(new MySqlConnection())))
            {
                cmd.CommandType = ct;
                cmd.Parameters.AddRange(paras);
                res = cmd.ExecuteNonQuery();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
            }
            return res;
        }
        #endregion

        #region 5、执行不带参数的查询SQL语句或存储过程
        /// <summary>
        ///  执行不带参数的查询SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">查询SQL语句或存储过程</param>
        /// <param name="ct">命令类型</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string sqlStr, CommandType ct = CommandType.Text)
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand(sqlStr, GetConn(new MySqlConnection()));
            cmd.CommandType = ct;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            using (adapter.SelectCommand = cmd)
            {
                adapter.Fill(dt);
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
            }
            return dt;
        }
        #endregion

        #region 6、执行带参数的查询SQL语句或存储过程
        /// <summary>
        ///  执行带参数的查询SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">查询SQL语句或存储过程</param>
        /// <param name="paras">参数集合</param>
        /// <param name="ct">命令类型</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string sqlStr, Dictionary<string, object> dictParas, CommandType ct = CommandType.Text)
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand(sqlStr, GetConn(new MySqlConnection()));
            cmd.CommandType = ct;
            cmd.Parameters.AddRange(ConvertToParas(dictParas));
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            using (adapter.SelectCommand = cmd)
            {
                adapter.Fill(dt);
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
            }
            return dt;
        }
        #endregion

        #region 7、执行带参数的查询SQL语句或存储过程
        /// <summary>
        ///  执行带参数的查询SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">查询SQL语句或存储过程</param>
        /// <param name="paras">参数集合</param>
        /// <param name="ct">命令类型</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string sqlStr, MySqlParameter[] paras, CommandType ct = CommandType.Text)
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand(sqlStr, GetConn(new MySqlConnection()));
            cmd.CommandType = ct;
            cmd.Parameters.AddRange(paras);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            using (adapter.SelectCommand = cmd)
            {
                adapter.Fill(dt);
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
            }
            return dt;
        }
        #endregion

        #region 8、执行事务
        public bool ExcuteTransaction(List<string> sqlList, Dictionary<string, object> paraList = null)
        {
            bool result = false;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    if (paraList != null && paraList.Count > 0)
                    {
                        cmd.Parameters.AddRange(ConvertToParas(paraList));
                    }
                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        cmd.CommandText = sqlList[i];
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                    tx = conn.BeginTransaction();
                    result = true;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    result = false;
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
            }
            return result;
        }
        #endregion

        #region Dictionary转MySqlParameters
        /// <summary>
        /// Dictionary转MySqlParameters
        /// </summary>
        /// <param name="dictParas">Dictionary<string, object> dictParas</param>
        /// <returns>MySqlParameter[]</returns>
        private MySqlParameter[] ConvertToParas(Dictionary<string, object> dictParas)
        {
            MySqlParameter[] paras = new MySqlParameter[dictParas.Count];
            int i = 0;
            foreach (var item in dictParas)
            {
                paras[i] = new MySqlParameter("?" + item.Key, item.Value);
                i++;
            }
            return paras;
        }
        #endregion
    }
}
