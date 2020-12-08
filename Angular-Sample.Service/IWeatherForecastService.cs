using Angular_Sample.Service.Models;
using System.Collections.Generic;

namespace Angular_Sample.Service
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}
