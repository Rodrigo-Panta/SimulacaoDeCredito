namespace SimulacaoDeCredito.Application.Profiles;

using AutoMapper;
using SimulacaoDeCredito.Application.DTOs.Response;
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

        // Data Models
        CreateMap<Simulacao, SimulacaoModel>()
        .ForMember(dest => dest.SimulacaoTabelas, opt => opt.MapFrom(src => src.ResultadoSimulacao));

        CreateMap<SimulacaoTabela, SimulacaoTabelaModel>()
        .ForMember(dest => dest.SimulacaoParcelas, opt => opt.MapFrom(src => src.Parcelas));

        CreateMap<SimulacaoParcela, SimulacaoParcelaModel>();
        CreateMap<SimulacaoModel, Simulacao>();
        CreateMap<SimulacaoTabelaModel, SimulacaoTabela>();
        CreateMap<SimulacaoParcelaModel, SimulacaoParcela>();
    }
}