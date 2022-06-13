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


            RuleFor(x => x.Nome)
                .Custom((nome, context) =>
                {
                    if (nome != null)
                    {
                        if (nome.Length < 5)
                            context.AddFailure("O campo 'Nome do paciente' deve ter no mínimo 5 caracteres!");
                    }
                });

            RuleFor(x => x.Nome)
                .Custom((nome, context) =>
                {
                    if (nome != null)
                    {
                        if (Regex.IsMatch(nome, "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", RegexOptions.IgnoreCase) == false)
                            context.AddFailure("O campo 'Nome do paciente' não deve conter números ou caracteres especiais!");
                    }
                });

            RuleFor(x => x.CartaoSUS)
                .NotNull().WithMessage("O campo 'Cartão SUS' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Cartão SUS' é obrigatório!");

            RuleFor(x => x.CartaoSUS)
                 .Custom((cartaoSUS, context) =>
                 {
                     if (cartaoSUS != null)
                     {
                         if (cartaoSUS.Length != 15)
                             context.AddFailure("O campo 'Cartão SUS' deve ter 15 dígitos!");
                     }
                 });

            RuleFor(x => x.CartaoSUS)
                 .Custom((cartaoSUS, context) =>
                 {
                     if (cartaoSUS != null)
                     {
                         if (Regex.IsMatch(cartaoSUS, "^[0-9]+$", RegexOptions.IgnoreCase) == false)
                             context.AddFailure("O campo 'Cartão SUS' deve conter somente números!");
                     }
                 });
        }
    }
}
