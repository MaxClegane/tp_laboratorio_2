using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
using Excepciones;
using Archivos;
using System.Xml;
using System.Xml.Serialization;

namespace EntidadesInstanciables
{
    [Serializable]
    public class Jornada
    {
        #region Atributos
        private List<Alumno> _alumnos;
        private Universidad.EClases _clase;
        private Profesor _instructor;
        #endregion

        #region Propiedades
        public List<Alumno> Alumnos
        {
            get { return this._alumnos; }
            set { this._alumnos = value; }
        }
        
        public Universidad.EClases Clase
        {
            get { return this._clase; }
            set { this._clase = value; }
        }
        public Profesor Instructor
        {
            get { return this._instructor; }
            set { this._instructor = value; }
        }
        #endregion 

        #region Constructores
        /// <summary>
        /// Se inicializará la lista de alumnos en el constructor por defecto.
        /// </summary>
        private Jornada()
        {
            this._alumnos = new List<Alumno>();
        }

        public Jornada(Universidad.EClases clase, Profesor instructor) :this()
        {
            this.Instructor = instructor;
            this.Clase = clase;
        }
        #endregion

        #region Sobrecargas
        /// <summary>
        /// Sobrecarga que permite Agregar Alumnos a la clase por medio del operador +, validando que no estén previamente cargados.
        /// </summary>
        /// <param name="j">Jornada que contiene la lista de alumnos</param>
        /// <param name="a">alumnos a agregar</param>
        /// <returns>Retorna la jornada con el alumno agregado, si este no pertenecia a la misma</returns>
        public static Jornada operator +(Jornada j, Alumno a)
        { 
            bool seEncuentra=false;

            for (int i = 0; i < j._alumnos.Count; i++)
            {
                if (j._alumnos[i] == a)
                {
                    seEncuentra = true;
                    break;
                }
            }
            if (!seEncuentra)
            { j._alumnos.Add(a); }

            return j;
        }

        /// <summary>
        /// Una Jornada será igual a un Alumno si el mismo participa de la clase.
        /// O sea, si el alumno que se recibe ocmo parametros está en la lista
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator ==(Jornada j, Alumno a)
        {
            bool seEncuentra = false;

            for (int i = 0; i < j._alumnos.Count; i++)
            {
                if (j._alumnos[i] == a)
                {
                    seEncuentra = true;
                    break;
                }
            }
            return seEncuentra;
        }

        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j == a);
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("JORNADA:");
            sb.AppendLine("CLASE DE " + this._clase.ToString() + " POR " + this._instructor.ToString());
            sb.AppendLine("NACIONALIDAD: " + this._instructor.Nacionalidad.ToString());
            sb.AppendLine();
            sb.AppendLine("ALUMNOS: ");
            foreach (Alumno item in this._alumnos)
            {
                sb.AppendLine(item.ToString());
            }
            sb.AppendLine("<--------------------------------------------------->");

            return sb.ToString();
        }
        #endregion

        #region Salida a Texto
        public static bool Guardar(Jornada jornada)
        {
            Texto t = new Texto();
            return t.guardar(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Jornada.txt", jornada.ToString());
        }
        #endregion
    }
}
