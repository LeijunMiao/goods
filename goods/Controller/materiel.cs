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
        #endregion

        #region 获取安全库存
        public DataTable getStock(List<int> ids)
        {
            string sql = "SELECT m.id,m.num,m.name,me.name metering,s.safetystock,s.maxstock FROM metering me,materiel m left join safetystock s " +
                           " on m.id = s.materiel where m.metering = me.id ";
            if (ids.Count > 0)
            {
                sql += " and m.id in ( ";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0) sql += ids[i];
                    else sql += "," + ids[i];
                }
                sql += ")";
            }
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        public int getCountAll(string key, int category)
        {
            string sql = "SELECT Count(*) FROM materiel m left join category c on m.category = c.id ";
            string select = "";
            if (key != "")
            {
                select += " where m.num like '%" + key + "%' or m.name like '%" + key + "%'";
            }
            if (category > 0)
            {
                if (select != "") select += " and ";
                else select += " where ";
                select += " m.category = '" + category + "' ";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        public int getCount(string key, List<int> ids)
        {
            string sql = "SELECT Count(*) FROM materiel where isActive = 1 and type = '外购件' ";
            string select = "";
            string filter = "";
            if (key != "")
            {
                select += " and num like '%" + key + "%' or name like '%" + key + "%'";
            }
            if (ids.Count > 0)
            {
                filter += " and id not in (";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0) filter += ids[i];
                    else filter += "," + ids[i];
                }
                filter += ")";
                sql += filter;
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        public DataTable[] getFilterList(int pageIndex, int pageSize, string keyword,int category)
        {
            DataTable[] dts = new DataTable[2];
            string areaids = "";
            if(category >0)
            {
                string sqlarea = "select queryChildrenAreaInfo('"+ category + "');";
                DataTable dtarea = h.ExecuteQuery(sqlarea, CommandType.Text);
                areaids = dtarea.Rows[0][0].ToString();
            }
            string sql = "SELECT m.id,m.num,m.name,m.specifications,m.conversion,m.type,m.tax,m.isActive,meter.name metering,meter2.name subMetering,m.isBatch,case m.isBatch when 1 then '是' else '否' end as batchStatus ,c.name category " +
                "FROM metering meter,materiel m left join metering meter2 on m.subMetering = meter2.id left join category c on m.category = c.id ";
            string select = " where m.metering = meter.id ";
            if (keyword != "")
            {
                select += " and (m.num like '%" + keyword + "%' or m.name like '%" + keyword + "%')";
            }
            if (areaids != "")
            {
                select += " and m.category in (" + areaids + ") ";
            }
            sql += select + " order by m.id desc ";

            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            dts[0] = dt;

            string sqlcount = "SELECT Count(*) FROM materiel m left join category c on m.category = c.id ";
            string selectcount = "";
            if (keyword != "")
            {
                selectcount += " where m.num like '%" + keyword + "%' or m.name like '%" + keyword + "%'";
            }
            if (areaids != "")
            {
                if (selectcount != "") selectcount += " and ";
                else selectcount += " where ";
                selectcount += " m.category in (" + areaids + ") ";
            }
            sqlcount += selectcount;
            DataTable dtcount = h.ExecuteQuery(sqlcount, CommandType.Text);
            dts[1] = dtcount;
            return dts;

        }
        public DataTable getStockWarning(int pageIndex, int pageSize, string keyword)
        {
            string sql = "SELECT m.id,m.num,m.name,me.name metering,s.safetystock,s.maxstock,s.stock FROM metering me,materiel m , safetystock s " +
                           " where m.id = s.materiel and m.metering = me.id and (s.stock - s.safetystock < 0) ";
            if (keyword != "")
            {
                sql += " and (m.num like '%" + keyword + "%' or m.name like '%" + keyword + "%')";
            }
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }
        
        public int getStockWarningCount(string keyword)
        {
            string sql = "SELECT count(s.id) FROM materiel m, safetystock s " +
                           " where m.id = s.materiel  and (s.stock - s.safetystock < 0) ";
            string select = "";
            if (keyword != "")
            {
                sql += " and (m.num like '%" + keyword + "%' or m.name like '%" + keyword + "%')";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        public DataTable getFilterListLimit(int pageIndex, int pageSize, string keyword, List<int> ids)
        {
            string sql = "SELECT * FROM materiel where isActive = 1 and type = '外购件' ";
            string select = "";
            string filter = "";
            if (keyword != "")
            {
                select += " and (num like '%" + keyword + "%' or name like '%" + keyword + "%')";
            }
            sql += select;
            if (ids.Count > 0)
            {
                filter += " and id not in (";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0) filter += ids[i];
                    else filter += "," + ids[i];
                }
                filter += ")";
                sql += filter;
            }
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }
        
        public DataTable getByids(List<int> ids)
        {
            string sql = "SELECT m.id,m.num,m.name,m.specifications,m.conversion,m.type,m.tax,meter.name metering,meter2.name subMetering,count(ms.id) attrnum " +
                "FROM metering meter,materiel m left join metering meter2 on m.subMetering = meter2.id left join materielsolidbacking ms on m.id = ms.materiel  where m.metering = meter.id";
            string select = " and m.id in (";
            for (int i = 0; i < ids.Count; i++)
            {
                if(i == 0) select += ids[i];
                else select += "," + ids[i];
            }
            select += ")  group by m.id";
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }

        #region 获取物料根据id
        public DataTable getByid(int id)
        {
            string sql = "SELECT m.id,m.num,m.name,m.specifications,m.conversion,m.type,m.tax,m.metering,m.subMetering,m.isBatch,m.category,c.name categoryName FROM materiel m left join category c on m.category = c.id where m.id  = '" + id + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取物料图片根据id
        public DataTable getImages(int id)
        {
            string sql = "SELECT * FROM image where materiel = '" + id + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        
        #region 新建
        public MessageModel add(object obj)
        {
            MaterielModel s = (MaterielModel)obj;
            string sql = "";
            if (s.Id > 0) sql = "UPDATE materiel SET num = '" + s.Num + "',name = '" + s.Name + "',specifications = '" + s.Specifications + "',metering = '" + s.Metering + "',subMetering = @subMetering,conversion = @conversion,type = '" + s.Type + "',tax = '" + s.Tax + "',isBatch = @isBatch,category = '"+s.Catgegory+"' WHERE id = '" + s.Id + "' ";
            else sql = "insert into materiel (num,name,specifications,metering,subMetering,conversion,type,tax,isBatch,category) values('"
                + s.Num + "','" + s.Name + "','" + s.Specifications + "','" + s.Metering + "',@subMetering,@conversion,'" + s.Type + "','" + s.Tax + "',@isBatch, '" + s.Catgegory + "');";
            MessageModel msg;
            try
            {
                Dictionary<string, object> paras = new Dictionary<string, object>();
                paras.Add("@isBatch", s.IsBatch);
                paras.Add("@subMetering", s.SubMetering);
                paras.Add("@conversion", s.Conversion);
                int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);
                if (res > 0)
                {
                    msg = new MessageModel(0, "保存成功", s);
                }
                else msg = new MessageModel(10005, "保存失败");
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
        #region 新建安全库存
        public MessageModel addstock(List<MaterielModel> list_m)
        {
            List<string> sqlList = new List<string>();

            string sql = "";
            for (int i = 0; i < list_m.Count; i++)
            {
                sql = "insert into safetystock (materiel,safetystock,maxstock,stock) values "+
                      " ('" + list_m[i].Id + "','" + list_m[i].safetystock + "','" + list_m[i].maxstock + "',(SELECT sum(avaquantity) FROM stock where materiel = '"+ list_m[i].Id + "')) " +
                      " ON DUPLICATE KEY UPDATE safetystock = '" + list_m[i].safetystock + "',maxstock = '" + list_m[i].maxstock + "' ";
                sqlList.Add(sql);
            }
            MessageModel msg;
            try
            {
                bool result = h.ExcuteTransaction(sqlList);
                msg = new MessageModel();
                if (result == true)
                {
                    msg = new MessageModel(0, "设置成功");
                }
                else msg = new MessageModel(10005, "设置失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！" +e.Message;
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion
        #region 新建物料图片
        public MessageModel addImage(ImageModel img)
        {
            List<string> sqlList = new List<string>();

            string sql = "insert into image (name,url,materiel) values (@name,@url,@materiel)";
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@name", img.name);
            paras.Add("@url", img.url);
            paras.Add("@materiel", img.materiel);
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);
                if (res > 0)
                {
                    msg = new MessageModel(0, "保存成功");
                }
                else msg = new MessageModel(10005, "保存失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！" + e.Message;
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion
        #region 删除物料图片
        public MessageModel delImage(ImageModel img)
        {
            List<string> sqlList = new List<string>();

            string sql = "delete from image where name = @name and materiel = @materiel";
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@name", img.name);
            paras.Add("@materiel", img.materiel);
            MessageModel msg;
            try
            {
                int res = h.ExecuteNonQuery(sql, paras, CommandType.Text);
                if (res > 0)
                {
                    msg = new MessageModel(0, "删除成功");
                }
                else msg = new MessageModel(10005, "删除失败");
            }
            catch (Exception e)
            {
                string err = "服务器错误，请重试！" + e.Message;
                msg = new MessageModel(10005, err);
            }
            return msg;
        }
        #endregion
        
        #region 更新物料状态
        public MessageModel switchStatus(int id, bool isActive)
        {
            MessageModel msg = new MessageModel();
            string sql = "UPDATE materiel SET isActive = @isActive WHERE id = '" + id + "' ";
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

        public int findOrderMateral(int id)
        {
            string sql = "select count(id) from ordermateriel where materiel = '"+ id + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return Convert.ToInt32( dt.Rows[0][0]);
        }
    }
}
