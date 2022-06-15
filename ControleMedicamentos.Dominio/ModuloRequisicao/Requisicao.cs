using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class Requisicao : EntidadeBase<Requisicao>
    {
        public Requisicao()
        {

        }

        public Medicamento Medicamento { get; set; }

        public Paciente Paciente { get; set; }

        public int QtdMedicamento { get; set; }

        public DateTime Data { get; set; }

        public Funcionario Funcionario { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Requisicao requisicao &&
                   Id == requisicao.Id &&
                   EqualityComparer<Medicamento>.Default.Equals(Medicamento, requisicao.Medicamento) &&
                   EqualityComparer<Paciente>.Default.Equals(Paciente, requisicao.Paciente) &&
                   QtdMedicamento == requisicao.QtdMedicamento &&
                   Data == requisicao.Data &&
                   EqualityComparer<Funcionario>.Default.Equals(Funcionario, requisicao.Funcionario);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Medicamento, Paciente, QtdMedicamento, Data, Funcionario);
        }

        public override string ToString()
        {
            return $"ID: {Id} - Data da requisição: {Data.ToShortDateString()} - Medicamento: {Medicamento.Nome}" +
                $" - Qtd medicamento: {QtdMedicamento} - Paciente: {Paciente.Nome} - Funcionário: {Funcionario.Nome}";
        }
    }
}
