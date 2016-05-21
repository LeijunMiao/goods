using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace goods
{
    class License
    {
        public string EncryptText(string str)
        {
            System.Security.Cryptography.MD5 md5;
            md5 = System.Security.Cryptography.MD5.Create();
            return System.BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str))).Replace("-", "");
        }
        ///   <summary>   
        ///   加密数据   
        ///   </summary>   
        ///   <param   name="Text"></param>   
        ///   <param   name="sKey"></param>   
        ///   <returns></returns>   
        public string EnText(string Text, string sKey)
        {
            StringBuilder ret = new StringBuilder();
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray;
                inputByteArray = Encoding.Default.GetBytes(Text);
                //通过两次哈希密码设置对称算法的初始化向量   
                des.Key = ASCIIEncoding.ASCII.GetBytes(EncryptText(EncryptText(Text)).Substring(0, 8));
                //des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile
                //(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8), "sha1").Substring(0, 8));
                //通过两次哈希密码设置算法的机密密钥   
                des.IV = ASCIIEncoding.ASCII.GetBytes(EncryptText(EncryptText(sKey)).Substring(0, 8));
                //des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile
                //(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8), "md5").Substring(0, 8));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                foreach (byte b in ms.ToArray())
                {
                    ret.Append(String.Format("{0:X2}", b));
                }
                return ret.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
       
        ///   <summary>   
        ///   将加密的字符串转换为注册码形式   
        ///   </summary>   
        ///   <param   name="input">要加密字符串</param>   
        ///   <returns>装换后的字符串</returns>   
        public string transform(string input, string skey)
        {
            string transactSn = string.Empty;
            if (input == "")
            {
                return transactSn;
            }
            string initSn = string.Empty;
            try
            {
                initSn = this.EnText(this.EnText(input, skey), skey).ToString();
                transactSn = initSn.Substring(0, 5) + "-" + initSn.Substring(5, 5) +
                "-" + initSn.Substring(10, 5) + "-" + initSn.Substring(15, 5) +
                "-" + initSn.Substring(20, 5);
                return transactSn;
            }
            catch
            {
                return transactSn;
            }
        }
    }
}
