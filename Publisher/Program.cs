using System;
using System.Text;
using RabbitMQ.Client;

namespace Publisher
{
	class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() {HostName = "localhost"};

			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "FirstQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
				
				string message = "Message from publisher";

				var body = Encoding.UTF8.GetBytes(message);
				channel.BasicPublish("", "FirstQueue", null, body);

				Console.WriteLine("Message is sent to Default Exchange");
			}
		}
	}
}
