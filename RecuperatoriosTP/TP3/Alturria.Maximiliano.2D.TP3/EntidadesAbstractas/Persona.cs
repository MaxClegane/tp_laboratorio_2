using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Excepciones;
using System.Xml;
using System.Xml.Serialization;


namespace EntidadesAbstractas
{
    [Serializable]
    abstract public class Persona
    {
        #region Enumerado ENacionalidad
        public enum ENacionalidad
        {
            Argentino,
            Extranjero
        }
        #endregion 
        
        #region Atributos
        private string _nombre;
        private string _apellido;
        private ENacionalidad _nacionalidad;
        private int _dni;
        #endregion

        #region Propiedades
        public int DNI
        {
            get { return this._dni; }
            set { this._dni = ValidarDNI(this.Nacionalidad,value); }
        }

        public string Apellido
        {
            get { return this._apellido; }
            set { this._apellido = ValidarNombreApellido(value); }
        }

        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = ValidarNombreApellido(value); }
        }

        public ENacionalidad Nacionalidad
        {
            get { return this._nacionalidad; }
            set { this._nacionalidad = value; }
        }

        public string StringToDNI
        {
            set { this._dni =  ValidarDNI(this.Nacionalidad,value); }
        }
        #endregion

        #region Constructores
        public Persona() { }

        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {
            this.Nacionalidad = nacionalidad;
            this.Apellido = apellido;
            this.Nombre = nombre;
        }

        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad)
            : this(nombre, apellido, nacionalidad)
        {
            this.DNI = dni;
        }

        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : this(nombre, apellido, nacionalidad)
        {
            this.StringToDNI = dni;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo para validar que el DNI este dentro de los rangos dependiendo de la nacionalidad.
        /// Si es Argentino: su DNI entre 1-89999999
        /// Si es Extranjero: su DNI entre 90000000 - 99999999
        /// </summary>
        /// <param name="nacionalidad">Nacionalidad a validar</param>
        /// <param name="dato">Es el DNI a validar</param>
        /// <returns>Retorna el dato si pasa las validaciones. Caso contrario sale por exception</returns>
        private int ValidarDNI(ENacionalidad nacionalidad, int dato)
        {
            switch (nacionalidad)
            {
                case ENacionalidad.Argentino:
                    {
                        if (dato >= 1 && dato <= 89999999)
                            return dato;
                        else
                            throw new NacionalidadInvalidaException();
                    }

                case ENacionalidad.Extranjero:
                    {
                        if (dato >= 90000000 && dato <= 99999999)
                            return dato;
                        else
                            throw new NacionalidadInvalidaException();
                    }
                default:
                    throw new DniInvalidoException();
            }
         }
            


        /// <summary>
        /// Este metodo validar que el dato sea numerico con el tryParse. Si es numerico, lo valido con el otro metodo ValidarDNI que recibe un entero
        /// </summary>
        /// <param name="nacionalidad">Nacionalidad a validar</param>
        /// <param name="dato">DNI string a validar.</param>
        /// <returns>Retorna el dato si pasa las validaciones. Caso contrario sale por exception. </returns>
        private int ValidarDNI(ENacionalidad nacionalidad, string dato)
        {
            int datoParseado, retorno;
            if (Int32.TryParse(dato, out datoParseado))
            {
                retorno = ValidarDNI(nacionalidad, datoParseado);
            }
            else
            {
                throw new DniInvalidoException("El DNI informado no es numerico");
            }
            return retorno;
        }

        /// <summary>
        /// Validará que los nombres sean cadenas con caracteres válidos para nombres y que no sean null. Caso contrario, no se cargará y retornara "".
        /// </summary>
        /// <param name="dato">Es el dato (string) a validar.</param>
        /// <returns>Devuelve el dato si pasa las validaciones. Caso contrario sale por exception.</returns>
        private string ValidarNombreApellido(string dato)
        {
            Regex reg = new Regex("^[A-Za-z]+$");
            if (!Equals(dato,null) && reg.IsMatch(dato))
                return dato;
            else
                return "";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("NOMBRE COMPLETO: " + this.Apellido + ", " + this.Nombre);
            sb.AppendLine("NACIONALIDAD: " + this.Nacionalidad.ToString());
            return sb.ToString();
        }
        #endregion
    }
}
