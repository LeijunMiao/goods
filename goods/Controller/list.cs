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
        public DataTable getFilterList(int pageIndex, int pageSize, string supplier, bool isDate, DateTime date, string orderNum, string materielNum)
        {
            string sql = " SELECT g.date,g.num,s.name supplierName, w.name warehouseName, p.name positionName, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,em.price,u.fullName " +
                " FROM entrymateriel em,godownentry g,supplier s,warehouse w,position p,materiel m,metering me,metering me2,user u " +
                " WHERE em.entry = g.id AND g.supplier = s.id AND g.warehouse = w.id AND g.position = p.id AND em.materiel = m.id AND g.user = u.id AND m.subMetering = me2.id AND m.metering = me.id ";
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
                select += " and g.date = '" + date + "' ";
            }
            sql += select;
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        public DataTable getbyNum(string num)
        {
            string sql = " SELECT em.id,g.date,g.num,s.name supplierName, w.name warehouseName, p.name positionName, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,em.price,u.fullName " +
                " FROM entrymateriel em,godownentry g,supplier s,warehouse w,position p,materiel m,metering me,metering me2,user u " +
                " WHERE em.entry = g.id AND g.supplier = s.id AND g.warehouse = w.id AND g.position = p.id AND em.materiel = m.id AND g.user = u.id AND m.subMetering = me2.id AND m.metering = me.id and g.num = '" + num + "'";
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }
        public int getCount(string supplier, bool isDate, DateTime date, string orderNum, string materielNum)
        {
            string sql = " SELECT count(em.id) FROM entrymateriel em,godownentry g,supplier s,materiel m " +
                " WHERE em.entry = g.id AND g.supplier = s.id  AND em.materiel = m.id  ";
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
                select += " and g.date = '" + date + "' ";
            }
            sql += select;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
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
        public MessageModel setList(List<ListModel> list)
        {
            List<string> sqlList = new List<string>();
            string sql = "";
            for (int i = 0; i < list.Count; i++)
            {
                sql  = "update entrymateriel set quantity = '" + list[i].quantity + "' ,subquantity = '" + list[i].conversion * list[i].quantity + "' where id = '" + list[i].id + "' ";
                sqlList.Add(sql);
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
            string sql = "DELETE FROM godownentry WHERE num = '" + obj.ToString() + "' ";
            int res = h.ExecuteNonQuery(sql, CommandType.Text);
            MessageModel msg;
            if (res > 0)
            {
                msg = new MessageModel(0, "删除成功");
            }
            else msg = new MessageModel(10005, "删除失败");
            return msg;
        }
        #endregion
    }
}
