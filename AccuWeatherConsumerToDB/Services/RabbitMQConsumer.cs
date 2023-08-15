using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace AccuWeatherConsumerToDB.Services;

public class RabbitMQConsumer
{
    private ConnectionFactory factory;
    private string messageQueueEndPoint;
    private string messageQueueUserName;
    private string messageQueuePassword;
    
    public RabbitMQConsumer()
    {
        messageQueueEndPoint = System.Environment.GetEnvironmentVariable("RabbitMQServiceEndPoint");
        messageQueueUserName = System.Environment.GetEnvironmentVariable("RabbitMQServiceUserName");
        messageQueuePassword = System.Environment.GetEnvironmentVariable("RabbitMQServicePassword");

        //ConnectionFactory factory = new();
        factory = new();
        factory.Uri = new Uri("amqp://" + messageQueueUserName 
                                        +":"+ messageQueuePassword + "@" + messageQueueEndPoint);
    
        factory.ClientProvidedName = "AccuWeather Consumer to DB";
        
    }

    public int ReadMessageFromQueue()
    {
        IConnection cnn = factory.CreateConnection();

        IModel channel = cnn.CreateModel();

        string exchangeName = "DemoExchange";
        string routingKey = "demo-routing-key";
        string queueName = "DemoQueue";

        channel.ExchangeDeclare(exchangeName,ExchangeType.Direct);
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName,exchangeName,routingKey,null);
        channel.BasicQos(0,1,false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, args) =>
        {
            var body = args.Body.ToArray();

            string message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Message received: {message}");

            channel.BasicAck(args.DeliveryTag, false);

        };

        string consumerTag = channel.BasicConsume(queueName,false,consumer);

        Console.ReadLine(); 

        channel.BasicCancel(consumerTag);

        channel.Close();
        cnn.Close();

        return 1;
    }
}