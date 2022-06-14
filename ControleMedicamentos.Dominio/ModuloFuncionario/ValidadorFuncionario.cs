using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario : AbstractValidator<Funcionario>
    {

        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
                  .NotNull().WithMessage("O campo 'Nome do funcionário' é obrigatório!")
                  .NotEmpty().WithMessage("O campo 'Nome do funcionário' é obrigatório!");


            RuleFor(x => x.Nome)
                .Custom((nome, context) =>
                {
                    if (nome != null)
                    {
                        if (nome.Length < 4)
                            context.AddFailure("O campo 'Nome do funcionário' deve ter no mínimo 4 caracteres!");
                    }
                });

            RuleFor(x => x.Nome)
                .Custom((nome, context) =>
                {
                    if (nome != null)
                    {
                        if (Regex.IsMatch(nome, "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", RegexOptions.IgnoreCase) == false)
                            context.AddFailure("O campo 'Nome do funcionário' não deve conter números ou caracteres especiais!");
                    }
                });


            RuleFor(x => x.Login)
                 .NotNull().WithMessage("O campo 'Login do funcionário' é obrigatório!")
                 .NotEmpty().WithMessage("O campo 'Login do funcionário' é obrigatório!");


            RuleFor(x => x.Login)
               .Custom((login, context) =>
               {
                   if (string.IsNullOrEmpty(login) == false)
                   {
                       if (login.Length < 5)
                           context.AddFailure("O campo 'Login do funcionário' deve ter no mínimo 5 caracteres!");
                   }
               });

            RuleFor(x => x.Senha)
                 .NotNull().WithMessage("O campo 'Senha do funcionário' é obrigatório!")
                 .NotEmpty().WithMessage("O campo 'Senha do funcionário' é obrigatório!");


            RuleFor(x => x.Senha)
               .Custom((senha, context) =>
               {
                   if (string.IsNullOrEmpty(senha) == false)
                   {
                       if (senha.Length < 5)
                           context.AddFailure("O campo 'Senha do funcionário' deve ter no mínimo 5 caracteres!");
                   }
               });
        }
    }
}
