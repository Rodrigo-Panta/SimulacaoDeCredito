using AutoMapper;
using FluentValidation;
using MediatR;
using SimulacaoDeCredito.Domain.EventPublishers;
using SimulacaoDeCredito.Domain.Factories;
using SimulacaoDeCredito.Domain.Repositories;

namespace SimulacaoDeCredito.Application.Commands.CreateSimulacao;

public class CreateSimulacaoValidator : AbstractValidator<CreateSimulacaoCommand>
{
    public CreateSimulacaoValidator()
    {
        RuleFor(x => x.Prazo).GreaterThan(0).WithMessage("Prazo deve ser maior que 0");
        RuleFor(x => x.ValorDesejado).GreaterThan(0).WithMessage("Valor deve ser maior que 0");
    }
}
