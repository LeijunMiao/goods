using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace goods
{
    public partial class numberbox1 : UserControl
    {
        public numberbox1()
        {
            InitializeComponent();
            this.textBox1.KeyPress += NumberBox_KeyPress;
        }
        private void NumberBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (allowDecimal)
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
                {
                    e.Handled = true;
                }
                //可以在第一位输出一个负号
                if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                {
                    e.Handled = true;
                }
                //判断小数位数
                if (((TextBox)sender).Text.Split('.').Length > 1)
                {
                    if (((TextBox)sender).Text.Split('.').Length > decimalDigits)
                    {
                        if (e.KeyChar != '\b')
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            else
            {
                if (e.KeyChar != '\b')
                {
                    if (e.KeyChar < '0' || e.KeyChar > '9')
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        #region 是否可以输入小数
        public bool allowDecimal = true;
        #endregion
        #region 允许输入小数的位数   
        public int decimalDigits = 2;
        #endregion
        public string TextNumber
        {
            set
            {
                Decimal number = 0;
                try
                {
                    number = Decimal.Parse(value);
                }
                catch
                {

                }
                if (number > 0)
                {
                    this.textBox1.Text = value;
                }
                else
                {
                    this.textBox1.Text = "";
                }
            }
            get
            {
                if (this.textBox1.Text == "") return "0";
                else
                {
                    return this.textBox1.Text;
                }
            }
        }
        /// <summary>
        /// 设置textbox长宽
        /// </summary>
        /// <param name="width">长</param>
        /// <param name="height">宽</param>
        public void style(int width, int height)
        {
            this.textBox1.Width = width;
            this.textBox1.Height = height;
        }
    }
}
