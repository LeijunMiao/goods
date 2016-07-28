using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace goods.Model
{
    class OrderModel
    {
        private int id;
        private int supplier;
        private int user;
        private string num;
        private string summary;
        private DateTime date;
        private DateTime deliveryDate;


        public OrderModel() { }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public int Supplier
        {
            get
            {
                return supplier;
            }
            set
            {
                supplier = value;
            }
        }
        public int User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }
        public string Num
        {
            get
            {
                return num;
            }
            set
            {
                num = value;
            }
        }
        public string Summary
        {
            get
            {
                return summary;
            }
            set
            {
                summary = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public DateTime DeliveryDate
        {
            get
            {
                return deliveryDate;
            }
            set
            {
                deliveryDate = value;
            }
        }


    }

    class orderParmas
    {
        public int supplier { get; set; }
        public string summary { get; set; }
        public int user { get; set; }
        public DateTime date { get; set; }
        public DateTime deliveryDate { get; set; }
        public List<ListModel> listM { get; set; }

        public orderParmas(int supplier, int user, string summary, DateTime date, DateTime deliveryDate)
        {
            this.supplier = supplier;
            this.user = user;
            this.summary = summary;
            this.date = date;
            this.deliveryDate = deliveryDate;
        }
    }

    public class ListModel
    {
        public int id { get; set; }
        public int line { get; set; }
        public int materiel { get; set; }
        public double quantity { get; set; }
        public double? conversion { get; set; }
        public double subquantity { get; set; }
        public double price { get; set; }
        public double tax { get; set; }
        public double amount { get; set; }
        public string summary { get; set; }
        public bool isBatch { get; set; }
        public int? batch { get; set; }

        public string name { get; set; }
        public int warehouse { get; set; }
        public int? position { get; set; }
        public int supplier { get; set; }

        public int? combination { get; set; }

        public DateTime deliveryDate { get; set; }

        public ListModel() { }

        public ListModel(DataRow dr) {
            this.materiel = Convert.ToInt32(dr["materiel"]);
            this.price = Convert.ToInt32(dr["price"]);
            this.quantity = Convert.ToInt32(dr["quantity"]);
            if (dr["name"] != DBNull.Value) this.name = dr["name"].ToString();
         }


    }

    public class parmas
    {
        public int id { get; set; }
        public int mid { get; set; }
        public string num { get; set; }

        public int? combination { get; set; }

        public parmas(int id, int mid, string num)
        {
            this.id = id;
            this.mid = mid;
            this.num = num;
        }
    }
    public class printModel
    {
        public string num { get; set; }
        public string name { get; set; }
        public string specifications { get; set; }
        public string metering { get; set; }
        //public string subMetering { get; set; }
        //public string type { get; set; }
        //public string price { get; set; }
        //public string tax { get; set; }
        //public string taxprice { get; set; }
        public string quantity { get; set; }
        //public string conversion { get; set; }
        //public string subquantity { get; set; }
        //public string amount { get; set; }
        //public string taxamount { get; set; }
        public string allamount { get; set; }
        public string deliveryDate { get; set; }
        //public string summary { get; set; }

        public printModel() { }
        public printModel(System.Windows.Forms.DataGridViewRow dr)
        {
            this.num = dr.Cells["num"].Value.ToString();
            this.name = dr.Cells["name"].Value.ToString();
            this.specifications = dr.Cells["specifications"].Value.ToString();
            this.metering = dr.Cells["metering"].Value.ToString();
            //this.subMetering = dr.Cells["subMetering"].Value.ToString();
            //this.type = dr.Cells["type"].Value.ToString();
            //this.taxamount = dr.Cells["taxamount"].Value.ToString();
            this.allamount = dr.Cells["allamount"].Value.ToString();
            //this.price = dr.Cells["price"].Value.ToString();
            //this.taxprice = dr.Cells["taxprice"].Value.ToString();
            //this.amount = dr.Cells["amount"].Value.ToString();
            //this.tax = dr.Cells["tax"].Value.ToString();
            this.deliveryDate = (DateTime.Parse(dr.Cells["deliveryDate"].Value.ToString())).ToString("yyyy-MM-dd");
            this.quantity = dr.Cells["quantity"].Value.ToString();
            //this.subquantity = dr.Cells["subquantity"].Value.ToString();
            //this.conversion = dr.Cells["conversion"].Value.ToString();
            //if (dr.Cells["summary"].Value != null) this.summary = dr.Cells["summary"].Value.ToString();
            //else this.summary = "";
        }
        //public printModel(string num, string name, string specifications, string metering, string subMetering, string type, string taxamount, string allamount
        //    , string price, string taxprice, string amount, string tax, string deliveryDate, string quantity, string subquantity, string conversion, string summary)
        //{
        //    this.num = num;
        //    this.name = name;
        //    this.specifications = specifications;
        //    this.metering = metering;
        //    this.subMetering = subMetering;
        //    this.type = type;
        //    this.taxamount = taxamount;
        //    this.allamount = allamount;
        //    this.price = price;
        //    this.taxprice = taxprice;
        //    this.amount = amount;
        //    this.tax = tax;
        //    this.deliveryDate = deliveryDate;
        //    this.quantity = quantity;
        //    this.subquantity = subquantity;
        //    this.conversion = conversion;
        //    this.summary = summary;
        //}
    }


    /// <summary>
    /// 入库单打印模型
    /// </summary>
    public class printListModel
    {
        public string num { get; set; }
        public string name { get; set; }
        public string specifications { get; set; }
        public string metering { get; set; }
        public string subMetering { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string conversion { get; set; }
        public string subquantity { get; set; }
        public string amount { get; set; }

        public printListModel() { }
        public printListModel(System.Windows.Forms.DataGridViewRow dr)
        {
            this.num = dr.Cells["num"].Value.ToString();
            this.name = dr.Cells["name"].Value.ToString();
            this.specifications = dr.Cells["specifications"].Value.ToString();
            this.metering = dr.Cells["metering"].Value.ToString();
            this.subMetering = dr.Cells["subMetering"].Value.ToString();
            this.price = dr.Cells["price"].Value.ToString();
            this.amount = dr.Cells["amount"].Value.ToString();
            this.quantity = dr.Cells["quantity"].Value.ToString();
            this.subquantity = dr.Cells["subquantity"].Value.ToString();
            this.conversion = dr.Cells["conversion"].Value.ToString();
        }
    }
    /// <summary>
    /// 领料单打印模型
    /// </summary>
    public class printCallSlipModel
    {
        public string num { get; set; }
        public string name { get; set; }
        public string specifications { get; set; }
        public string metering { get; set; }
        public string subMetering { get; set; }
        public string quantity { get; set; }
        public string conversion { get; set; }
        public string subquantity { get; set; }

        public printCallSlipModel() { }
        public printCallSlipModel(System.Windows.Forms.DataGridViewRow dr)
        {
            this.num = dr.Cells["num"].Value.ToString();
            this.name = dr.Cells["name"].Value.ToString();
            this.specifications = dr.Cells["specifications"].Value.ToString();
            this.metering = dr.Cells["metering"].Value.ToString();
            this.subMetering = dr.Cells["subMetering"].Value.ToString();
            this.quantity = dr.Cells["quantity"].Value.ToString();
            this.subquantity = dr.Cells["subquantity"].Value.ToString();
            this.conversion = dr.Cells["conversion"].Value.ToString();
        }
    }

    public class OrderMaterielChange
    {
        public int materiel { get; set; }
        public int purchaseorder { get; set; }
        public string type { get; set; }
        public double price { get; set; }
        public double quantity { get; set; }
        public double? conversion { get; set; }
        public double tax { get; set; }
        public string summary { get; set; }

        public OrderMaterielChange(int materiel, int purchaseorder, string type)
        {
            this.materiel = materiel;
            this.purchaseorder = purchaseorder;
            this.type = type;
        }
        public OrderMaterielChange(int materiel, int purchaseorder, string type, double price, double quantity)
        {
            this.materiel = materiel;
            this.purchaseorder = purchaseorder;
            this.type = type;
            this.price = price;
            this.quantity = quantity;
        }
        public OrderMaterielChange(int materiel, int purchaseorder, string type, double price, double quantity, double? conversion, double tax,string summary)
        {
            this.materiel = materiel;
            this.purchaseorder = purchaseorder;
            this.type = type;
            this.price = price;
            this.quantity = quantity;
            this.conversion = conversion;
            this.tax = tax;
            this.summary = summary;
        }
    }
}
