using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleRequisicaos.Infra.BancoDados.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoDB : RepositorioBaseDB
    {
        #region SQL QUERIES

        private const string sqlInserir =
            @"INSERT INTO [TBMEDICAMENTO]
               (
				    [NOME],
				    [DESCRICAO],
				    [LOTE],
				    [VALIDADE],
				    [QUANTIDADEDISPONIVEL],
				    [FORNECEDOR_ID]
		       )
            VALUES
               (
				    @NOME,
				    @DESCRICAO,
				    @LOTE,
				    @VALIDADE,
				    @QUANTIDADE_DISPONIVEL,
				    @FORNECEDOR_ID
		       );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBMEDICAMENTO]
                SET 
	                [NOME] = @NOME,
	                [DESCRICAO] = @DESCRICAO,
	                [LOTE] = @LOTE,
	                [VALIDADE] = @VALIDADE,
	                [QUANTIDADEDISPONIVEL] = @QUANTIDADE_DISPONIVEL,
	                [FORNECEDOR_ID] = @FORNECEDOR_ID
                WHERE
		            [ID] = @ID;";

        private const string sqlExcluir =
            @"DELETE FROM [TBMEDICAMENTO]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
		        [ID],
		        [NOME],
		        [DESCRICAO],
		        [LOTE],
		        [VALIDADE],
		        [QUANTIDADEDISPONIVEL],
		        [FORNECEDOR_ID]
            FROM [TBMEDICAMENTO]";

        private const string sqlSelecionarPorId =
            @"SELECT
                [ID],
		        [NOME],
		        [DESCRICAO],
		        [LOTE],
		        [VALIDADE],
		        [QUANTIDADEDISPONIVEL],
		        [FORNECEDOR_ID]
            FROM [TBMEDICAMENTO]
                WHERE
                    [ID] = @ID";

        #endregion

        public ValidationResult Inserir(Medicamento novoRegistro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMedicamento(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();

            novoRegistro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Medicamento registro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMedicamento(registro, comandoEdicao);

            comandoEdicao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();

            comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Medicamento registro)
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

        public Medicamento SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            Medicamento medicamento = null;

            if (leitorMedicamento.Read())
                medicamento = ConverterParaMedicamento(leitorMedicamento);

            conexaoComBanco.Close();

            return medicamento;
        }

        public List<Medicamento> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();

            while (leitorMedicamento.Read())
            {
                Medicamento medicamento = ConverterParaMedicamento(leitorMedicamento);

                medicamentos.Add(medicamento);
            }

            conexaoComBanco.Close();

            return medicamentos;

        }

        #region Métodos privados

        private ValidadorMedicamento ObterValidador()
        {
            return new ValidadorMedicamento();
        }

        public List<Requisicao> CarregarRequisicoes(Medicamento medicamento)
        {
            var repositorioRequisicao = new RepositorioRequisicaoDB();

            return repositorioRequisicao.SelecionarRequisicoesMedicamentoEspecifico(medicamento);
        }

        private Fornecedor CarregarFornecedor(int idFornecedor)
        {
            var repositorioFornecedor = new RepositorioFornecedorDB();

            return repositorioFornecedor.SelecionarPorId(idFornecedor);
        }

        private void ConfigurarParametrosMedicamento(Medicamento novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NOME", novoRegistro.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", novoRegistro.Descricao);
            comando.Parameters.AddWithValue("LOTE", novoRegistro.Lote);
            comando.Parameters.AddWithValue("VALIDADE", novoRegistro.Validade);
            comando.Parameters.AddWithValue("QUANTIDADE_DISPONIVEL", novoRegistro.QuantidadeDisponivel);
            comando.Parameters.AddWithValue("FORNECEDOR_ID", novoRegistro.Fornecedor.Id);
        }

        private Medicamento ConverterParaMedicamento(SqlDataReader leitorMedicamento)
        {
            var id = Convert.ToInt32(leitorMedicamento["ID"]);
            var nome = Convert.ToString(leitorMedicamento["NOME"]);
            var descricao = Convert.ToString(leitorMedicamento["DESCRICAO"]);
            var lote = Convert.ToString(leitorMedicamento["LOTE"]);
            var validade = Convert.ToDateTime(leitorMedicamento["VALIDADE"]);
            var qtdDisponivel = Convert.ToInt32(leitorMedicamento["QUANTIDADEDISPONIVEL"]);
            var idFornecedor = Convert.ToInt32(leitorMedicamento["FORNECEDOR_ID"]);

            Fornecedor fornecedor = CarregarFornecedor(idFornecedor);

            var medicamento = new Medicamento()
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Lote = lote,
                Validade = validade,
                QuantidadeDisponivel = qtdDisponivel,
                Fornecedor = fornecedor,
                Requisicoes = new List<Requisicao>()
            };
            
            //medicamento.Requisicoes = CarregarRequisicoes(medicamento);

            return medicamento;
        }

        #endregion
    }
}
