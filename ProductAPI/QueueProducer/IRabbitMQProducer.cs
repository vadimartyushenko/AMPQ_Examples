namespace ProductAPI.QueueProducer;

public interface IRabbitMQProducer
{
    public void SendMessage<T>(T message);
}