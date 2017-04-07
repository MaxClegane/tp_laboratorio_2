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
    class Calculadora
    {
        /// <summary>
        /// Metodo para ejecutar la operacion. Se utilizo switch para realizar las operaciones de acuerdo al valor del operador.
        /// Se agrego la validacion por si el usario quiere dividir por 0.
        /// </summary>
        /// <param name="numero1">Hace referencia al primer operando (txtNumero1)</param>
        /// <param name="numero2">Hace referencia al segundo operando (txtNumero2)</param>
        /// <param name="operador">Operador ingresado ya validado por validarOpera(string)</param>
        /// <returns>Devuelve el resultado de la opcion con tipo double</returns>
        public static double operar(Numero numero1, Numero numero2, string operador)
        {
            double retorno = 0;
            switch (operador)
            {
                case "-":
                    retorno = numero1.getNumero() - numero2.getNumero();
                    break;
                case "/":
                    if (numero2.getNumero() == 0)
                    { MessageBox.Show("Esta intentando dividir por cero, lo cual no es posible. \n Pulse aceptar para continuar!"); }
                    else { retorno = numero1.getNumero() / numero2.getNumero(); }
                    break;
                case "*":
                    retorno = numero1.getNumero() * numero2.getNumero();
                    break;
            default:
                    //ACLARACION: Se utiliza el default para la suma, ya que el mismo se utiliza si el operador es + o invalido.
                    retorno = numero1.getNumero() + numero2.getNumero();
                    break;
            }
            return retorno;
        }

        /// <summary>
        /// Metodo para validar el operador ingresado por el usuario. El valor de retorno se inicializa como 
        /// "+", este solo se actualiza al operador ingresado si el mismo es valido (*, / o -).
        /// </summary>
        /// <param name="operador">Hace referencia al string ingresado en cmbOperacion</param>
        /// <returns>Retorna el operador validado tipo string. En caso de haberse ingresado un operador invalido, devuelve "+" y emite un alerta</returns>
        public static string validarOperador(string operador)
        {
            string retorno="+";

            if (operador == "+" || operador == "*" || operador == "-" || operador == "/")
                retorno = operador;
            else
                MessageBox.Show("El operando ingresado es incorrecto, se reemplazara por + .\n Pulse aceptar para continuar!");

            return retorno;
        }
    }
}
