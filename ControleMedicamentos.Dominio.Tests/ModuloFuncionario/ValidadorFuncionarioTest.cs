using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTest
    {
        [TestMethod]
        public void Nome_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var f1 = new Funcionario("", "matheus_user", "m@theuSKl90");
            var f2 = new Funcionario(null, "matheus_user", "m@theuSKl90");
            var f3 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);
            var resultado3 = validador.Validate(f3);

            //assert
            Assert.AreEqual("O campo 'Nome do funcionário' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Nome do funcionário' é obrigatório!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Nome_do_funcionario_deve_ter_quatro_caracteres_no_minimo()
        {
            //arrange
            var f1 = new Funcionario("zé", "matheus_user", "m@theuSKl90");
            var f2 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Nome do funcionário' deve ter no mínimo 4 caracteres!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
        }

        [TestMethod]
        public void Login_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var f1 = new Funcionario("Matheus", "", "m@theuSKl90");
            var f2 = new Funcionario("Matheus", null, "m@theuSKl90");
            var f3 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);
            var resultado3 = validador.Validate(f3);

            //assert
            Assert.AreEqual("O campo 'Login do funcionário' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Login do funcionário' é obrigatório!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Login_do_funcionario_deve_ter_cinco_caracteres_no_minimo()
        {
            //arrange
            Funcionario f1 = new Funcionario("Matheus", "mat", "m@theuSKl90");
            Funcionario f2 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Login do funcionário' deve ter no mínimo 5 caracteres!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var f1 = new Funcionario("Matheus", "matheus_user", "");
            var f2 = new Funcionario("Matheus", "matheus_user", null);
            var f3 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);
            var resultado3 = validador.Validate(f3);

            //assert
            Assert.AreEqual("O campo 'Senha do funcionário' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Senha do funcionário' é obrigatório!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ter_cinco_caracteres_no_minimo()
        {
            //arrange
            Funcionario f1 = new Funcionario("Matheus", "matheus_user", "m@");
            Funcionario f2 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Senha do funcionário' deve ter no mínimo 5 caracteres!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
        }
    }
}
