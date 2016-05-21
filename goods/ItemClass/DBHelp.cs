using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace GoodsReportManage
{
    class DBHelp
    {
        public static string connectionString = "server="+ServerInfo.ServerIP+";DataBase=db_GoodsManage;uid="+ServerInfo.User_ID+";pwd="+ServerInfo.User_Pwd+"";
        //Data Source=.;Initial Catalog=db_GoodsManage;Persist Security Info=True;User ID="+ServerInfo.User_ID+";Password="+ServerInfo.User_Pwd+"";
        private static SqlConnection connection;
        public static SqlConnection Connection
        {
            get
            {

                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }

        public void Openconnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            else if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            else if (connection.State == System.Data.ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }
        }

        public void Openconnection1()
        {
            connection.Close();
            connection.Open();
        }

        public void Closeconnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
            }
            else if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }

        }

        public static int ExecuteCommand(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        public static int ExecuteCommand(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }

        public static int GetScalar(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            return result;
        }

        public static int GetScalar(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }

        public static SqlDataReader GetReader(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            connection.Close();
            return reader;
        }

        public static SqlDataReader GetReader(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public static DataTable GetDataSet(string safeSql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            connection.Close();
            return ds.Tables[0];
        }

        public static DataTable GetDataSet(string sql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }


        public static int GetSQL(string strsql)
        {
            SqlCommand comm = new SqlCommand(strsql, Connection);
            try
            {
                int Result = comm.ExecuteNonQuery();
                connection.Close();
                return Result;
            }
            catch (Exception ex)
            {
                string sa = ex.Message;
                return 0;
            }
        }


        public static void InsertAll()
        {
            string sql = "select Inventory_no,goods_no,goods_model from sys_Inventory";
            DataTable dt = GetDataSet(sql);
            sql = " insert into sys_stock (stock_no,goods_no,goods_model) values(@stock_no,@goods_no,@goods_model)";
            SqlCommand cmd = new SqlCommand(sql, Connection);

            cmd.Parameters.Add(new SqlParameter(@"stock_no", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter(@"goods_no", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter(@"goods_model", SqlDbType.NVarChar));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.Parameters[@"stock_no"].Value = dt.Rows[i]["Inventory_no"].ToString();
                cmd.Parameters[@"goods_no"].Value = dt.Rows[i]["goods_no"].ToString();
                cmd.Parameters[@"goods_model"].Value = dt.Rows[i]["goods_model"].ToString();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {

                }
            }
            sql = "update sys_Inventory set enabled='N'";
            GetSQL(sql);
        }
    }
}
