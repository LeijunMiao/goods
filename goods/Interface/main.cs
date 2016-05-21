using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using goods.Model;

namespace goods.Interface
{
    interface main
    {
        DataTable get();
        MessageModel add(object obj);
        MessageModel set(object obj);
        MessageModel del(object obj);
    }
}
