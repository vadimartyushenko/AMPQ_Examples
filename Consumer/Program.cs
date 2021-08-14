using RabbitMQ.Client;
using System;
using System.Text;
using RabbitMQ.Client.Events;

namespace Consumer
{
	class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			var queueName = "FirstQueue";

			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

				var consumer = new EventingBasicConsumer(channel);

				consumer.Received += (sender, e) =>
				{
					var body = e.Body;
					var message = Encoding.UTF8.GetString(body.ToArray());
					Console.WriteLine(" Received message: {0}", message);
				};

				channel.BasicConsume(queueName, true, consumer);

				Console.WriteLine("Subscribed to the queue '{0}'", queueName);
			}
		}

	}
}
