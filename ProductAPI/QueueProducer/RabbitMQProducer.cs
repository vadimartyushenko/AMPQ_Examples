using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ProductAPI.QueueProducer;

public class RabbitMQProducer : IRabbitMQProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        
        //Create the RabbitMQ connection using connection factory
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare("product", exclusive: false);
        //Serialize the message
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        //put the data on to the product queue
        channel.BasicPublish(exchange: "", routingKey: "product", body: body);
    }
}