using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{

    [TestClass]
    public class ValidadorPacienteTest
    {
        public ValidadorPacienteTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_paciente_deve_ser_obrigatorio()
        {
            //arrange
            var p1 = new Paciente("", "111111111111111");
            //var p2 = new Paciente();
            //p2.Nome = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado1 = validador.Validate(p1);
            //var resultado2 = validador.Validate(p2);

            //assert
            Assert.AreEqual("O campo 'Nome do paciente' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            //Assert.AreEqual("O campo 'Nome do paciente' é obrigatório!", resultado2.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_do_paciente_deve_ter_cinco_caracteres_no_minimo()
        {
            //arrange
            var p1 = new Paciente("Kaka", "111111111111111");

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado1 = validador.Validate(p1);

            //assert
            Assert.AreEqual("O campo 'Nome do paciente' deve ter no mínimo 5 caracteres!", resultado1.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_do_paciente_nao_deve_ter_caracteres_especiais_ou_numeros()
        {
            //arrange
           // var p1 = new Paciente("M@theus", "111111111111111");
            var p2 = new Paciente("Matheus123", "111111111111111");

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            //var resultado1 = validador.Validate(p1);
            var resultado2 = validador.Validate(p2);

            //assert
            //Assert.AreEqual("O campo 'Nome do paciente' não deve conter números ou caracteres especiais!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Nome do paciente' não deve conter números ou caracteres especiais!", resultado2.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void Cartao_SUS_deve_ser_obrigatorio()
        {
            //arrange
            var p1 = new Paciente("Matheus", "");
            //var p2 = new Paciente("Matheus", null);

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado1 = validador.Validate(p1);
            //var resultado2 = validador.Validate(p2);

            //assert
            Assert.AreEqual("O campo 'Cartão SUS' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            //Assert.AreEqual("O campo 'Cartão SUS' é obrigatório", resultado2.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cartao_SUS_deve_ter_somente_15_digitos()
        {
            //arrange
            var p1 = new Paciente("Matheus", "123");
            var p2 = new Paciente("Matheus", "111111111111111");
            var p3 = new Paciente("Matheus", "1111111111111112343");

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado1 = validador.Validate(p1);
            var resultado2 = validador.Validate(p2);
            var resultado3 = validador.Validate(p3);

            //assert
            Assert.AreEqual("O campo 'Cartão SUS' deve ter somente 15 dígitos!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
            Assert.AreEqual("O campo 'Cartão SUS' deve ter somente 15 dígitos!", resultado3.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cartao_SUS_deve_ter_somente_numeros()
        {
            //arrange
            var p1 = new Paciente("Matheus", "11111111111111a");

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado1 = validador.Validate(p1);

            //assert
            Assert.AreEqual("O campo 'Cartão SUS' deve conter somente números!", resultado1.Errors[0].ErrorMessage);
        }
    }
}
