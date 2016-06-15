using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return id ;
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

        public orderParmas(int supplier, int user, string summary, DateTime date, DateTime deliveryDate )
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
        public double conversion { get; set; }
        public double subquantity { get; set; }
        public double price { get; set; }
        public double tax { get; set; }
        public double amount { get; set; }
        public string summary { get; set; }
        public DateTime deliveryDate { get; set; }
            
        public ListModel() { }


    }

    public class parmas
    {
        public int id { get; set; }
        public int mid { get; set; }
        public string num { get; set; }

        public parmas(int id, int mid, string num)
        {
            this.id = id;
            this.mid = mid;
            this.num = num;
        }
    }
}
