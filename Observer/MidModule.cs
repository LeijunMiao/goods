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

    public class MidModule
    {
        /// <summary>
        /// 消息发送事件
        /// </summary>
        public static event MsgDlg EventSend;


        public static void SendMessage(object sender, object msg)
        {
            if (EventSend != null)//
            {
                EventSend(sender, msg);
            }
        }
    }
}
