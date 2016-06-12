using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class MaterielModel
    {
        private int id;
        private String num; 
        private String name;
        private String specifications;
        private int metering;
        private int? subMetering;
        private double conversion;
        private String type;
        private double tax;
        private bool isBatch;

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
        public double Conversion
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

    }
}
