using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class categoryCtrl
    {

        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select id,name,isActive, case isActive when 1 then '活动' else '注销' end as status,parent from category";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 获取
        public DataTable getActive()
        {
            string sql = "select id,name,parent from category where isActive = 1";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            CategoryModel model = (CategoryModel)obj;
            string sql = "insert into category (name,parent) values(@name,@parent);";
            MessageModel msg;
            try
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras.Add("@parent", model.parent);
                paras.Add("@name", model.name);

                int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);


                if (res > 0)
                {
                    msg = new MessageModel(0, "保存成功");
                }
                else msg = new MessageModel(10005, "保存失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！" +e.ToString();
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion
        #region 更新
        public MessageModel set(object obj)
        {
            CategoryModel model = (CategoryModel)obj;
            MessageModel msg = new MessageModel();
            string sql = "UPDATE category SET name = @name WHERE id = @id ";
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@id", model.id);
            paras.Add("@name", model.name);
            int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);

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
            MessageModel msg = new MessageModel();
            return msg;
        }
        #endregion

        #region 更新状态
        public MessageModel switchStatus(int id, bool isActive)
        {
            string sql = "";
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@id", id);
            paras.Add("@isActive", !isActive);
            if (isActive)
            {
                sql = "select count(*) from category where isActive = 1 and parent = @id";
                DataTable dt = h.ExecuteQuery(sql, paras, CommandType.Text);
                if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    return new MessageModel(20003, "请先注销子项。");
                }
            }
            MessageModel msg = new MessageModel();
            sql = "UPDATE category SET isActive = @isActive WHERE id = @id ";
            int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);

            if (res > 0)
            {
                msg = new MessageModel(0, "更新成功");
            }
            else msg = new MessageModel(10005, "更新失败");

            return msg;
        }
        #endregion
    }
}
