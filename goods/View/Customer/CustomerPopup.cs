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
    public partial class CustomerPopup : Form
    {
        customerCtrl ctrl = new customerCtrl();
        public CustomerView parentForm;
        public CustomerPopup(CustomerView form)
        {
            InitializeComponent();
            parentForm = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("编码名称不得为空！");
                return;
            }
            CustomerModel model = new CustomerModel(textBox1.Text, textBox2.Text);
            MessageModel msg = ctrl.add(model);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
