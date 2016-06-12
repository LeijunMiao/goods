using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace goods
{
    class PropertyClass
    {
        private static string sendnamevalue;
        private static int role;
        /// <summary>
        /// 传递登录人员账号
        /// </summary>
        public static string SendNameValue
        {
            set
            {
                sendnamevalue = value;
            }
            get
            {
                return sendnamevalue;
            }
        }

        private static string sendpopedomvalue;
        /// <summary>
        /// 传递用户权限
        /// </summary>
        public static string SendPopedomValue
        {
            set
            {
                sendpopedomvalue = value;
            }
            get
            {
                return sendpopedomvalue;
            }
        }

        private static string password;
        /// <summary>
        /// 传递用户ID属性
        /// </summary>
        public static string Password
        {
            set
            {
                password = value;
            }
            get
            {
                return password;
            }
        }
        public static int Role
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


    }
}
