using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using goods.Model;
using System.Data;
namespace goods.Controller
{
    class listCtrl
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
        public DataTable getFilterList(int pageIndex, int pageSize, string supplier, bool isDate, DateTime date, string orderNum, string materielNum,string deficit)
        {
            bool isDeficit = false;
            if (deficit == "红字")
            {
                isDeficit = true;
            }
            var lagedate = date.AddDays(1);

            string sql = " SELECT g.date,g.num,s.name supplierName, w.name warehouseName, p.name positionName, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,em.price, u.fullName, g.isDeficit  " +
                " FROM entrymateriel em,warehouse w,supplier s,godownentry g left join position p on g.position = p.id,materiel m left join metering me2 on m.subMetering = me2.id,metering me,user u " +
                " WHERE em.entry = g.id AND g.supplier = s.id AND g.warehouse = w.id  AND em.materiel = m.id AND g.user = u.id  AND m.metering = me.id ";
            string select = "";
            if (orderNum != "")
            {
                select += " and g.num like '%" + orderNum + "%' ";
            }
            if (materielNum != "")
            {
                select += " and (m.num like '%" + materielNum + "%' or m.name like '%" + materielNum + "%') ";
            }
            if (supplier != "")
            {
                select += " and s.name like '%" + supplier + "%' ";
            }
            if (isDate == true)
            {
                select += " and g.date >= '" + date + "' and g.date < '" + lagedate + "' ";
                
            }
            if (deficit != "全部")
            {
                select += " AND g.isDeficit = @isDeficit";
            }
            sql += select + " order by em.id desc ";

            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@isDeficit", isDeficit);
            DataTable dt = h.ExecuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #region 获取领料单
        public DataTable getCallSlips(int pageIndex, int pageSize, bool isDate, DateTime date, string num, string materielNum, string deficit)
        {
            bool isDeficit = false;
            if (deficit == "红字")
            {
                isDeficit = true;
            }

            var lagedate = date.AddDays(1);

            string sql = " SELECT g.date,g.num, w.name warehouseName, p.name positionName, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,u.fullName,g.isDeficit " +
                " FROM callslipmateriel em,warehouse w,callslip g left join position p on g.position = p.id,metering me,materiel m left join metering me2 on m.subMetering = me2.id,user u " +
                " WHERE em.callslip = g.id AND g.warehouse = w.id AND em.materiel = m.id AND g.user = u.id  AND m.metering = me.id ";
            string select = "";
            if (num != "")
            {
                select += " and g.num like '%" + num + "%' ";
            }
            if (materielNum != "")
            {
                select += " and (m.num like '%" + materielNum + "%' or m.name like '%" + materielNum + "%') ";
            }
            if (isDate == true)
            {
                select += " and g.date >= '" + date + "' and g.date < '" + lagedate + "' ";
            }
            if (deficit != "全部")
            {
                select += " AND g.isDeficit = @isDeficit";
            }
            sql += select + " order by em.id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;

            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@isDeficit", isDeficit);
            DataTable dt = h.ExecuteQuery(sql, paras, CommandType.Text);
            return dt;
        }
        #endregion
        #region 获取领料单数量
        public int getCallSlipCount(bool isDate, DateTime date, string orderNum, string materielNum, string deficit)
        {
            bool isDeficit = false;
            if (deficit == "红字")
            {
                isDeficit = true;
            }
            var lagedate = date.AddDays(1);

            string sql = " SELECT count(em.id) FROM callslipmateriel em,callslip g,materiel m " +
                " WHERE em.callslip = g.id  AND em.materiel = m.id  ";
            string select = "";
            if (orderNum != "")
            {
                select += " and g.num like '%" + orderNum + "%' ";
            }
            if (materielNum != "")
            {
                select += " and (m.num like '%" + materielNum + "%' or m.name like '%" + materielNum + "%') ";
            }
            if (isDate == true)
            {
                select += " and g.date >= '" + date + "' and g.date < '" + lagedate + "' ";
            }
            if (deficit != "全部")
            {
                select += " AND g.isDeficit = @isDeficit";
            }
            sql += select;
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@isDeficit", isDeficit);

            DataTable dt = h.ExecuteQuery(sql, paras, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        #endregion

        #region 获取移入单
        public DataTable getInOrders(int pageIndex, int pageSize, bool isDate, DateTime date, string num, string materielNum)
        {
            var lagedate = date.AddDays(1);

            string sql = " SELECT g.date,g.num, w.name warehouseName, p.name positionName, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,u.fullName " +
                " FROM inmateriel em,warehouse w, inorder g left join position p on g.position = p.id,materiel m left join metering me2 on m.subMetering = me2.id,metering me,user u " +
                " WHERE em.orderlist = g.id AND g.warehouse = w.id  AND em.materiel = m.id AND g.user = u.id AND m.metering = me.id ";
            string select = "";
            if (num != "")
            {
                select += " and g.num like '%" + num + "%' ";
            }
            if (materielNum != "")
            {
                select += " and (m.num like '%" + materielNum + "%' or m.name like '%" + materielNum + "%') ";
            }
            if (isDate == true)
            {
                select += " and g.date >= '" + date + "' and g.date < '" + lagedate + "' ";
            }
            sql += select + " order by em.id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;

            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 获取移入单数量
        public int getInOrderCount(bool isDate, DateTime date, string orderNum, string materielNum)
        {
            var lagedate = date.AddDays(1);

            string sql = " SELECT count(em.id) FROM inmateriel em,inorder g,materiel m " +
                " WHERE em.orderlist = g.id  AND em.materiel = m.id ";
            string select = "";
            if (orderNum != "")
            {
                select += " and g.num like '%" + orderNum + "%' ";
            }
            if (materielNum != "")
            {
                select += " and (m.num like '%" + materielNum + "%' or m.name like '%" + materielNum + "%') ";
            }
            if (isDate == true)
            {
                select += " and g.date >= '" + date + "' and g.date < '" + lagedate + "' ";
            }
            sql += select;

            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        #endregion

        #region 获取移出单
        public DataTable getOutOrders(int pageIndex, int pageSize, bool isDate, DateTime date, string num, string materielNum)
        {
            var lagedate = date.AddDays(1);

            string sql = " SELECT g.date,g.num, w.name warehouseName, p.name positionName, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,u.fullName " +
                " FROM outmateriel em,warehouse w,outorder g left join position p on g.position = p.id,materiel m left join metering me2 on m.subMetering = me2.id,metering me,user u " +
                " WHERE em.orderlist = g.id AND g.warehouse = w.id  AND em.materiel = m.id AND g.user = u.id  AND m.metering = me.id ";
            string select = "";
            if (num != "")
            {
                select += " and g.num like '%" + num + "%' ";
            }
            if (materielNum != "")
            {
                select += " and (m.num like '%" + materielNum + "%' or m.name like '%" + materielNum + "%') ";
            }
            if (isDate == true)
            {
                select += " and g.date >= '" + date + "' and g.date < '" + lagedate + "' ";
            }
            sql += select + " order by em.id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;

            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 获取移出单数量
        public int getOutOrderCount(bool isDate, DateTime date, string orderNum, string materielNum)
        {
            var lagedate = date.AddDays(1);

            string sql = " SELECT count(em.id) FROM outmateriel em,outorder g,materiel m " +
                " WHERE em.orderlist = g.id  AND em.materiel = m.id  ";
            string select = "";
            if (orderNum != "")
            {
                select += " and g.num like '%" + orderNum + "%' ";
            }
            if (materielNum != "")
            {
                select += " and (m.num like '%" + materielNum + "%' or m.name like '%" + materielNum + "%') ";
            }
            if (isDate == true)
            {
                select += " and g.date >= '" + date + "' and g.date < '" + lagedate + "' ";
            }
            sql += select;

            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        #endregion

        public DataTable getbyNum(string num)
        {
            string sql = " SELECT em.id,g.id gid,g.date,g.num,g.isDeficit,s.name supplierName, g.warehouse ,w.name warehouseName,g.position , p.name positionName,em.materiel, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,em.price,u.fullName, u.id user ,m.isBatch " +
                " FROM entrymateriel em,godownentry g left join position p on g.position = p.id,materiel m left join metering me2 on m.subMetering = me2.id,metering me,supplier s,warehouse w,user u " +
                " WHERE em.entry = g.id AND g.supplier = s.id AND g.warehouse = w.id  AND em.materiel = m.id AND g.user = u.id AND m.metering = me.id and g.num = '" + num + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #region 领料单详情
        public DataTable getCallSlipbyNum(string num)
        {
            string sql = " SELECT em.id,g.id gid,g.isDeficit,g.date,g.num,g.warehouse,g.position , w.name warehouseName, p.name positionName, m.num MNum, m.name MName,em.materiel, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,u.fullName,u.id user " +
                " FROM callslipmateriel em,callslip g left join position p on g.position = p.id,warehouse w,materiel m left join metering me2 on m.subMetering = me2.id,metering me,user u " +
                " WHERE em.callslip = g.id  AND g.warehouse = w.id AND em.materiel = m.id AND g.user = u.id  AND m.metering = me.id and g.num = '" + num + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 移入单详情
        public DataTable getInOrderbyNum(string num)
        {
            string sql = " SELECT em.id,g.id gid,g.date,g.num,g.warehouse warehouse,g.position position, w.name warehouseName, p.name positionName, em.materiel,m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,u.fullName,u.id user ,em.batch" +
                " FROM inmateriel em,inorder g left join position p on g.position = p.id ,warehouse w,materiel m left join metering me2 on m.subMetering = me2.id,metering me ,user u " +
                " WHERE em.orderlist = g.id  AND g.warehouse = w.id AND em.materiel = m.id AND g.user = u.id  AND m.metering = me.id and g.num = '" + num + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        #region 移出单详情
        public DataTable getOutOrderbyNum(string num)
        {
            string sql = " SELECT em.id,g.id gid,g.date,g.num,g.warehouse,g.position,em.materiel, w.name warehouseName, p.name positionName, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,u.fullName,u.id user " +
                " FROM outmateriel em,outorder g left join position p on g.position = p.id,warehouse w,materiel m left join metering me2 on m.subMetering = me2.id,metering me,user u " +
                " WHERE em.orderlist = g.id  AND g.warehouse = w.id  AND em.materiel = m.id AND g.user = u.id  AND m.metering = me.id and g.num = '" + num + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion
        public int getCount(string supplier, bool isDate, DateTime date, string orderNum, string materielNum,string deficit)
        {
            bool isDeficit = false;
            if (deficit == "红字")
            {
                isDeficit = true;
            }
            var lagedate = date.AddDays(1);

            string sql = " SELECT count(em.id) FROM entrymateriel em,godownentry g,supplier s,materiel m " +
                " WHERE em.entry = g.id AND g.supplier = s.id  AND em.materiel = m.id   ";
            string select = "";
            if (orderNum != "")
            {
                select += " and g.num like '%" + orderNum + "%' ";
            }
            if (materielNum != "")
            {
                select += " and (m.num like '%" + materielNum + "%' or m.name like '%" + materielNum + "%') ";
            }
            if (supplier != "")
            {
                select += " and s.name like '%" + supplier + "%' ";
            }
            if (isDate == true)
            {
                select += " and g.date >= '" + date + "' and g.date < '" + lagedate + "' ";
            }
            if (deficit != "全部")
            {
                select += " AND g.isDeficit = @isDeficit";
            }
            sql += select;
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@isDeficit", isDeficit);

            DataTable dt = h.ExecuteQuery(sql, paras, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        #region 新建
        public MessageModel add(object obj)
        {
            MessageModel msg = new MessageModel();
            return msg;
        }
        #endregion
        #region 更新入库单物料数量
        public MessageModel setList(GoDownEntryModel gm ,List<ListModel> list)
        {
            List<string> sqlList = new List<string>();

            string sql = "";
            string sqlstock = "";
            string sqlSafetyStock = "";
            string mark = "-";
            if (gm.isDeficit) mark = "+";
            for (int i = 0; i < list.Count; i++)
            {
                sql  = "update entrymateriel set quantity = '" + list[i].quantity + "' ,subquantity = '" + list[i].conversion * list[i].quantity + "' where id = '" + list[i].id + "' ";
                sqlstock = "update stock set lastModifiedAt = NOW(),quantity = quantity " + mark + list[i].quantity + ",avaquantity = avaquantity  " + mark + list[i].quantity + " where uqkey = '" + list[i].materiel + "|" + gm.warehouse + "'|";
                if(gm.position != null ) sqlstock += "'" + gm.position + "'";
                sqlList.Add(sql);
                sqlList.Add(sqlstock);

                sqlSafetyStock = "Update safetystock set stock = stock " + mark + list[i].quantity + " where materiel = '" + list[i].materiel + "'";

                sqlList.Add(sqlSafetyStock);
            }
            bool result = h.ExcuteTransaction(sqlList);
            MessageModel msg = new MessageModel();
            if (result == true)
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
        #region 删除入库单
        public MessageModel delorder(GoDownEntryModel gm, List<ListModel> list)
        {
            List<string> sqlList = new List<string>();

            string mark = "-";
            if (gm.isDeficit) mark = "+";

            string sql = "DELETE FROM godownentry WHERE id = '" + gm.id + "' ";
            string sqlstock = "DELETE s FROM stock s inner join entrymateriel em on s.entrymateriel = em.id WHERE em.entry = '" + gm.id + "'  and s.batchnum >0";
            string sqlup = "";
            string sqlSafetyStock = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].isBatch)
                {
                    sqlup = "update stock set lastModifiedAt = NOW(),quantity = quantity " + mark + list[i].quantity + ",avaquantity = avaquantity  " + mark + list[i].quantity + " where uqkey = '" + list[i].materiel + "|" + gm.warehouse + "|'";
                    if (gm.position != null) sqlup += "'" + gm.position + "'";
                    sqlList.Add(sqlup);
                }
                sqlSafetyStock = "Update safetystock set stock = stock " + mark + list[i].quantity + " where materiel = '" + list[i].materiel + "'";

                sqlList.Add(sqlSafetyStock);
            }
            sqlList.Add(sqlstock);
            sqlList.Add(sql);
            bool result = h.ExcuteTransaction(sqlList);
            MessageModel msg;
            if (result)
            {
                msg = new MessageModel(0, "删除成功");
            }
            else msg = new MessageModel(10005, "删除失败");
            return msg;
        }
        #endregion

        #region 删除领料单
        public MessageModel delCallSlip(CallSlipModel cp, List<ListModel> list)
        {
            List<string> sqlList = new List<string>();
            string sql = "DELETE FROM callslip WHERE id = '" + cp.id + "' ";
            string sqlup = "";
            string sqlSafetyStock = "";
            string mark = "+";
            if (cp.isDeficit) mark = "-";
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].isBatch)
                {
                    sqlup = "update stock set lastModifiedAt = NOW(),quantity = quantity " + mark + list[i].quantity + ",avaquantity = avaquantity  " + mark + list[i].quantity + " where uqkey = '" + list[i].materiel + "|" + cp.warehouse + "'|";
                    if (cp.position != null) sqlup += "'" + cp.position + "'";

                }
                else
                {
                    sqlup = "update stock set lastModifiedAt = NOW(),quantity = quantity  " + mark + list[i].quantity + ",avaquantity = avaquantity  " + mark + list[i].quantity + " where uqkey = '" + list[i].materiel + "|" + cp.warehouse + "'|";
                    if (cp.position != null) sqlup += "'" + cp.position + "'";
                    sqlup += "|'" + list[i].batch + "'";
                }

                sqlSafetyStock = "Update safetystock set stock = stock " + mark + list[i].quantity + " where materiel = '" + list[i].materiel + "'";

                sqlList.Add(sqlSafetyStock);

                sqlList.Add(sqlup);
            }
            sqlList.Add(sql);
            bool result = h.ExcuteTransaction(sqlList);

            MessageModel msg;
            if (result)
            {
                msg = new MessageModel(0, "删除成功");
            }
            else msg = new MessageModel(10005, "删除失败");
            return msg;
        }
        #endregion
        #region 删除移入单
        public MessageModel delInOrder(listParmas cp, List<ListModel> list)
        {
            List<string> sqlList = new List<string>();
            string sql = "DELETE FROM inorder WHERE id = '" + cp.id + "' ";

            string sqlstock = "DELETE FROM stock s inner join inmateriel im on s.inorder = im.id WHERE im.orderlist = '" + cp.id + "'  and s.batchnum >0";
            string sqlup = "";
            string sqlSafetyStock = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].isBatch)
                {
                    sqlup = "update stock set lastModifiedAt = NOW(),quantity = quantity - " + list[i].quantity + ",avaquantity = avaquantity - " + list[i].quantity + " where uqkey = '" + list[i].materiel + "|" + cp.warehouse + "'|";
                    if (cp.position != null) sqlup += "'" + cp.position + "'";
                    sqlList.Add(sqlup);
                }
                sqlSafetyStock = "Update safetystock set stock = stock  - "  + list[i].quantity + " where materiel = '" + list[i].materiel + "'";

                sqlList.Add(sqlSafetyStock);
            }
            sqlList.Add(sqlstock);
            sqlList.Add(sql);
            bool result = h.ExcuteTransaction(sqlList);

            MessageModel msg;
            if (result)
            {
                msg = new MessageModel(0, "删除成功");
            }
            else msg = new MessageModel(10005, "删除失败");
            return msg;
        }
        #endregion
        #region 删除移出单
        public MessageModel delOutOrder(listParmas cp, List<ListModel> list)
        {
            List<string> sqlList = new List<string>();
            string sql = "DELETE FROM outorder WHERE id = '" + cp.id + "' ";
            string sqlup = "";
            string sqlSafetyStock = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].isBatch)
                {
                    sqlup = "update stock set lastModifiedAt = NOW(),quantity = quantity + " + list[i].quantity + ",avaquantity = avaquantity + " + list[i].quantity + " where uqkey = '" + list[i].materiel + "|" + cp.warehouse + "'|";
                    if (cp.position != null) sqlup += "'" + cp.position + "'";
                    
                }
                else
                {
                    sqlup = "update stock set lastModifiedAt = NOW(),quantity = quantity + " + list[i].quantity + ",avaquantity = avaquantity + " + list[i].quantity + " where uqkey = '" + list[i].materiel + "|" + cp.warehouse + "'|";
                    if (cp.position != null) sqlup += "'" + cp.position + "'";
                    sqlup += "|'" + list[i].batch + "'";
                }
                sqlSafetyStock = "Update safetystock set stock = stock  + " + list[i].quantity + " where materiel = '" + list[i].materiel + "'";

                sqlList.Add(sqlSafetyStock);

                sqlList.Add(sqlup);
            }
            sqlList.Add(sql);
            bool result = h.ExcuteTransaction(sqlList);

            MessageModel msg;
            if (result)
            {
                msg = new MessageModel(0, "删除成功");
            }
            else msg = new MessageModel(10005, "删除失败");
            return msg;
        }
        #endregion

        #region 库存是否变动
        public int stockChanged(int id)
        {
            string sql = "select count(id) FROM  entrymateriel WHERE entry = '" + id + "' and ischange = 1";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        # endregion

        #region 库存是否变动
        public int inOrderStockChanged(int id)
        {
            string sql = "select count(id) FROM  inmateriel WHERE orderlist = '" + id + "' and ischange = 1";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        # endregion

        
    }
}
