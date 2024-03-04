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
    public partial class KlijentForma : Form
    {
        public Klijent klijent;
        public bool prijatelj;
        public bool rezervacije;
        public PrijavljivanjeForma pf;

        public KlijentForma()
        {
            InitializeComponent();
        }

        public void ucitajPutovanja(Klijent kli)
        {
            listBoxPutovanja.DataSource = kli.pribaviInteresovanja();
            listBoxPutovanja.DisplayMember = "kratakOpis";

        }
        public void ucitajPutovanja(Destinacija dest)
        {
            listBoxPutovanja.DataSource = Putovanje.pribaviPutovanjaPremaMestu(dest.mesto);
            listBoxPutovanja.DisplayMember = "kratakOpis";
        }
        public void ucitajPutovanja(string Drzava)
        {
            listBoxPutovanja.DataSource = Putovanje.pribaviPutovanjaPremaDrzavi(Drzava);
            listBoxPutovanja.DisplayMember = "kratakOpis";
        }



        private void KlijentForma_Load(object sender, EventArgs e)
        {
            List<String> drzave = Destinacija.pribaviSveDrzave();
            comboBoxDrzava.DataSource = drzave.Distinct().ToList();
        }

        private void KlijentForma_FormClosed(object sender, FormClosedEventArgs e)
        {
            pf.Show();
        }

        private void comboBoxDrzava_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxMesto.DataSource = Destinacija.pribaviSvaMesta(comboBoxDrzava.SelectedItem.ToString());
            listBoxMesto.DisplayMember = "mesto";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ucitajPutovanja(comboBoxDrzava.SelectedItem.ToString());
            panelPocetni.Hide();
            panelPonude.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ucitajPutovanja((Destinacija)listBoxMesto.SelectedItem);
            panelPocetni.Hide();
            panelPonude.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelPocetni.Hide();

            rezervacije = true;
            List<Putovanje> rezervisanaPutovanja = new List<Putovanje>();

            foreach (Rezervisao put in klijent.pribaviRezervacije())
            {
                rezervisanaPutovanja.Add(put.putovanje);
            }
            listBoxPutovanja.DataSource = rezervisanaPutovanja;
            listBoxPutovanja.DisplayMember = "kratakOpis";
            buttonRezervacijaMesta.Hide();
            buttonInteresujeMe.Hide();
            listBoxPutovanja.Height = 435;
            labelMesta.Show();
            labelBrMesta.Show();
            panelPonude.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelPocetni.Hide();
            ucitajPutovanja(klijent);
            buttonInteresujeMe.Text = "Ukloni iz interesovanja";
            panelPonude.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panelPocetni.Hide();
            panelPrijatelji.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            prijatelj = true;
            panelPrijatelji.Hide();
            ucitajPutovanja((Klijent)listBoxPrijatelji.SelectedItem);
            panelPonude.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBoxZahtevi.SelectedItem != null)
            {
                klijent.prihvatiZahtev(((Klijent)listBoxZahtevi.SelectedItem).email);
                listBoxZahtevi.DataSource = klijent.pribaviZahteve();
                listBoxZahtevi.DisplayMember = "ime";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (listBoxZahtevi.SelectedItem != null)
            {
                klijent.obrisiZahtev(((Klijent)listBoxZahtevi.SelectedItem).email);
                listBoxZahtevi.DataSource = klijent.pribaviZahteve();
                listBoxZahtevi.DisplayMember = "ime";
            }
        }

        private void buttonPosaljiZah_Click(object sender, EventArgs e)
        {

            if (textBoxZahtev.Text != "")
            {
                foreach (Klijent k in klijent.pribaviPrijatelje())
                {
                    if (k.email == textBoxZahtev.Text)
                    {
                        MessageBox.Show("Ovaj klijent vam je već prijatelj");
                        return;
                    }

                }
                foreach (Klijent k in klijent.pribaviZahteve())
                {
                    if (k.email == textBoxZahtev.Text)
                    {
                        MessageBox.Show("Ovaj klijent vam je poslao zahtev, proverite listu zahteva");
                        return;
                    }

                }
                klijent.posaljiZahtev(textBoxZahtev.Text);
                MessageBox.Show("Uspesno ste poslali zahtev.");
                textBoxZahtev.ResetText();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panelPrijatelji.Hide();
            panelPocetni.Show();
            prijatelj = false;
        }

        private void listBoxPrijatelji_SelectedIndexChanged(object sender, EventArgs e)
        {
            label19.Text = ((Klijent)listBoxPrijatelji.SelectedItem).email;
            label20.Text = ((Klijent)listBoxPrijatelji.SelectedItem).telefon;
        }

        private void button14_Click(object sender, EventArgs e)
        {

            panelPonude.Hide();
            if (!prijatelj)
                panelPocetni.Show();
            else
                panelPrijatelji.Show();
            labelCena.Text = "";
            labelDolazak.Text = "";
            labelDrzava.Text = "";
            labelGrad.Text = "";
            labelPolazak.ResetText();
            labelSmestaj.ResetText();
            textBoxProgram.ResetText();
            textBoxSmestaj.ResetText();
            buttonInteresujeMe.Text = "Interesuje me";
            rezervacije = false;
            //listBoxPutovanja.Height = 355;
            labelMesta.Hide();
            labelBrMesta.Hide();
            buttonInteresujeMe.Show();
            buttonRezervacijaMesta.Show();
        }

        private void buttonRezervacijaMesta_Click(object sender, EventArgs e)
        {
            RezervacijaForma rezervacija = new RezervacijaForma();
            rezervacija.klijent = this.klijent;
            rezervacija.putovanje = (Putovanje)listBoxPutovanja.SelectedItem;
            rezervacija.Show();
        }

        private void buttonInteresujeMe_Click(object sender, EventArgs e)
        {
            if (buttonInteresujeMe.Text != "Ukloni iz interesovanja" && listBoxPutovanja.SelectedItem != null)
            {
                klijent.zainteresujSe((Putovanje)listBoxPutovanja.SelectedItem);
                MessageBox.Show("Putovanje \"" + ((Putovanje)listBoxPutovanja.SelectedItem).kratakOpis + "\" je dodato u sekciju Moja interesovanja");
            }
            else
            {
                if (listBoxPutovanja.SelectedItem != null)
                {
                    klijent.obrisiZainteresovanost((Putovanje)listBoxPutovanja.SelectedItem);
                    MessageBox.Show("Uklanjanje interesovanja uspelo");
                    ucitajPutovanja(klijent);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panelSmestaj.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            klijent.oceniPutovanje((Putovanje)listBoxPutovanja.SelectedItem, Convert.ToInt32(numericUpDown1.Value));
            labelOcena.Text = ((Putovanje)listBoxPutovanja.SelectedItem).prosecnaOcena().ToString();
        }

        private void listBoxPutovanja_SelectedIndexChanged(object sender, EventArgs e)
        {

            //popuni informacije o putovanju i smestaju
            Putovanje p = (Putovanje)listBoxPutovanja.SelectedItem;
            labelGrad.Text = ((Destinacija)listBoxMesto.SelectedItem).mesto;
            labelDrzava.Text = comboBoxDrzava.SelectedItem.ToString();
            labelCena.Text = "" + p.cena + " EUR";
            labelDolazak.Text = p.vremeDolaska;
            labelPolazak.Text = p.vremePolaska;
            labelSmestaj.Text = p.pribaviSmestaj().naziv;
            textBoxSmestaj.Text = p.pribaviSmestaj().opis;
            textBoxProgram.Text = p.program;
            if (p.prosecnaOcena() != 0)
                labelOcena.Text = p.prosecnaOcena().ToString();
            else
                labelOcena.Text = "Još nema ocena";
            labelMV.Text = p.mestoIVremePolaska;

            if (rezervacije)
            {
                labelMesta.Text = klijent.pribaviBrojMesta((Putovanje)listBoxPutovanja.SelectedItem).ToString();
            }

            if (klijent.ocenio(p))
            {
                numericUpDown1.Hide();
                button5.Hide();
            }
            else
            {
                button5.Show();
                numericUpDown1.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panelSmestaj.Hide();
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            button8.ForeColor = Color.Gray;
            button8.BackColor = Color.Beige;
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            button8.ForeColor = Color.Gray;
            button8.BackColor = Color.Beige;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.ForeColor = Color.Beige;
            button8.BackColor = Color.Transparent;
        }

        private void buttonInteresujeMe_MouseEnter(object sender, EventArgs e)
        {
            buttonInteresujeMe.ForeColor = Color.Gray;
            buttonInteresujeMe.BackColor = Color.Beige;
        }

        private void buttonInteresujeMe_MouseHover(object sender, EventArgs e)
        {
            buttonInteresujeMe.ForeColor = Color.Gray;
            buttonInteresujeMe.BackColor = Color.Beige;
        }

        private void buttonInteresujeMe_MouseLeave(object sender, EventArgs e)
        {
            buttonInteresujeMe.ForeColor = Color.Beige;
            buttonInteresujeMe.BackColor = Color.Transparent;
        }

        private void buttonRezervacijaMesta_MouseEnter(object sender, EventArgs e)
        {
            buttonRezervacijaMesta.ForeColor = Color.Gray;
            buttonRezervacijaMesta.BackColor = Color.Beige;
        }

        private void buttonRezervacijaMesta_MouseHover(object sender, EventArgs e)
        {
            buttonRezervacijaMesta.ForeColor = Color.Gray;
            buttonRezervacijaMesta.BackColor = Color.Beige;
        }

        private void buttonRezervacijaMesta_MouseLeave(object sender, EventArgs e)
        {
            buttonRezervacijaMesta.ForeColor = Color.Beige;
            buttonRezervacijaMesta.BackColor = Color.Transparent;
        }

        private void button14_MouseEnter(object sender, EventArgs e)
        {
            button14.ForeColor = Color.Gray;
            button14.BackColor = Color.Beige;
        }

        private void button14_MouseHover(object sender, EventArgs e)
        {
            button14.ForeColor = Color.Gray;
            button14.BackColor = Color.Beige;
        }

        private void button14_MouseLeave(object sender, EventArgs e)
        {
            button14.ForeColor = Color.Beige;
            button14.BackColor = Color.Transparent;
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.ForeColor = Color.Gray;
            button6.BackColor = Color.Beige;
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            button6.ForeColor = Color.Gray;
            button6.BackColor = Color.Beige;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.ForeColor = Color.Beige;
            button6.BackColor = Color.Transparent;
        }

        private void buttonPosaljiZah_MouseEnter(object sender, EventArgs e)
        {
            buttonPosaljiZah.ForeColor = Color.Gray;
            buttonPosaljiZah.BackColor = Color.Beige;
        }

        private void buttonPosaljiZah_MouseHover(object sender, EventArgs e)
        {
            buttonPosaljiZah.ForeColor = Color.Gray;
            buttonPosaljiZah.BackColor = Color.Beige;
        }

        private void buttonPosaljiZah_MouseLeave(object sender, EventArgs e)
        {
            buttonPosaljiZah.ForeColor = Color.Beige;
            buttonPosaljiZah.BackColor = Color.Transparent;
        }

        private void button13_MouseEnter(object sender, EventArgs e)
        {
            button13.ForeColor = Color.Gray;
            button13.BackColor = Color.Beige;
        }

        private void button13_MouseHover(object sender, EventArgs e)
        {
            button13.ForeColor = Color.Gray;
            button13.BackColor = Color.Beige;
        }

        private void button13_MouseLeave(object sender, EventArgs e)
        {
            button13.ForeColor = Color. Beige;
            button13.BackColor = Color.Transparent;
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.ForeColor = Color.Gray;
            button11.BackColor = Color.PeachPuff;
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            button11.ForeColor = Color.Gray;
            button11.BackColor = Color.PeachPuff;
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.ForeColor = Color.PeachPuff;
            button11.BackColor = Color.Transparent;
        }

        private void button12_MouseEnter(object sender, EventArgs e)
        {
            button12.ForeColor = Color.Gray;
            button12.BackColor = Color.PeachPuff;
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            button12.ForeColor = Color.Gray;
            button12.BackColor = Color.PeachPuff;
        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            button12.ForeColor = Color.PeachPuff;
            button12.BackColor = Color.Transparent;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Gray;
            button4.BackColor = Color.PeachPuff;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Gray;
            button4.BackColor = Color.PeachPuff;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.PeachPuff;
            button4.BackColor = Color.Transparent;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Gray;
            button3.BackColor = Color.PeachPuff;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Gray;
            button3.BackColor = Color.PeachPuff;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.PeachPuff;
            button3.BackColor = Color.Transparent;
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
            button9.ForeColor = Color.Gray;
            button9.BackColor = Color.Beige;
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            button9.ForeColor = Color.Gray;
            button9.BackColor = Color.Beige;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.ForeColor = Color.Beige;
            button9.BackColor = Color.Transparent;
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

        private void button10_MouseEnter(object sender, EventArgs e)
        {
            button10.ForeColor = Color.Gray;
            button10.BackColor = Color.Beige;
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            button10.ForeColor = Color.Gray;
            button10.BackColor = Color.Beige;
        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            button10.ForeColor = Color.Beige;
            button10.BackColor = Color.Transparent;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Gray;
            button2.BackColor = Color.PeachPuff;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Gray;
            button2.BackColor = Color.PeachPuff;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.PeachPuff;
            button2.BackColor = Color.Transparent;
        }
    }
}
