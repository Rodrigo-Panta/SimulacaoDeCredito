using System.Text;
using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using SimulacaoDeCredito.Domain.EventPublishers;

namespace SimulacaoDeCredito.Infra.EventPublishers;

public class EventHubPublisher : IEventPublisher
{
    private readonly EventHubProducerClient _producer;

    public EventHubPublisher(string connectionString)
    {
        _producer = new EventHubProducerClient(connectionString);
    }

    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(@event);
        using EventDataBatch batch = await _producer.CreateBatchAsync(cancellationToken);
        batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(json)));
        await _producer.SendAsync(batch, cancellationToken);
    }
}