using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using Hilo;

namespace Navegador
{
    public partial class frmWebBrowser : Form
    {
        private const string ESCRIBA_AQUI = "Escriba aquí...";
        Archivos.Texto archivos;

        public frmWebBrowser()
        {
            InitializeComponent();
        }

        private void frmWebBrowser_Load(object sender, EventArgs e)
        {
            this.txtUrl.SelectionStart = 0;  //This keeps the text
            this.txtUrl.SelectionLength = 0; //from being highlighted
            this.txtUrl.ForeColor = Color.Gray;
            this.txtUrl.Text = frmWebBrowser.ESCRIBA_AQUI;

            archivos = new Archivos.Texto(frmHistorial.ARCHIVO_HISTORIAL);
        }

        #region "Escriba aquí..."
        private void txtUrl_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.IBeam; //Without this the mouse pointer shows busy
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtUrl.Text.Equals(frmWebBrowser.ESCRIBA_AQUI) == true)
            {
                this.txtUrl.Text = "";
                this.txtUrl.ForeColor = Color.Black;
            }
        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtUrl.Text.Equals(null) == true || this.txtUrl.Text.Equals("") == true)
            {
                this.txtUrl.Text = frmWebBrowser.ESCRIBA_AQUI;
                this.txtUrl.ForeColor = Color.Gray;
            }
        }

        private void txtUrl_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtUrl.SelectAll();
        }
        #endregion

        delegate void ProgresoDescargaCallback(int progreso);
        private void ProgresoDescarga(int progreso)
        {
            if (statusStrip.InvokeRequired)
            {
                ProgresoDescargaCallback d = new ProgresoDescargaCallback(ProgresoDescarga);
                this.Invoke(d, new object[] { progreso });
            }
            else
            {
                tspbProgreso.Value = progreso;
            }
        }
        delegate void FinDescargaCallback(string html);
        private void FinDescarga(string html)
        {
            if (rtxtHtmlCode.InvokeRequired)
            {
                FinDescargaCallback d = new FinDescargaCallback(FinDescarga);
                this.Invoke(d, new object[] { html });
            }
            else
            {
                rtxtHtmlCode.Text = html;
            }
        }


 

        void descargador_progresoDescarga(int progreso)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Funcion hecha por VS al hacer doble click en ->. Esto debe ser el "search" del navegador.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIr_Click(object sender, EventArgs e)
        {
            // Analizo la url ingresada. Si no comienza con http://, lo fuerzo.
            if (!this.txtUrl.Text.Contains("http://"))
            {
                this.txtUrl.Text = "http://" + this.txtUrl.Text;
            }

            // Si no tiene extension tira 

            try
            {
                // Comienzo declarando una variable "Uri" y creo un Descargador.
                Uri link = new Uri(this.txtUrl.Text);
                Descargador descargador = new Descargador(link);
                // Invovo al evento publico progresoDescarga y le paso el proceso. Luego al evento publico finDescarga.
                descargador.progresoDescarga += this.ProgresoDescarga;
                descargador.finDescarga += this.FinDescarga;
                // Creo un hilo, pasandole referencia del metodo "public void IniciarDescarga()"
                Thread hilo = new Thread(descargador.IniciarDescarga);
                hilo.Start();
                // Guardo la URL en el archivo.
                archivos.guardar(this.txtUrl.Text);

            }
            catch (Exception exc)
            {
                this.rtxtHtmlCode.Text = exc.Message;
            }
        }

        /// <summary>
        /// Funcion hecha por VS al hacer doble click en "Mostrar todo el historial". Esto debe mostrar el historial.....
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mostrarTodoElHistorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistorial formHistorial = new frmHistorial();
            formHistorial.ShowDialog();
        }



    }
}
