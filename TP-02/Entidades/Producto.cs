using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_2017
{
    /// <summary>
    /// La clase Producto será abstracta, evitando que se instancien elementos de este tipo.
    /// </summary>
    public abstract class Producto // Se cambia sealed por abstract
    {
        public enum EMarca // Se hace publica
        {
            Serenisima, Campagnola, Arcor, Ilolay, Sancor, Pepsico
        }
        EMarca _marca;
        string _codigoDeBarras;
        ConsoleColor _colorPrimarioEmpaque;

        /// <summary>
        /// Se agrega constructor
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="marca"></param>
        /// <param name="color"></param>
        protected Producto(string codigo, EMarca marca, ConsoleColor color)
        {
            this._codigoDeBarras = codigo;
            this._colorPrimarioEmpaque = color;
            this._marca = marca;
        }

        /// <summary>
        /// ReadOnly: Retornará la cantidad de ruedas del vehículo
        /// </summary>
        public abstract short CantidadCalorias { get; } // Se hace publica ya que al ser abstract no pued ser privada y se elimino set ya que su summary dice ser "readonly"

        /// <summary>
        /// Publica todos los datos del Producto.
        /// </summary>
        /// <returns></returns>
        public virtual string Mostrar() // Necesito acceder a ella , la hago public y la hago virtual para que las hijas usen override
        {
            return (string)this; // Se agrega (string) por ser explicito
        }

        public static explicit operator string(Producto p) // Debe ser publico
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("CODIGO DE BARRAS: {0}\r\n", p._codigoDeBarras);// Se cambia a AppendFormat
            sb.AppendFormat("MARCA          : {0}\r\n", p._marca.ToString());// Se cambia a AppendFormat
            sb.AppendFormat("COLOR EMPAQUE  : {0}\r\n", p._colorPrimarioEmpaque.ToString());// Se cambia a AppendFormat
            sb.AppendLine("---------------------");

            return sb.ToString(); // Se agrega ToString() ya que el metodo devuelve string
        }

        /// <summary>
        /// Dos productos son iguales si comparten el mismo código de barras
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator ==(Producto v1, Producto v2)
        {
            return (v1._codigoDeBarras == v2._codigoDeBarras);
        }
        /// <summary>
        /// Dos productos son distintos si su código de barras es distinto
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator !=(Producto v1, Producto v2)
        {
            //return (v1._codigoDeBarras == v2._codigoDeBarras); Lo comento y lo hago abajo.
            return !(v1==v2);
        }
    }
}
