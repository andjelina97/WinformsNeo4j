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
    public partial class AdminDestinacija : Form
    {
        public Admin admin;
        public AdminDestinacija(Admin a)
        {
            admin = a;
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxDest.SelectedItem != null)
            {
                admin.obrisiDestinaciju((Destinacija)listBoxDest.SelectedItem);
                MessageBox.Show("Destinacija uspesno obrisana");
                comboBoxDr.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
                comboBox1.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && textBox2.Text != "")
            {
                admin.dodajDestinaciju(new Destinacija(comboBox1.Text, textBox2.Text));
                textBox2.ResetText();
                MessageBox.Show("Uspesno dodavanje destinacije");
                comboBoxDr.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
                comboBox1.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
            }
        }

        private void comboBoxDr_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxDest.DataSource = Destinacija.pribaviSvaMesta(comboBoxDr.SelectedItem.ToString());
            listBoxDest.DisplayMember = "mesto";
        }

        private void AdminDestinacija_Load(object sender, EventArgs e)
        {
            comboBoxDr.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
            comboBox1.DataSource = Destinacija.pribaviSveDrzave().Distinct().ToList();
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
            button1.ForeColor = Color.Gray;
            button1.BackColor = Color.Beige;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Gray;
            button1.BackColor = Color.Beige;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Beige;
            button1.BackColor = Color.Transparent;
        }
    }
}
