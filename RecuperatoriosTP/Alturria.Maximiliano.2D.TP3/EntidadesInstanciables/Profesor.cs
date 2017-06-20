using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
using System.Xml;
using System.Xml.Serialization;

namespace EntidadesInstanciables
{
    [Serializable]
    public class Profesor:Universitario
    {
        private Queue<Universidad.EClases> _clasesDelDia;
        private static Random _random;

        #region Constructores
        /// <summary>
        /// Se inicializará a Random sólo en un constructor
        /// </summary>
        static Profesor()
        {
            _random = new Random();
        }

        public Profesor() { }

        /// <summary>
        /// En el constructor de instancia se inicializará ClasesDelDia y se asignarán dos clases al azar al Profesor mediante el método randomClases. Las dos clases pueden o no ser la misma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Profesor(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this._clasesDelDia = new Queue<Universidad.EClases>();
            _randomClases();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo que asigna 2 EClases al queue
        /// </summary>
        private void _randomClases()
        {
            this._clasesDelDia.Enqueue((Universidad.EClases)_random.Next(0, 3));
            this._clasesDelDia.Enqueue((Universidad.EClases)_random.Next(0, 3));
        }

        /// <summary>
        /// ParticiparEnClase retornará la cadena "CLASES DEL DÍA " junto al nombre de la clases que da.
        /// </summary>
        /// <returns></returns>
        protected override string ParticiparEnClase()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CLASES DEL DIA:");
            foreach (Universidad.EClases item in this._clasesDelDia)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.MostrarDatos());
            sb.AppendLine(this.ParticiparEnClase());
            return sb.ToString();
        }

        public override string ToString()
        {
            return MostrarDatos();
        }
        #endregion

        #region Sobrecargas
        /// <summary>
        /// Un Profesor será igual a un EClase si da esa clase.
        /// o sea, si en la instancia de p, la clase esta en su quueue
        /// </summary>
        /// <param name="clase"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool operator ==(Universidad.EClases clase, Profesor p)
        {
            bool daLaClase = false;
            foreach (Universidad.EClases item in p._clasesDelDia)
            {
                if (item == clase)
                {
                    daLaClase = true;
                    break;
                }
            }
            return daLaClase;
        }

        public static bool operator !=(Universidad.EClases clase, Profesor p)
        {
            return !(clase == p);
        }
        #endregion
    }
}
