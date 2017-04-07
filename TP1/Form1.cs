using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP01
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Aca utilizo el constructor sin parametros. Probablemente esté mal crear un nuevo objeto para esto, no obstante no se me
            // ocurrio otra manera para utilizar este constructor
            Numero nuevoNumero = new Numero();
            txtNumero1.Text = (nuevoNumero.getNumero()).ToString();
            Numero nuevoNumero2 = new Numero();
            txtNumero2.Text = (nuevoNumero2.getNumero()).ToString();
            cmbOperacion.Text = null;
            lblResultado.Text = null;
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            Numero nuevoNumero = new Numero(txtNumero1.Text);
            txtNumero1.Text = (nuevoNumero.getNumero()).ToString();
            Numero nuevoNumero2 = new Numero(txtNumero2.Text);
            txtNumero2.Text = (nuevoNumero2.getNumero()).ToString();

            cmbOperacion.Text = Calculadora.validarOperador(cmbOperacion.Text);
            lblResultado.Text = Convert.ToString(Calculadora.operar(nuevoNumero, nuevoNumero2, cmbOperacion.Text));
        }
    }
}
