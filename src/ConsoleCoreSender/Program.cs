using System;
using System.Text;
using RabbitMQ.Client;

namespace ConsoleSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, this is the sender application!");

            var factory = new ConnectionFactory {HostName = "localhost"};

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "msgKey",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                Console.WriteLine("Enter message to send:");

                var message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "msgKey",
                    basicProperties: null,
                    body: body);

                Console.WriteLine($" [x] Sent {message}");
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
