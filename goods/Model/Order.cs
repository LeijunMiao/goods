using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class OrderModel
    {
        private int id;

        public OrderModel() { }
        public int Id
        {
            get
            {
                return id ;
            }
            set
            {
                id = value;
            }
        }

    }
}
