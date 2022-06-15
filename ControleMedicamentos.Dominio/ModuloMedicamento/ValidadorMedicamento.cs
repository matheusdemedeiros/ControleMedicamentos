using FluentValidation;
using System;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
                      .NotNull().WithMessage("O campo 'Nome do medicamento' é obrigatório!")
                      .NotEmpty().WithMessage("O campo 'Nome do medicamento' é obrigatório!");

            RuleFor(x => x.Nome)
                    .Custom((nome, context) =>
                    {
                        if (nome != null)
                        {
                            if (nome.Length < 5)
                                context.AddFailure("O campo 'Nome do medicamento' deve ter no mínimo 5 caracteres!");
                        }
                    });

            RuleFor(x => x.Descricao)
                      .NotNull().WithMessage("O campo 'Descrição do medicamento' é obrigatório!")
                      .NotEmpty().WithMessage("O campo 'Descrição do medicamento' é obrigatório!");

            RuleFor(x => x.Descricao)
                    .Custom((descricao, context) =>
                    {
                        if (descricao != null)
                        {
                            if (descricao.Length < 5)
                                context.AddFailure("O campo 'Descrição do medicamento' deve ter no mínimo 5 caracteres!");
                        }
                    });

            RuleFor(x => x.Lote)
                      .NotNull().WithMessage("O campo 'Lote do medicamento' é obrigatório!")
                      .NotEmpty().WithMessage("O campo 'Lote do medicamento' é obrigatório!");

            RuleFor(x => x.Lote)
                    .Custom((lote, context) =>
                    {
                        if (lote != null)
                        {
                            if (lote.Length < 3)
                                context.AddFailure("O campo 'Lote do medicamento' deve ter no mínimo 3 caracteres!");
                        }
                    });

            RuleFor(x => x.Validade)
                      .NotNull().WithMessage("O campo 'Validade do medicamento' é obrigatório!")
                      .NotEmpty().WithMessage("O campo 'Validade do medicamento' é obrigatório!");

            RuleFor(x => x.Validade)
                    .Custom((validade, context) =>
                    {
                        if (validade.Date < DateTime.Now.Date)
                        {
                            context.AddFailure("O campo 'Validade do medicamento' deve conter uma data váliada!");
                        }
                    });

            RuleFor(x => x.Fornecedor)
                      .NotNull().WithMessage("O campo 'Fornecedor do medicamento' é obrigatório!")
                      .NotEmpty().WithMessage("O campo 'Fornecedor do medicamento' é obrigatório!");
        }
    }
}
