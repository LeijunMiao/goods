using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goods.Models
{
    public class CallSlipModel
    {
        public DateTime date { get; set; }
        public int warehouse { get; set; }
        public int position { get; set; }
        public int user { get; set; }
        public bool isDeficit { get; set; }
        public CallSlipModel() { }


    }
    public class CallSlipMaterielModel
    {
        public int id { get; set; }
        public string entry { get; set; }
        public int materiel { get; set; }
        public double quantity { get; set; }
        public double conversion { get; set; }
        public double subquantity { get; set; }
        public CallSlipMaterielModel() { }


    }
}