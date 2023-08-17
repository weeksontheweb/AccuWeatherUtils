using System.Text;
using RabbitMQ.Client;

namespace AccuWeatherWorkService.Services;

public class RabbitMQProducer
{
    private ConnectionFactory factory;
    private string messageQueueEndPoint;
    private string messageQueueUserName;
    private string messageQueuePassword;
    
    public RabbitMQProducer()
    {
        messageQueueEndPoint = System.Environment.GetEnvironmentVariable("RabbitMQServiceEndPoint");
        messageQueueUserName = System.Environment.GetEnvironmentVariable("RabbitMQServiceUserName");
        messageQueuePassword = System.Environment.GetEnvironmentVariable("RabbitMQServicePassword");

        //ConnectionFactory factory = new();
        factory = new();
        factory.Uri = new Uri("amqp://" + messageQueueUserName 
                                        +":"+ messageQueuePassword + "@" + messageQueueEndPoint);
    
        factory.ClientProvidedName = "Rabbit Sender App";

       
    }

    public int AddMessageToQueue(string message)
    {
        IConnection cnn = factory.CreateConnection();

        IModel channel = cnn.CreateModel();

        string exchangeName = "DemoExchange";
        string routingKey = "demo-routing-key";
        string queueName = "DemoQueue";

        channel.ExchangeDeclare(exchangeName,ExchangeType.Direct);
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName,exchangeName,routingKey,null);

        //byte[] messageBodyBytes = Encoding.UTF8.GetBytes("Hello");
        byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchangeName,routingKey,null,messageBodyBytes);

        channel.Close();
        cnn.Close();

        return 1;
    }
}