using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class User
    {
        /// <summary>
        /// 用户属性
        /// </summary>

        private  int id; //Id
        private  String userName; //用户名
        private  String fullName; //姓名
        private  Boolean isActive; //状态
        private  String mobile; //电话
        private  String email; //邮箱
        private  String hashed_password; //密码哈希值
        private  String type; //用户类型
        private  int role; //角色
        private  DateTime createdAt; //创建时间
        private  DateTime lastModifiedAt; // 最后修改时间
        private  DateTime lastLoginAt; // 最后登录时间

        public User() { }
        public User(int id, String userName) {
            this.userName = userName;
            this.id = id;
        }
        public User(String userName,string fullName, String mobile, String email, String hashed_password, int role)
        {
            this.userName = userName;
            this.fullName = fullName;
            this.mobile = mobile;
            this.email = email;
            this.hashed_password = hashed_password;
            this.role = role;
        }
        public User(String userName, string fullName, int role, String hashed_password)
        {
            this.userName = userName;
            this.fullName = fullName;
            this.hashed_password = hashed_password;
            this.role = role;
        }
        public User(int id, String userName, string fullName, int role)
        {
            this.id = id;
            this.userName = userName;
            this.fullName = fullName;
            this.role = role;
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
        public  string UserName
        {
            set
            {
                userName = value;
            }
            get
            {
                return userName;
            }
        }
        public string FullName
        {
            set
            {
                fullName = value;
            }
            get
            {
                return fullName;
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

        public  string Mobile
        {
            set
            {
                mobile = value;
            }
            get
            {
                return mobile;
            }
        }

        public  string Email
        {
            set
            {
                email = value;
            }
            get
            {
                return email;
            }
        }

        public  string Hashed_password
        {
            set
            {
                hashed_password = value;
            }
            get
            {
                return hashed_password;
            }
        }


        public  string Type
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
        }
        public  int Role
        {
            set
            {
                role = value;
            }
            get
            {
                return role;
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
        public  DateTime LastLoginAt
        {
            set
            {
                lastLoginAt = value;
            }
            get
            {
                return lastLoginAt;
            }
        }





    }
}
