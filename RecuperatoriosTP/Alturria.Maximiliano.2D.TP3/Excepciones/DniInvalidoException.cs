using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class DniInvalidoException : Exception
    {
        private static string _mensajeBase = "El DNI ingresado no es un número válido.";

        public DniInvalidoException() { }

        public DniInvalidoException(Exception e)
            : base(_mensajeBase, e)
        { }

        public DniInvalidoException(string message): 
            base(message)
        {}

        public DniInvalidoException(string message, Exception e): 
            base(_mensajeBase + message,e)
        { }
    }
}
