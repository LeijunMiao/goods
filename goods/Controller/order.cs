using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using goods.Model;
namespace goods.Controller
{
    class orderCtrl
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

        public DataTable getbyId(int id)
        {
            string sql = "SELECT po.id poid,po.num ponum,po.date,po.supplier supplierId,s.name supplier,po.summary posummary,u.fullName user,po.user   pouser,po.deliveryDate podeliveryDate,m.id id,m.num,m.name,me.name metering,m.specifications,om.price,om.quantity,om.amount ,om.conversion,om.tax,om.summary, om.deliveryDate,om.id omid " +
            " FROM metering me,materiel m,supplier s,user u,ordermateriel om,purchaseorder po "+
            " WHERE me.id = m.metering AND om.materiel = m.id AND po.supplier = s.id AND po.user = u.id AND om.purchaseorder = po.id AND po.id = " + id;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        public Dictionary<int, attrNumModel> getbyOMIds(List<int> ids)
        {
            string sql = "SELECT om.id,av.name,av.solidbacking,av.id avid FROM ordermateriel om inner join attrcombination c on om.combination = c.id inner join combinationitem ci on c.id = ci.combination inner join attrvalue av on ci.attrvalue = av.id  where om.id in (";
            for (int i = 0; i < ids.Count; i++)
            {
                if (i != 0) sql += ",";
                sql += ids[i];
            }
            sql += ")";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            Dictionary<int, attrNumModel> map = new Dictionary<int, attrNumModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var id = Convert.ToInt32(dt.Rows[i]["id"]);
                if (!map.Keys.Contains(id))
                {
                    map.Add(id,new attrNumModel(0,""));
                    map[id].list = new List<sbKeyValueModel>();
                }
                if (map[id].value != "") map[id].value += ",";
                map[id].value += dt.Rows[i]["name"].ToString();
                map[id].num++;
                sbKeyValueModel skv = new sbKeyValueModel(Convert.ToInt32(dt.Rows[i]["solidbacking"]), Convert.ToInt32(dt.Rows[i]["avid"]), dt.Rows[i]["name"].ToString());
                map[id].list.Add(skv);
            }
            return map;
        }
            
        public DataTable getFilterOrderMateriel(int pageIndex, int pageSize, string supplier,bool isDate ,DateTime date, string orderNum, string materielNum)
        {
            string sql = " SELECT om.id,po.id poid,po.num ponum, m.num mnum,m.id mid,po.date date,s.name sname,s.id supplier,m.name mname,m.specifications specifications, meter.name metering,om.price,om.quantity,om.amount,om.tax,om.deliveryDate,om.closed,om.combination," +
                "(SELECT SUM(quantity) FROM entrymateriel e, godownentry g WHERE e.entry = g.id and g.purchaseOrder = po.id and e.materiel = m.id and g.isDeficit = 0) quantityAll" +
                " FROM ordermateriel om ,purchaseorder po,materiel m,supplier s,metering meter where om.purchaseorder = po.id and om.materiel = m.id and po.supplier = s.id and m.metering = meter.id";
            string select = "";
            if (orderNum != "")
            {
                select += " and po.num like '%" + orderNum + "%' ";
            }
            if (materielNum != "")
            {
                select += " and m.num like '%" + materielNum + "%' ";
            }
            if(supplier != "")
            {
                select += " and s.name like '%" + supplier + "%' ";
            }
            if(isDate == true)
            {
                select += " and po.date = '" + date + "' ";
            }
            sql += select + " order by om.id desc ";
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }
        public int getCount(string supplier, bool isDate, DateTime date, string orderNum, string materielNum)
        {
            string sql = " SELECT count(om.id) FROM ordermateriel om ,purchaseorder po,materiel m,supplier s" +
            " where om.purchaseorder = po.id and om.materiel = m.id and po.supplier = s.id ";
            string select = "";
            if(orderNum != "")
            {
                select += " and po.num like '%" + orderNum + "%' ";
            }
            if (materielNum != "")
            {
                select += " and m.num like '%" + materielNum + "%' ";
            }
            if (supplier != "")
            {
                select += " and s.name like '%" + supplier + "%' ";
            }
            if (isDate == true)
            {
                select += " and po.date = '" + date + "' ";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }
        /// <summary>
        /// 供应商订单入库情况
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isStart"></param>
        /// <param name="start"></param>
        /// <param name="isEnd"></param>
        /// <param name="end"></param>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public DataTable getSupplierOrder(int pageIndex, int pageSize, bool isStart,DateTime start,bool isEnd,DateTime end,string supplier)
        {
            string dateStr = "";
            if (isStart == true)
            {
                dateStr = " and om.deliveryDate >= '" + start + "' ";
            }
            if (isEnd == true)
            {
                dateStr += " and om.deliveryDate <= '" + end + "' ";
            }
            string sqlAll = " (SELECT SUM(om.quantity) sum FROM " +
                            " ordermateriel om, purchaseorder po,supplier s WHERE " +
                            " om.purchaseorder = po.id AND om.deliveryDate < curdate() AND s.id = po.supplier AND s.id = s2.id "+ dateStr + " ) allquantity ";
            string sqlright = " (SELECT SUM(em.quantity) FROM " +
                            " entrymateriel em,godownentry g,purchaseorder po,ordermateriel om WHERE " +
                            " em.entry = g.id AND em.materiel = om.materiel AND g.purchaseOrder = om.purchaseorder and om.purchaseorder = po.id and g.isDeficit = 0 " +
                            " AND om.deliveryDate < curdate() AND g.date <= date_add(om.deliveryDate, INTERVAL 1 day) AND g.supplier = s2.id " + dateStr + ") rightquantity ";
            string sql = " SELECT s2.id, s2.name, s2.num , "+ sqlAll +","+ sqlright +
                            " from supplier s2 ";
            if (supplier != "")
            {
                sql += " where s2.name like '%" + supplier + "%' ";
            }
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }

        /// <summary>
        /// 获取交货分析的供货商数量
        /// </summary>
        /// <param name="isStart"></param>
        /// <param name="start"></param>
        /// <param name="isEnd"></param>
        /// <param name="end"></param>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public int getSupplierOrderCount(string supplier)
        {
            string sql = " SELECT count(id) from supplier s2 ";
            if (supplier != "")
            {
                sql += " where s2.name like '%" + supplier + "%' ";
            }

            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count; 

        }

        public List<string> updateBatch(List<parmas> ids)
        {
            Guid tempCartId = Guid.NewGuid();
            List<string> sqlList = new List<string>();
            //string sqlBatch = "insert into batchid (num,date) values " +
            //        " ('" + ids.Count + "',curdate() ) " +
            //        " ON DUPLICATE KEY UPDATE num = num +" + ids.Count;
            //sqlList.Add(sqlBatch);
            string num = "(SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_NAME = 'batchmateriel')";
            var dateStr = string.Format("{0:yyyyMMdd}", DateTime.Now);  //"" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Date;
            
            string sqlbatch = " ";
            List<string> uuids = new List<string>();
            for (int i = 0; i < ids.Count; i++)
            {
                var uuid = tempCartId.ToString() + i;
                sqlbatch = "insert into batchmateriel (date,ordermateriel,materiel,num,uuid,supplier,combination) values (now(), '" + ids[i].id + "','" + ids[i].mid + "', concat('" + dateStr + "','" + ids[i].num + "',LPAD(" + num + ",9,'0')),'" + uuid + "','" + ids[i].supplier + "'";
                if (ids[i].combination != null) sqlbatch += ",'" + ids[i].combination + "')";
                else sqlbatch += ",null)";
                sqlList.Add(sqlbatch);
                uuids.Add(uuid);
            }
            
            bool result = h.ExcuteTransaction(sqlList);
            return uuids;
        }
        public DataTable getqrcode(List<string> ids)
        {
            var sql = " SELECT bm.num,bm.date,m.num mnum,m.name,m.specifications spe,s.name supplier,av.name avname FROM batchmateriel bm inner join materiel m on bm.materiel = m.id inner join supplier s on bm.supplier = s.id left join attrcombination c on bm.combination = c.id left join combinationitem ci on c.id = ci.combination left join attrvalue av on ci.attrvalue = av.id where bm.uuid in ( ";
            for (int i = 0; i < ids.Count; i++)
            {
                if (i == 0) sql += "'" + ids[i] + "'";
                else sql += ",'" + ids[i] + "'";
            }
            sql += ")";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("num");
            dtNew.Columns.Add("date");
            dtNew.Columns.Add("mnum");
            dtNew.Columns.Add("name");
            dtNew.Columns.Add("spe");
            dtNew.Columns.Add("supplier");
            dtNew.Columns.Add("attribute");
            
            Dictionary<string, DataRow> map = new Dictionary<string, DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var id = dt.Rows[i]["num"].ToString();
                if (!map.Keys.Contains(id))
                {
                    DataRow dr = dtNew.NewRow();
                    dr["num"] = dt.Rows[i]["num"];
                    dr["date"] = dt.Rows[i]["date"];
                    dr["mnum"] = dt.Rows[i]["mnum"];
                    dr["name"] = dt.Rows[i]["name"];
                    dr["spe"] = dt.Rows[i]["spe"];
                    dr["supplier"] = dt.Rows[i]["supplier"];
                    dr["attribute"] = "";
                    map.Add(id, dr);
                }
                if (map[id]["attribute"].ToString() != "") map[id]["attribute"] += ",";
                map[id]["attribute"] += dt.Rows[i]["avname"].ToString();
            }
            foreach (var item in map.Values)
            {
                dtNew.Rows.Add(item);
            }
            return dtNew;
        }
        #region 新建
        public MessageModel add(orderParmas gm)
        {
            List<string> sqlList = new List<string>();
            string num = "(SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_NAME = 'purchaseorder')";

            string sqlOrder = "insert into purchaseorder (date,supplier,summary,user,deliveryDate,num) " +
                " values(@date, @supplier, @summary,@user,@deliveryDate, concat('PO',LPAD(" + num + ",9,'0')));";
            string sqlMat = "insert into ordermateriel (purchaseorder,line,materiel,quantity,price,tax,amount,summary,deliveryDate,conversion,subquantity,combination) values ";
            string sqlTemp = "insert into supmaterielprice (supplier,materiel,price) values ";
            for (int i = 0; i < gm.listM.Count; i++)
            {
                if (i != 0)
                {
                    sqlMat += ",";
                    sqlTemp += ",";
                }
                sqlMat += " (last_insert_id(),'" + gm.listM[i].line + "','" + gm.listM[i].materiel + "' ,'" + gm.listM[i].quantity + "','" + gm.listM[i].price + "','" + gm.listM[i].tax + "','" + gm.listM[i].price * gm.listM[i].quantity + "','" + gm.listM[i].summary + "','" + gm.listM[i].deliveryDate + "'";
                sqlTemp += "(@supplier,'" + gm.listM[i].materiel + "','" + gm.listM[i].price + "')";
                if (gm.listM[i].conversion != null) sqlMat += ",'" + gm.listM[i].conversion + "','" + gm.listM[i].conversion * gm.listM[i].quantity + "'";
                else sqlMat += ", null,null";
                if (gm.listM[i].combination != null && gm.listM[i].combination >0) sqlMat += ",'"+ gm.listM[i].combination + "'";
                else sqlMat += ", null";
                sqlMat += ") ";
            }
            sqlList.Add(sqlOrder);
            sqlList.Add(sqlMat);
            sqlList.Add(sqlTemp);
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("@date", gm.date);
            paras.Add("@supplier", gm.supplier);
            paras.Add("@summary", gm.summary);
            paras.Add("@user", gm.user);
            paras.Add("@deliveryDate", gm.deliveryDate);

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

        #region 新建信息
        public void addMsg(string num,string supplier,string date,double amount)
        {
            string[] strArray = new string[] {
                num, supplier, date,amount.ToString() };
            string text = string.Format("采购订单提醒：新增订单,订单号：{0}，供应商：{1}，交货日期：{2}，金额：{3}。", (object[])strArray);
            msgHandle(new List<string> { text }, "order");
            //string user = "";
            //var sqluser = "SELECT * FROM messageuser where type = 'order'";
            //DataTable dt = h.ExecuteQuery(sqluser, CommandType.Text);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (i != 0) user += "|";
            //    user += dt.Rows[i]["usernum"];
            //    //if(i != 0) sqlMsg += ",";
            //    //sqlMsg += "('"+ text + "','" + dt.Rows[i]["user"] + "','" + dt.Rows[i]["usernum"] + "')";
            //}
            //var sqlMsg = "insert into message (text,usernum) values ('" + text + "','" + user + "');";
            //h.ExecuteNonQuery(sqlMsg,CommandType.Text);
        }
        #endregion
        public MessageModel setList(OrderModel order, List<ListModel> list)
        {
            MessageModel msg = new MessageModel();
            List<int> list_del = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                list_del.Add(list[i].materiel);
            }
            string oldsql = "SELECT om.materiel, om.price,om.quantity,m.name FROM ordermateriel om inner join materiel m on om.materiel = m.id WHERE om.purchaseorder = '" + order.Id + "'";
            DataTable dtold = h.ExecuteQuery(oldsql, CommandType.Text);

            List<OrderMaterielChange> list_change = new List<OrderMaterielChange>();

            List<string> list_changeMsg = new List<string>();
            var textpre = "订单修改，订单号" + order.Num + "，供应商" + order.SupName+",";
            var textNew = "新增物料名称:{0}，单价：{1}，数量：{2}。";
            //var textUP1 = "修改物料名称:{0}，{1}由{2}修改为{3}。";
            var textDel = "删除物料名称:{0}。";

            OrderMaterielChange change;
            Dictionary<int, ListModel> map_old = new Dictionary<int, ListModel>();
            for (int i = 0; i < dtold.Rows.Count; i++)
            {
                var materiel = Convert.ToInt32(dtold.Rows[i]["materiel"]);
                map_old.Add(materiel, new ListModel(dtold.Rows[i]));
                if(!list_del.Contains(materiel))
                {
                    change = new OrderMaterielChange(materiel, order.Id, "delete");
                    list_change.Add(change);

                    list_changeMsg.Add(textpre + string.Format(textDel, dtold.Rows[i]["name"].ToString()));
                }
            }
            
            for (int i = 0; i < list.Count; i++)
            {
                if (map_old.ContainsKey(list[i].materiel))
                {
                    var text = "";
                    var price = Convert.ToDouble(map_old[list[i].materiel].price);
                    var quantity = Convert.ToDouble(map_old[list[i].materiel].quantity);
                    var name = map_old[list[i].materiel].name;
                    if (list[i].price != price || list[i].quantity != quantity)
                    {
                        if(list[i].price == price)
                        {
                            change = new OrderMaterielChange(list[i].materiel, order.Id, "update", -1, list[i].quantity);

                            text = "修改物料名称:"+ name + ",数量由"+ quantity + "修改为"+ list[i].quantity + "。";
                        }
                        else if (list[i].quantity == quantity)
                        {
                            change = new OrderMaterielChange(list[i].materiel, order.Id, "update", list[i].price, -1);

                            text = "修改物料名称:" + name + ",单价由" + price + "修改为" + list[i].price + "。";
                        }
                        else
                        {
                            change = new OrderMaterielChange(list[i].materiel, order.Id, "update", list[i].price, list[i].quantity);

                            text = "修改物料名称:" + name + ",单价由" + price + "修改为" + list[i].price + "，数量由" + quantity + "修改为" + list[i].quantity + "。";
                        }
             
                        list_changeMsg.Add(textpre + text);
                    }
                    else
                    {
                        change = new OrderMaterielChange(list[i].materiel, order.Id, "update", -1, -1);
                    }
                    change.line = list[i].line;
                    change.combination = list[i].combination;
                    change.deliveryDate = list[i].deliveryDate;
                    change.summary = list[i].summary;
                    change.tax = list[i].tax;
                    list_change.Add(change);
                    //list_del.Add(list[i].materiel);
                }
                else
                {
                    change = new OrderMaterielChange(list[i].materiel, order.Id, "new",list[i].price, list[i].quantity, list[i].conversion, list[i].tax, list[i].summary, list[i].deliveryDate, list[i].combination, list[i].line);
                    list_change.Add(change);

                    list_changeMsg.Add(textpre + string.Format(textNew, list[i].name, list[i].price, list[i].quantity));
                }
            }

            List<string> sqlList = new List<string>();
            if (order.Summary != null) sqlList.Add("update purchaseorder set summary = '"+ order.Summary + "' where id = '"+ order .Id+ "'");
            var sqlchange = "";
            var sqlnew = "";
            var sqlupdate = "";
            var sqldel = "";

            for (int i = 0; i < list_change.Count; i++)
            {
                if(list_change[i].type != "update" || list_change[i].price != -1 || list_change[i].quantity != -1)
                {
                    if (sqlchange == "") sqlchange = "insert into ordermaterielchange(materiel, purchaseorder, type, price, quantity) values ";
                    else sqlchange += ",";

                    sqlchange += " ('" + list_change[i].materiel + "', '" + list_change[i].purchaseorder + "',  '" + list_change[i].type + "', '" + list_change[i].price + "', '" + list_change[i].quantity + "') ";
                }

                if(list_change[i].type == "new")
                {
                    if (sqlnew == "")
                    {
                        sqlnew += "insert into ordermateriel(materiel, purchaseorder, conversion,subquantity,tax, price, quantity,summary,amount,deliveryDate,line,combination) values ";
                    }
                    else
                    {
                        sqlnew += ",";
                    }
                    sqlnew += "('" + list_change[i].materiel + "', '" + list_change[i].purchaseorder + "',  '" + list_change[i].conversion + "', '" + 
                        list_change[i].conversion* list_change[i].quantity + "', '" + list_change[i].tax + "', '" + list_change[i].price + "', '" + 
                        list_change[i].quantity + "', '" + list_change[i].summary + "', '" + list_change[i].price * list_change[i].quantity + "','"+ list_change[i].deliveryDate + "','" + list_change[i].line + "'";
                    if(list_change[i].combination != null && list_change[i].combination >0) sqlnew += ",'" + list_change[i].combination + "')";
                    else sqlnew += ",null)";

                }
                else if(list_change[i].type == "update")
                {
                    sqlupdate = "update ordermateriel set tax = '" + list_change[i].tax + "',summary = '" + list_change[i].summary + "', deliveryDate = '" + list_change[i].deliveryDate + "', line ='" + list_change[i].line + "'";
                    if (list_change[i].combination != null && list_change[i].combination > 0) sqlupdate += ",combination = '" + list_change[i].combination + "'"; 
                    if (list_change[i].quantity != -1) sqlupdate += ", quantity = '" + list_change[i].quantity + "'";
                    if (list_change[i].price != -1)
                    {
                        sqlupdate += ",price = '" + list_change[i].price + "'";
                    }
                    sqlupdate += " where materiel = '" + list_change[i].materiel + "' AND purchaseorder = '" + order.Id + "'";
                    sqlList.Add(sqlupdate);
                }
                else
                {
                    if(sqldel == "") sqldel = "delete from ordermateriel where purchaseorder = '" + order.Id + "' AND materiel in (";
                    else sqldel += ",";
                    sqldel += " '" + list_change[i].materiel + "' ";
                }
            }
            if(sqldel != "")
            {
                sqldel += ")";
                sqlList.Add(sqldel);
            }
            if (sqlchange != "")  sqlList.Add(sqlchange);
            if(sqlnew != "") sqlList.Add(sqlnew);
            bool result = h.ExcuteTransaction(sqlList);
            
            if (result == true)
            {
                msg = new MessageModel(0, "保存成功");
                if(list_changeMsg.Count > 0) msgHandle(list_changeMsg,"order");
            }
            else msg = new MessageModel(10005, "保存失败");
            return msg;
        }

        public void msgHandle(List<string> list,string type)
        {
            var user = "";
            var sqluser = "SELECT * FROM messageuser where type = '"+ type + "'";
            DataTable dt = h.ExecuteQuery(sqluser, CommandType.Text);
            var sqlMsg = "insert into message (text,usernum) values ";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0) user += "|";
                user += dt.Rows[i]["usernum"];
            }
            if(user != "")
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != 0) sqlMsg += ",";
                    sqlMsg += "('" + list[i] + "','" + user + "')";
                }
                h.ExecuteNonQuery(sqlMsg, CommandType.Text);
            }
        }

        #region 更新
        public MessageModel set(object obj)
        {
            MessageModel msg = new MessageModel();
            return msg;
        }
        #endregion

        #region 更新
        public MessageModel setClosed(int id)
        {
            MessageModel msg = new MessageModel();
            string sql = "UPDATE ordermateriel SET closed = 1 WHERE id = '" + id + "' ";
            int res = h.ExecuteNonQuery(sql, CommandType.Text);

            if (res > 0)
            {
                msg = new MessageModel(0, "更新成功");
            }
            else msg = new MessageModel(10005, "更新失败");

            return msg;
        }
        #endregion

        
        #region 删除
        public MessageModel del(object obj,string num, string supplier)
        {
            string sql = "DELETE FROM purchaseorder WHERE id = '" + obj.ToString() + "' ";
            int res = h.ExecuteNonQuery(sql, CommandType.Text);
            MessageModel msg;
            if (res > 0)
            {
                msg = new MessageModel(0, "删除成功");
                string[] strArray = new string[] {
                num, supplier };
                string text = string.Format("订单删除，订单号{0}，供应商{1}被删除。", (object[])strArray);
                msgHandle(new List<string> { text }, "order");
            }
            else msg = new MessageModel(10005, "删除失败");
            return msg;
        }
        #endregion

        public int hasGoDownEntry(int poid)
        {
            string sql = "SELECT count(id) FROM godownentry where purchaseOrder = "+ poid;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }

        public DataTable getbyOMId(int omid)
        {
            string sql = "SELECT bm.num,bm.date,m.num mnum,m.name,m.specifications spe,s.name supplier,av.name avname FROM batchmateriel bm inner join ordermateriel om on bm.ordermateriel = om.id inner join materiel m on bm.materiel = m.id  inner join supplier s on bm.supplier = s.id left join attrcombination c on bm.combination = c.id left join combinationitem ci on c.id = ci.combination left join attrvalue av on ci.attrvalue = av.id where  bm.ordermateriel = '" + omid + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("num");
            dtNew.Columns.Add("date");
            dtNew.Columns.Add("mnum");
            dtNew.Columns.Add("name");
            dtNew.Columns.Add("spe");
            dtNew.Columns.Add("supplier");
            dtNew.Columns.Add("attribute");

            Dictionary<string, DataRow> map = new Dictionary<string, DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var id = dt.Rows[i]["num"].ToString();
                if (!map.Keys.Contains(id))
                {
                    DataRow dr = dtNew.NewRow();
                    dr["num"] = dt.Rows[i]["num"];
                    dr["date"] = dt.Rows[i]["date"];
                    dr["mnum"] = dt.Rows[i]["mnum"];
                    dr["name"] = dt.Rows[i]["name"];
                    dr["spe"] = dt.Rows[i]["spe"];
                    dr["supplier"] = dt.Rows[i]["supplier"];
                    dr["attribute"] = "";
                    map.Add(id, dr);
                }
                if (map[id]["attribute"].ToString() != "") map[id]["attribute"] += ",";
                map[id]["attribute"] += dt.Rows[i]["avname"].ToString();
            }
            foreach (var item in map.Values)
            {
                dtNew.Rows.Add(item);
            }
            return dtNew;
        }


        #region 按id获取订单
        public DataTable getlastinsert(int user)
        {
            string sql = "select num from purchaseorder where user = '" + user + "' order by id desc limit 1";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #endregion

    }
}
