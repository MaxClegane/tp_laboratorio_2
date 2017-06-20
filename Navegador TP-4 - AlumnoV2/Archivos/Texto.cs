using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //agrego namespace para imput-output

namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        private string _archivo; //agrego atributo

        public Texto(string archivo)
        {
            this._archivo = archivo; //Paso el valor del parametro al atributo
        }

        /// <summary>
        /// Declaro metodo para guardar los datos. Sera formato txt
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool guardar(string datos)
        {
            try
            {
                if (File.Exists(this._archivo)) // Si el archivo existe, lo sobreescribo con StreamWriter = true
                {
                    StreamWriter archivoSalida = new StreamWriter(_archivo, true);
                    archivoSalida.WriteLine(datos);
                    archivoSalida.Close();
                }
                else // Entra en el else, si el archivo no existe.
                {
                    StreamWriter archivoSalida = new StreamWriter(_archivo, false);
                    archivoSalida.WriteLine(datos);
                    archivoSalida.Close();
                }
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool leer(out List<string> datos)
        {
            List<string> datosAux = new List<string>(); // creo una lista auxiliar para luego hacer el output
            try
            {
                StreamReader ArchivoEnMemoria = new StreamReader(this._archivo); //leo el archivo tomando el directorio desde la variable.
                while (!(ArchivoEnMemoria.EndOfStream))
                {
                    // hasta no llegar al final, recorro y asigno a la lista auxiliar.
                    datosAux.Add(ArchivoEnMemoria.ReadLine());
                }
                datos = datosAux;  // Hago el output asignado la lista auxiliar, al parametro out.
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
