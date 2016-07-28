using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace goods.Model
{
    public class CategoryEventArgs : EventArgs
    {
        public int id { get; set; }
        public string name { get; set; }
        public CategoryEventArgs(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
