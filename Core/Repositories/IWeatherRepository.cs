using System.Collections.Generic;
using ProjectFinal101.Core.Models;

namespace ProjectFinal101.Core.Repositories
{
    public interface IWeatherRepository
    {
        List<WeatherForecast> GetForecasts();
    }
}
