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
    public partial class PrijavljivanjeForma : Form
    {
        public PrijavljivanjeForma()
        {
            InitializeComponent();
        }

        private void PrijavljivanjeForma_Load(object sender, EventArgs e)
        {
            if (!Admin.postojiAdmin())
            {
                Admin.kreirajAdmina();
                MessageBox.Show("Tokom prvog logovanja u svojstvu admina, koristite sledeće podatke za logovanje:\n\nemail: admin \nšifra: admin\n\nPreporučuje se njihovo menjanje tokom prvog korišćenja");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin a = Admin.LogIn(textBox1.Text, textBox2.Text);
            if (a != null)
            {
                AdminForma af = new AdminForma();
                af.pf = this;
                af.admin = a;
                af.Show();
                this.Hide();
            }

            else
            {
                Klijent klijent = Klijent.LogIn(textBox1.Text, textBox2.Text);
                if (klijent != null)
                {
                    //otvaranje nove forme i prosledjivanje klijenta njoj
                    this.Hide();
                    KlijentForma klijentF = new KlijentForma();
                    klijentF.pf = this;
                    klijentF.klijent = klijent;
                    klijentF.Show();
                }
                else
                {
                    MessageBox.Show("Prijavljivanje nije uspelo. Pokušajte ponovo.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NoviNalogForma forma1 = new NoviNalogForma();
            forma1.Show();
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
