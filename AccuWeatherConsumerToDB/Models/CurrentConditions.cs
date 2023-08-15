using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuWeatherConsumerToDB.Models;

internal class CurrentConditions
{
    
    public string LocalObservationDateTime { get; set; }
    
    public int EpochTime { get; set; }
    public string? WeatherText { get; set; }
    public int WeatherIcon { get; set; }
    public bool HasPrecipitation { get; set; }
    public object PrecipitationType { get; set; }
    public bool IsDayTime { get; set; }
    public Temperature Temperature { get; set; }
    public Temperature RealFeelTemperature { get; set; }
    public Temperature RealFeelTemperatureShade { get; set; }
    public int RelativeHumidity { get; set; }
    public int IndoorRelativeHumidity { get; set; }
    public Temperature DewPoint { get; set; }
    public Wind Wind { get; set; }
    public Speed WindGust { get; set; }
    public int UVIndex { get; set; }
    public string UVIndexText { get; set; }
    public Temperature Visibility { get; set; }
    public string ObstructionsToVisibility { get; set; }
    public int CloudCover { get; set; }
    public Temperature Ceiling { get; set; }
    public Temperature Pressure { get; set; }
    public PressureTendency PressureTendency { get; set; }
    public Temperature Past24HourTemperatureDeparture { get; set; }
    public Temperature ApparentTemperature { get; set; }
    public Temperature WindChillTemperature { get; set; }
    public Temperature WetBulbTemperature { get; set; }
    public Temperature Precip1hr { get; set; }
    public PrecipitationSummary PrecipitationSummary { get; set; }
    public TemperatureSummary TemperatureSummary { get; set; }
    
    public string MobileLink { get; set; }
    public string Link { get; set; }
}

public class Temperature
{
    public MetricUnit Metric { get; set; }
    public ImperialUnit Imperial { get; set; }
}

public class MetricUnit
{
    public double Value { get; set; }
    public string Unit { get; set; }
    public int UnitType { get; set; }
}

public class ImperialUnit
{
    public double Value { get; set; }
    public string Unit { get; set; }
    public int UnitType { get; set; }
}

public class Wind
{
    public Direction Direction { get; set; }
    public Speed Speed { get; set; }
}

public class Direction
{
    public int Degrees { get; set; }
    public string Localized { get; set; }
    public string English { get; set; }
}

public class Speed
{
    public MetricUnit Metric { get; set; }
    public ImperialUnit Imperial { get; set; }
}

public class PressureTendency
{
    public string LocalizedText { get; set; }
    public string Code { get; set; }
}

public class PrecipitationSummary
{
    public Temperature Precipitation { get; set; }
    public Temperature PastHour { get; set; }
    public Temperature Past3Hours { get; set; }
    public Temperature Past6Hours { get; set; }
    public Temperature Past9Hours { get; set; }
    public Temperature Past12Hours { get; set; }
    public Temperature Past18Hours { get; set; }
    public Temperature Past24Hours { get; set; }
}

public class TemperatureSummary
{
    public TemperatureRange Past6HourRange { get; set; }
    public TemperatureRange Past12HourRange { get; set; }
    public TemperatureRange Past24HourRange { get; set; }
}

public class TemperatureRange
{
    public Temperature Minimum { get; set; }
    public Temperature Maximum { get; set; }
}



