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
    public partial class SupplierPopup : Form
    {
        supplierCtrl ctrl = new supplierCtrl();
        SupplierView parentForm;
        SupplierModel olds;
        public SupplierPopup(SupplierView form,object obj)
        {
            parentForm = form;
            InitializeComponent();
            if(obj != null)
            {
                olds = (SupplierModel)obj;
                textBox1.Text = olds.Num;
                textBox2.Text = olds.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("字段必填！");
            }
            else
            {
                SupplierModel s;
                if (olds!= null && olds.Id >0 ) s = new SupplierModel(olds.Id,textBox1.Text, textBox2.Text);
                else s = new SupplierModel(textBox1.Text, textBox2.Text);
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
