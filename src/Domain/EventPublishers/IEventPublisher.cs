namespace SimulacaoDeCredito.Domain.EventPublishers;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default);
}