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
    public partial class AdminForma : Form
    {
        public Admin admin;
        public PrijavljivanjeForma pf;
        public AdminForma()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDestinacija ad = new AdminDestinacija(admin);

            ad.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminSmestaj ad = new AdminSmestaj(admin);

            ad.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminPutovanje ap = new AdminPutovanje(admin);
            ap.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IzmenaNaloga izmena = new IzmenaNaloga(admin);
            izmena.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminForma_FormClosed(object sender, FormClosedEventArgs e)
        {
            pf.Show();
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

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Beige;
            button1.BackColor = Color.Gray;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Beige;
            button1.BackColor = Color.Gray;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Gray;
            button1.BackColor = Color.Transparent;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Beige;
            button2.BackColor = Color.Gray;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Beige;
            button2.BackColor = Color.Gray;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Gray;
            button2.BackColor = Color.Transparent;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Beige;
            button3.BackColor = Color.Gray;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Beige;
            button3.BackColor = Color.Gray;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Gray;
            button3.BackColor = Color.Transparent;
        }
    }
}
