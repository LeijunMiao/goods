using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class solidbackingCtrl
    {
        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select id,name,isActive, case isActive when 1 then '活动' else '注销' end as status  from solidbacking";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 获取
        public DataTable getAttrValue(int solidbacking)
        {
            string sql = "select id,name,isActive, case isActive when 1 then '活动' else '注销' end as status  from attrvalue where solidbacking = '"+ solidbacking + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        
        #region 新建
        public MessageModel add(object obj)
        {
            SolidBackingModel model = (SolidBackingModel)obj;
            string sql = "insert into solidbacking (name) values('" + model.name + "');";
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, CommandType.Text);


                if (res > 0)
                {
                    msg = new MessageModel(0, "保存成功");
                }
                else msg = new MessageModel(10005, "保存失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！" + e.ToString();
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion
        #region 新建属性值
        public MessageModel addAttrValue(object obj)
        {
            AttrValueModel model = (AttrValueModel)obj;
            string sql = "insert into attrvalue (name,solidbacking) values('" + model.name + "','" + model.solidbacking + "');";
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, CommandType.Text);


                if (res > 0)
                {
                    msg = new MessageModel(0, "保存成功");
                }
                else msg = new MessageModel(10005, "保存失败");
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
            SolidBackingModel model = (SolidBackingModel)obj;
            MessageModel msg = new MessageModel();
            string sql = "UPDATE solidbacking SET name = @name WHERE id = @id ";
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
        #region 更新属性值
        public MessageModel setAttrValue(object obj)
        {
            AttrValueModel model = (AttrValueModel)obj;
            MessageModel msg = new MessageModel();
            string sql = "UPDATE attrvalue SET name = @name WHERE id = @id ";
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
            MessageModel msg = new MessageModel();
            string sql = "UPDATE solidbacking SET isActive = @isActive WHERE id = '" + id + "' ";
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

        #region 更新属性值状态
        public MessageModel switchAttrValueStatus(int id, bool isActive)
        {
            MessageModel msg = new MessageModel();
            string sql = "UPDATE attrvalue SET isActive = @isActive WHERE id = '" + id + "' ";
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

        #region 增加物料-辅助属性
        public MessageModel addMaterielSolidBacking(object obj)
        {
            MaterSolidBackingM model = (MaterSolidBackingM)obj;
            string sql = "insert into materielsolidbacking (materiel,solidbacking) values('" + model.materiel + "','" + model.solidbacking + "');";
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, CommandType.Text);


                if (res > 0)
                {
                    msg = new MessageModel(0, "保存成功");
                }
                else msg = new MessageModel(10005, "保存失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！" + e.ToString();
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion

        #region 删除物料-辅助属性
        public MessageModel delMaterielSolidBacking(object obj)
        {
            MaterSolidBackingM model = (MaterSolidBackingM)obj;
            string sql = "delete from materielsolidbacking where materiel = '" + model.materiel + "' and solidbacking = '" + model.solidbacking + "'";
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, CommandType.Text);


                if (res > 0)
                {
                    msg = new MessageModel(0, "删除成功");
                }
                else msg = new MessageModel(10005, "删除失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！" + e.ToString();
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion
        
        #region 获取物料辅助属性
        public DataTable getMaterielSolidBacking(int materiel)
        {
            string sql = "SELECT s.id,s.name,ms.id msid FROM solidbacking s left join materielsolidbacking ms on s.id = ms.solidbacking and ms.materiel = '" + materiel + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 根据物料获取辅助属性
        public DataTable getMaterielAttrValue(int materiel)
        {
            string sql = "SELECT a.id, a.name, s.id sId,s.name sName FROM attrvalue a inner join solidbacking s on a.solidbacking = s.id and s.isActive = 1 inner join materielsolidbacking ms on ms.solidbacking = s.id and ms.materiel = '" + materiel + "' where a.isActive = 1; ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        public int getCombin(int materiel, Dictionary<int, attrClass> map)
        {
            string sql = "SELECT ci.combination,ci.attrvalue,ci.solidbacking FROM combinationitem ci inner join attrcombination ab on ci.combination = ab.id and ab.materiel = '" + materiel + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            var attrNum = map.Keys.Count;
            Dictionary<int, int> map_com_num = new Dictionary<int, int>();
            int solidbacking, combination;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                solidbacking = Convert.ToInt32(dt.Rows[i]["solidbacking"]);
                combination = Convert.ToInt32(dt.Rows[i]["combination"]);
                if (!map_com_num.Keys.Contains(combination)) map_com_num.Add(combination,0);
                if (map.Keys.Contains(solidbacking) && map[solidbacking].valueId == Convert.ToInt32(dt.Rows[i]["attrvalue"]))
                {
                    map_com_num[combination]++;
                    if (map_com_num[combination] == attrNum) return combination;
                 }
            }
            List<string> sqlList = new List<string>();
            string sqladd = "insert into attrcombination (materiel) values ('"+ materiel + "')";
            string sqlAttr = "insert into combinationitem (combination,solidbacking, attrvalue ) values ";
            int j = 0;
            foreach (int i in map.Keys)
            {
                if (j != 0) sqlAttr += ",";
                sqlAttr += " (last_insert_id(),"+ i + ","+ map[i].valueId+ ") ";
                j++;
            }
            sqlList.Add(sqladd);
            sqlList.Add(sqlAttr);
            bool result = h.ExcuteTransaction(sqlList);
            if (result == true) {
                string sqlSelect = "select id from attrcombination where materiel = '"+ materiel + "' order by id desc limit 1";
                DataTable dtcom = h.ExecuteQuery(sqlSelect, CommandType.Text);
                return Convert.ToInt32(dtcom.Rows[0][0]); 
            }
            else return -1;
        }

        #region 获取组合属性
        public Dictionary<int, string> getbyCombIds(List<int> ids)
        {
            Dictionary<int, string> map = new Dictionary<int, string>();
            if (ids.Count == 0) return map;
            string sql = "SELECT c.id,av.name FROM attrcombination c inner join combinationitem ci on c.id = ci.combination inner join attrvalue av on ci.attrvalue = av.id  where c.id in (";
            for (int i = 0; i < ids.Count; i++)
            {
                if (i != 0) sql += ",";
                sql += ids[i];
            }
            sql += ")";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var id = Convert.ToInt32(dt.Rows[i]["id"]);
                if (!map.Keys.Contains(id))
                {
                    map.Add(id, "");
                }
                if (map[id] != "") map[id] += ",";
                map[id] += dt.Rows[i]["name"].ToString();
            }
            return map;
        }
        #endregion
    }
}
