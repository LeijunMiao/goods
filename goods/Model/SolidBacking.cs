using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace goods.Model
{
    class SolidBackingModel
    {
        public int id { get; set; }
        public string name { get; set; }

        public bool isActive { get; set; }
    }

    /// <summary>
    /// 辅助属性类
    /// </summary>
    public class attrClass
    {
        public int attrId { get; set; }
        public int valueId { get; set; }
        public string valueName { get; set; }
        public attrClass(int attrId, int valueId, string valueName)
        {
            this.attrId = attrId;
            this.valueId = valueId;
            this.valueName = valueName;
        }
    }
}
