using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.Compartilhado;
using ControleMedicamentos.Dominio.ModuloFornecedor;
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
	            REQ.ID AS ID_REQUISICAO,
	            REQ.DATA AS DATA_REQUISICAO,
	            REQ.QUANTIDADEMEDICAMENTO, 
	            
	            MED.ID AS ID_MEDICAMENTO,
	            MED.NOME AS NOME_MEDICAMENTO,
	            MED.DESCRICAO AS DESCRICAO_MEDICAMENTO,
	            MED.LOTE AS LOTE_MEDICAMENTO,
	            MED.VALIDADE AS VALIDADE_MEDICAMENTO,
	            MED.QUANTIDADEDISPONIVEL,
	            
	            FUNC.ID AS ID_FUNCIONARIO,
	            FUNC.NOME AS NOME_FUNCIONARIO,
	            FUNC.LOGIN AS LOGIN_FUNCIONARIO,
	            FUNC.SENHA AS SENHA_FUNCIONARIO,
	            
	            PAC.ID AS ID_PACIENTE,
	            PAC.NOME AS NOME_PACIENTE,
	            PAC.CARTAOSUS,
	            
	            FORNE.ID AS ID_FORNECEDOR,
	            FORNE.NOME AS NOME_FORNECEDOR,
	            FORNE.TELEFONE AS TELEFONE_FORNECEDOR,
	            FORNE.EMAIL AS EMAIL_FORNECEDOR,
	            FORNE.CIDADE AS CIDADE_FORNECEDOR,
	            FORNE.ESTADO AS ESTADO_FORNECEDOR
            FROM 
            	TBREQUISICAO AS REQ INNER JOIN TBPACIENTE AS PAC
            ON
            	REQ.PACIENTE_ID = PAC.ID INNER JOIN
            	TBFUNCIONARIO AS FUNC
            ON
            	REQ.FUNCIONARIO_ID = FUNC.ID INNER JOIN
            	TBMEDICAMENTO AS MED
            ON
            	REQ.MEDICAMENTO_ID = MED.ID INNER JOIN
            	TBFORNECEDOR AS FORNE
            ON
            	MED.FORNECEDOR_ID = FORNE.ID";

        private const string sqlSelecionarPorId =
            @"SELECT
	            REQ.ID AS ID_REQUISICAO,
	            REQ.DATA AS DATA_REQUISICAO,
	            REQ.QUANTIDADEMEDICAMENTO, 
	            
	            MED.ID AS ID_MEDICAMENTO,
	            MED.NOME AS NOME_MEDICAMENTO,
	            MED.DESCRICAO AS DESCRICAO_MEDICAMENTO,
	            MED.LOTE AS LOTE_MEDICAMENTO,
	            MED.VALIDADE AS VALIDADE_MEDICAMENTO,
	            MED.QUANTIDADEDISPONIVEL,
	            
	            FUNC.ID AS ID_FUNCIONARIO,
	            FUNC.NOME AS NOME_FUNCIONARIO,
	            FUNC.LOGIN AS LOGIN_FUNCIONARIO,
	            FUNC.SENHA AS SENHA_FUNCIONARIO,
	            
	            PAC.ID AS ID_PACIENTE,
	            PAC.NOME AS NOME_PACIENTE,
	            PAC.CARTAOSUS,
	            
	            FORNE.ID AS ID_FORNECEDOR,
	            FORNE.NOME AS NOME_FORNECEDOR,
	            FORNE.TELEFONE AS TELEFONE_FORNECEDOR,
	            FORNE.EMAIL AS EMAIL_FORNECEDOR,
	            FORNE.CIDADE AS CIDADE_FORNECEDOR,
	            FORNE.ESTADO AS ESTADO_FORNECEDOR
            FROM 
            	TBREQUISICAO AS REQ INNER JOIN TBPACIENTE AS PAC
            ON
            	REQ.PACIENTE_ID = PAC.ID INNER JOIN
            	TBFUNCIONARIO AS FUNC
            ON
            	REQ.FUNCIONARIO_ID = FUNC.ID INNER JOIN
            	TBMEDICAMENTO AS MED
            ON
            	REQ.MEDICAMENTO_ID = MED.ID INNER JOIN
            	TBFORNECEDOR AS FORNE
            ON
            	MED.FORNECEDOR_ID = FORNE.ID
            WHERE
                REQ.ID = @ID";

        private const string sqlCarregarRequisicoesMedicamento =
            @"SELECT
	            REQ.ID AS ID_REQUISICAO,
	            REQ.DATA AS DATA_REQUISICAO,
	            REQ.QUANTIDADEMEDICAMENTO, 
	            
	            MED.ID AS ID_MEDICAMENTO,
	            MED.NOME AS NOME_MEDICAMENTO,
	            MED.DESCRICAO AS DESCRICAO_MEDICAMENTO,
	            MED.LOTE AS LOTE_MEDICAMENTO,
	            MED.VALIDADE AS VALIDADE_MEDICAMENTO,
	            MED.QUANTIDADEDISPONIVEL,
	            
	            FUNC.ID AS ID_FUNCIONARIO,
	            FUNC.NOME AS NOME_FUNCIONARIO,
	            FUNC.LOGIN AS LOGIN_FUNCIONARIO,
	            FUNC.SENHA AS SENHA_FUNCIONARIO,
	            
	            PAC.ID AS ID_PACIENTE,
	            PAC.NOME AS NOME_PACIENTE,
	            PAC.CARTAOSUS,
	            
	            FORNE.ID AS ID_FORNECEDOR,
	            FORNE.NOME AS NOME_FORNECEDOR,
	            FORNE.TELEFONE AS TELEFONE_FORNECEDOR,
	            FORNE.EMAIL AS EMAIL_FORNECEDOR,
	            FORNE.CIDADE AS CIDADE_FORNECEDOR,
	            FORNE.ESTADO AS ESTADO_FORNECEDOR
            FROM 
            	TBREQUISICAO AS REQ INNER JOIN TBPACIENTE AS PAC
            ON
            	REQ.PACIENTE_ID = PAC.ID INNER JOIN
            	TBFUNCIONARIO AS FUNC
            ON
            	REQ.FUNCIONARIO_ID = FUNC.ID INNER JOIN
            	TBMEDICAMENTO AS MED
            ON
            	REQ.MEDICAMENTO_ID = MED.ID INNER JOIN
            	TBFORNECEDOR AS FORNE
            ON
            	MED.FORNECEDOR_ID = FORNE.ID
            WHERE
                MED.ID = @ID_MEDICAMENTO";

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
                Requisicao = ConverterParaRequisicao(leitorRequisicao, true);

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
                Requisicao Requisicao = ConverterParaRequisicao(leitorRequisicao, true);

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
                Requisicao Requisicao = ConverterParaRequisicao(leitorRequisicao, false);

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
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", novoRegistro.Funcionario.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", novoRegistro.Medicamento.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", novoRegistro.Paciente.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", novoRegistro.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", novoRegistro.Data);
        }

        private Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao, bool carregarMedicamento)
        {
            var id = Convert.ToInt32(leitorRequisicao["ID_REQUISICAO"]);
            var qtdMedicamentoRequisicao = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            var data = Convert.ToDateTime(leitorRequisicao["DATA_REQUISICAO"]);

            var paciente = ConverterParaPaciente(leitorRequisicao);

            var funcionario = ConverterParaFuncionario(leitorRequisicao);


            Medicamento medicamento = null;

            if (carregarMedicamento)
            {
                var fornecedor = ConverterParaFornecedor(leitorRequisicao);

                medicamento = ConverterParaMedicamento(leitorRequisicao);

                medicamento.Fornecedor = fornecedor;
            }


            var requisicao = new Requisicao()
            {
                Id = id,
                QtdMedicamento = qtdMedicamentoRequisicao,
                Data = data,
                Medicamento = medicamento,
                Funcionario = funcionario,
                Paciente = paciente
            };

            return requisicao;
        }

        private Funcionario ConverterParaFuncionario(SqlDataReader leitorRequisicao)
        {
            var idFuncionario = Convert.ToInt32(leitorRequisicao["ID_FUNCIONARIO"]);
            var nomeFuncionario = Convert.ToString(leitorRequisicao["NOME_FUNCIONARIO"]);
            var loginFuncionario = Convert.ToString(leitorRequisicao["LOGIN_FUNCIONARIO"]);
            var senhaFuncionario = Convert.ToString(leitorRequisicao["SENHA_FUNCIONARIO"]);

            Funcionario f = new Funcionario()
            {
                Id = idFuncionario,
                Nome = nomeFuncionario,
                Login = loginFuncionario,
                Senha = senhaFuncionario
            };
            return f;
        }

        private Paciente ConverterParaPaciente(SqlDataReader leitorRequisicao)
        {
            var idPaciente = Convert.ToInt32(leitorRequisicao["ID_PACIENTE"]);
            var nomePaciente = Convert.ToString(leitorRequisicao["NOME_PACIENTE"]);
            var cartaoSUSPaciente = Convert.ToString(leitorRequisicao["CARTAOSUS"]);

            Paciente p = new Paciente()
            {
                Id = idPaciente,
                Nome = nomePaciente,
                CartaoSUS = cartaoSUSPaciente
            };

            return p;
        }

        private Medicamento ConverterParaMedicamento(SqlDataReader leitorRequisicao)
        {
            var idMedicamento = Convert.ToInt32(leitorRequisicao["ID_MEDICAMENTO"]);
            var nomeMedicamento = Convert.ToString(leitorRequisicao["NOME_MEDICAMENTO"]);
            var descricaoMedicamento = Convert.ToString(leitorRequisicao["DESCRICAO_MEDICAMENTO"]);
            var loteMedicamento = Convert.ToString(leitorRequisicao["LOTE_MEDICAMENTO"]);
            var validadeMedicamento = Convert.ToDateTime(leitorRequisicao["VALIDADE_MEDICAMENTO"]);
            var qtdDisponivelMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEDISPONIVEL"]);

            Medicamento m = new Medicamento()
            {
                Id = idMedicamento,
                Nome = nomeMedicamento,
                Descricao = descricaoMedicamento,
                Lote = loteMedicamento,
                Validade = validadeMedicamento,
                QuantidadeDisponivel = qtdDisponivelMedicamento
            };

            return m;
        }

        private Fornecedor ConverterParaFornecedor(SqlDataReader leitorRequisicao)
        {
            var idFornecedor = Convert.ToInt32(leitorRequisicao["ID_FORNECEDOR"]);
            var nomeFornecedor = Convert.ToString(leitorRequisicao["NOME_FORNECEDOR"]);
            var telefoneFornecedor = Convert.ToString(leitorRequisicao["TELEFONE_FORNECEDOR"]);
            var emailFornecedor = Convert.ToString(leitorRequisicao["EMAIL_FORNECEDOR"]);
            var cidadeFornecedor = Convert.ToString(leitorRequisicao["CIDADE_FORNECEDOR"]);
            var estadoFornecedor = Convert.ToString(leitorRequisicao["ESTADO_FORNECEDOR"]);

            return new Fornecedor(nomeFornecedor, telefoneFornecedor, emailFornecedor, cidadeFornecedor, estadoFornecedor);
        }

        #endregion

    }
}
