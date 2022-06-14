using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{

    [TestClass]
    public class RepositorioFornecedorDBTest
    {
        public RepositorioFornecedorDBTest()
        {
            string sql =
             @"DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)";

            DB.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_um_fornecedor_no_DB()
        {
            //arrange
            Fornecedor novoFornecedor = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");

            var repositorio = new RepositorioFornecedorDB();

            //action
            repositorio.Inserir(novoFornecedor);

            //assert
            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorId(novoFornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(novoFornecedor, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_editar_fornecedor_do_DB()
        {
            //arrange
            Fornecedor novoFornecedor = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");
            var repositorio = new RepositorioFornecedorDB();
            repositorio.Inserir(novoFornecedor);
            Fornecedor fornecedorAtualizado = repositorio.SelecionarPorId(novoFornecedor.Id);
            fornecedorAtualizado.Nome = "Distribuidor B";
            fornecedorAtualizado.Telefone = "(41) 9 9956-8777";
            fornecedorAtualizado.Email = "distribuidorB@gmail.com";
            fornecedorAtualizado.Cidade = "Curitiba";
            fornecedorAtualizado.Estado = "PR";
            //action
            repositorio.Editar(fornecedorAtualizado);

            //assert
            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorId(novoFornecedor.Id);
            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedorAtualizado, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_fornecedor_do_DB()
        {
            //arrange
            Fornecedor novoFornecedor = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");
            var repositorio = new RepositorioFornecedorDB();
            repositorio.Inserir(novoFornecedor);

            //action
            repositorio.Excluir(novoFornecedor);

            //assert
            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorId(novoFornecedor.Id);

            Assert.IsNull(fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_fornecedores_do_DB()
        {
            //arrange
            var repositorio = new RepositorioFornecedorDB();

            Fornecedor f1 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");
            Fornecedor f2 = new Fornecedor("Distribuidor B", "(49) 9 8857-7766", "distribuidorB@gmail.com", "Curitibanos", "SC");
            Fornecedor f3 = new Fornecedor("Distribuidor C", "(49) 9 8466-8881", "distribuidorC@gmail.com", "Fraiburgo", "SC");

            repositorio.Inserir(f1);
            repositorio.Inserir(f2);
            repositorio.Inserir(f3);

            //action
            var fornecedores = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, fornecedores.Count);
            Assert.AreEqual(f1, fornecedores[0]);
            Assert.AreEqual(f2, fornecedores[1]);
            Assert.AreEqual(f3, fornecedores[2]);
        }

        [TestMethod]
        public void Deve_selecionar_fornecedor_do_DB_por_ID()
        {
            //arrange
            var repositorio = new RepositorioFornecedorDB();
            Fornecedor f1 = new Fornecedor("Distribuidor A", "(49) 9 9956-8765", "distribuidorA@gmail.com", "Lages", "SC");

            repositorio.Inserir(f1);

            //action
            var fornecedorEncontrado = repositorio.SelecionarPorId(f1.Id);

            //assert
            Assert.AreEqual(f1, fornecedorEncontrado);
        }
    }
}
