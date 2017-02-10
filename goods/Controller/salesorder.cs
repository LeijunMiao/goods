using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class salesOrderCtrl
    {

        MySqlHelper h = new MySqlHelper();
        #region 获取
        public DataTable  getFilterOrderMateriel(int pageIndex, int pageSize, string order, string materiel, string customer, bool isDate, DateTime date)
        {
            string sql = " SELECT om.id,so.id soid,so.num sonum, m.num mnum,m.id mid,so.date date,c.num cnum,c.name cname,m.name mname, meter.name metering,om.price,om.quantity,om.amount,om.tax,(om.price*om.tax) taxprice,om.amount*(1+om.tax) allamount,om.deliveryDate,om.combination " +
                " FROM salesordermateriel om ,salesorder so,materiel m,customer c,metering meter where om.salesorder = so.id and om.materiel = m.id and so.customer = c.num and m.metering = meter.id";
            string select = "";
            if (order != "")
            {
                select += " and so.num like '%" + order + "%' ";
            }
            if (materiel != "")
            {
                select += " and (m.num like '%" + materiel + "%' || m.name like '%" + materiel + "%') ";
            }
            if (customer != "")
            {
                select += " and (c.num like '%" + customer + "%' || c.name like '%" + customer + "%') ";
            }
            if (isDate == true)
            {
                select += " and so.date = '" + date + "' ";
            }
            sql += select + " order by om.id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 订单物料筛选
        public DataTable get4Select(int pageIndex, int pageSize, string order, string materiel, string customer)
        {
            string sql = " SELECT om.id,so.id soid,so.num sonum, m.num,c.name customer,m.name name, meter.name metering,om.line " +
                " FROM salesordermateriel om ,salesorder so,materiel m,customer c,metering meter where om.salesorder = so.id and om.materiel = m.id and so.customer = c.num and m.metering = meter.id";
            string select = "";
            if (order != "")
            {
                select += " and so.num like '%" + order + "%' ";
            }
            if (materiel != "")
            {
                select += " and (m.num like '%" + materiel + "%' || m.name like '%" + materiel + "%') ";
            }
            if (customer != "")
            {
                select += " and (c.num like '%" + customer + "%' || c.name like '%" + customer + "%') ";
            }
            sql += select + " order by om.id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取通过订单id
        public DataTable getbyId(int id)
        {
            string sql = "SELECT so.id soid,so.num sonum,so.date,c.name customer,so.summary sosummary,u.fullName user,so.user   souser,so.deliveryDate sodeliveryDate,m.id id,m.num,m.name,me.name metering,om.price,om.quantity,om.amount ,om.tax,(om.tax*om.price) taxprice,om.summary, om.deliveryDate,om.id omid,om.combination,om.line " +
            " FROM metering me,materiel m,customer c,user u,salesordermateriel om,salesorder so " +
            " WHERE me.id = m.metering AND om.materiel = m.id AND so.customer = c.num AND so.user = u.id AND om.salesorder = so.id AND so.id = '"+ id + "' order by om.id desc";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        #region 获取订单物料通过id
        public DataTable getbySalesMaterielId(int id)
        {
            string sql = "SELECT dtl.materiel,so.id soid,so.num sonum,c.name customer,m.id id,m.num,m.name,me.name metering,om.quantity,om.id omid,om.line,dtlm.num dtlnum,dtlm.name dtlname,dtlm.specifications,dtlm.type, dtlme.name dtlme,bom.id bomid,bom.quantity bomqty,dtl.quantity dtlqty,(om.quantity*(dtl.quantity/bom.quantity)) issuedqty " +
            " FROM metering me,materiel m left join bommain bom on m.id = bom.materiel and bom.isActive = 1 left join bomdetail dtl on dtl.parent = bom.id left join materiel dtlm on dtl.materiel = dtlm.id left join metering dtlme on dtlm.metering = dtlme.id,customer c,salesordermateriel om,salesorder so " +
            " WHERE me.id = m.metering AND om.materiel = m.id AND so.customer = c.num  AND om.salesorder = so.id AND om.id = '" + id + "' ";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        public int getCount(string order, string materiel, string customer, bool isDate, DateTime date)
        {
            string sql = " SELECT count(om.id) FROM salesordermateriel om ,salesorder so,materiel m,customer c" +
            " where om.salesorder = so.id and om.materiel = m.id and so.customer = c.num ";
            string select = "";
            if (order != "")
            {
                select += " and so.num like '%" + order + "%' ";
            }
            if (materiel != "")
            {
                select += " and (m.num like '%" + materiel + "%' || m.name like '%" + materiel + "%') ";
            }
            if (customer != "")
            {
                select += " and (c.num like '%" + customer + "%' || c.name like '%" + customer + "%') ";
            }
            if (isDate == true)
            {
                select += " and so.date = '" + date + "' ";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        #region 新建
        public MessageModel add(SalesOrder so)
        {
            var dateStr = string.Format("{0:yyyyMMdd}", DateTime.Now);
            List<string> sqlList = new List<string>();
            string num = "(select count(a.id)+1 from (select tmp.* from salesorder tmp where tmp.date = curdate()) a)";

            string sqlOrder = "insert into salesorder (date,customer,summary,user,deliveryDate,num) " +
                " values(@date, @customer, @summary,@user,@deliveryDate, concat('SALE','"+ dateStr + "',LPAD(" + num + ",6,'0')));";
            string sqlMat = "insert into salesordermateriel (salesorder, materiel,line, quantity, price, tax, summary, deliveryDate,combination) values ";
            for (int i = 0; i < so.listM.Count; i++)
            {
                if (i != 0) sqlMat += ",";
                sqlMat += " (last_insert_id(),'" + so.listM[i].materiel + "' ,'" + so.listM[i].line + "' ,'" + so.listM[i].quantity + "','" + so.listM[i].price + "','" + so.listM[i].tax + "','" + so.listM[i].summary + "','" + so.listM[i].deliveryDate + "'";
                if (so.listM[i].combination != null && so.listM[i].combination > 0) sqlMat += ",'" + so.listM[i].combination + "'";
                else sqlMat += ", null";
                sqlMat += ") ";
            }
            sqlList.Add(sqlOrder);
            sqlList.Add(sqlMat);
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@date", so.date);
            paras.Add("@customer", so.customer);
            paras.Add("@summary", so.summary);
            paras.Add("@user", so.user);
            paras.Add("@deliveryDate", so.deliveryDate);

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

        #region 获取物料辅助属性
        public Dictionary<int, string> getbyOMIds(List<int> ids)
        {
            string sql = "SELECT om.id,av.name FROM salesordermateriel om inner join attrcombination c on om.combination = c.id inner join combinationitem ci on c.id = ci.combination inner join attrvalue av on ci.attrvalue = av.id  where om.id in (";
            for (int i = 0; i < ids.Count; i++)
            {
                if (i != 0) sql += ",";
                sql += ids[i];
            }
            sql += ")";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            Dictionary<int, string> map = new Dictionary<int, string>();
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

        #region 修改销售订单
        public MessageModel setList(int orderid, string num, string user,string date, List<SalesOrderMateriel> list)
        {
            MessageModel msg = new MessageModel();
            string oldsql = "SELECT om.materiel, om.price,om.quantity,m.name,om.combination FROM salesordermateriel om inner join materiel m on om.materiel = m.id WHERE om.salesorder = '" + orderid + "'";
            DataTable dtold = h.ExecuteQuery(oldsql, CommandType.Text);

            List<SalesMaterielChange> list_change = new List<SalesMaterielChange>();

            List<string> list_changeMsg = new List<string>();
            var textpre = "销售员"+ user + "于"+ date + "修改销售订单" + num + ",";

            SalesMaterielChange change;
            Dictionary<int,Dictionary<int, SalesOrderMateriel>> map_old = new Dictionary<int, Dictionary<int, SalesOrderMateriel>>();
            for (int i = 0; i < dtold.Rows.Count; i++)
            {
                var materiel = Convert.ToInt32(dtold.Rows[i]["materiel"]);
                var combination = -1;
                if (dtold.Rows[i]["combination"] != DBNull.Value) combination = Convert.ToInt32(dtold.Rows[i]["combination"]);
                if (!map_old.ContainsKey(materiel)) map_old.Add(materiel, new Dictionary<int, SalesOrderMateriel>());
                if (!map_old[materiel].ContainsKey(combination)) map_old[materiel].Add(combination, new SalesOrderMateriel(dtold.Rows[i]));
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (map_old.ContainsKey(list[i].materiel))
                {
                    var materiel = list[i].materiel;
                    int combination = - 1;
                    if (list[i].combination != null) combination = (int)list[i].combination;
                    var text = "";
                    var price = Convert.ToDouble(map_old[materiel][combination].price);
                    var quantity = Convert.ToDouble(map_old[materiel][combination].quantity);
                    var name = map_old[materiel][combination].name;
                    if (list[i].price != price || list[i].quantity != quantity)
                    {
                        if (list[i].price == price)
                        {
                            change = new SalesMaterielChange(materiel, combination, orderid, "update", -1, list[i].quantity);

                            text = "修改物料名称:" + name;
                            if (list[i].attrs != "") text += ",辅助属性:" + list[i].attrs;
                            text +=    ",数量由" + quantity + "修改为" + list[i].quantity + "。";
                        }
                        else if (list[i].quantity == quantity)
                        {
                            change = new SalesMaterielChange(materiel, combination, orderid, "update", list[i].price, -1);

                            text = "修改物料名称:" + name;
                            if (list[i].attrs != "") text += ",辅助属性:" + list[i].attrs;
                            text += ",单价由" + price + "修改为" + list[i].price + "。";
                        }
                        else
                        {
                            change = new SalesMaterielChange(materiel, combination, orderid, "update", list[i].price, list[i].quantity);

                            text = "修改物料名称:" + name;
                            if (list[i].attrs != "") text += ",辅助属性:" + list[i].attrs;
                            text += ",单价由" + price + "修改为" + list[i].price + "，数量由" + quantity + "修改为" + list[i].quantity + "。";
                        }
                        list_change.Add(change);

                        list_changeMsg.Add(textpre + text);
                    }
                    //list_del.Add(list[i].materiel);
                }
            }

            List<string> sqlList = new List<string>();
            var sqlchange = "insert into salesmaterielchange(materiel, salesorder, type, price, quantity,combination) values ";
            var sqlupdate = "";

            if (list_change.Count == 0)
            {
                return new MessageModel(0, "无保存项");
            }
            for (int i = 0; i < list_change.Count; i++)
            {
                if (i != 0) sqlchange += ",";
                sqlchange += " ('" + list_change[i].materiel + "', '" + list_change[i].salesorder + "',  '" + list_change[i].type + "', '" + list_change[i].price + "', '" + list_change[i].quantity + "' ";
                if (list_change[i].combination != null) sqlchange += ",'" + list_change[i].combination + "')";
                else sqlchange += ",null)";
                if (list_change[i].type == "update")
                {
                    sqlupdate = "";
                    if (list_change[i].quantity != -1) sqlupdate += " update salesordermateriel set quantity = '" + list_change[i].quantity + "'";
                    if (list_change[i].price != -1)
                    {
                        if (sqlupdate == "") sqlupdate += "update salesordermateriel set price = '" + list_change[i].price + "'";
                        else sqlupdate += ",price = '" + list_change[i].price + "'";
                    }
                    sqlupdate += " where materiel = '" + list_change[i].materiel + "' AND salesorder = '" + orderid + "' ";
                    if (list_change[i].combination != null) sqlupdate += " and combination = '" + list_change[i].combination + "'";
                    sqlList.Add(sqlupdate);
                }
            }
            sqlList.Add(sqlchange);
            bool result = h.ExcuteTransaction(sqlList);

            if (result == true)
            {
                msg = new MessageModel(0, "保存成功");
                msgHandle(list_changeMsg, "salesorder");
            }
            else msg = new MessageModel(10005, "保存失败");
            return msg;
        }
        #endregion
        #region 新建信息
        public void addMsg(string user,string num, string customer, string date, double amount)
        {
            string[] strArray = new string[] {
                 user,date, customer,num,amount.ToString() };
            string text = string.Format("销售员{0}于{1}新增客户{2}销售订单{3}，订单总金额为{4}。", (object[])strArray);
            msgHandle(new List<string> { text }, "salesorder");
        }
        #endregion

        #region 按id获取最近订单
        public DataTable getlastinsert(int user)
        {
            string sql = "select num from salesorder where user = '" + user + "' order by id desc limit 1";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

        public void msgHandle(List<string> list, string type)
        {
            var user = "";
            var sqluser = "SELECT * FROM messageuser where type = '" + type + "'";
            DataTable dt = h.ExecuteQuery(sqluser, CommandType.Text);
            var sqlMsg = "insert into message (text,usernum) values ";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0) user += "|";
                user += dt.Rows[i]["usernum"];
            }
            if (user != "")
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != 0) sqlMsg += ",";
                    sqlMsg += "('" + list[i] + "','" + user + "')";
                }
                h.ExecuteNonQuery(sqlMsg, CommandType.Text);
            }
        }
    }
}
