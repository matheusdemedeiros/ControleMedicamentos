using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.Compartilhado;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleRequisicaos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoDB : RepositorioBaseDB, IRepositorio<Requisicao>
    {

        #region SQL QUERIES

        private const string sqlInserir =
            @"INSERT INTO [TBREQUISICAO]
                (
				    [FUNCIONARIO_ID],
				    [PACIENTE_ID],
				    [MEDICAMENTO_ID],
				    [QUANTIDADEMEDICAMENTO],
				    [DATA]
		        )
            VALUES
                (
				    @FUNCIONARIO_ID,
				    @PACIENTE_ID,
				    @MEDICAMENTO_ID,
				    @QUANTIDADEMEDICAMENTO,
				    @DATA
		        ); SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBREQUISICAO]
	            SET 
		            [FUNCIONARIO_ID] = @FUNCIONARIO_ID,
		            [PACIENTE_ID] = @PACIENTE_ID,
		            [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
		            [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO,
		            [DATA] = @DATA
	        WHERE
	                [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBREQUISICAO]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT
	            [ID],
                [FUNCIONARIO_ID],
                [PACIENTE_ID],
                [MEDICAMENTO_ID],
                [QUANTIDADEMEDICAMENTO],
                [DATA]
            FROM
                [TBREQUISICAO]";

        private const string sqlSelecionarPorId =
            @"SELECT
	            [ID],
                [FUNCIONARIO_ID],
                [PACIENTE_ID],
                [MEDICAMENTO_ID],
                [QUANTIDADEMEDICAMENTO],
                [DATA]
            FROM
                [TBREQUISICAO]
            WHERE
                [ID] = @ID";

        private const string sqlCarregarRequisicoesMedicamento =
            @"SELECT 
	            [ID],
                [FUNCIONARIO_ID],
                [PACIENTE_ID],
                [MEDICAMENTO_ID],
                [QUANTIDADEMEDICAMENTO],
                [DATA]
            FROM
	            [TBREQUISICAO]
            WHERE
	            MEDICAMENTO_ID = @ID_MEDICAMENTO";

        #endregion

        public ValidationResult Inserir(Requisicao novoRegistro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();

            novoRegistro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao registro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(registro, comandoEdicao);

            comandoEdicao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();

            comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Requisicao registro)
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

        public Requisicao SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();

            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao Requisicao = null;

            if (leitorRequisicao.Read())
                Requisicao = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return Requisicao;
        }

        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> Requisicaos = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao Requisicao = ConverterParaRequisicao(leitorRequisicao);

                Requisicaos.Add(Requisicao);
            }

            conexaoComBanco.Close();

            return Requisicaos;

        }

        public List<Requisicao> SelecionarRequisicoesMedicamentoEspecifico(Medicamento medicamento)
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlCarregarRequisicoesMedicamento, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID_MEDICAMENTO", medicamento.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> Requisicaos = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao Requisicao = ConverterParaRequisicao(leitorRequisicao);

                Requisicaos.Add(Requisicao);
            }

            conexaoComBanco.Close();

            return Requisicaos;

        }

        #region Métodos privados

        private ValidadorRequisicao ObterValidador()
        {
            return new ValidadorRequisicao();
        }

        private void ConfigurarParametrosRequisicao(Requisicao novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("FUNCIONARIO_ID",novoRegistro.Funcionario.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID",novoRegistro.Medicamento.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID",novoRegistro.Paciente.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO",novoRegistro.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA",novoRegistro.Data);
        }

        private Medicamento CarregarMedicamento(int idMedicamento)
        {
            var repositorioMedicamento = new RepositorioMedicamentoDB();

            return repositorioMedicamento.SelecionarPorId(idMedicamento);
        }
        
        private Funcionario CarregarFuncionario(int idFuncionario)
        {
            var repositorioFuncionario = new RepositorioFuncionarioDB();

            return repositorioFuncionario.SelecionarPorId(idFuncionario);
        }

        private Paciente CarregarPaciente(int idPaciente)
        {
            var repositorioPaciente = new RepositorioPacienteDB();

            return repositorioPaciente.SelecionarPorId(idPaciente);
        }

        private Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            var id = Convert.ToInt32(leitorRequisicao["ID"]);
            var idFuncionario = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);
            var idMedicamento = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_ID"]);
            var idPaciente = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            var qtdMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            var data = Convert.ToDateTime(leitorRequisicao["DATA"]);


            Medicamento medicamento = CarregarMedicamento(idMedicamento);
            Funcionario funcionario = CarregarFuncionario(idFuncionario);
            Paciente paciente = CarregarPaciente(idPaciente);

            var requisicao = new Requisicao()
            {
                Id = id,
                QtdMedicamento = qtdMedicamento,
                Data = data,
                Medicamento = medicamento,
                Funcionario = funcionario,
                Paciente = paciente
            };
            
            return  requisicao;
        }

        #endregion

    }
}
