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
            string sql = "SELECT po.id poid,po.num ponum,po.date,s.name supplier,po.summary posummary,u.fullName user,po.user   pouser,po.deliveryDate podeliveryDate,m.id id,m.num,m.name,me.name metering,m.specifications,om.price,om.quantity,om.amount ,om.conversion,om.tax,om.summary, om.deliveryDate,om.id omid " +
            " FROM metering me,materiel m,supplier s,user u,ordermateriel om,purchaseorder po "+
            " WHERE me.id = m.metering AND om.materiel = m.id AND po.supplier = s.id AND po.user = u.id AND om.purchaseorder = po.id AND po.id = " + id;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        public DataTable getFilterOrderMateriel(int pageIndex, int pageSize, string supplier,bool isDate ,DateTime date, string orderNum, string materielNum)
        {
            string sql = " SELECT om.id,po.id poid,po.num ponum, m.num mnum,m.id mid,po.date date,s.name sname,m.name mname,m.specifications specifications, meter.name metering,om.price,om.quantity,om.amount,om.tax,om.deliveryDate,om.closed,  " +
                "(SELECT SUM(quantity) FROM entrymateriel e, godownentry g WHERE e.entry = g.id and g.purchaseOrder = po.id and e.materiel = m.id) quantityAll" +
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
            string sqlAll = " (SELECT SUM(quantity) sum FROM " +
                            " ordermateriel om, purchaseorder po,supplier s WHERE " +
                            " om.purchaseorder = po.id AND om.deliveryDate <= curdate() AND s.id = po.supplier AND s.id = s2.id "+ dateStr + " ) allquantity ";
            string sqlright = " (SELECT SUM(em.quantity) FROM " +
                            " entrymateriel em,godownentry g,purchaseorder po,ordermateriel om WHERE " +
                            " em.entry = g.id AND em.materiel = om.materiel AND g.purchaseOrder = om.purchaseorder " +
                            " AND om.deliveryDate < curdate() AND g.date <= om.deliveryDate AND g.supplier = s2.id " + dateStr + ") rightquantity ";
            string sql = " SELECT s2.id, s2.name, s2.num , "+ sqlAll +","+ sqlright +
                            " from db_goodsmanage.supplier s2 ";
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
            string sql = " SELECT count(id) from db_goodsmanage.supplier s2 ";
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
                sqlbatch = "insert into batchmateriel (date,ordermateriel,materiel,num,uuid) values (now(), '" + ids[i].id + "','" + ids[i].mid + "', concat('" + dateStr + "','" + ids[i].num + "',LPAD(" + num + ",9,'0')),'" + uuid + "'); ";
                sqlList.Add(sqlbatch);
                uuids.Add(uuid);
            }
            
            bool result = h.ExcuteTransaction(sqlList);
            return uuids;
        }
        public DataTable getqrcode(List<string> ids)
        {
            var sql = " SELECT num FROM batchmateriel where uuid in ( ";
            for (int i = 0; i < ids.Count; i++)
            {
                if (i == 0) sql += "'" + ids[i] + "'";
                else sql += ",'" + ids[i] + "'";
            }
            sql += ")";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        #region 新建
        public MessageModel add(orderParmas gm)
        {
            List<string> sqlList = new List<string>();   
            string num = "(SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_NAME = 'purchaseorder')";

            string sqlOrder = "insert into purchaseorder (date,supplier,summary,user,deliveryDate,num) " +
                " values(@date, @supplier, @summary,@user,@deliveryDate, concat('PO',LPAD(" + num + ",9,'0')));";
            string sqlMat = "insert into ordermateriel (purchaseorder,line,materiel,quantity,price,tax,amount,summary,deliveryDate,conversion,subquantity) values ";
            for (int i = 0; i < gm.listM.Count; i++)
            {
                if (i != 0) sqlMat += ",";
                sqlMat += " (last_insert_id(),'" + gm.listM[i].line + "','" + gm.listM[i].materiel + "' ,'" + gm.listM[i].quantity + "','" + gm.listM[i].price + "','" + gm.listM[i].tax + "','" + gm.listM[i].price * gm.listM[i].quantity + "','" + gm.listM[i].summary + "','" + gm.listM[i].deliveryDate + "'";
                if (gm.listM[i].conversion != null) sqlMat += ",'" + gm.listM[i].conversion + "','" + gm.listM[i].conversion * gm.listM[i].quantity + "'";
                else sqlMat += ", null,null";
                sqlMat += ") ";
            }
            sqlList.Add(sqlOrder);
            sqlList.Add(sqlMat);
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
        public void addMsg(string num,string supplier,string date)
        {
            string[] strArray = new string[] {
                num, supplier, date };
            string text = string.Format("采购订单提醒：新增订单,订单号：{0}，供应商：{1}，交货日期：{2}。", (object[])strArray);
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
        public MessageModel setList(int orderid,string num,string supplier, List<ListModel> list)
        {
            MessageModel msg = new MessageModel();
            List<int> list_del = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                list_del.Add(list[i].materiel);
            }
            string oldsql = "SELECT om.materiel, om.price,om.quantity,m.name FROM ordermateriel om inner join materiel m on om.materiel = m.id WHERE om.purchaseorder = '" + orderid + "'";
            DataTable dtold = h.ExecuteQuery(oldsql, CommandType.Text);

            List<OrderMaterielChange> list_change = new List<OrderMaterielChange>();

            List<string> list_changeMsg = new List<string>();
            var textpre = "订单修改，订单号" + num + "，供应商" + supplier+",";
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
                    change = new OrderMaterielChange(materiel, orderid, "delete");
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
                            change = new OrderMaterielChange(list[i].materiel, orderid, "update", -1, list[i].quantity);

                            text = "修改物料名称:"+ name + ",数量由"+ quantity + "修改为"+ list[i].quantity + "。";
                        }
                        else if (list[i].quantity == quantity)
                        {
                            change = new OrderMaterielChange(list[i].materiel, orderid, "update", list[i].price, -1);

                            text = "修改物料名称:" + name + ",单价由" + price + "修改为" + list[i].price + "。";
                        }
                        else
                        {
                            change = new OrderMaterielChange(list[i].materiel, orderid, "update", list[i].price, list[i].quantity);

                            text = "修改物料名称:" + name + ",单价由" + price + "修改为" + list[i].price + "，数量由" + quantity + "修改为" + list[i].quantity + "。";
                        }
                        list_change.Add(change);

                        list_changeMsg.Add(textpre + text);
                    }
                    //list_del.Add(list[i].materiel);
                }
                else
                {
                    change = new OrderMaterielChange(list[i].materiel, orderid,"new",list[i].price, list[i].quantity, list[i].conversion, list[i].tax, list[i].summary);
                    list_change.Add(change);

                    list_changeMsg.Add(textpre + string.Format(textNew, dtold.Rows[i]["name"].ToString(), list[i].price, list[i].quantity));
                }
            }

            List<string> sqlList = new List<string>();
            var sqlchange = "insert into ordermaterielchange(materiel, purchaseorder, type, price, quantity) values ";
            var sqlnew = "";
            var sqlupdate = "";
            var sqldel = "";

            if (list_change.Count == 0)
            {
                return new MessageModel(0, "无保存项");
            }
            for (int i = 0; i < list_change.Count; i++)
            {
                if(i != 0)sqlchange += ",";
                sqlchange += " ('" + list_change[i].materiel+ "', '" + list_change[i].purchaseorder + "',  '" + list_change[i].type + "', '" + list_change[i].price + "', '" + list_change[i].quantity + "') ";

                if(list_change[i].type == "new")
                {
                    if (sqlnew == "")
                    {
                        sqlnew += "insert into ordermateriel(materiel, purchaseorder, conversion,subquantity,tax, price, quantity,summary,amount) values ";
                    }
                    else
                    {
                        sqlnew += ",";
                    }
                    sqlnew += "('" + list_change[i].materiel + "', '" + list_change[i].purchaseorder + "',  '" + list_change[i].conversion + "', '" + 
                        list_change[i].conversion* list_change[i].quantity + "', '" + list_change[i].tax + "', '" + list_change[i].price + "', '" + 
                        list_change[i].quantity + "', '" + list_change[i].summary + "', '" + list_change[i].price * list_change[i].quantity + "')";

                }
                else if(list_change[i].type == "update")
                {
                    sqlupdate = "";
                    if (list_change[i].quantity != -1) sqlupdate += " update ordermateriel set quantity = '" + list_change[i].quantity + "'";
                    if (list_change[i].price != -1)
                    {
                        if (sqlupdate == "") sqlupdate += "update ordermateriel set price = '" + list_change[i].price + "'";
                        else sqlupdate += ",price = '" + list_change[i].price + "'";
                    }
                    sqlupdate += " where materiel = '" + list_change[i].materiel + "' AND purchaseorder = '" + orderid + "'";
                    sqlList.Add(sqlupdate);
                }
                else
                {
                    if(sqldel == "") sqldel = "delete from ordermateriel where purchaseorder = '" + orderid + "' AND materiel in (";
                    else sqldel += ",";
                    sqldel += " '" + list_change[i].materiel + "' ";
                }
            }
            if(sqldel != "")
            {
                sqldel += ")";
                sqlList.Add(sqldel);
            }
            sqlList.Add(sqlchange);
            if(sqlnew != "") sqlList.Add(sqlnew);
            bool result = h.ExcuteTransaction(sqlList);
            
            if (result == true)
            {
                msg = new MessageModel(0, "保存成功");
                msgHandle(list_changeMsg,"order");
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

        public DataTable getbyOMId(int id)
        {
            string sql = "SELECT num,date FROM batchmateriel " +
                "where  ordermateriel = '"+id+"'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
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
