using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public static class DB
    {

        private const string connectionString =
                 "Data Source=(LocalDB)\\MSSqlLocalDB;" +
                 "Initial Catalog=ControleMedicamentosTrabalho;" +
                 "Integrated Security=True;" +
                 "Pooling=False";

        public static void ExecutarSql(string sql)
        {
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand(sql, conexaoComBanco);
            conexaoComBanco.Open();
            comando.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

    }
}
