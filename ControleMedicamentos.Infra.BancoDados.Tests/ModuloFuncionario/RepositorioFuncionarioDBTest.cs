using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{

    [TestClass]
    public class RepositorioFuncionarioDBTest
    {

        public RepositorioFuncionarioDBTest()
        {
            string sql =
                @"DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)";

            DB.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_um_funcionario_no_DB()
        {

            //arrange
            Funcionario novoFuncionario = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            var repositorio = new RepositorioFuncionarioDB();

            //action
            repositorio.Inserir(novoFuncionario);

            //assert
            Funcionario funcionarioEncontrado = repositorio.SelecionarPorId(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(novoFuncionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_editar_funcionario_do_DB()
        {
            //arrange
            Funcionario novoFuncionario = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");

            var repositorio = new RepositorioFuncionarioDB();
            repositorio.Inserir(novoFuncionario);

            Funcionario funcionarioAtuaizado = repositorio.SelecionarPorId(novoFuncionario.Id);
            funcionarioAtuaizado.Nome = "Paula";
            funcionarioAtuaizado.Login = "paula_user";
            funcionarioAtuaizado.Senha = "P@uLa87";

            //action
            repositorio.Editar(funcionarioAtuaizado);

            //assert
            Funcionario funcionarioEncontrado = repositorio.SelecionarPorId(novoFuncionario.Id);
            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionarioAtuaizado, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_funcionario_do_DB()
        {
            //arrange
            Funcionario novoFuncionario = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");
            var repositorio = new RepositorioFuncionarioDB();
            repositorio.Inserir(novoFuncionario);

            //action
            repositorio.Excluir(novoFuncionario);

            //assert
            Funcionario funcionarioEncontrado = repositorio.SelecionarPorId(novoFuncionario.Id);

            Assert.IsNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_funcionaris_do_DB()
        {
            //arrange
            var repositorio = new RepositorioFuncionarioDB();

            Funcionario f1 = new Funcionario("Matheus", "matheus_user", "m@theuSKl90");
            Funcionario f2 = new Funcionario("Paula", "paula_user", "m@uLa87");
            Funcionario f3 = new Funcionario("João", "joao_user", "JO@Oksr578");

            repositorio.Inserir(f1);
            repositorio.Inserir(f2);
            repositorio.Inserir(f3);

            //action
            var funcionarios = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, funcionarios.Count);
            Assert.AreEqual(f1, funcionarios[0]);
            Assert.AreEqual(f2, funcionarios[1]);
            Assert.AreEqual(f3, funcionarios[2]);
        }

        [TestMethod]
        public void Deve_selecionar_funcionario_do_DB_por_ID()
        {
            //arrange
            var repositorio = new RepositorioFuncionarioDB();

            Funcionario funcionario = new Funcionario("João", "joao_user", "JO@Oksr578");
            repositorio.Inserir(funcionario);

            //action
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            //assert
            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }
    }
}
