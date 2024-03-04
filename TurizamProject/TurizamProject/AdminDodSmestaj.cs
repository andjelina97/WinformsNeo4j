using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurizamProject.DomainModel;

namespace TurizamProject
{
    public partial class AdminDodSmestaj : Form
    {
        public Admin admin;
        public AdminDodSmestaj(Admin a)
        {
            admin = a;
            InitializeComponent();
            comboBox1.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && textBox2.Text != null && textBox1.Text != null)
            {
                Smestaj s = new Smestaj(textBox2.Text, textBox1.Text);
                admin.dodajSmestaj(s, (Destinacija)comboBox2.SelectedItem);
                MessageBox.Show("Smeštaj je uspešno dodat");
                textBox2.ResetText();
                textBox1.ResetText();
            }
            else
                MessageBox.Show("Sva polja su obavezna");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Gray;
            button1.BackColor = Color.Beige;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Gray;
            button1.BackColor = Color.Beige;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Beige;
            button1.BackColor = Color.Transparent;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Gray;
            button2.BackColor = Color.Beige;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Gray;
            button2.BackColor = Color.Beige;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Beige;
            button2.BackColor = Color.Transparent;
        }
    }
}
