using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPacienteDB : RepositorioBaseDB, IRepositorioPaciente
    {

        #region SQL QUERIES

        private const string sqlInserir =
            @"INSERT INTO [TBPACIENTE]
                (
		            [NOME],
                    [CARTAOSUS]
		        )
            VALUES
                (
		            @NOME,
		            @CARTAO_SUS
		        );SELECT SCOPE_IDENTITY();";

       
        private const string sqlEditar =
            @"UPDATE[TBPACIENTE]
		        SET
			        [NOME] = @NOME,
			        [CARTAOSUS] = @CARTAO_SUS
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBPACIENTE]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
             @"SELECT 
		            [ID], 
		            [NOME],
                    [CARTAOSUS]
	            FROM 
		            [TBPACIENTE]";

        private const string sqlSelecionarPorId =
            @"SELECT 
		            [ID], 
		            [NOME],
                    [CARTAOSUS]
	            FROM 
		            [TBPACIENTE]
		        WHERE
                    [ID] = @ID";

        #endregion


        public ValidationResult Inserir(Paciente novoRegistro)
        {
            var validador = ObterValidador();
            
            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosPaciente(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();

            novoRegistro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Paciente registro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosPaciente(registro, comandoEdicao);

            comandoEdicao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();

            comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Paciente registro)
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

        public Paciente SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            Paciente paciente = null;

            if (leitorPaciente.Read())
                paciente = ConverterParaPaciente(leitorPaciente);

            conexaoComBanco.Close();

            return paciente;
        }


        public List<Paciente> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            List<Paciente> pacientes = new List<Paciente>();

            while (leitorPaciente.Read())
            {
                Paciente paciente = ConverterParaPaciente(leitorPaciente);

                pacientes.Add(paciente);
            }

            conexaoComBanco.Close();

            return pacientes;
        }

        #region Métodos privados

        private ValidadorPaciente ObterValidador()
        {
            return new ValidadorPaciente();
        }

        private void ConfigurarParametrosPaciente(Paciente novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NOME", novoRegistro.Nome);
            comando.Parameters.AddWithValue("CARTAO_SUS", novoRegistro.CartaoSUS);
        }

        private Paciente ConverterParaPaciente(SqlDataReader leitorPaciente)
        {
            var id = Convert.ToInt32(leitorPaciente["ID"]);
            var nome = Convert.ToString(leitorPaciente["NOME"]);
            var cartaoSUS = Convert.ToString(leitorPaciente["CARTAOSUS"]);
            
            var paciente = new Paciente()
            {
                Id = id,
                Nome = nome,
                CartaoSUS =cartaoSUS
            };

            return paciente;
        }
        
        #endregion
    }
}

