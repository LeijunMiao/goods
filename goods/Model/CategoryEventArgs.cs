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

    public class CustomerEventArgs : EventArgs
    {
        public string num { get; set; }
        public string name { get; set; }
        public CustomerEventArgs(string num, string name)
        {
            this.num = num;
            this.name = name;
        }
    }
    public class MaterielEventArgs : EventArgs
    {
        public List<int> ids { get; set; }

        public MaterielEventArgs(List<int> ids)
        {
            this.ids = ids;
        }
    }

    public class SalesMaterielEventArgs : EventArgs
    {
        public int id { get; set; }

        public SalesMaterielEventArgs(int id)
        {
            this.id = id;
        }
    }
    

    public class SolidBackingEventArgs : EventArgs
    {
        public int materiel { get; set; }
        public int id { get; set; }
        public attrClass ac { get; set; }
        public int line { get; set; }
        public SolidBackingEventArgs(int line,int materiel, int id, attrClass ac)
        {
            this.line = line;
            this.materiel = materiel;
            this.id = id;
            this.ac = ac;
        }
    }

    public class UserEventArgs : EventArgs
    {
        public List<User> list_uids { get; set; }
        public UserEventArgs(List<User> list)
        {
            this.list_uids = list;
        }
    }
    


}
