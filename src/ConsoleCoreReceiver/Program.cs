using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsoleReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RECEIVER");

            var connFactory = new ConnectionFactory();
            connFactory.HostName = "localhost";

            using (var connection = connFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "msgKey", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" -- Received Message: {message}");
                };

                channel.BasicConsume(queue: "msgKey", autoAck: true, consumer: consumer);

                Console.WriteLine("Press Enter to Exit");
                Console.ReadLine();
            }
        }
    }
}
