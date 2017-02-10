using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class stockCtrl
    {

        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable get()
        {
            string sql = "select * from ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 按条件获取批次库存
        public DataTable[] getFilterListLimit(int pageIndex, int pageSize, string materirl,string warehouse,string position, string supplier, int category, List<int> ids)
        {
            DataTable[] dts = new DataTable[2];

            string areaids = "";
            if (category > 0)
            {
                string sqlarea = "select queryChildrenAreaInfo('" + category + "');";
                DataTable dtarea = h.ExecuteQuery(sqlarea, CommandType.Text);
                areaids = dtarea.Rows[0][0].ToString();
            }

            string sql = " SELECT s.id,s.avaquantity,s.batchnum,bm.num batchTNum,sup.name supplier,m.num,m.name,w.name wname,p.name pname, s.combination,c.name category FROM materiel m left join category c on m.category = c.id, warehouse w,stock s left join position p on s.position = p.id left join batchmateriel bm on s.batchnum = bm.id  left join supplier sup on s.supplier = sup.id ";
            string select = " Where m.id = s.materiel AND w.id = s.warehouse AND s.avaquantity > 0 ";
            string filter = "";
            if (materirl != "")
            {
                select += " AND (m.num like '%" + materirl + "%' or m.name like '%" + materirl + "%')";
            }
            if (warehouse != "")
            {
                select += " AND (w.num like '%" + warehouse + "%' or w.name like '%" + warehouse + "%')";
            }
            if (position != "")
            {
                select += " AND (p.num like '%" + position + "%' or p.name like '%" + position + "%')";
            }
            if (supplier != "")
            {
                select += " AND (sup.name like '%" + supplier + "%')";
            }
            if (areaids != "")
            {
                select += " AND m.category in (" + areaids + ") ";
            }
            sql += select;
            if (ids.Count > 0)
            {
                filter += " AND s.id not in (";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0) filter += ids[i];
                    else filter += "," + ids[i];
                }
                filter += ")";
                sql += filter;
            }
            sql += " order by s.id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            dts[0] = h.ExecuteQuery(sql, CommandType.Text);

            string sqlCount = " SELECT Count(s.id) FROM materiel m, warehouse w,stock s left join position p on s.position = p.id left join supplier sup on s.supplier = sup.id ";
            string selectCount = " Where m.id = s.materiel AND w.id = s.warehouse AND s.avaquantity > 0 ";
            string filterCount = "";
            if (materirl != "")
            {
                selectCount += " AND (m.num like '%" + materirl + "%' or m.name like '%" + materirl + "%')";
            }
            if (warehouse != "")
            {
                selectCount += " AND (w.num like '%" + warehouse + "%' or w.name like '%" + warehouse + "%')";
            }
            if (position != "")
            {
                selectCount += " AND (p.num like '%" + position + "%' or p.name like '%" + position + "%')";
            }
            if (supplier != "")
            {
                selectCount += " AND (sup.name like '%" + supplier + "%')";
            }
            if (areaids != "")
            {
                selectCount += " AND m.category in (" + areaids + ") ";
            }
            sqlCount += selectCount;
            if (ids.Count > 0)
            {
                filterCount += " AND s.id not in (";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0) filterCount += ids[i];
                    else filterCount += "," + ids[i];
                }
                filterCount += ")";
                sqlCount += filterCount;
            }
            dts[1] = h.ExecuteQuery(sqlCount, CommandType.Text);

            return dts;
        }
        #endregion
        #region 按条件获取盘点单
        public DataTable getCheckList(int pageIndex, int pageSize,string num,DateTime date,bool isDate)
        {
            var lagedate = date.AddDays(1);
            string sql = " SELECT cl.num,cl.date,case cl.status when 1 then '结束' else '开始' end as status ,u.fullname user FROM checklist cl inner join user u on cl.user = u.id ";
            string filter = "";
            if(num != "")
            {
                filter += " where cl.num like '%" + num + "%' ";
            }
            if (isDate)
            {
                if (filter != "") filter += " AND ";
                else filter += " where ";
                filter += " cl.date >= '" + date + "' and  cl.date < '" + lagedate + "'";
            }
            sql += filter + " order by cl.id desc ";
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 按条件获取盘点单数量
        public int getCheckListCount( string num, DateTime date, bool isDate)
        {
            var lagedate = date.AddDays(1);
            string sql = " SELECT Count(id) FROM checklist ";
            string filter = "";
            if (num != "")
            {
                filter += " where num like '%" + num + "%' ";
            }
            if (isDate)
            {
                if (filter != "") filter += " AND ";
                else filter += " where ";
                filter += " date >= '" + date + "' and  date < '" + lagedate + "'";
            }
            sql += filter;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        #endregion
        
        #region 获取批次库存数量
        public int getCount(string materirl, string warehouse, string position, List<int> ids)
        {
            string sql = " SELECT Count(s.id) FROM materiel m, warehouse w,stock s left join position p on s.position = p.id ";
            string select = " Where m.id = s.materiel AND w.id = s.warehouse AND s.avaquantity > 0 ";
            string filter = "";
            if (materirl != "")
            {
                select += " AND (m.num like '%" + materirl + "%' or m.name like '%" + materirl + "%')";
            }
            if (warehouse != "")
            {
                select += " AND (w.num like '%" + warehouse + "%' or w.name like '%" + warehouse + "%')";
            }
            if (position != "")
            {
                select += " AND (p.num like '%" + position + "%' or p.name like '%" + position + "%')";
            }

            sql += select;
            if (ids.Count > 0)
            {
                filter += " AND s.id not in (";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0) filter += ids[i];
                    else filter += "," + ids[i];
                }
                filter += ")";
                sql += filter;
            }
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        #endregion
        #region 按id获取批次库存
        public DataTable getByids(List<int> ids)
        {
            string sql = " SELECT s.id,s.batchnum,bm.num batchTNum,m.id materiel,m.name,w.id warehouse,w.name wname,p.id position,p.name pname,s.avaquantity,sup.name supplier,sup.id supplierId, s.combination FROM materiel m, warehouse w,stock s left join position p on s.position = p.id left join batchmateriel bm on s.batchnum = bm.id left join supplier sup on s.supplier = sup.id Where m.id = s.materiel AND w.id = s.warehouse AND s.avaquantity > 0";
            string filter = "";
            if (ids.Count > 0)
            {
                filter += " AND s.id in (";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0) filter += ids[i];
                    else filter += "," + ids[i];
                }
                filter += ")";
                sql += filter;
            }
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 按id获取批次库存
        public DataTable getlastinsert(int user)
        {
            string sql = "select * from checklist where user = '"+user+ "' order by id desc limit 1";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 按编号批次库存
        public DataTable getByNum(string num)
        {
            string sql = " SELECT c.id,c.date, cm.batchnum,bm.num batchTNum,m.name,w.name wname,p.name pname,cm.truequantity,cm.avaquantity,cm.combination, sup.name supplier " +
                    " FROM materiel m, checklist c, warehouse w, checkmateriel cm left join position p on cm.position = p.id left join batchmateriel bm on cm.batchnum = bm.id, supplier sup " +
                    " where m.id = cm.materiel AND w.id = cm.warehouse  and cm.checklist = c.id and cm.supplier = sup.id and c.num = '" + num + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 新建
        public MessageModel add(CheckListModel ck)
        {
            List<string> sqlList = new List<string>();
            string num = "(SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_NAME = 'checklist')";

            string sqlOrder = "insert into checklist (user,num) " +
                " values(@user, concat('CHECK',LPAD(" + num + ",9,'0')));";
            
            string sqlMat = "insert into checkmateriel (checklist,supplier,materiel,truequantity,warehouse,batchnum,position,combination) values ";
            for (int i = 0; i < ck.list_sm.Count; i++)
            {
                if (i != 0) sqlMat += ",";
                sqlMat += " (last_insert_id(),'" + ck.list_sm[i].supplier + "','" + ck.list_sm[i].materiel + "','" + ck.list_sm[i].truequantity + "','" + ck.list_sm[i].warehouse + "',";
                if (ck.list_sm[i].batchnum != null) sqlMat += "'" + ck.list_sm[i].batchnum + "',";
                else sqlMat += "null,";
                if(ck.list_sm[i].position != null) sqlMat += "'" + ck.list_sm[i].position + "',";
                else sqlMat += "null,";
                if (ck.list_sm[i].combination != null)
                {
                    sqlMat += "'" + ck.list_sm[i].combination + "')";
                }
                else
                {
                     sqlMat += "null)";
                }
            }

            sqlList.Add(sqlOrder);
            sqlList.Add(sqlMat);

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@user", ck.user);

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
    }
}
