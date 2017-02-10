using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace goods.Model
{
    class SalesOrder
    {
        public string customer { get; set; }
        public string summary { get; set; }
        public int user { get; set; }
        public DateTime date { get; set; }
        public DateTime deliveryDate { get; set; }
        public List<SalesOrderMateriel> listM { get; set; }

        public SalesOrder(string customer, int user, string summary, DateTime date, DateTime deliveryDate)
        {
            this.customer = customer;
            this.user = user;
            this.summary = summary;
            this.date = date;
            this.deliveryDate = deliveryDate;
        }
    }
    public class SalesOrderMateriel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int line { get; set; }
        public int materiel { get; set; }
        public double quantity { get; set; }
        public double price { get; set; }
        public double tax { get; set; }
        public double amount { get; set; }
        public string summary { get; set; }
        public int? combination { get; set; }
        public DateTime deliveryDate { get; set; }
        public string attrs { get; set; }

        public SalesOrderMateriel() { }

        public SalesOrderMateriel(DataRow dr)
        {
            this.materiel = Convert.ToInt32(dr["materiel"]);
            this.price = Convert.ToInt32(dr["price"]);
            this.quantity = Convert.ToInt32(dr["quantity"]);
            this.name = dr["name"].ToString();
            if(dr["combination"] != DBNull.Value) this.combination = Convert.ToInt32(dr["combination"]);
        }


    }

    public class SalesMaterielChange
    {
        public int materiel { get; set; }
        public int salesorder { get; set; }
        public string type { get; set; }
        public double price { get; set; }
        public double quantity { get; set; }
        public double? conversion { get; set; }
        public double tax { get; set; }
        public string summary { get; set; }
        public int? combination { get; set; }

        public SalesMaterielChange(int materiel, int salesorder, string type)
        {
            this.materiel = materiel;
            this.salesorder = salesorder;
            this.type = type;
        }
        public SalesMaterielChange(int materiel, int? combination, int salesorder, string type, double price, double quantity)
        {
            this.materiel = materiel;
            this.salesorder = salesorder;
            this.type = type;
            this.price = price;
            this.quantity = quantity;
            if(combination != -1) this.combination = combination;
        }
        public SalesMaterielChange(int materiel, int salesorder, string type, double price, double quantity, double? conversion, double tax, string summary)
        {
            this.materiel = materiel;
            this.salesorder = salesorder;
            this.type = type;
            this.price = price;
            this.quantity = quantity;
            this.conversion = conversion;
            this.tax = tax;
            this.summary = summary;
        }
    }
}
