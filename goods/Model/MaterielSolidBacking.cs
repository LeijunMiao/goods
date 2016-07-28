using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace goods.Model
{
    class MaterSolidBackingM
    {
        public int id { get; set; }
        public int materiel { get; set; }
        public int solidbacking { get; set; }

        public MaterSolidBackingM(int materiel,int solidbacking)
        {
            this.materiel = materiel;
            this.solidbacking = solidbacking;
        }
    }
}
