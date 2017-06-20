using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net; // Avisar del espacio de nombre
using System.ComponentModel;

namespace Hilo
{
    public class Descargador
    {
        private string html;
        private Uri direccion;

        public Descargador(Uri direccion)
        {
            this.html = "";
            this.direccion = direccion; // Agrego direccion
        }

        public void IniciarDescarga()
        {
            try
            {
                WebClient cliente = new WebClient();
                cliente.DownloadProgressChanged += WebClientDownloadProgressChanged;
                cliente.DownloadStringCompleted += WebClientDownloadCompleted;

                cliente.DownloadStringAsync(this.direccion, this.html); // Agrego parametros a la sobrecarga
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                this.direccion = new Uri("about:blank");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public delegate void ProgresoDescargaCallback(int progreso); //Declaro un delegado para el evento
        public event ProgresoDescargaCallback progresoDescarga;      //Declaro un evento, llamado igual que el delegado.
        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progresoDescarga(e.ProgressPercentage); // Invoco al evento recien creado, pasandole como parametro el porcentaje de descarga que trae DownloadProgressChangedEventArgs
        }

        public delegate void FinDescargaCallback(string html);      //Declaro un delegado para el evento
        public event FinDescargaCallback finDescarga;               //Declaro un evento, llamado igual que el delegado.
        private void WebClientDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            finDescarga(e.Result);                  // Invoco al evento recien creado, pasandole como parametro el resultado de la descarga que trae DownloadStringCompletedEventArgs
        }
    }
}
