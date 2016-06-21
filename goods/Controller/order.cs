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
        public DataTable getFilterOrderMateriel(int pageIndex, int pageSize, string supplier,bool isDate ,DateTime date, string orderNum, string materielNum)
        {
            string sql = " SELECT om.id,po.num ponum, m.num mnum,m.id mid,po.date date,s.name sname,m.name mname,m.specifications specifications, meter.name metering,om.price,om.quantity,om.amount,om.tax,om.deliveryDate, " +
                "(SELECT SUM(quantity) FROM entrymateriel e, godownentry g WHERE e.entry = g.id and g.purchaseOrder = po.id) quantityAll" +
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
            sql += select;

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
            string sqlMat = "insert into ordermateriel (purchaseorder,line,materiel,quantity,conversion,subquantity,price,tax,amount,summary,deliveryDate) values ";
            for (int i = 0; i < gm.listM.Count; i++)
            {
                if (i != 0) sqlMat += ",";
                sqlMat += " (last_insert_id(),'" + gm.listM[i].line + "','" + gm.listM[i].materiel + "','" + gm.listM[i].quantity + "','" + gm.listM[i].conversion + "','" + gm.listM[i].conversion * gm.listM[i].quantity + "',"+
                    "'" + gm.listM[i].price + "','" + gm.listM[i].tax + "','" + gm.listM[i].price* gm.listM[i].quantity + "','" + gm.listM[i].summary + "','" + gm.listM[i].deliveryDate + "') ";
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
