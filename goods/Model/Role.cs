using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class RoleModel
    {
        /// <summary>
        /// 角色属性
        /// </summary>

        private  int id; //Id
        private  int departmentId; //部门
        private  String name; //角色名
        private  Boolean isActive; //状态
        private  DateTime createdAt; //创建时间
        private  DateTime lastModifiedAt; // 最后修改时间

        public RoleModel() { }
        public RoleModel( String name, int departmentId)
        {
            this.name = name;
            this.departmentId = departmentId;
        }
        public RoleModel(int id,String name)
        {
            this.name = name;
            this.id = id;
        }
        public RoleModel(int id)
        {
            this.id = id;
        }
        public RoleModel(int id, int departmentId, String name, Boolean isActive, DateTime createdAt, DateTime lastModifiedAt)
        {
            this.name = name;
            this.name = name;
            this.departmentId = departmentId;
            this.isActive = isActive;
            this.createdAt = createdAt;
            this.lastModifiedAt = lastModifiedAt;
        }
        public  int Id
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
        public  int DepartmentId
        {
            set
            {
                departmentId = value;
            }
            get
            {
                return departmentId;
            }
        }
        
        public  string Name
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
        public  Boolean IsActive
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
        public  DateTime CreatedAt
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
        public  DateTime LastModifiedAt
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
