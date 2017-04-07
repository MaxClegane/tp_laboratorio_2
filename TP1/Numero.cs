using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP01
{
    class Numero
    {
        #region Atributos
        private double _numero;

        /// <summary>
        /// Contructor sin parametro, el mismo coloca a _numero en 0.     
        /// </summary>
        /// <param name="numero"></param>

        public Numero()
        {
            this._numero = 0;
        }
        #endregion

        #region Constructores
        /// <summary>
        /// Contructor con parametro double.
        /// </summary>
        /// <param name="numero"></param>
        public Numero(double numero)
        {
            this._numero = numero;
        }

        /// <summary>
        /// Contructor con parametro string.
        /// </summary>
        /// <param name="numero"></param>
        public Numero(string numero)
        {
            setNumero(numero);
        }

        /// <summary>
        /// Metodo para que los metodos publicos accedan a _numero.
        /// </summary>
        /// <returns>Retorna el valor seteado en _numero</returns>
        public double getNumero()
        {
            return this._numero;
        }
        #endregion

        #region Metodo
        /// <summary>
        /// Metodo llamado por los constructores para setear _numero. Este metodo llama a validarNumero(string).
        /// </summary>
        /// <param name="numero">Hace referencia al valor ingresado en el textbox.</param>
        private void setNumero(string numero)
        {
           
            this._numero = validarNumero(numero);
        }

        /// <summary>
        /// Metodo para validar el numero. Este solo puede ser llamado desde setNumero(string)
        /// La validacion realizada es que toda la cadena de string recibida sea numerica.
        /// </summary>
        /// <param name="numeroString">Hace referencia al valor ingresado en el textbox.</param>
        /// <returns>Si numeroString es un valor numerico lo devuelve, sino devuelve 0 y emite un alerta.</returns>
        private static double validarNumero(string numeroString)
        {
            double retorno;

            if (!(double.TryParse(numeroString, out retorno)))
            {
                retorno = 0;
                MessageBox.Show("Se ingreso un numero invalido. Se modificara a cero.\n Pulse aceptar para continuar!");
            }
            return retorno;
        }
        #endregion

    }
}
