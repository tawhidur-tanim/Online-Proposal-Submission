using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFinal101.Persistance.Repositories
{
    public class WeatherRepositoryFake : IWeatherRepository
    {
        private readonly string[] summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private List<WeatherForecast> WeatherForecasts { get; set; }

        public WeatherRepositoryFake()
        {
            Initialize(50);
        }

        private void Initialize(int quantity)
        {
            var rng = new Random();
            WeatherForecasts = Enumerable.Range(1, quantity).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            }).ToList();
        }

        public List<WeatherForecast> GetForecasts()
        {
            return WeatherForecasts;
        }
    }
}
