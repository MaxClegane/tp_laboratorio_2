using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excepciones;

namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        /// <summary>
        /// guarda el string que recibo en el parametros datos.
        /// </summary>
        /// <param name="archivo">Parametro donde recibe el path</param>
        /// <param name="datos">Parametro con datos a guardar</param>
        /// <returns></returns>
        public bool guardar(string archivo, string datos)
        {
            bool retorno = false;
            try
            {
                StreamWriter escritura = new StreamWriter(archivo, false, Encoding.UTF8);
                escritura.Write(datos);
                escritura.Close();
                retorno = true;
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
            return retorno;
        }

        public bool leer(string archivo, out string datos)
        {
            bool retorno = false;
            try
            {
                StreamReader lectura = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Jornada.txt");
                datos = lectura.ReadToEnd();
                retorno = true;
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
            return retorno;
        }

    }
}