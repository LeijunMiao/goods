using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace goods.Model
{
    public class MaterielModel
    {
        private int id;
        private String num; 
        private String name;
        private String specifications;
        private int metering;
        private string meteringName;
        private int? subMetering;
        private double? conversion;
        private String type;
        private double tax;
        private bool isBatch;
        public double? safetystock;
        public double? maxstock;
        private int? catgegory;
        private double? normprice;

        public MaterielModel() { }
        public MaterielModel(String num, String name, String specifications, int metering, bool isBatch)
        {
            this.num = num;
            this.name = name;
            this.metering = metering;
            this.isBatch = isBatch;
            this.specifications = specifications;
            this.subMetering = null;
        }
        public MaterielModel(int id,String num, String name, string meteringName)
        {
            this.id = id;
            this.num = num;
            this.name = name;
            this.meteringName = meteringName;
        }

        public MaterielModel(int id, double safetystock, double maxstock)
        {
            this.id = id;
            this.safetystock = safetystock;
            this.maxstock = maxstock;
        }
        public int Id
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }
        public int? Catgegory
        {
            set
            {
                catgegory = value;
            }
            get
            {
                return catgegory;
            }
        }
        public int Metering
        {
            set
            {
                metering = value;
            }
            get
            {
                return metering;
            }
        }
        public string MeteringName
        {
            set
            {
                meteringName = value;
            }
            get
            {
                return meteringName;
            }
        }
        public int? SubMetering
        {
            set
            {
                subMetering = value;
            }
            get
            {
                return subMetering;
            }
        }

        public String Num
        {
            set
            {
                num = value;
            }
            get
            {
                return num;
            }
        }
        public String Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }
        public String Specifications
        {
            set
            {
                specifications = value;
            }
            get
            {
                return specifications;
            }
        }
        public String Type
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
        }
        public double? Conversion
        {
            set
            {
                conversion = value;
            }
            get
            {
                return conversion;
            }
        }
        public double Tax
        {
            set
            {
                tax = value;
            }
            get
            {
                return tax;
            }
        }
        public bool IsBatch
        {
            set
            {
                isBatch = value;
            }
            get
            {
                return isBatch;
            }
        }
        public double? NormPrice
        {
            set
            {
                normprice = value;
            }
            get
            {
                return normprice;
            }
        }
        

    }
    public class attrNumModel
    {
        public int num { get; set; }
        public string value { get; set; }

        public List<sbKeyValueModel> list { get; set; }
        public attrNumModel() { }
        public attrNumModel(int num, string value)
        {
            this.num = num;
            this.value = value;
        }
    }

    public class sbKeyValueModel
    {
        public int solidbacking { get; set; }
        public int key { get; set; }
        public string value { get; set; }
        public sbKeyValueModel() { }
        public sbKeyValueModel(int solidbacking,int key, string value)
        {
            this.solidbacking = solidbacking;
            this.key = key;
            this.value = value;
        }
    }
}
