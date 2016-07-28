using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace goods.Model
{
    class ImageModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int materiel { get; set; }

        public ImageModel() { }
        public ImageModel(string name, string url, int materiel)
        {
            this.name = name;
            this.url = url;
            this.materiel = materiel;
        }
        public ImageModel(string name, int materiel)
        {
            this.name = name;
            this.materiel = materiel;
        }
    }
}
