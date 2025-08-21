public record ResultadoPaginadoDto<T>(
    int pagina,
    int qtdRegistrosPagina,
    int qtdRegistros,
    IEnumerable<T> registros
);