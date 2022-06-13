using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public ValidadorFornecedor()
        {
            //RuleFor(x => x.Nome)
            //      .NotNull().WithMessage("O campo 'Nome do fornecedor' é obrigatório!")
            //      .NotEmpty().WithMessage("O campo 'Nome do fornecedor' é obrigatório!");

            //RuleFor(x => x.Nome.Length)
            //    .GreaterThan(4)
            //    .WithMessage("O campo 'Nome do fornecedor' deve ter no mínimo 5 caracteres!");

            //RuleFor(x => Regex.IsMatch(x.Nome, "[^A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", RegexOptions.IgnoreCase))
            //.NotEqual(true)
            //.WithMessage("O campo 'Nome do fornecedor' não deve conter números ou caracteres especiais!");

            //RuleFor(x => x.CartaoSUS)
            //    .NotNull().WithMessage("O campo 'Cartão SUS' é obrigatório!")
            //    .NotEmpty().WithMessage("O campo 'Cartão SUS' é obrigatório!");

            //RuleFor(x => x.CartaoSUS.Length)
            //    .Equal(15)
            //    .WithMessage("O campo 'Cartão SUS' deve ter somente 15 dígitos!");

            //RuleFor(x => Regex.IsMatch(x.CartaoSUS, "[^0-9]+", RegexOptions.IgnoreCase))
            //    .NotEqual(true)
            //    .WithMessage("O campo 'Cartão SUS' deve conter somente números!");

        }

    }
}
