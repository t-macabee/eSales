﻿using EasyNetQ;
using eSales.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//var factory = new ConnectionFactory { HostName = "localhost" };
//using var connection = factory.CreateConnection();
//using var channel = connection.CreateModel();

//channel.QueueDeclare(queue: "product_added",
//                                durable: false,
//                                exclusive: false,
//                                autoDelete: false,
//                                arguments: null);
//Console.WriteLine(" [*] Waiting for messages.");

//var consumer = new EventingBasicConsumer(channel);
//consumer.Received += (model, ea) =>
//{
//    var body = ea.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine($" [x] Recieved {message}");
//};
//channel.BasicConsume(queue: "product_added",
//                     autoAck: true,
//                     consumer: consumer);

//Console.WriteLine(" Press [enter] to exit");
//Console.ReadLine();

Console.WriteLine("Provide subscriptionId: ");
var subscriptionId = Console.ReadLine();

using (var bus = RabbitHutch.CreateBus("host=localhost"))
{
    bus.PubSub.Subscribe<Proizvodi>(subscriptionId, HandleTextMessage);
    Console.WriteLine("Listening for messages. Hit <return> to quit.");
    Console.ReadLine();
}

void HandleTextMessage(Proizvodi entity)
{
    Console.WriteLine($"Recieved: {entity?.ProizvodId}, {entity?.Naziv}");
}