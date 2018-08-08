using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSC.Desktop.App
{
    public partial class frmPrincipal : Form
    {

        protected Forms.frmPecuarista formPecuarista;
        protected Forms.frmAnimal formAnimal;
        protected Forms.frmCompra formCompra;
        protected Forms.frmConsultaCompra formConsultaCompra;

        public frmPrincipal()
        {

            InitializeComponent();

            formPecuarista = new Forms.frmPecuarista();
            formPecuarista.MdiParent = this;

            formAnimal = new Forms.frmAnimal();
            formAnimal.MdiParent = this;

            formCompra = new Forms.frmCompra();
            formCompra.MdiParent = this;

            formConsultaCompra = new Forms.frmConsultaCompra();
            formConsultaCompra.MdiParent = this;

        }

        private void pecuaristaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!formPecuarista.Visible)
            {
                formPecuarista = new Forms.frmPecuarista();
                formPecuarista.MdiParent = this;
            }
            formPecuarista.Show();
        }

        private void animalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!formAnimal.Visible)
            {
                formAnimal = new Forms.frmAnimal();
                formAnimal.MdiParent = this;
            }
            formAnimal.Show();
        }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!formCompra.Visible)
            {
                formCompra = new Forms.frmCompra();
                formCompra.MdiParent = this;
            }
            formCompra.Show();

        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!formConsultaCompra.Visible)
            {
                formConsultaCompra = new Forms.frmConsultaCompra();
                formConsultaCompra.MdiParent = this;
            }
            formConsultaCompra.Show();
        }
    }
}
