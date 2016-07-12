using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //命名空间包含允许读写文件和数据流的类型以及提供基本文件和目录支持的类型。
using System.Reflection; //映射类
using System.Net; //为当前网络上使用的多种协议提供了简单的编程接口
using System.Management; //提供对一组丰富的管理信息和管理事件（它们是关于符合 Windows Management Instrumentation (WMI) 基础结构的系统、设备和应用程序的）的访问
using System.Text; //表示 ASCII 和 Unicode 字符编码的类;

namespace goods
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());

            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + "//Menu.ini";
            //if (File.Exists(path))
            //{
            //    string s = "", mac = "";
            //    // 
            //    //name
            //    //
            //    string hostInfo = Dns.GetHostName();
            //    //MessageBox.Show(hostInfo);
            //    //
            //    //IP
            //    IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            //    for (int i = 0; i < addressList.Length; i++)
            //    {
            //        s += addressList[i].ToString();
            //    }
            //    // MessageBox.Show(s);

            //    ManagementClass mc;
            //    mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //    ManagementObjectCollection moc = mc.GetInstances();
            //    foreach (ManagementObject mo in moc)
            //    {
            //        if (mo["IPEnabled"].ToString() == "True")
            //            mac = mo["MacAddress"].ToString();
            //    }
                
            //    string code = mac.ToString();
            //    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //    StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            //    List<string> key = new List<string>();
            //    string line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        key.Add(line);
            //    }
            //    sr.Close();
            //    fs.Close();
            //    License lc = new License();
            //    //code = "28:B2:BD:B6:DC:F3";
            //    string password = lc.EnText(code, "KDSoft");
            //    int j = 0;
            //    bool flag = false;
            //    while (j<key.Count) {
            //        //if (key[j] == lc.transform(password, "KDSoft")) flag = true;
            //        if (key[j] == code) flag = true;
            //        j++;
            //    }
            //    if (flag)
            //    {
            //        //ServerInfo.GetServerInfo();
            //        Application.EnableVisualStyles();
            //        Application.SetCompatibleTextRenderingDefault(false);
            //        Application.Run(new Login());//UserView Department RoleView SupplierView 
            //    }
            //    else
            //    {
            //        MessageBox.Show("机器异常");
            //        //GetKey gk = new GetKey();
            //        //if (gk.ShowDialog() == DialogResult.OK)
            //        //{
            //        //    Application.EnableVisualStyles();
            //        //    Application.SetCompatibleTextRenderingDefault(false);
            //        //    Application.Run(new Login());
            //        //}
            //    }

            //}
        }
    }
}
