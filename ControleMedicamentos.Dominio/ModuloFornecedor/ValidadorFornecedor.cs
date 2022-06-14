using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
                  .NotNull().WithMessage("O campo 'Nome do fornecedor' é obrigatório!")
                  .NotEmpty().WithMessage("O campo 'Nome do fornecedor' é obrigatório!");

            RuleFor(x => x.Nome)
             .Custom((nome, context) =>
             {
                 if (nome != null)
                 {
                     if (nome.Length < 3)
                         context.AddFailure("O campo 'Nome do fornecedor' deve ter no mínimo 3 caracteres!");
                 }
             });

            RuleFor(x => x.Cidade)
                  .NotNull().WithMessage("O campo 'Cidade do fornecedor' é obrigatório!")
                  .NotEmpty().WithMessage("O campo 'Cidade do fornecedor' é obrigatório!");

            RuleFor(x => x.Cidade)
             .Custom((cidade, context) =>
             {
                 if (cidade != null)
                 {
                     if (cidade.Length < 3)
                         context.AddFailure("O campo 'Cidade do fornecedor' deve ter no mínimo 3 caracteres!");
                 }
             });

            RuleFor(x => x.Estado)
                  .NotNull().WithMessage("O campo 'Estado do fornecedor' é obrigatório!")
                  .NotEmpty().WithMessage("O campo 'Estado do fornecedor' é obrigatório!");

            RuleFor(x => x.Estado)
             .Custom((estado, context) =>
             {
                 if (estado != null)
                 {
                     if (estado.Length != 2)
                         context.AddFailure("O campo 'Estado do fornecedor' deve ter somente 2 caracteres!");
                 }
             });

            RuleFor(x => x.Telefone)
                .NotNull().WithMessage("O campo 'Telefone do fornecedor' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Telefone do fornecedor' é obrigatório!");

            RuleFor(x => x.Telefone)
              .Custom((telefone, context) =>
              {
                  if (telefone != null)
                  {
                      if ((Regex.IsMatch(telefone, @"^\([1-9]{2}\) (?:[2-8]|9 [1-9])[0-9]{3}\-?[0-9]{4}$")) == false)
                          context.AddFailure("O campo 'Telefone do fornecedor' deve conter um telefone válido!");
                  }
              });


            RuleFor(x => x.Email)
                  .NotNull().WithMessage("O campo 'Email do fornecedor' é obrigatório!")
                  .NotEmpty().WithMessage("O campo 'Email do fornecedor' é obrigatório!");


            RuleFor(x => x.Email)
              .Custom((email, context) =>
              {
                  if (string.IsNullOrEmpty(email) == false)
                  {
                      if (System.Net.Mail.MailAddress.TryCreate(email, out _) == false)
                          context.AddFailure("O campo 'Email do fornecedor' deve conter um email válido!");
                  }
              });

        }

    }
}
