using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{

    [TestClass]
    public class ValidadorMedicamentoTest
    {
        [TestMethod]
        public void Nome_do_medicamento_deve_ser_obrigatorio()
        {
            Fornecedor f1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");

            Medicamento m1 = new Medicamento("", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento(null, "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado1 = validador.Validate(m1);
            var resultado2 = validador.Validate(m2);
            var resultado3 = validador.Validate(m3);

            Assert.AreEqual("O campo 'Nome do medicamento' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Nome do medicamento' é obrigatório!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);

        }

        [TestMethod]
        public void Nome_do_medicamento_deve_ter_cinco_caracteres_no_minimo()
        {
            Fornecedor f1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");

            Medicamento m1 = new Medicamento("XX", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento("ZXC", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado1 = validador.Validate(m1);
            var resultado2 = validador.Validate(m2);
            var resultado3 = validador.Validate(m3);

            Assert.AreEqual("O campo 'Nome do medicamento' deve ter no mínimo 5 caracteres!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Nome do medicamento' deve ter no mínimo 5 caracteres!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Descricao_do_medicamento_deve_ser_obrigatorio()
        {
            Fornecedor f1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");

            Medicamento m1 = new Medicamento("Aspirina", "", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento("Paracetamol", null, "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado1 = validador.Validate(m1);
            var resultado2 = validador.Validate(m2);
            var resultado3 = validador.Validate(m3);

            Assert.AreEqual("O campo 'Descrição do medicamento' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Descrição do medicamento' é obrigatório!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Descricao_do_medicamento_deve_ter_cinco_caracteres_no_minimo()
        {
            Fornecedor f1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");

            Medicamento m1 = new Medicamento("Aspirina", "An", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento("Paracetamol", "Anti", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado1 = validador.Validate(m1);
            var resultado2 = validador.Validate(m2);
            var resultado3 = validador.Validate(m3);

            Assert.AreEqual("O campo 'Descrição do medicamento' deve ter no mínimo 5 caracteres!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Descrição do medicamento' deve ter no mínimo 5 caracteres!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Lote_do_medicamento_deve_ser_obrigatorio()
        {
            Fornecedor f1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");

            Medicamento m1 = new Medicamento("Aspirina", "Analgésico", "", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento("Paracetamol", "Anti-inflamatório", null, new DateTime(2050, 12, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado1 = validador.Validate(m1);
            var resultado2 = validador.Validate(m2);
            var resultado3 = validador.Validate(m3);

            Assert.AreEqual("O campo 'Lote do medicamento' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Lote do medicamento' é obrigatório!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Lote_do_medicamento_deve_ter_tres_caracteres_no_minimo()
        {
            Fornecedor f1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");

            Medicamento m1 = new Medicamento("Aspirina", "Analgésico", "a", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento("Paracetamol", "Anti-inflamatório", "ab", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado1 = validador.Validate(m1);
            var resultado2 = validador.Validate(m2);
            var resultado3 = validador.Validate(m3);

            Assert.AreEqual("O campo 'Lote do medicamento' deve ter no mínimo 3 caracteres!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Lote do medicamento' deve ter no mínimo 3 caracteres!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Validade_do_medicamento_deve_ser_maior_que_a_data_de_criacao()
        {
            Fornecedor f1 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");

            Medicamento m1 = new Medicamento("Aspirina", "Analgésico", "abc-123", new DateTime(2020, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento("Paracetamol", "Anti-inflamatório", "abc-123", new DateTime(2022, 06, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2022, 12, 25), f1, 100);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado1 = validador.Validate(m1);
            var resultado2 = validador.Validate(m2);
            var resultado3 = validador.Validate(m3);

            Assert.AreEqual("O campo 'Validade do medicamento' deve conter uma data váliada!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Validade do medicamento' deve conter uma data váliada!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

        [TestMethod]
        public void Fornecedor_do_medicamento_deve_ser_obrigatorio()
        {
            Fornecedor f1 = null;
            Fornecedor f2 = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");
            Medicamento m1 = new Medicamento("Aspirina", "Analgésico", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento("Paracetamol", "Anti-inflamatório", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f2, 100);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado1 = validador.Validate(m1);
            var resultado2 = validador.Validate(m2);
            var resultado3 = validador.Validate(m3);

            Assert.AreEqual("O campo 'Fornecedor do medicamento' é obrigatório!", resultado1.Errors[0].ErrorMessage);
            Assert.AreEqual("O campo 'Fornecedor do medicamento' é obrigatório!", resultado2.Errors[0].ErrorMessage);
            Assert.AreEqual(0, resultado3.Errors.Count);
        }

    }
}
