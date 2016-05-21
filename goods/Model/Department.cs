using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class DepartmentModel
    {
        /// <summary>
        /// 部门属性
        /// </summary>
        private int id; //Id
        private int? parentId; //父级
        private int sortby; //排序
        private String name; //部门名
        private Boolean isActive; //状态
        private DateTime createdAt; //创建时间
        private DateTime lastModifiedAt; // 最后修改时间

        public DepartmentModel() { }
        public DepartmentModel(int id) { this.id = id; }
        public DepartmentModel(int id, String name) { this.id = id; this.name = name; }
        public DepartmentModel(String name, int? parentId) { this.name = name; this.parentId = parentId; }

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
        public int? ParentId
        {
            set
            {
                parentId = value;
            }
            get
            {
                return parentId;
            }
        }
        public int Sortby
        {
            set
            {
                sortby = value;
            }
            get
            {
                return sortby;
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
        public Boolean IsActive
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
    }
}
