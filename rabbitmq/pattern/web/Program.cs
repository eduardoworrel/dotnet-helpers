using rabbitmq;

var builder = WebApplication.CreateBuilder(args);


var client = new CustomRabbitMQClient(
    builder.Configuration["RabbitMQ:HostName"],
    builder.Configuration["RabbitMQ:User"],
    builder.Configuration["RabbitMQ:Password"],
    builder.Configuration["RabbitMQ:VirtualHost"]
);

builder.Services.AddSingleton(client);

var app = builder.Build();

app.MapGet("/", (CustomRabbitMQClient client, string txt ) => {

    try{
        client.Publish("worker", txt);
        return "Sucesso!";

    }catch(Exception ex){
        
        Console.WriteLine(ex.Message);
        return ex.Message;
    }
});

app.Run();
