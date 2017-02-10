using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using goods.Model;
using System.Data;
namespace goods.Controller
{
    class bomCtrl
    {

        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get(int pageIndex, int pageSize,string materiel)
        {
            string sql = "select m.id,bom.id bomid,bom.num,m.num mnum,m.name,m.specifications,case bom.isActive when 1 then '活动' else '注销' end as status  from bommain bom inner join materiel m on bom.materiel = m.id";
            if (materiel != "")
            {
                sql += " and (m.num like '%" + materiel + "%' or m.name like '%" + materiel + "%') ";
            }
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 获取
        public int getCount(string materiel)
        {
            string sql = "select count(*)  from bommain bom inner join materiel m on bom.materiel = m.id ";
            if (materiel != "")
            {
                sql += " and (m.num like '%" + materiel + "%' or m.name like '%" + materiel + "%') ";
            }
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        #endregion
        #region 获取byId
        public DataTable getbyid(List<int> ids)
        {
            string sql = "select bom.id,bom.num,bom.quantity mainqty,dtl.id did,dtl.quantity dtlqty,dtl.children,m.id mid,m.num mnum,m.name,m.specifications,m.type,meter.name metering,dtl.materiel,dtl.quantity,dtl.remark,mchild.num mcnum,mchild.name mcname,mchild.type mctype,mchild.specifications msspe,mchild.normprice ,meter2.name mcmetering  from bommain bom inner join bomdetail dtl on dtl.parent = bom.id inner join materiel mchild on dtl.materiel = mchild.id inner join materiel m on bom.materiel = m.id and bom.id in ( ";
            for (int i = 0; i < ids.Count; i++)
            {
                if (i != 0) sql += ",";
                sql += ids[i];
            }
            sql += ") inner join metering meter on meter.id = m.metering inner join metering meter2 on meter2.id = mchild.metering ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取byId下层循环
        public DataTable getbyid2(List<int> ids)
        {
            string sql = "select bom.id,bom.num,bom.quantity mainqty,dtl.id did,dtl2.id dparent,dtl.quantity dtlqty,dtl.children,m.id mid,m.num mnum,m.name,m.specifications,m.type,meter.name metering,dtl.materiel,dtl.quantity,dtl.remark,mchild.num mcnum,mchild.name mcname,mchild.type mctype,mchild.specifications msspe,mchild.normprice ,meter2.name mcmetering  from bommain bom inner join bomdetail dtl on dtl.parent = bom.id inner join materiel mchild on dtl.materiel = mchild.id inner join materiel m on bom.materiel = m.id  inner join bomdetail dtl2 on dtl2.children = bom.id and dtl2.id in ( ";
            for (int i = 0; i < ids.Count; i++)
            {
                if (i != 0) sql += ",";
                sql += ids[i];
            }
            sql += ") inner join metering meter on meter.id = m.metering inner join metering meter2 on meter2.id = mchild.metering ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取by主BOM Id
        public DataTable getbyMianId(int id)
        {
            string sql = "SELECT main.num,main.quantity,main.jump,main.isActive,m2.id mmid,m2.num mnum,m2.name mname,m2.specifications,meter2.name metering,m.id, m.num cmnum, m.name cmname, m.specifications cmspe,meter.name cmetering, dtl.quantity dtlquantity,dtl.remark FROM bomdetail dtl inner join bommain main on dtl.parent = main.id inner join materiel m on dtl.materiel = m.id inner join materiel m2 on main.materiel = m2.id inner join metering meter on meter.id = m.metering inner join metering meter2 on meter2.id = m2.metering where main.id = '" + id + "' ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 新建
        public MessageModel add(BOMMainModel obj)
        {
            List<string> sqlList = new List<string>();
            string num = "(SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_NAME = 'bommain')";

            string sqlMain = "insert into bommain (materiel,quantity,jump,isActive,num) " +
                " values(@materiel, @quantity, @jump,@isActive, concat('BOM',LPAD(" + num + ",9,'0')));";
            sqlList.Add(sqlMain);
            string sqlDetail = "insert into bomdetail (parent,materiel,quantity,remark,children) values ";

            //string sqlMateriel = "update materiel set bom = 1 where id = @materiel";
            if (obj.isActive)
            {
                string sqlOtherBOM = "update bommain set isActive = 0 where materiel = @materiel and id != last_insert_id()";
                sqlList.Add(sqlOtherBOM);
                string sqlUpdateDetail = "update bomdetail dtl inner join bommain main on dtl.parent = main.id set dtl.children = last_insert_id() where dtl.materiel = @materiel and main.isActive = 1";
                sqlList.Add(sqlUpdateDetail);
            }

            string children;
            for (int i = 0; i < obj.list.Count; i++)
            {
                if (i != 0)
                {
                    sqlDetail += ",";
                }
                children = "(select id from bommain where materiel = '"+ obj.list[i].materiel + "' and isActive = 1 limit 1)";
                sqlDetail += " (last_insert_id(),'" + obj.list[i].materiel + "','" + obj.list[i].quantity + "' ,'" + obj.list[i].remark + "',"+ children + ")";
            }

            sqlList.Add(sqlDetail);
            //sqlList.Add(sqlMateriel);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@materiel", obj.materiel);
            paras.Add("@quantity", obj.quantity);
            paras.Add("@jump", obj.jump);
            paras.Add("@isActive", obj.isActive);
            bool result = h.ExcuteTransaction(sqlList, paras);
            MessageModel msg = new MessageModel();
            if (result == true)
            {
                msg = new MessageModel(0, "新建成功");
            }
            else msg = new MessageModel(10005, "新建失败");
            return msg;
        }
        #endregion
        #region 更新
        public MessageModel set(BOMMainModel obj)
        {
            MessageModel msg = new MessageModel();
            List<int> list_del = new List<int>();
            for (int i = 0; i < obj.list.Count; i++)
            {
                list_del.Add(obj.list[i].materiel);
            }
            string oldsql = "SELECT dtl.id,dtl.materiel, dtl.remark,dtl.quantity FROM bomdetail dtl inner join bommain main on dtl.parent = main.id WHERE dtl.parent = '" + obj.id + "'";
            DataTable dtold = h.ExecuteQuery(oldsql, CommandType.Text);

            Dictionary<int, BOMDetailModel> map_old = new Dictionary<int, BOMDetailModel>();
            List<string> sqlList = new List<string>();
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@id", obj.id);
            paras.Add("@quantity", obj.quantity);
            paras.Add("@jump", obj.jump);

            var sqlnew = "";
            var sqlupdate = "";
            var sqldel = "";
            var children = "";
            var sqlUpMain = "update bommain set quantity = @quantity , jump = @jump where id = @id";
            sqlList.Add(sqlUpMain);

            for (int i = 0; i < dtold.Rows.Count; i++)
            {
                var materiel = Convert.ToInt32(dtold.Rows[i]["materiel"]);
                map_old.Add(materiel, new BOMDetailModel(dtold.Rows[i]));
                if (!list_del.Contains(materiel))
                {
                    if (sqldel == "") sqldel = "delete from bomdetail where parent = '" + obj.id + "' AND materiel in (";
                    else sqldel += ",";
                    sqldel += " '" + materiel + "' ";
                }
            }
            if (sqldel != "")
            {
                sqldel += ")";
                sqlList.Add(sqldel);
            }
            for (int i = 0; i < obj.list.Count; i++)
            {
                if (map_old.ContainsKey(obj.list[i].materiel))
                {
                    var remark = map_old[obj.list[i].materiel].remark;
                    var quantity = map_old[obj.list[i].materiel].quantity;
                    var id = map_old[obj.list[i].materiel].id;
                    if (obj.list[i].remark != remark || obj.list[i].quantity != quantity)
                    {
                        sqlupdate = "";
                        if (obj.list[i].remark != remark) sqlupdate += " update bomdetail set remark = '" + obj.list[i].remark + "'";
                        if (obj.list[i].quantity != quantity)
                        {
                            if (sqlupdate == "") sqlupdate += "update bomdetail set quantity = '" + obj.list[i].quantity + "'";
                            else sqlupdate += ",quantity = '" + obj.list[i].quantity + "'";
                        }
                        sqlupdate += " where materiel = '" + obj.list[i].materiel + "' AND parent = '" + obj.id + "'";
                        sqlList.Add(sqlupdate);
                    }
                }
                else
                {
                    if (sqlnew == "")
                    {
                        sqlnew += "insert into bomdetail (parent,materiel,quantity,remark,children) values  ";
                    }
                    else
                    {
                        sqlnew += ",";
                    }
                    children = "(select id from bommain where materiel = '" + obj.list[i].materiel + "' and isActive = 1 limit 1)";
                    sqlnew += " ('"+ obj.id + "','" + obj.list[i].materiel + "','" + obj.list[i].quantity + "' ,'" + obj.list[i].remark + "'," + children + ")";
                }
            }

            if (sqlnew != "") sqlList.Add(sqlnew);
            bool result = h.ExcuteTransaction(sqlList, paras);

            if (result == true)
            {
                msg = new MessageModel(0, "保存成功");
            }
            else msg = new MessageModel(10005, "保存失败");
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

        #region 按id获取bom
        public DataTable getlastinsert(int materiel)
        {
            string sql = "select num from bommain where materiel = '" + materiel + "' limit 1";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 更新状态
        public MessageModel switchStatus(int id, int materiel, bool isActive)
        {
            List<string> sqlList = new List<string>();
            MessageModel msg = new MessageModel();
            string sql = "UPDATE bommain SET isActive = !isActive WHERE id = '" + id + "' ";
            string sqlUpdateDetail;
            sqlList.Add(sql);
            if (!isActive)
            {
                string sqlSelet = "SELECT dtl.id did,dtl.children,dtl.materiel,main.id FROM bomdetail dtl left join bommain main on dtl.materiel = main.materiel and main.isActive = 1 where dtl.parent  = '" + id + "'";
                DataTable dt = h.ExecuteQuery(sqlSelet, CommandType.Text);

                string sqlOtherBOM = "update bommain set isActive = 0 where materiel = '" + materiel + "' and id != '" + id + "'";
                sqlList.Add(sqlOtherBOM);
                 sqlUpdateDetail = "update bomdetail dtl inner join bommain main on dtl.parent = main.id set dtl.children = '" + id + "' where dtl.materiel = '" + materiel + "' and main.isActive = 1";

                string sqlUp = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["id"] == DBNull.Value) sqlUp = "update bomdetail dtl inner join bommain main on dtl.parent = main.id set dtl.children = null where dtl.id = " + dt.Rows[i]["did"].ToString();
                    else sqlUp = "update bomdetail dtl inner join bommain main on dtl.parent = main.id set dtl.children = " + dt.Rows[i]["id"].ToString() + " where dtl.id = " + dt.Rows[i]["did"].ToString();
                    sqlList.Add(sqlUp);
                }
            }
            else
            {
                sqlUpdateDetail = "update bomdetail dtl inner join bommain main on dtl.parent = main.id set dtl.children = null where dtl.materiel = '" + materiel + "' and main.isActive = 1";
            }
            sqlList.Add(sqlUpdateDetail);
            bool result = h.ExcuteTransaction(sqlList);
            if (result)
            {
                msg = new MessageModel(0, "更新成功");
            }
            else msg = new MessageModel(10005, "更新失败");

            return msg;
        }
        #endregion
        #region 获取所有父节点
        public List<int> getParent(int id)
        {
            string areaids = "";
            string sqlarea = "select queryParentAreaInfo('" + id + "');";
            DataTable dtarea = h.ExecuteQuery(sqlarea, CommandType.Text);
            areaids = dtarea.Rows[0][0].ToString();
            List<int> list = areaids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Cast<int>().ToList<int>();
            return list;
        }
        #endregion
    }
}
