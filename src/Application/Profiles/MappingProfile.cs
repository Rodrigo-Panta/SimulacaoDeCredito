namespace SimulacaoDeCredito.Application.Profiles;

using AutoMapper;
using SimulacaoDeCredito.Application.DTOs.Response;
using SimulacaoDeCredito.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Simulacao, CreateSimulacaoResponseDto>();
        CreateMap<SimulacaoTabela, SimulacaoTabelaResponseDto>();
        CreateMap<SimulacaoParcela, SimulacaoParcelaResponseDto>();
    }
}