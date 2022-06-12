namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public Paciente()
        {

        }

        public Paciente(string nome, string cartaoSUS)
        {
            this.Nome = nome;
            this.CartaoSUS = cartaoSUS;
        }

        public string Nome { get; set; }
        
        public string CartaoSUS { get; set; }

        public override string ToString()
        {
            return $"Id {Id} - Nome; {Nome} - Cartão SUS: {CartaoSUS}";
        }

    }

}
