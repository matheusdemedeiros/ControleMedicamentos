using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTest
    {

        [TestMethod]
        public void Nome_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f1 = new Fornecedor("", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");
            var f2 = new Fornecedor(null, "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Nome do fornecedor' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Nome do fornecedor' é obrigatório!", resultado2.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_do_fornecedor_deve_ter_tres_caracteres_no_minimo()
        {
            //arrange
            Fornecedor novoFornecedor = new Fornecedor("Di", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(novoFornecedor);

            //assert
            Assert.AreEqual("O campo 'Nome do fornecedor' deve ter no mínimo 3 caracteres!", resultado1.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f1 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "", "SC");
            var f2 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", null, "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Cidade do fornecedor' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Cidade do fornecedor' é obrigatório!", resultado2.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_do_fornecedor_deve_ter_tres_caracteres_no_minimo()
        {
            //arrange
            Fornecedor novoFornecedor = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "La", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(novoFornecedor);

            //assert
            Assert.AreEqual("O campo 'Cidade do fornecedor' deve ter no mínimo 3 caracteres!", resultado1.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f1 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "");
            var f2 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", null);

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Estado do fornecedor' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Estado do fornecedor' é obrigatório!", resultado2.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_do_fornecedor_deve_ter_exatamente_dois_caracteres()
        {
            //arrange
            Fornecedor f1 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SCSD");
            Fornecedor f2 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "S");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Estado do fornecedor' deve ter somente 2 caracteres!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Estado do fornecedor' deve ter somente 2 caracteres!", resultado2.Errors[0].ErrorMessage);
        }
        
        [TestMethod]
        public void Telefone_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f1 = new Fornecedor("Distribuidor A", "", "distribuidorA@gmail.com", "Lages", "SC");
            var f2 = new Fornecedor("Distribuidor A", null, "distribuidorA@gmail.com", "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Telefone do fornecedor' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Telefone do fornecedor' é obrigatório!", resultado2.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_do_fornecedor_deve_ser_valido()
        {
            //arrange
            Fornecedor f1 = new Fornecedor("Distribuidor A", "4999956-8765", "distribuidorA@gmail.com", "Lages", "SC");
            Fornecedor f2 = new Fornecedor("Distribuidor A", "(49) f9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Telefone do fornecedor' deve conter um telefone válido!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Telefone do fornecedor' deve conter um telefone válido!", resultado2.Errors[0].ErrorMessage);
        }
        
        [TestMethod]
        public void Email_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f1 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "", "Lages", "SC");
            var f2 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", null, "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);

            //assert
            Assert.AreEqual("O campo 'Email do fornecedor' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Email do fornecedor' é obrigatório!", resultado2.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_do_fornecedor_deve_ser_valido()
        {
            //arrange
            Fornecedor f1 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "@gmail.com", "Lages", "SC");
            Fornecedor f2 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@", "Lages", "SC");
            Fornecedor f3 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA", "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado1 = validador.Validate(f1);
            var resultado2 = validador.Validate(f2);
            var resultado3 = validador.Validate(f3);

            //assert
            Assert.AreEqual("O campo 'Email do fornecedor' deve conter um email válido!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Email do fornecedor' deve conter um email válido!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Email do fornecedor' deve conter um email válido!", resultado3.Errors[0].ErrorMessage);
        }
    }
}
