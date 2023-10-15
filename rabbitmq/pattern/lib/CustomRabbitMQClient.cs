
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace rabbitmq;

public class CustomRabbitMQClient
{
    private readonly ConnectionFactory factory;
    public CustomRabbitMQClient(
        string hostName,
        string userName,
        string password,
        string virtualHost
    )
    {
        factory = new ConnectionFactory() { 
            HostName = hostName,
            UserName = userName,
            Password = password,
            VirtualHost = virtualHost
        };
    }
    public void Publish(
        string queue,
        string message,
        string exchange = ""
        )
    {
        using(var connection = factory.CreateConnection()) 
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: exchange,
                                routingKey: queue,
                                basicProperties: null,
                                body: body);
        
        }
    }

    public async Task Consume(
        string queue,
        Action<string> callback
        )
    {
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        
        channel.QueueDeclare(queue: queue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            callback(message);
        };

        consumer.Shutdown += (model, ea) =>
        {
            channel.Close();
        };
        await Task.Run(()=>{
    
            channel.BasicConsume(queue: queue,
                                autoAck: true,
                                consumer: consumer);
        });
    
    }
}
