using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                   .NotNull().WithMessage("O campo 'Nome do paciente' é obrigatório!")
                   .NotEmpty().WithMessage("O campo 'Nome do paciente' é obrigatório!");

            RuleFor(x => x.Nome.Length)
                .GreaterThan(4)
                .WithMessage("O campo 'Nome do paciente' deve ter no mínimo 5 caracteres!");

            RuleFor(x => Regex.IsMatch(x.Nome, "[^A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", RegexOptions.IgnoreCase))
            .NotEqual(true)
            .WithMessage("O campo 'Nome do paciente' não deve conter números ou caracteres especiais!");

            RuleFor(x => x.CartaoSUS)
                .NotNull().WithMessage("O campo 'Cartão SUS' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Cartão SUS' é obrigatório!");

            RuleFor(x => x.CartaoSUS.Length)
                .Equal(15)
                .WithMessage("O campo 'Cartão SUS' deve ter somente 15 dígitos!");

            RuleFor(x => Regex.IsMatch(x.CartaoSUS, "[^0-9]+", RegexOptions.IgnoreCase))
                .NotEqual(true)
                .WithMessage("O campo 'Cartão SUS' deve conter somente números!");

        }
    }
}
