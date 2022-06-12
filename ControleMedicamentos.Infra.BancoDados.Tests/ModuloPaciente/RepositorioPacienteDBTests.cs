using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{

    [TestClass]
    public class RepositorioPacienteDBTests
    {
        public RepositorioPacienteDBTests()
        {
            string sql =
             @"DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)";

            DB.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_um_Paciente_No_DB()
        {
            //arrange
            Paciente novoPaciente = new Paciente("Matheus", "111111111111111");

            var repositorio = new RepositorioPacienteDB();

            //action
            repositorio.Inserir(novoPaciente);

            //assert
            Paciente pacienteEncontrado = repositorio.SelecionarPorId(novoPaciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(novoPaciente.Id, pacienteEncontrado.Id);
            Assert.AreEqual(novoPaciente.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(novoPaciente.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_editar_paciente_do_DB()
        {
            //arrange
            Paciente novoPaciente = new Paciente("Matheus", "111111111111111");

            var repositorio = new RepositorioPacienteDB();
            repositorio.Inserir(novoPaciente);

            Paciente pacienteAtualizado = repositorio.SelecionarPorId(novoPaciente.Id);
            pacienteAtualizado.Nome = "Paula";
            pacienteAtualizado.CartaoSUS = "222222222222222";

            //action
            repositorio.Editar(pacienteAtualizado);

            //assert
            Paciente pacienteEncontrado = repositorio.SelecionarPorId(novoPaciente.Id);
            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(pacienteAtualizado.Id, pacienteEncontrado.Id);
            Assert.AreEqual(pacienteAtualizado.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(pacienteAtualizado.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_excluir_paciente_do_DB()
        {
            //arrange
            Paciente novoPaciente = new Paciente("Matheus", "111111111111111");
            var repositorio = new RepositorioPacienteDB();
            repositorio.Inserir(novoPaciente);

            //action
            repositorio.Excluir(novoPaciente);

            //assert
            Paciente pacienteEncontrado = repositorio.SelecionarPorId(novoPaciente.Id);

            Assert.IsNull(pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_pacientes_do_DB()
        {
            //arrange
            var repositorio = new RepositorioPacienteDB();

            Paciente p1 = new Paciente("Matheus", "111111111111111");
            Paciente p2 = new Paciente("Paula", "222222222222222");
            Paciente p3 = new Paciente("Carlos", "333333333333333");

            repositorio.Inserir(p1);
            repositorio.Inserir(p2);
            repositorio.Inserir(p3);

            //action
            var pacientes = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, pacientes.Count);
            Assert.AreEqual("Matheus", pacientes[0].Nome);
            Assert.AreEqual("Paula", pacientes[1].Nome);
            Assert.AreEqual("Carlos", pacientes[2].Nome);
        }

        [TestMethod]
        public void Deve_selecionar_paciente_do_DB_por_ID()
        {
            //arrange
            var repositorio = new RepositorioPacienteDB();

            Paciente p1 = new Paciente("Matheus", "111111111111111");

            repositorio.Inserir(p1);

            //action
            var pacienteEncontrado = repositorio.SelecionarPorId(p1.Id);

            //assert
            Assert.AreEqual(p1.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(p1.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }
    }
}
