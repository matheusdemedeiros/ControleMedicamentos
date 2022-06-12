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

        public override string ToString()
        {
            return $"ID: {Id} - Data da requisição: {Data.ToShortDateString()} - Medicamento: {Medicamento.Nome}" +
                $" - Qtd medicamento: {QtdMedicamento} - Paciente: {Paciente.Nome} - Funcionário: {Funcionario.Nome}"; 
        }

    }
}
