using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EntidadesAbstractas
{
    [Serializable]
    public abstract class Universitario : Persona
    {
        #region Atributo
        private int _legajo;
        #endregion

        #region Constructor
        public Universitario() { }

        public Universitario(int legajo, string nombre, string apellido, string dni, ENacionalidad nacionalidad) :base(nombre, apellido,dni,nacionalidad)
        {
            this._legajo = legajo;
        }
        #endregion

        #region Metodos
        protected virtual string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("LEGAJO NUMERO: " + (this._legajo).ToString());
            return sb.ToString();
        }

        protected abstract string ParticiparEnClase();
        #endregion

        #region Sobrecargas
        public static bool operator ==(Universitario pg1, Universitario pg2)
        {
            bool retorno = false;
            if (pg1.GetType() == pg2.GetType())
            {
                if (pg1.DNI == pg2.DNI || pg1._legajo == pg2._legajo)
                    retorno = true;
            }
            return retorno;
        }

        public static bool operator !=(Universitario pg1, Universitario pg2)
        {
            return !(pg1 == pg2);
        }
        #endregion
    }
}
