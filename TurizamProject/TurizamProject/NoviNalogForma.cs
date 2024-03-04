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
    public partial class NoviNalogForma : Form
    {
        public NoviNalogForma()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxIme.Text) && !String.IsNullOrWhiteSpace(textBoxEmail.Text) && !String.IsNullOrWhiteSpace(textBoxLozinka.Text) && !String.IsNullOrWhiteSpace(textBoxTelefon.Text))
            {
                if (Klijent.pribaviKlijenta(textBoxEmail.Text) == null)
                {
                    Klijent noviKlijent = new Klijent(textBoxIme.Text, textBoxEmail.Text, textBoxLozinka.Text, textBoxTelefon.Text);
                    noviKlijent.sacuvajUDB();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Uneti e-mail je već u upotrebi.");
                }
            }
            else
                MessageBox.Show("Sva polja su obavezna.");
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
