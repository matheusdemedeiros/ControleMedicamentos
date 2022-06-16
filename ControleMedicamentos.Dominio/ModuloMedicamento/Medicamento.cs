using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public Medicamento()
        {
            Requisicoes = new List<Requisicao>();
        }

        public Medicamento(string nome, string descricao, string lote,
            DateTime validade, Fornecedor fornecedor, int quantidadeDisponivel)
        {
            Nome = nome;
            Descricao = descricao;
            Lote = lote;
            Validade = validade;
            Fornecedor = fornecedor;
            QuantidadeDisponivel = quantidadeDisponivel;
            Requisicoes = new List<Requisicao>();

        }

        public string Nome { get; set; }
        
        public string Descricao { get; set; }
        
        public string Lote { get; set; }
        
        public DateTime Validade { get; set; }
        
        public int QuantidadeDisponivel { get; set; }

        public List<Requisicao> Requisicoes { get; set; }

        public Fornecedor Fornecedor{ get; set; }

        public int QuantidadeRequisicoes { get { return Requisicoes.Count; } }

        public override string ToString()
        {
            return $"Id {Id} - Nome: {Nome} - Descrição: {Descricao} - Lote: {Lote} - Validade: {Validade} - Fornecedor: {Fornecedor.Nome}";
        }

        public override bool Equals(object obj)
        {

            bool retorno = false;

            retorno = obj is Medicamento medicamento &&
                   Id == medicamento.Id &&
                   Nome == medicamento.Nome &&
                   Descricao == medicamento.Descricao &&
                   Lote == medicamento.Lote &&
                   Validade == medicamento.Validade &&
                   QuantidadeDisponivel == medicamento.QuantidadeDisponivel &&
                   QuantidadeRequisicoes == medicamento.QuantidadeRequisicoes;

            return retorno;
        
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Nome);
            hash.Add(Descricao);
            hash.Add(Lote);
            hash.Add(Validade);
            hash.Add(QuantidadeDisponivel);
            hash.Add(Requisicoes);
            hash.Add(Fornecedor);
            hash.Add(QuantidadeRequisicoes);
            return hash.ToHashCode();
        }
    }
}
