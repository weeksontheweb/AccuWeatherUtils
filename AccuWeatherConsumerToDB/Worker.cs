using AccuWeatherConsumerToDB.Services;

namespace AccuWeatherConsumerToDB;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            RabbitMQConsumer messageQueue = new RabbitMQConsumer();
            messageQueue.ReadMessageFromQueue();


            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}