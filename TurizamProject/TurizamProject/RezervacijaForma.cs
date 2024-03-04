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
    public partial class RezervacijaForma : Form
    {
        public Klijent klijent;
        public Putovanje putovanje;

        public RezervacijaForma()
        {
            InitializeComponent();
            numericUpDown1.Maximum = 15;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Izvršiti rezervaciju?", "Rezervacija", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                klijent.rezervisi(putovanje, Convert.ToInt32(numericUpDown1.Value));
                MessageBox.Show("Očekujte poziv iz agencije u roku od 48 sati povodom potvrde rezervacije i dogovora oko detalja plaćanja.\n Hvala na poverenju!");
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = "" + numericUpDown1.Value * putovanje.cena + " EUR";
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.SeaShell;
            button1.BackColor = Color.Gray;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.ForeColor = Color.SeaShell;
            button1.BackColor = Color.Gray;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
            button1.BackColor = Color.Snow;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.SeaShell;
            button2.BackColor = Color.Gray;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ForeColor = Color.SeaShell;
            button2.BackColor = Color.Gray;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Black;
            button2.BackColor = Color.SeaShell;
        }
    }
}
