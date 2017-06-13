using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using System.Xml;
using System.Xml.Serialization;
using Archivos;
using EntidadesAbstractas;

namespace EntidadesInstanciables
{
    [Serializable]
    public class Universidad
    {
        public enum EClases
	    {
	        Programacion,
            Laboratorio,
            Legislacion,
            SPD
	    }

        private List<Alumno> _alumnos;
        private List<Jornada> _jornada;
        private List<Profesor> _profesores;

        #region Constructor
        public Universidad()
        {
            this._alumnos = new List<Alumno>();
            this._profesores = new List<Profesor>();
            this._jornada = new List<Jornada>();
        }
        #endregion

        #region Propiedad
        public Jornada this[int i]
        {
            get
            {
                return this._jornada[i];
            }
        }
        public List<Alumno> Alumnos
        {
            get { return this._alumnos; }
            set { }
        }

        public List<Profesor> Instructores
        {
            get { return this._profesores; }
            set { }
        }

        public List<Jornada> Jornadas
        {
            get { return this._jornada; }
            set { }
        }
        #endregion

        #region Sobrecargas
        /// <summary>
        /// Un Universidad será igual a un Alumno si el mismo está inscripto en él.
        /// </summary>
        public static bool operator ==(Universidad g, Alumno a)
        {
            bool retorno = false;
            if (!object.Equals(g, null) && !object.Equals(a, null))
            {
                foreach (Alumno item in g._alumnos)
                {
                    if (item == a)
                        retorno = true;
                }
            }
            return retorno;
        }

        public static bool operator !=(Universidad g, Alumno a)
        {
            return !(g == a);
        }

        /// <summary>
        /// Un Universidad será igual a un Profesor si el mismo está dando clases en él.
        /// </summary>
        public static bool operator ==(Universidad g, Profesor i)
        {
            bool retorno = false;
            if (!object.Equals(g, null) && !object.Equals(i, null))
            {
                for (int j = 0; j < g._profesores.Count; j++)
                {
                    if (g._profesores[j] == i)
                        retorno = true;
                }
            }
            return retorno;
        }

        public static bool operator !=(Universidad g, Profesor i)
        {
            return !(g == i);
        }

        /// <summary>
        /// Se agregarán Alumnos mediante el operador +, validando que no estén previamente
        /// cargados.
        /// </summary>
        public static Universidad operator +(Universidad g, Alumno a)
        {
            foreach (var item in g._alumnos)
            {
                if (g == a)
                {
                    throw new AlumnoRepetidoException();
                }
            }

            g._alumnos.Add(a);
            return g;
        }

        /// <summary>
        /// Se agregarán profesores mediante el operador +, validando que no estén previamente
        /// cargados.
        /// </summary>
        public static Universidad operator +(Universidad g, Profesor i)
        {
            foreach (var item in g._profesores)
            {
                if (item == i)
                {
                    throw new AlumnoRepetidoException();
                }
            }

            g._profesores.Add(i);
            return g;
        }

        /// <summary>
        /// Al agregar una clase a un Universidad se deberá generar y agregar una nueva Jornada indicando la clase, un Profesor que pueda darla (según su atributo ClasesDelDia) y la lista de alumnos que la toman (todos los que coincidan en su campo ClaseQueToma).
        /// </summary>
        public static Universidad operator +(Universidad g, Universidad.EClases clase)
        {
            Profesor i = null;
            bool hayProfesor = false;
            foreach (Profesor prof in g._profesores)
            {
                if (clase == prof)
                {
                    i = prof;
                    hayProfesor = true;
                }
            }
            if (hayProfesor)
            {
                Jornada jornada = new Jornada(clase, i);
                foreach (Alumno alu in g._alumnos)
                {
                    if (alu == clase)
                        jornada += alu;
                }

                g._jornada.Add(jornada);
            }
            else
            {
                throw new SinProfesorException();
            }

            return g;
        }

        #endregion

        #region MÉTODOS
        /// <summary>
        /// ToString hará públicos los datos del Alumno.
        /// Llama a MostrarDatos()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Universidad.MostrarDatos(this);
        }


        static string MostrarDatos(Universidad uni)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < uni._jornada.Count; i++)
            {
                sb.AppendLine(uni[i].ToString());
            }
            return sb.ToString();
        }
        #endregion

        #region Serializacion

        public static bool Guardar(Universidad uni)
        {
            Xml<Universidad> xmlFile = new Xml<Universidad>();
            return xmlFile.guardar(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\universidad.xml", uni);
        }

        /// <summary>
        /// Recupera los datos del Gimnasio de un archivo xml
        /// </summary>
        /// <returns>Gimnasio con los datos recuperados</returns>
        public static Universidad Leer()
        {
            try
            {
                Universidad g = new Universidad();
                Xml<Universidad> xml = new Xml<Universidad>();
                xml.leer(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\universidad.Xml", out g);
                return g;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion
    }
}
