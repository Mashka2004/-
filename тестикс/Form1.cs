using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace тестикс
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "user")
            {
                if (textBox2.Text == "user")
                {
                    mainForm mainForm = new mainForm();
                    this.Visible = false;
                    mainForm.ShowDialog();
                    this.Visible = true;
                }
                else
                {
                    Captcha captcha = new Captcha();
                    this.Visible = false;
                    captcha.ShowDialog();
                    this.Visible = true;
                }
            }

            else
            {
                MessageBox.Show("Данного пользователя не существует");
            }
        }
       

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                label2.Visible = false;
            }
            else
            {
                label2.Visible = true;
            }

            if (textBox2.Text.Length > 0)
            {
                label3.Visible = false;
            }
            else
            {
                label3.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           

            if (textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
