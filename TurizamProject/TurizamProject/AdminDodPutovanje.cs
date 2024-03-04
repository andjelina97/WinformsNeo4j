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
    public partial class AdminDodPutovanje : Form
    {

        Admin admin;
        public AdminDodPutovanje(Admin a)
        {
            admin = a;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrWhiteSpace(textBox1.Text) && String.IsNullOrWhiteSpace(textBox2.Text)
               && String.IsNullOrWhiteSpace(textBox3.Text) && String.IsNullOrWhiteSpace(textBox4.Text) && comboBox3.SelectedItem != null))
            {
                Putovanje novo = new Putovanje();
                novo.mestoIVremePolaska = textBox4.Text;
                novo.cena = Convert.ToInt32(textBox1.Text);
                novo.kratakOpis = textBox2.Text;
                novo.program = textBox3.Text;
                novo.vremePolaska = textBox5.Text;
                novo.vremeDolaska = textBox6.Text;
                admin.dodajPutovanje(novo, (Smestaj)comboBox3.SelectedItem);
                MessageBox.Show("Uspesno ste dodali putovanje");

                textBox1.ResetText();
                textBox2.ResetText();
                textBox3.ResetText();
                textBox4.ResetText();
                textBox5.ResetText();
                textBox6.ResetText();
            }
            else
                MessageBox.Show("Sva polja su obavezna");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminDodPutovanje_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != null)
            {
                comboBox2.DataSource = Destinacija.pribaviSvaMesta(comboBox1.SelectedItem.ToString());
                comboBox2.DisplayMember = "mesto";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Destinacija)comboBox2.SelectedItem != null)
            {
                comboBox3.DataSource = admin.pribaviSmestaje((Destinacija)comboBox2.SelectedItem);
                comboBox3.DisplayMember = "naziv";
            }
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
