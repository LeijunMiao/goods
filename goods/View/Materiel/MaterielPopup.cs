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
    public partial class MaterielPopup : Form
    {
        materielCtrl ctrl = new materielCtrl();
        meteringCtrl mctrl = new meteringCtrl();
        MaterielView parentForm;
        public MaterielPopup(MaterielView form)
        {
            parentForm = form;
            InitializeComponent();
            loadDate();
        }

        private void loadDate()
        {
            var depdt = mctrl.get();

            DataRow dr = depdt.NewRow();
            dr["id"] = "0";
            dr["name"] = "请选择";
            depdt.Rows.InsertAt(dr, 0);

            this.comboBox1.DataSource = depdt;
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "id";
            this.comboBox1.SelectedIndex = 0;

            var depdt2 = depdt.Copy();
            this.comboBox2.DataSource = depdt2;
            this.comboBox2.DisplayMember = "name";
            this.comboBox2.ValueMember = "id";
            //this.comboBox2.SelectedIndex = 0;

            this.comboBox3.SelectedIndex = 1;

            this.textBox8.Text = "0.17";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int metering = Convert.ToInt32(this.comboBox1.SelectedValue.ToString());
            int subMetering = Convert.ToInt32(this.comboBox2.SelectedValue.ToString());
            string type = this.comboBox3.Text;
            MaterielModel mm = new MaterielModel(textBox1.Text, textBox2.Text, textBox3.Text, metering, checkBox1.Checked);
            if (subMetering > 0)
            {
                mm.SubMetering = subMetering;
                mm.Conversion = Convert.ToDouble(textBox6.Text);
            }
            if(type != "") mm.Type = type;
            MessageModel msg = ctrl.add(mm);
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
