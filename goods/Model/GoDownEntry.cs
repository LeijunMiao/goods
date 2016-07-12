using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace goods.Model
{
    public class GoDownEntryModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int? supplier { get; set; }
        public int? warehouse { get; set; }
        public int? position { get; set; }
        public double amount { get; set; }
        public int? user { get; set; }
        public int? purchaseOrder { get; set; }

        public int? batch { get; set; }

        public bool isDeficit { get; set; }
        public GoDownEntryModel() {
            this.supplier = null;
            this.warehouse = null;
            this.position = null;
            this.user = null;
            this.purchaseOrder = null;
            this.batch = null;
            this.isDeficit = false;
        }


    }

    public class EntryMaterielModel
    {
        public int id { get; set; }
        public int entry { get; set; }
        public int materiel { get; set; }
        public double? quantity { get; set; }
        public double? conversion { get; set; }
        public double? subquantity { get; set; }
        public double? price { get; set; }
        public EntryMaterielModel() {
            this.quantity = null;
            //this.conversion = null;
            this.subquantity = null;
            //this.price = null;
        }
        public EntryMaterielModel(DataRow dr)
        {
            this.materiel = Convert.ToInt32(dr["id"]);
            this.conversion = Convert.ToDouble(dr["conversion"]) ;
            this.price = Convert.ToDouble(dr["price"]); 
        }


    }
    //public class MaterielModel
    //{
    //    public int id { get; set; }
    //    public string num { get; set; }
    //    public string name { get; set; }
    //    public string specifications { get; set; }
    //    public int metering { get; set; }
    //    public int subMetering { get; set; }
    //    public double conversion { get; set; }
    //    public string type { get; set; }
    //    public double tax { get; set; }
    //    public MaterielModel() { }

    //}

    public class goDownEntryParmas
    {
        public int supplier { get; set; }
        public int warehouse { get; set; }
        public int position { get; set; }
        public double amount { get; set; }
        public int user { get; set; }
        public int purchaseOrder { get; set; }
        public bool isDeficit { get; set; }

        public int batch { get; set; }


        public List<EntryMaterielModel> listM { get; set; }
    }
    public class callSlipParmas
    {
        public int warehouse { get; set; }
        public int position { get; set; }
        public int user { get; set; }
        public bool isDeficit { get; set; }

        public int batch { get; set; }
        

        public DateTime date { get; set; }
        public List<CallSlipMaterielModel> listM { get; set; }
    }

    public class listParmas
    {
        public int id { get; set; }
        public int warehouse { get; set; }
        public int? position { get; set; }
        public int user { get; set; }
        public DateTime date { get; set; }
        public List<ListModel> listM { get; set; }
    }
}