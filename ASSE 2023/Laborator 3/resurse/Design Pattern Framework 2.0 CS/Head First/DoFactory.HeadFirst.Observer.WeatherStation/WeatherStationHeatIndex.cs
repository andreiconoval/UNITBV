using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Observer.WeatherStation
{
    class WeatherStationHeatIndex
    {
        static void Main(string[] args)
        {
            WeatherData weatherData = new WeatherData();

            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherData);
            StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
            ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);
            HeatIndexDisplay heatIndexDisplay = new HeatIndexDisplay(weatherData);

            weatherData.SetMeasurements(80, 65, 30.4f);
            weatherData.SetMeasurements(82, 70, 29.2f);
            weatherData.SetMeasurements(78, 90, 29.2f);

            // Wait for user
            Console.Read();
        }
    }

    #region Subject

    public interface ISubject 
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    public class WeatherData : ISubject 
    {
        private List<IObserver> observers = new List<IObserver>();
        private float temperature;
        private float humidity;
        private float pressure;

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(IObserver observer in observers)
            {
                observer.Update(temperature, humidity, pressure);
            }
        }

        public void MeasurementsChanged()
        {
            NotifyObservers();
        }

        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            this.pressure = pressure;
            MeasurementsChanged();
        }
    }
    #endregion

    #region Observer

    public interface IObserver 
    {
        void Update(float temperature, float humidity, float pressure);
    }

    public interface IDisplayElement 
    {
        void Display();
    }

    public class CurrentConditionsDisplay : IObserver, IDisplayElement 
    {
        private float temperature;
        private float humidity;
        private ISubject weatherData;

        public CurrentConditionsDisplay(ISubject weatherData)
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            Display();
        }

        public void Display()
        {
            Console.WriteLine("Current conditions: " + temperature 
                + "F degrees and " + humidity + "% humidity");
        }
    }

    public class ForecastDisplay : IObserver, IDisplayElement 
    {
        private float currentPressure = 29.92f;  
        private float lastPressure;
        private WeatherData weatherData;

        public ForecastDisplay(WeatherData weatherData) 
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure) 
        {
            lastPressure = currentPressure;
            currentPressure = pressure;

            Display();
        }

        public void Display() 
        {
            Console.Write("Forecast: ");

            if (currentPressure > lastPressure) 
            {
                Console.WriteLine("Improving weather on the way!");
            } 
            else if (currentPressure == lastPressure) 
            {
                Console.WriteLine("More of the same");
            } 
            else if (currentPressure < lastPressure) 
            {
                Console.WriteLine("Watch out for cooler, rainy weather");
            }
        }
    }

    public class HeatIndexDisplay : IObserver, IDisplayElement 
    {
        float heatIndex = 0.0f;
        private WeatherData weatherData;

        public HeatIndexDisplay(WeatherData weatherData) 
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure) 
        {
            heatIndex = ComputeHeatIndex(temperature, humidity);
            Display();
        }
    
        private float ComputeHeatIndex(float t, float rh) 
        {
            float heatindex = (float)((16.923 + (0.185212 * t) + (5.37941 * rh) - (0.100254 * t * rh) 
                + (0.00941695 * (t * t)) + (0.00728898 * (rh * rh)) 
                + (0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) +
                (0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 * 
                (rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) + 
                (0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) +
                0.000000000843296 * (t * t * rh * rh * rh)) -
                (0.0000000000481975 * (t * t * t * rh * rh * rh)));
            return heatindex;
        }

        public void Display() 
        {
            Console.WriteLine("Heat index is " + heatIndex + "\n");
        }
    }

    public class StatisticsDisplay : IObserver, IDisplayElement 
    {
        private float maxTemp = 0.0f;
        private float minTemp = 200;
        private float tempSum = 0.0f;
        private int numReadings;
        private WeatherData weatherData;

        public StatisticsDisplay(WeatherData weatherData) 
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure) 
        {
            tempSum += temperature;
            numReadings++;

            if (temperature > maxTemp) 
            {
                maxTemp = temperature;
            }
 
            if (temperature < minTemp) 
            {
                minTemp = temperature;
            }

            Display();
        }

        public void Display() 
        {
            Console.WriteLine("Avg/Max/Min temperature = " + (tempSum / numReadings)
                + "/" + maxTemp + "/" + minTemp);
        }
    }

    #endregion
}