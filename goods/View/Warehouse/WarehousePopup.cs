using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using goods.Controller;
using goods.Model;
namespace goods
{
    public partial class WarehousePopup : Form
    {
        warehouseCtrl ctrl = new warehouseCtrl();
        positionCtrl pctrl = new positionCtrl();
        WarehouseView parentForm;
        string type;
        int wid;
        WarehouseModel oldwm;
        PositionModel oldpm;
        public WarehousePopup(WarehouseView form,string type,int wid, object obj)
        {
            parentForm = form;
            this.type = type;
            this.wid = wid;
            InitializeComponent();
            if (type == "cw")  this.Text = "仓位";
            if (obj != null)
            {
                if (type == "cw") {
                    oldpm = (PositionModel)obj;
                    textBox1.Text = oldpm.Num;
                    textBox2.Text = oldpm.Name;
                }
                else
                {
                    oldwm = (WarehouseModel)obj;
                    textBox1.Text = oldwm.Num;
                    textBox2.Text = oldwm.Name;
                }
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("字段必填！");
            }
            else if(type == "ck")
            {
                WarehouseModel s;
                if (oldwm != null && oldwm.Id > 0) s = new WarehouseModel(oldwm.Id, textBox1.Text, textBox2.Text);
                else s = new WarehouseModel(textBox1.Text, textBox2.Text);
                MessageModel msg = ctrl.add(s);
                if (msg.Code == 0)
                {
                    parentForm.loadData();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
            else
            {
                PositionModel p;
                if (oldpm != null && oldpm.Id > 0) p = new PositionModel(oldpm.Id, textBox1.Text, textBox2.Text);
                else p = new PositionModel(textBox1.Text, textBox2.Text, wid);
                MessageModel msg = pctrl.add(p);
                if (msg.Code == 0)
                {
                    parentForm.loadData();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
