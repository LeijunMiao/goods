using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace goods
{
    class BOMMainModel
    {
        public int id { get; set; }
        public int materiel { get; set; }
        public string num { get; set; }
        public bool jump { get; set; }
        public double quantity { get; set; }
        public bool isActive { get; set; }
        public List<BOMDetailModel> list { get; set; }
        public BOMMainModel() { }
    }

    class BOMDetailModel
    {
        public int id { get; set; }
        public int materiel { get; set; }
        public int parent { get; set; }
        public string num { get; set; }
        public double quantity { get; set; }
        public string remark { get; set; }

        public BOMDetailModel() { }
        public BOMDetailModel(DataRow dr)
        {
            this.id = Convert.ToInt32(dr["id"]);
            this.materiel = Convert.ToInt32(dr["materiel"]);
            this.remark = dr["remark"].ToString();
            this.quantity = Convert.ToInt32(dr["quantity"]);
        }
    }
}
