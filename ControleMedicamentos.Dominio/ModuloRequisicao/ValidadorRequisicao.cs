using FluentValidation;
using System;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Medicamento)
                  .NotNull().WithMessage("O campo 'Medicamento da requisição' é obrigatório!")
                  .NotEmpty().WithMessage("O campo 'Medicamento da requisição' é obrigatório!");

            RuleFor(x => x.Paciente)
                  .NotNull().WithMessage("O campo 'Paciente da requisição' é obrigatório!")
                  .NotEmpty().WithMessage("O campo 'Paciente da requisição' é obrigatório!");

            RuleFor(x => x.Funcionario)
                  .NotNull().WithMessage("O campo 'Funcuionário da requisição' é obrigatório!")
                  .NotEmpty().WithMessage("O campo 'Funcuionário da requisição' é obrigatório!");

            RuleFor(x => x.QtdMedicamento)
                .GreaterThan(0)
                .WithMessage("O campo 'QTD de medicamento da requisição' deve ser no mínimo um!");

            RuleFor(x => x.Data)
                      .NotNull().WithMessage("O campo 'Data da requisição' é obrigatório!")
                      .NotEmpty().WithMessage("O campo 'Data da requisição' é obrigatório!");

            RuleFor(x => x.Data)
                    .Custom((data, context) =>
                    {
                        if (data.Date < DateTime.Now)
                        {
                            context.AddFailure("O campo 'Data da requisição' deve conter uma data váliada!");
                        }
                    });
        }
    }
}
