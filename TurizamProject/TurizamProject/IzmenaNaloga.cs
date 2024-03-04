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
    public partial class IzmenaNaloga : Form
    {
        public Admin admin;
        public IzmenaNaloga(Admin a)
        {
            admin = a;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                admin.promeniKorisnicko(textBox1.Text);
                admin.promeniSifru(textBox2.Text);
                MessageBox.Show("Uspešno ste izmenili admina");
                textBox1.ResetText();
                textBox2.ResetText();
                this.Close();
            }
            else
                MessageBox.Show("Sva polja su obavezna");
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
