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
        public DataTable getFilterListLimit(int pageIndex, int pageSize, string materirl,string warehouse,string position, List<int> ids)
        {
            string sql = " SELECT s.id,s.avaquantity,s.batchnum,bm.num batchTNum,sup.name supplier,m.name,w.name wname,p.name pname FROM materiel m, warehouse w,stock s left join position p on s.position = p.id left join batchmateriel bm on s.batchnum = bm.id left join ordermateriel om on bm.ordermateriel = om.id left join purchaseorder po on om.purchaseorder = po.id left join supplier sup on po.supplier = sup.id ";
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
            sql += " order by s.id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
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
            string sql = " SELECT Count(id) FROM db_goodsmanage.checklist ";
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
            string sql = " SELECT s.id,s.batchnum,bm.num batchTNum,m.id materiel,m.name,w.id warehouse,w.name wname,p.id position,p.name pname,s.avaquantity FROM materiel m, warehouse w,stock s left join position p on s.position = p.id left join batchmateriel bm on s.batchnum = bm.id Where m.id = s.materiel AND w.id = s.warehouse AND s.avaquantity > 0";
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
            string sql = " SELECT c.id,c.date, cm.batchnum,bm.num batchTNum,m.name,w.name wname,p.name pname,cm.truequantity,cm.avaquantity " +
                    " FROM materiel m, checklist c, warehouse w, checkmateriel cm left join position p on cm.position = p.id left join batchmateriel bm on cm.batchnum = bm.id " +
                    " where m.id = cm.materiel AND w.id = cm.warehouse  and cm.checklist = c.id and c.num = '"+ num + "'";
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
            
            string sqlMat = "insert into checkmateriel (checklist,materiel,truequantity,warehouse,batchnum,position) values ";
            for (int i = 0; i < ck.list_sm.Count; i++)
            {
                if (i != 0) sqlMat += ",";
                sqlMat += " (last_insert_id(),'" + ck.list_sm[i].materiel + "','" + ck.list_sm[i].truequantity + "','" + ck.list_sm[i].warehouse + "',";
                if (ck.list_sm[i].batchnum != null) sqlMat += "'" + ck.list_sm[i].batchnum + "',";
                else sqlMat += "null,";
                if(ck.list_sm[i].position != null) sqlMat += "'" + ck.list_sm[i].position + "')";
                else sqlMat += "null)";
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
