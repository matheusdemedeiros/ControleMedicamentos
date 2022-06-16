using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        private Fornecedor GetFornecedor()
        {
            Fornecedor fornecedor;

            var repositorioFornecedor = new RepositorioFornecedorDB();

            fornecedor = repositorioFornecedor.SelecionarPorId(1);

            if (fornecedor == null)
            {
                fornecedor = new Fornecedor("Distribuidor X", "(49) 9 9956-8765", "distribuidorX@gmail.com", "Lages", "SC");

                repositorioFornecedor.Inserir(fornecedor);
            }

            return fornecedor;
        }

        public RepositorioMedicamentoEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)";

            DB.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            //arrange
            Fornecedor f1 = GetFornecedor();

            Medicamento m1 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            var repositorio = new RepositorioMedicamentoDB();

            //action
            repositorio.Inserir(m1);

            //assert
            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(m1.Id);

            Assert.IsNotNull(medicamentoEncontrado);

            Assert.AreEqual(m1, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_editar_medicamento_do_DB()
        {
            //arrange
            Fornecedor f1 = GetFornecedor();
            Medicamento m1 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            var repositorio = new RepositorioMedicamentoDB();
            repositorio.Inserir(m1);

            Medicamento medicamentoAtualizado = repositorio.SelecionarPorId(m1.Id);
            medicamentoAtualizado.Nome = "Paracetamol";
            medicamentoAtualizado.Descricao = "Anti-inflamatório";
            medicamentoAtualizado.Lote = "abc-123";
            medicamentoAtualizado.Validade = new DateTime(2050, 12, 05);
            medicamentoAtualizado.Fornecedor = f1;
            medicamentoAtualizado.QuantidadeDisponivel = 100;

            //action
            repositorio.Editar(medicamentoAtualizado);

            //assert
            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(medicamentoAtualizado.Id);
            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamentoAtualizado, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_medicamento_do_DB()
        {
            //arrange
            Fornecedor f1 = GetFornecedor();
            Medicamento m1 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            var repositorio = new RepositorioMedicamentoDB();
            repositorio.Inserir(m1);

            //action
            repositorio.Excluir(m1);

            //assert
            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(m1.Id);
            Assert.IsNull(medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos_do_DB()
        {
            //arrange

            Fornecedor f1 = GetFornecedor();

            var repositorio = new RepositorioMedicamentoDB();

            Medicamento m1 = new Medicamento("Aspirina", "Analgésico", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m2 = new Medicamento("Paracetamol", "Anti-inflamatório", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            Medicamento m3 = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), f1, 100);

            repositorio.Inserir(m1);
            repositorio.Inserir(m2);
            repositorio.Inserir(m3);

            //action
            var medicamentos = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, medicamentos.Count);
            Assert.AreEqual(m1, medicamentos[0]);
            Assert.AreEqual(m2, medicamentos[1]);
            Assert.AreEqual(m3, medicamentos[2]);
        }

        [TestMethod]
        public void Deve_selecionar_medicamento_do_DB_por_ID()
        {
            //arrange
            var repositorio = new RepositorioMedicamentoDB();

            Fornecedor f1 = GetFornecedor();

            Medicamento m1 = new Medicamento("Paracetamol", "Anti-inflamatório", "abc-123", new DateTime(2050, 12, 05), f1, 100);
            repositorio.Inserir(m1);

            //action
            var medicamentoEncontrado = repositorio.SelecionarPorId(m1.Id);

            //assert
            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(m1, medicamentoEncontrado);
        }

    }
}
