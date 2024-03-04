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
    public partial class Rezervacije : Form
    {
        public Putovanje putovanje;
        public Admin admin;
        public Rezervacije(Putovanje p, Admin a)
        {
            putovanje = p;
            admin = a;
            InitializeComponent();
        }


        private void Rezervacije_Load(object sender, EventArgs e)
        {
            foreach (Rezervisao r in putovanje.pribaviRezervacije())
            {
                this.dataGridView1.Rows.Add(r.klijent.ime, r.klijent.email, r.klijent.telefon, r.broj_putnika);
            }
        }
    }
}
