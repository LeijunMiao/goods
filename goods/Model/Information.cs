using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class Information
    {
        public int id { get; set; }
        public string text { get; set; }
        public int user { get; set; }
        public string usernum { get; set; }
        public DateTime date { get; set; }

        public Information() { }
    }

    class InformationUser
    {
        public int id { get; set; }
        public string type { get; set; }
        public int user { get; set; }

        public InformationUser() { }
    }
}
