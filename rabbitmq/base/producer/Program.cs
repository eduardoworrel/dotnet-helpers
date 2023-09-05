using System;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { 
    HostName = "jackal.rmq.cloudamqp.com",
    UserName = "njrnlxts",
    Password = "Mi2HXkBMfAvO4-qj39L_fbHQhbJ2OPO6",
    VirtualHost = "njrnlxts"
};

using(var connection = factory.CreateConnection()) //disposable
using(var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

    string message = "Hello World!";
    
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "",
                        routingKey: "worker",
                        basicProperties: null,
                        body: body);

    Console.WriteLine(" [x] Sent {0}", message);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();