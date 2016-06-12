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
    public partial class MeteringPopup : Form
    {
        MeteringView pForm;
        meteringCtrl ctrl = new meteringCtrl();
        public MeteringPopup(MeteringView form)
        {
            pForm = form;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("字段必填！");
            }
            MeteringModel m = new MeteringModel(textBox1.Text, textBox2.Text);
            MessageModel msg = ctrl.add(m);
            if (msg.Code == 0)
            {
                this.Hide();
                pForm.loadData();
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
