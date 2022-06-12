namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public abstract class RepositorioBaseDB
    {
        protected const string connectionString =
                 "Data Source=(LocalDB)\\MSSqlLocalDB;" +
                 "Initial Catalog=ControleMedicamentosTrabalho;" +
                 "Integrated Security=True;" +
                 "Pooling=False";
    }
}
