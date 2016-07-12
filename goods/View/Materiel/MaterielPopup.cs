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
using System.Text.RegularExpressions;

namespace goods
{
    public partial class MaterielPopup : Form
    {
        materielCtrl ctrl = new materielCtrl();
        meteringCtrl mctrl = new meteringCtrl();
        MaterielView parentForm;
        private string pattern = @"(^(\d*\.)?\d*$)"; //@"(^[1-9]\.\d{0,2}$)";
        private string param1 = null;
        private string param2 = null;
        int id;
        public MaterielPopup(MaterielView form,int id)
        {
            parentForm = form;
            InitializeComponent();
            this.id = id;
            loadData();
        }

        private void loadData()
        {
            var depdt = mctrl.get();

            DataRow dr = depdt.NewRow();
            dr["id"] = "0";
            dr["name"] = "请选择";
            depdt.Rows.InsertAt(dr, 0);

            this.comboBox1.DataSource = depdt;
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "id";

            var depdt2 = depdt.Copy();
            this.comboBox2.DataSource = depdt2;
            this.comboBox2.DisplayMember = "name";
            this.comboBox2.ValueMember = "id";

            if(id > 0)
            {
                DataTable dt = ctrl.getByid(id);
                if(dt.Rows.Count > 0)
                {
                    this.comboBox1.SelectedValue = dt.Rows[0]["metering"];
                    if(dt.Rows[0]["subMetering"] != DBNull.Value) this.comboBox2.SelectedValue = dt.Rows[0]["subMetering"];
                    else
                    {
                        setConversion(false);
                    }
                    this.comboBox3.SelectedItem = dt.Rows[0]["type"];
                    this.textBox8.Text = dt.Rows[0]["tax"].ToString();
                    this.textBox1.Text = dt.Rows[0]["num"].ToString();
                    this.textBox2.Text = dt.Rows[0]["name"].ToString();
                    this.textBox3.Text = dt.Rows[0]["specifications"].ToString();
                    if(dt.Rows[0]["conversion"] != DBNull.Value) this.textBox6.Text = dt.Rows[0]["conversion"].ToString();
                    this.checkBox1.Checked = Convert.ToBoolean( dt.Rows[0]["isBatch"]);
                }
                else
                {
                    MessageBox.Show("加载失败，请重试。");
                    this.Close();
                }

            }
            else
            {
                this.comboBox1.SelectedIndex = 0;
                this.comboBox3.SelectedIndex = 1;
                this.textBox8.Text = "0.17";
                setConversion(false);
            }
            var can = ctrl.findOrderMateral(id);
            if (can > 0)
            {
                this.textBox1.ReadOnly = true;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = false;
                setConversion(false);
                this.checkBox1.Enabled = false;
            }

        }
        private void setConversion(bool showConversion)
        {
            this.label13.Visible = showConversion;
            this.textBox6.Enabled = showConversion;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int metering = Convert.ToInt32(this.comboBox1.SelectedValue.ToString());
            int subMetering = Convert.ToInt32(this.comboBox2.SelectedValue.ToString());

            if (textBox1.Text == "" || textBox2.Text == "" || metering == 0 ||(subMetering != 0 && textBox6.Text == ""))
            {
                MessageBox.Show("请填写必填项！");
                return;
            }
            
            string type = this.comboBox3.Text;
            MaterielModel mm = new MaterielModel(textBox1.Text, textBox2.Text, textBox3.Text, metering, checkBox1.Checked);
            if (textBox8.Text != "") mm.Tax = Convert.ToDouble(textBox8.Text);
            if (subMetering > 0)
            {
                mm.SubMetering = subMetering;
                mm.Conversion = Convert.ToDouble(textBox6.Text);
            }
            if(type != "") mm.Type = type;
            if (id>0)
            {
                mm.Id = id;
            }
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.comboBox2.SelectedValue.ToString()) >0)
            {
                setConversion(true); 
            }
            else
            {
                setConversion(false);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox8.Text.Trim() == "")
            {
                textBox8.Text = "";
            }
            else
            {
                Match m = Regex.Match(this.textBox8.Text, pattern);   // 匹配正则表达式

                if (!m.Success)   // 输入的不是数字
                {
                    this.textBox8.Text = param1;   // textBox内容不变

                    // 将光标定位到文本框的最后
                    this.textBox8.SelectionStart = this.textBox8.Text.Length;
                }
                else   // 输入的是数字
                {
                    param1 = this.textBox8.Text;   // 将现在textBox的值保存下来
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox6.Text.Trim() == "")
            {
                textBox6.Text = "";
            }
            else
            {
                Match m = Regex.Match(this.textBox6.Text, pattern);   // 匹配正则表达式

                if (!m.Success)   // 输入的不是数字
                {
                    this.textBox6.Text = param2;   // textBox内容不变

                    // 将光标定位到文本框的最后
                    this.textBox6.SelectionStart = this.textBox6.Text.Length;
                }
                else   // 输入的是数字
                {
                    param2 = this.textBox6.Text;   // 将现在textBox的值保存下来
                }
            }
        }
    }
}
