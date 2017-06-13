using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntidadesAbstractas;
using EntidadesInstanciables;
using Excepciones;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExcepcionDNIInvalido()
        {
            //Se validara que el DNI sea un numero valido, caso contrario sera DniInvalidoException
            string dni = "*sf/dfffdfsf4535345";
            try
            {
                Alumno alu = new Alumno(1, "Maxi", "Alturria", dni, Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio);
                Assert.Fail("Sin excepción para DNI inválido: " + dni);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(DniInvalidoException));
            }
        }

        [TestMethod]
        public void ExcepcionDNIFueraDeRango()
        {
            //Se validara que si DNI esta fuera del rango de extranjeros, sera NacionalidadInvalidaException
            string dni = "35345";
            try
            {
                Alumno alu = new Alumno(1, "Maxi", "Alturria", dni, Persona.ENacionalidad.Extranjero, Universidad.EClases.Laboratorio);
                Assert.Fail("Sin excepción para DNI inválido: " + dni);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(NacionalidadInvalidaException));
            }
        }

        [TestMethod]
        public void ExcepcionNulo()
        {
            //Instancio instructor y jornada. Analizo que sus atributos sean no nulos.
            Profesor i = new Profesor(1, "pepe", "picapiedra", "3123123", Persona.ENacionalidad.Argentino);
            Jornada j = new Jornada(Universidad.EClases.Laboratorio, i);

            Assert.IsNotNull(i.Nacionalidad);
            Assert.IsNotNull(i.Nombre);
            Assert.IsNotNull(i.Apellido);
            Assert.IsNotNull(i.DNI);

            Assert.IsNotNull(j.Alumnos);
            Assert.IsNotNull(j.Clase);
            Assert.IsNotNull(j.Instructor);
        }
    }

}