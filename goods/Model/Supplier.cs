﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class SupplierModel
    {
        private int id;
        private String num;
        private String name;
        private int? isActive;
        private DateTime createdAt; //创建时间
        private DateTime lastModifiedAt; // 最后修改时间

        public SupplierModel() { }

        public SupplierModel(int id,string name) {
            this.id = id;
            this.name = name;
        }
        public SupplierModel(String num, String name) {
            this.num = num;
            this.name = name;
        }
        public SupplierModel(int id, String num, string name)
        {
            this.id = id;
            this.num = num;
            this.name = name;
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
        public DateTime CreatedAt
        {
            set
            {
                createdAt = value;
            }
            get
            {
                return createdAt;
            }
        }
        public DateTime LastModifiedAt
        {
            set
            {
                lastModifiedAt = value;
            }
            get
            {
                return lastModifiedAt;
            }
        }
        public int? IsActive
        {
            set
            {
                isActive = value;
            }
            get
            {
                return isActive;
            }
        }
        

    }
}
