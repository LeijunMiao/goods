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
        public DataTable getFilterList(int pageIndex, int pageSize, string keyword)
        {
            string sql = " SELECT g.date,g.num,s.name supplierName, w.name warehouseName, p.name positionName, m.num MNum, m.name MName, " +
                " m.specifications ,me.name meterName,em.quantity,me2.name subMeterName,em.conversion,em.subquantity,em.price,u.fullName " +
                " FROM entrymateriel em,godownentry g,supplier s,warehouse w,position p,materiel m,metering me,metering me2,user u " +
                " WHERE em.entry = g.id AND g.supplier = s.id AND g.warehouse = w.id AND g.position = p.id AND em.materiel = m.id AND g.user = u.id AND m.subMetering = me2.id AND m.metering = me.id ";
            //string select = "";
            //if (keyword != "")
            //{
            //    select += " where num like '%" + keyword + "%'";
            //}
            //sql += select;
            if (pageIndex < 1) pageIndex = 1;
            sql += " LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
            DataTable dt = h.ExecuteQuery(sql, CommandType.Text);
            return dt;

        }
        #endregion
        #region 新建
        public MessageModel add(object obj)
        {
            MessageModel msg = new MessageModel();
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
