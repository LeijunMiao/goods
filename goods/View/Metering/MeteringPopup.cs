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
        MeteringModel olds;
        public MeteringPopup(MeteringView form, object obj)
        {
            pForm = form;
            InitializeComponent();
            if (obj != null)
            {
                olds = (MeteringModel)obj;
                textBox1.Text = olds.Num;
                textBox2.Text = olds.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("字段必填！");
                return;
            }
            MeteringModel m;
            if (olds != null && olds.Id > 0) m = new MeteringModel(olds.Id, textBox1.Text, textBox2.Text);
            else m = new MeteringModel(textBox1.Text, textBox2.Text);
            MessageModel msg = ctrl.add(m);
            if (msg.Code == 0)
            {
                pForm.loadData();
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
