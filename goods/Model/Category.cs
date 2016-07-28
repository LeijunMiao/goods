using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace goods.Model
{
    class CategoryModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? parent { get; set; }
        public bool isActive { get; set; }
    }
}
