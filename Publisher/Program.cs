using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Publisher
{
	class Program
	{
		static void Main(string[] args)
		{
			var count = 0;
			do
			{

				var timeToSleep = new Random().Next(1000, 3000);//величина задержки  (1...3 сек) 
				Thread.Sleep(timeToSleep);

				var factory = new ConnectionFactory() {HostName = "localhost"};

				using (var connection = factory.CreateConnection())
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare(queue: "FirstQueue", durable: false, exclusive: false, autoDelete: false,
						arguments: null);

					string message = $"Message from publisher [N: {count}]";

					var body = Encoding.UTF8.GetBytes(message);
					channel.BasicPublish("", "FirstQueue", null, body);

					Console.WriteLine($"Message is sent to Default Exchange [N: {count++}]");
				}
			} while (true);
		}
	}
}
