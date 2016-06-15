using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goods.Models
{
    public class OrderModel
    {
        public DateTime date { get; set; }
        public int warehouse { get; set; }
        public int position { get; set; }
        public int user { get; set; }
        public OrderModel() { }

    }
    
    public class ListModel
    {
        public int id { get; set; }
        public int order { get; set; }
        public int materiel { get; set; }
        public double quantity { get; set; }
        public double conversion { get; set; }
        public double subquantity { get; set; }
        public ListModel() { }


    }


}