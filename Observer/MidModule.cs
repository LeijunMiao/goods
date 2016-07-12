using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer { 
    /// <summary>
    /// 定义一个信息委托
    /// </summary>
    /// <param name="sender">发布者</param>
    /// <param name="msg">发送内容</param>
    public delegate void MsgDlg(object sender,object msg);
    public delegate void IdsDlg(object sender, List<int> ids);
    public delegate void stockDlg(object sender, List<int> ids);
    public delegate void UsersDlg(object sender, List<object> users);

    public class MidModule
    {
        /// <summary>
        /// 消息发送事件
        /// </summary>
        public static event MsgDlg EventSend;

        public static event IdsDlg EventSendIds;

        public static event stockDlg EventStock;

        public static event UsersDlg EventUser;
        public static void SendMessage(object sender, object msg)
        {
            if (EventSend != null)//
            {
                EventSend(sender, msg);

            }
        }
        public static void SendIds(object sender, List<int> ids)
        {
            if (EventSendIds != null)//
            {
                EventSendIds(sender, ids);
            }
        }

        public static void SendStocks(object sender, List<int> ids)
        {
            if (EventStock != null)//
            {
                EventStock(sender, ids);
            }
        }
        public static void SendUsers(object sender, List<object> users)
        {
            if (EventUser != null)//
            {
                EventUser(sender, users);
            }
        }
    }
}
