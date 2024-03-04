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
    public partial class Interesovanja : Form
    {
        public Putovanje putovanje;
        public Admin admin;
        public Interesovanja(Putovanje p, Admin a)
        {
            this.putovanje = p;
            this.admin = a;
            InitializeComponent();
        }

        private void Interesovanja_Load(object sender, EventArgs e)
        {
            foreach (Klijent k in putovanje.pribaviZainteresovane())
            {
                this.dataGridView1.Rows.Add(k.ime, k.email, k.telefon);
            }
        }
    }
}
