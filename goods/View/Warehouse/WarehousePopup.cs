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
        public WarehousePopup(WarehouseView form,string type,int wid)
        {
            parentForm = form;
            this.type = type;
            this.wid = wid;
            
            InitializeComponent();
            if (type == "cw") this.Text = "新建仓位";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("字段必填！");
            }
            else if(type == "ck")
            {
                WarehouseModel s = new WarehouseModel(textBox1.Text, textBox2.Text);
                MessageModel msg = ctrl.add(s);
                if (msg.Code == 0)
                {
                    this.Hide();
                    parentForm.loadData();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
            else
            {
                PositionModel p = new PositionModel(textBox1.Text, textBox2.Text, wid);
                MessageModel msg = pctrl.add(p);
                if (msg.Code == 0)
                {
                    this.Hide();
                    parentForm.loadData();
                }
                else
                {
                    MessageBox.Show(msg.Msg);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
