namespace SimulacaoDeCredito.Application.Profiles;

using AutoMapper;
using SimulacaoDeCredito.Application.Commands.CreateSimulacao;
using SimulacaoDeCredito.Application.Queries.GetSimulacoesPaginadas;
using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.src.Infra.BaseSimulacao.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Dtos
        CreateMap<Simulacao, CreateSimulacaoResponseDto>();
        CreateMap<SimulacaoTabela, SimulacaoTabelaResponseDto>();
        CreateMap<SimulacaoParcela, SimulacaoParcelaResponseDto>();

        CreateMap<Simulacao, GetSimulacaoResponseDto>();


        // Data Models
        CreateMap<Simulacao, SimulacaoModel>()
        .ForMember(dest => dest.SimulacaoTabelas, opt => opt.MapFrom(src => src.ResultadoSimulacao));

        CreateMap<SimulacaoTabela, SimulacaoTabelaModel>()
        .ForMember(dest => dest.SimulacaoParcelas, opt => opt.MapFrom(src => src.Parcelas));

        CreateMap<SimulacaoParcela, SimulacaoParcelaModel>();

        CreateMap<SimulacaoModel, Simulacao>()
        .ForMember(dest => dest.ResultadoSimulacao, opt => opt.MapFrom(src => src.SimulacaoTabelas));

        CreateMap<SimulacaoTabelaModel, SimulacaoTabela>();

        CreateMap<SimulacaoParcelaModel, SimulacaoParcela>();
    }
}