using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class CustomerModel
    {
        private string num;
        private string name;
        public CustomerModel() { }
        public CustomerModel(string num,string name)
        {
            this.num = num;
            this.name = name;
        }
        public string Num
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
        public string Name
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
