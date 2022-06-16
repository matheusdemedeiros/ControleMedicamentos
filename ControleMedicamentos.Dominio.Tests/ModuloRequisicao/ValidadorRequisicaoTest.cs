using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{

    [TestClass]
    public class ValidadorRequisicaoTest
    {
        [TestMethod]
        public void Medicamento_da_requisicao_deve_ser_obrigatorio()
        {
            Fornecedor fonrnecedor1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");
            Medicamento medicamento1 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), fonrnecedor1, 100);
            Paciente paciente1 = new Paciente("Matheus", "111111111111111");
            Funcionario funcionario1 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            Medicamento medicamento2 = null;

            Requisicao r1 = new Requisicao(medicamento2, paciente1, 5, DateTime.Now, funcionario1);
            Requisicao r2 = new Requisicao(medicamento1, paciente1, 5, DateTime.Now, funcionario1);

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado1 = validador.Validate(r1);
            var resultado2 = validador.Validate(r2);

            Assert.AreEqual("O campo 'Medicamento da requisição' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
        }

        [TestMethod]
        
        public void Paciente_da_requisicao_deve_ser_obrigatorio()
        {
            Fornecedor fonrnecedor1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");
            Medicamento medicamento1 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), fonrnecedor1, 100);
            Paciente paciente1 = new Paciente("Matheus", "111111111111111");
            Funcionario funcionario1 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            Paciente paciente2 = null;

            Requisicao r1 = new Requisicao(medicamento1, paciente2, 5, DateTime.Now, funcionario1);
            Requisicao r2 = new Requisicao(medicamento1, paciente1, 5, DateTime.Now, funcionario1);

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado1 = validador.Validate(r1);
            var resultado2 = validador.Validate(r2);

            Assert.AreEqual("O campo 'Paciente da requisição' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
        }
        
        [TestMethod]
        public void Funcionario_da_requisicao_deve_ser_obrigatorio()
        {
            Fornecedor fonrnecedor1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");
            Medicamento medicamento1 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), fonrnecedor1, 100);
            Paciente paciente1 = new Paciente("Matheus", "111111111111111");
            Funcionario funcionario1 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            Funcionario funcionario2 = null;

            Requisicao r1 = new Requisicao(medicamento1, paciente1, 5, DateTime.Now, funcionario2);
            Requisicao r2 = new Requisicao(medicamento1, paciente1, 5, DateTime.Now, funcionario1);

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado1 = validador.Validate(r1);
            var resultado2 = validador.Validate(r2);

            Assert.AreEqual("O campo 'Funcionário da requisição' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
        }

        [TestMethod]
        public void Data_da_requisicao_deve_ser_maior_que_a_data_de_criacao_da_requisicao()
        {
            Fornecedor fonrnecedor1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");
            Medicamento medicamento1 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), fonrnecedor1, 100);
            Paciente paciente1 = new Paciente("Matheus", "111111111111111");
            Funcionario funcionario1 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            DateTime dataInvalida = new DateTime(2020, 02, 15);

            Requisicao r1 = new Requisicao(medicamento1, paciente1, 5, dataInvalida, funcionario1);
            Requisicao r2 = new Requisicao(medicamento1, paciente1, 5, DateTime.Now, funcionario1);

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado1 = validador.Validate(r1);
            var resultado2 = validador.Validate(r2);

            Assert.AreEqual("O campo 'Data da requisição' deve conter uma data válida!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
        }

        [TestMethod]
        public void QtdMedicamentos_da_requisicao_deve_ser_maior_que_zero()
        {
            Fornecedor fonrnecedor1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");
            Medicamento medicamento1 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), fonrnecedor1, 100);
            Paciente paciente1 = new Paciente("Matheus", "111111111111111");
            Funcionario funcionario1 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            DateTime dataInvalida = new DateTime(2020, 02, 15);

            Requisicao r1 = new Requisicao(medicamento1, paciente1, 0, DateTime.Now, funcionario1);
            Requisicao r2 = new Requisicao(medicamento1, paciente1, 5, DateTime.Now, funcionario1);

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado1 = validador.Validate(r1);
            var resultado2 = validador.Validate(r2);

            Assert.AreEqual("O campo 'QTD de medicamento da requisição' deve ser no mínimo um!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado2.Errors.Count);
        }
    }
}
