using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class WarehouseModel
    {
        private int id;
        private String num;
        private String name;

        public WarehouseModel() { }
        public WarehouseModel(String num, String name)
        {
            this.num = num;
            this.name = name;
        }
        public WarehouseModel(int id,String num, String name)
        {
            this.id = id;
            this.num = num;
            this.name = name;
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
    }
}
