using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace goods.Model
{
    class MessageModel
    {
        private int code;
        private string msg;
        private object obj;
        public MessageModel(){}
        public MessageModel(int code, string msg)
        {
            this.code = code;
            this.msg = msg;
        }
        public MessageModel(int code, string msg,object obj)
        {
            this.code = code;
            this.msg = msg;
            this.obj = obj;
        }
        public MessageModel(JObject obj)
        {
            this.code = Convert.ToInt32(obj.GetValue("code"));
            this.msg = obj.GetValue("msg").ToString();
        }

        public int Code {
            set
            {
                code = value;
            }
            get
            {
                return code;
            }
        }
        public string Msg
        {
            set
            {
                msg = value;
            }
            get
            {
                return msg;
            }
        }
        public object Obj
        {
            set
            {
                obj = value;
            }
            get
            {
                return obj;
            }
        }
    }
}
