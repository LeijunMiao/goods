using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods
{
    class ServerInfo
    {
        public static string ServerIP = "192.168.1.248";//192.168.1.248， 127.0.0.1
        public static string User_ID = "sa";
        public static string User_Pwd = "sasasa";
        
        public static string GetLastLoginUser()
        {
            string UserNo = "";
            string fileName = "ipConfig.xml";
            if (!System.IO.File.Exists(fileName))
            {
                UserNo = "";
            }
            else
            {
                xmlDoc xmldoc = new xmlDoc();
                if (xmldoc.LoadXmlFile(fileName))
                {
                    UserNo = xmldoc.readByNodeName("UserNo");
                }
            }
            return UserNo;
        }

        public static string GetSettingIP()
        {
            string fileName = "ipConfig.xml";
            if (!System.IO.File.Exists(fileName))
            {
                return "";
            }
            else
            {
                xmlDoc xmldoc = new xmlDoc();
                if (xmldoc.LoadXmlFile(fileName))
                {
                    return xmldoc.readByNodeName("ServerIP");
                }
                else return "";
            }
            
        }

        public static string GetSettingUser()
        {
            string fileName = "ipConfig.xml";
            if (!System.IO.File.Exists(fileName))
            {
                return "";
            }
            else
            {
                xmlDoc xmldoc = new xmlDoc();
                if (xmldoc.LoadXmlFile(fileName))
                {
                    return xmldoc.readByNodeName("User_ID");
                }
                else return "";
            }

        }

        public static void SaveLastLoginUser()
        {
            string xmlstr = "";
            xmlstr += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n";
            xmlstr += "<setting>\r\n";
            xmlstr += "<注释>数据库配置信息</注释>\r\n";
            xmlstr += "<ServerIP>" + ServerIP + "</ServerIP>\r\n";
            xmlstr += "<User_ID>" + User_ID + "</User_ID>\r\n";
            xmlstr += "<User_Pwd>" + User_Pwd + "</User_Pwd>\r\n";
            xmlstr += "</setting>";

            try
            {
                xmlDoc xmldoc = new xmlDoc();
                xmldoc.LoadXmlString(xmlstr);
                xmldoc.SaveXmlFile("ipConfig.xml");
            }
            catch
            {
            }
        }

        public static void GetServerInfo()
        {
            MD5Cls md5Cls = new MD5Cls();
            string fileName = "ipConfig.xml";
            if (!System.IO.File.Exists(fileName))
            {
                return;
            }
            else
            {
                xmlDoc xmldoc = new xmlDoc();
                if (xmldoc.LoadXmlFile(fileName))
                {
                    ServerIP = xmldoc.readByNodeName("ServerIP");
                    User_ID = xmldoc.readByNodeName("User_ID");
                    User_Pwd = md5Cls.MD5Decrypt(xmldoc.readByNodeName("User_Pwd"), xmldoc.readByNodeName("Key"));
                    //md5Cls.MD5Decrypt(xmldoc.readByNodeName("User_Pwd"), xmldoc.readByNodeName("Key"))
                }
            }
        }
    }

}
