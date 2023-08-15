using AccuWeatherWorkService.Models;
using AccuWeatherWorkService.Services;
using Newtonsoft.Json;
using AccuWeatherWorkService.Services;

namespace AccuWeatherWorkService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string apiKey;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        apiKey = System.Environment.GetEnvironmentVariable("AccuWeatherApiKey");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            RabbitMQTransfer messageQueue = new RabbitMQTransfer();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                /*
                using (var httpClient = new HttpClient())
                {
                   using (HttpResponseMessage response = await httpClient.GetAsync("http://dataservice.accuweather.com/forecasts/v1/daily/5day/55488?apikey=" + apiKey,stoppingToken))
                   {
                        string apiResponse = await response.Content.ReadAsStringAsync(stoppingToken);

                        DailyConditions dailyConditions = JsonConvert.DeserializeObject<DailyConditions>(apiResponse);

                        Console.WriteLine("Rate: " + dailyConditions.Headline.Severity);
                   }
                }
                */
                using (var httpClient = new HttpClient())
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync(
                               "http://dataservice.accuweather.com/currentconditions/v1/1113694?apikey=" + apiKey + "&details=true",
                               stoppingToken))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync(stoppingToken);

                        messageQueue.AddMessageToQueue(apiResponse);
                        
                        List<CurrentConditions> currentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(apiResponse);

                        Console.WriteLine("Rate: " + currentConditions[0].LocalObservationDateTime);
                    }
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error found is - " + ex.ToString());    
        }
    }
}