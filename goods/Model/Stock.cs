using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class StockMateriel
    {
        public int id { get; set; }
        public int checklist { get; set; }

        public int materiel { get; set; }
        public double truequantity { get; set; }
        public int? batchnum { get; set; }

        public int warehouse { get; set; }

        public int? position { get; set; }
        public int? combination { get; set; }
        public int supplier { get; set; }
        public StockMateriel() { }

    }
    class CheckListModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string num { get; set; }
        public int user { get; set; }
        public int status { get; set; }

        public List<StockMateriel> list_sm { get; set; }
        public CheckListModel() { }
            
            
    }
}
