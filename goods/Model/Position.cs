using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class PositionModel
    {
        private int id;
        private String num;
        private String name;
        private int warehouse;

        public PositionModel() { }
        public PositionModel(String num, String name,int warehouse)
        {
            this.num = num;
            this.name = name;
            this.warehouse = warehouse;
        }
        public PositionModel(int id,String num, String name)
        {
            this.num = num;
            this.name = name;
            this.id = id;
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
        public int Warehouse
        {
            set
            {
                warehouse = value;
            }
            get
            {
                return warehouse;
            }
        }
        
    }
}
