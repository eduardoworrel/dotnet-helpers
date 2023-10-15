using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using rabbitmq;

namespace worker;

public class Worker : BackgroundService
{
    private readonly CustomRabbitMQClient _client;
    private readonly ILogger<Worker> _logger;
    public Worker(
        CustomRabbitMQClient client,
        ILogger<Worker> logger
    )
    {
        _logger = logger;
        _client = client;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    { 
        await _client.Consume("worker", (message)=>{ 
            _logger.LogInformation($" [x] Received {message}");
        });
    }
}
