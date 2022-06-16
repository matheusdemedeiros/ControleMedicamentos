using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleRequisicaos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoDBTest
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

        private Medicamento GetMedicamento()
        {
            Medicamento medicamento;

            var repositorioMedicamento = new RepositorioMedicamentoDB();

            medicamento = repositorioMedicamento.SelecionarPorId(1);

            if (medicamento == null)
            {
                medicamento = new Medicamento("Dorflex", "Relaxante muscular", "abc-123", new DateTime(2050, 12, 05), GetFornecedor(), 100);

                repositorioMedicamento.Inserir(medicamento);
            }

            return medicamento;
        }

        private Funcionario GetFuncionario()
        {
            Funcionario funcionario;

            var repositorioFuncionario = new RepositorioFuncionarioDB();

            funcionario = repositorioFuncionario.SelecionarPorId(1);

            if (funcionario == null)
            {
                funcionario = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

                repositorioFuncionario.Inserir(funcionario);
            }

            return funcionario;
        }

        private Paciente GetPaciente()
        {
            Paciente paciente;

            var repositorioPaciente = new RepositorioPacienteDB();

            paciente = repositorioPaciente.SelecionarPorId(1);

            if (paciente == null)
            {

                paciente = new Paciente("Matheus", "111111111111111");

                repositorioPaciente.Inserir(paciente);
            }

            return paciente;
        }

        public RepositorioRequisicaoDBTest()
        {
            string sql1 =
                @"DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)";

            string sql2 =
             @"DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)";

            string sql3 =
             @"DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)";

            string sql4 =
                @"DELETE FROM TBREQUISICAO;
                  DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)";

            DB.ExecutarSql(sql4);
            DB.ExecutarSql(sql1);
            DB.ExecutarSql(sql2);
            DB.ExecutarSql(sql3);
        }

        [TestMethod]
        public void Deve_inserir_requisicao()
        {
            //arrange
            Medicamento medicamento = GetMedicamento();
            Funcionario funcionario = GetFuncionario();
            Paciente paciente = GetPaciente();

            Requisicao requisicao = new Requisicao(medicamento, paciente, 5, DateTime.Today, funcionario);

            var repositorio = new RepositorioRequisicaoDB();

            //action
            repositorio.Inserir(requisicao);

            //assert
            Requisicao requisicaoEncontrada = repositorio.SelecionarPorId(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);

            //Assert.AreEqual(requisicao.Id, requisicaoEncontrada.Id);
            //Assert.AreEqual(requisicao.Medicamento, requisicaoEncontrada.Medicamento);
            //Assert.AreEqual(requisicao.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
            //Assert.AreEqual(requisicao.Funcionario, requisicaoEncontrada.Funcionario);
            //Assert.AreEqual(requisicao.Data, requisicaoEncontrada.Data);
            //Assert.AreEqual(requisicao.Paciente, requisicaoEncontrada.Paciente);
        }

        [TestMethod]
        public void Deve_editar_requisicao_do_DB()
        {
            //arrange
            Medicamento medicamento = GetMedicamento();
            Funcionario funcionario = GetFuncionario();
            Paciente paciente = GetPaciente();
            Requisicao requisicao = new Requisicao(medicamento, paciente, 5, DateTime.Today, funcionario);
            var repositorio = new RepositorioRequisicaoDB();
            repositorio.Inserir(requisicao);

            Requisicao requisicaoAtualizada = repositorio.SelecionarPorId(requisicao.Id);
            requisicaoAtualizada.Medicamento = medicamento;
            requisicaoAtualizada.Paciente = paciente;
            requisicaoAtualizada.QtdMedicamento = 10;
            requisicaoAtualizada.Data = DateTime.Today;
            requisicaoAtualizada.Funcionario = funcionario;

            //action
            repositorio.Editar(requisicaoAtualizada);


            //assert
            var requisicaoEncontrada = repositorio.SelecionarPorId(requisicaoAtualizada.Id);

            Assert.IsNotNull(requisicaoAtualizada);
            Assert.AreEqual(requisicaoAtualizada, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_excluir_requisicao_do_DB()
        {
            var repositorio = new RepositorioRequisicaoDB();
            Medicamento medicamento = GetMedicamento();
            Funcionario funcionario = GetFuncionario();
            Paciente paciente = GetPaciente();
            Requisicao requisicao = new Requisicao(medicamento, paciente, 5, DateTime.Today, funcionario);
            repositorio.Inserir(requisicao);

            //action
            repositorio.Excluir(requisicao);

            //assert
            Requisicao requisicaoEncontrada = repositorio.SelecionarPorId(requisicao.Id);

            //assert
            Assert.IsNull(requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_todas_as_requisicoes_do_DB()
        {
            //arrange
            var repositorio = new RepositorioRequisicaoDB();
            Medicamento medicamento = GetMedicamento();
            Funcionario funcionario = GetFuncionario();
            Paciente paciente = GetPaciente();

            Requisicao requisicao1 = new Requisicao(medicamento, paciente, 5, DateTime.Today, funcionario);
            Requisicao requisicao2 = new Requisicao(medicamento, paciente, 7, DateTime.Today, funcionario);

            repositorio.Inserir(requisicao1);
            repositorio.Inserir(requisicao2);

            //action
            List<Requisicao> requisicoesEncontradas = repositorio.SelecionarTodos();

            //assert

            Assert.IsNotNull(requisicoesEncontradas);
            Assert.AreEqual(2, requisicoesEncontradas.Count);
        }

        [TestMethod]
        public void Deve_selecionar_requisicao_do_DB_por_ID()
        {
            //Arrange
            Medicamento medicamento = GetMedicamento();
            Funcionario funcionario = GetFuncionario();
            Paciente paciente = GetPaciente();

            Requisicao requisicao = new Requisicao(medicamento, paciente, 5, DateTime.Today, funcionario);

            var repositorio = new RepositorioRequisicaoDB();
            repositorio.Inserir(requisicao);

            //action
            Requisicao requisicaoEncontrada = repositorio.SelecionarPorId(requisicao.Id);

            //assert
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);
        }

    }
}
