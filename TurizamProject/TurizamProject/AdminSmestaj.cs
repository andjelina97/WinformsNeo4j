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
    public partial class AdminSmestaj : Form
    {
        public Admin admin;
        public AdminSmestaj(Admin a)
        {
            admin = a;
            InitializeComponent();
            comboBox1.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminDodSmestaj dodaj = new AdminDodSmestaj(admin);
            dodaj.Show();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.ResetText();
            labelSmestaj.ResetText();
            comboBox2.DataSource = Destinacija.pribaviSvaMesta(comboBox1.SelectedItem.ToString());
            comboBox2.DisplayMember = "mesto";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.ResetText();
            labelSmestaj.ResetText();
            listSmestaji.DataSource = admin.pribaviSmestaje((Destinacija)comboBox2.SelectedItem);
            listSmestaji.DisplayMember = "naziv";
        }

        private void listSmestaji_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = ((Smestaj)listSmestaji.SelectedItem).opis;
            labelSmestaj.Text = ((Smestaj)listSmestaji.SelectedItem).naziv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin.obrisiSmestaj((Smestaj)listSmestaji.SelectedItem);
            MessageBox.Show("Uspesno brisanje smestaja");
            labelSmestaj.ResetText();
            textBox1.ResetText();
        }

        private void AdminSmestaj_Activated(object sender, EventArgs e)
        {
            comboBox1.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
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
    }
}
