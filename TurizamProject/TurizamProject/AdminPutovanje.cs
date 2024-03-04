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
    public partial class AdminPutovanje : Form
    {
        public Admin admin;
        public AdminPutovanje(Admin a)
        {
            admin = a;
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.DataSource = Destinacija.pribaviSvaMesta(comboBox1.SelectedItem.ToString());
            comboBox2.DisplayMember = "mesto";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = Putovanje.pribaviPutovanjaPremaMestu(((Destinacija)comboBox2.SelectedItem).mesto);
            listBox1.DisplayMember = "kratakOpis";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Putovanje p = (Putovanje)listBox1.SelectedItem;
            label10.Text = p.cena + " EUR";
            label11.Text = p.prosecnaOcena() + "/5";
            label12.Text = p.vremePolaska;
            label13.Text = p.vremeDolaska;
            label14.Text = p.mestoIVremePolaska;

            textBox1.Text = p.program;
            textBox2.Text = p.pribaviSmestaj().naziv;
            labelaNaziv.Text = p.pribaviSmestaj().naziv;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDodPutovanje add = new AdminDodPutovanje(admin);
            add.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da obrišete ovo putovanje?", "Brisanje putovanja", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                admin.obrisiPutovanje((Putovanje)listBox1.SelectedItem);
                textBox2.ResetText();
                labelaNaziv.ResetText();
                label9.ResetText();
                label10.ResetText();
                label11.ResetText();
                label12.ResetText();
                label14.ResetText();
                textBox1.ResetText();
            }
        }

            private void button3_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Rezervacije r = new Rezervacije((Putovanje)listBox1.SelectedItem, admin);
            r.Show();
        }

        private void AdminPutovanje_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Interesovanja inter = new Interesovanja((Putovanje)listBox1.SelectedItem, admin);
            inter.Show();
        }

        private void AdminPutovanje_Activated(object sender, EventArgs e)
        {
            listBox1.DataSource = Putovanje.pribaviPutovanjaPremaMestu(((Destinacija)comboBox2.SelectedItem).mesto);
            listBox1.DisplayMember = "kratakOpis";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Hide();
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

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Gray;
            button3.BackColor = Color.Beige;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Gray;
            button3.BackColor = Color.Beige;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Beige;
            button3.BackColor = Color.Transparent;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Gray;
            button4.BackColor = Color.Beige;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Gray;
            button4.BackColor = Color.Beige;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Beige;
            button4.BackColor = Color.Transparent;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.ForeColor = Color.Gray;
            button5.BackColor = Color.Beige;
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            button5.ForeColor = Color.Gray;
            button5.BackColor = Color.Beige;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.ForeColor = Color.Beige;
            button5.BackColor = Color.Transparent;
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
    }
}
