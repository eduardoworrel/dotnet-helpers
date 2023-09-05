using worker;
using rabbitmq;

var builder = Host.CreateApplicationBuilder(args);

var client = new CustomRabbitMQClient(
    builder.Configuration["RabbitMQ:HostName"],
    builder.Configuration["RabbitMQ:User"],
    builder.Configuration["RabbitMQ:Password"],
    builder.Configuration["RabbitMQ:VirtualHost"]
);
builder.Services.AddSingleton(client);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
