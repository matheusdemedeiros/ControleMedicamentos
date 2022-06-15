using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedorDB : RepositorioBaseDB
    {

        #region SQL QUERIES

        private const string sqlInserir =
            @"INSERT INTO [TBFORNECEDOR]
                (
			    	[NOME],
			    	[TELEFONE],
			    	[EMAIL],
			    	[CIDADE],
			    	[ESTADO]
			    )
            VALUES
                (	
                    @NOME,
		        	@TELEFONE,
		        	@EMAIL,
		        	@CIDADE,
		        	@ESTADO
		        ); SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBFORNECEDOR]
                  SET 
                    [NOME] = @NOME,
                    [TELEFONE] = @TELEFONE,
                    [EMAIL] = @EMAIL,
                    [CIDADE] = @CIDADE,
                    [ESTADO] = @ESTADO
                WHERE 
                    [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBFORNECEDOR]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT
                    [ID],
                    [NOME],
                    [TELEFONE],
                    [EMAIL],
                    [CIDADE],
                    [ESTADO]
             FROM 
                [TBFORNECEDOR]";

        private const string sqlSelecionarPorId =
            @"SELECT
                    [ID],
                    [NOME],
                    [TELEFONE],
                    [EMAIL],
                    [CIDADE],
                    [ESTADO]
             FROM 
                [TBFORNECEDOR]
            WHERE
                    [ID] = @ID";

       
        #endregion

        public ValidationResult Inserir(Fornecedor novoRegistro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFornecedor(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();

            novoRegistro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Fornecedor registro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFornecedor(registro, comandoEdicao);

            comandoEdicao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();

            comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Fornecedor registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Fornecedor SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            Fornecedor fornecedor = null;

            if (leitorFornecedor.Read())
                fornecedor = ConverterParaFornecedor(leitorFornecedor);

            conexaoComBanco.Close();

            return fornecedor;
        }

        public List<Fornecedor> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            List<Fornecedor> fornecedores = new List<Fornecedor>();

            while (leitorFornecedor.Read())
            {
                Fornecedor fornecedor = ConverterParaFornecedor(leitorFornecedor);

                fornecedores.Add(fornecedor);
            }

            conexaoComBanco.Close();

            return fornecedores;
        }

        #region Métodos privados

        private void ConfigurarParametrosFornecedor(Fornecedor registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("TELEFONE", registro.Telefone);
            comando.Parameters.AddWithValue("EMAIL", registro.Email);
            comando.Parameters.AddWithValue("CIDADE", registro.Cidade);
            comando.Parameters.AddWithValue("ESTADO", registro.Estado);
        }
        
        private ValidadorFornecedor ObterValidador()
        {
            return new ValidadorFornecedor();
        }

        public Fornecedor ConverterParaFornecedor(SqlDataReader leitorFornecedor)
        {
            var id = Convert.ToInt32(leitorFornecedor["ID"]);
            var nome = Convert.ToString(leitorFornecedor["NOME"]);
            var telefone = Convert.ToString(leitorFornecedor["TELEFONE"]);
            var email = Convert.ToString(leitorFornecedor["EMAIL"]);
            var cidade = Convert.ToString(leitorFornecedor["CIDADE"]);
            var estado = Convert.ToString(leitorFornecedor["ESTADO"]);

            var fornecedor = new Fornecedor()
            { 
              Id = id,  
              Nome = nome,
              Telefone = telefone,
              Email = email,
              Cidade = cidade,
              Estado = estado
            };

            return fornecedor;
        }
        
        #endregion
    }
}
