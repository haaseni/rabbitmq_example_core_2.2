using System;
using System.Text;
using RabbitMQ.Client;

namespace ConsoleSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SENDER");

            var connFactory = new ConnectionFactory();
            connFactory.HostName = "localhost";

            using (var connection = connFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "msgKey", durable: false, exclusive: false, autoDelete: false, arguments: null);

                Console.WriteLine("Enter message to send:");

                var message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "msgKey", basicProperties: null, body: body);

                Console.WriteLine($"-- Sent {message}");
            }

            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }
    }
}
