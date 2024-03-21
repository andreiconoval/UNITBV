using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Observer.WeatherStationObservable
{
    class WeatherStationHeatIndex
    {
        static void Main(string[] args)
        {
            WeatherData weatherData = new WeatherData();

            CurrentConditionsDisplay currentConditions = new CurrentConditionsDisplay(weatherData);
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

    #region Observable

    // This is what the Java built-in Observable class 
    // roughly looks like (except for the generic List)
    public class Observable
    {
        private bool changed;
        private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public bool Changed
        {
            set{ changed = value; }
        }

        public void NotifyObservers()
        {
            foreach(IObserver observer in observers)
            {
                observer.Update(this);
            }
        }
    }

    public class WeatherData : Observable 
    {
        private float temperature;
        private float humidity;
        private float pressure;
    
        public void MeasurementsChanged() 
        {
            Changed = true;
            NotifyObservers();
        }
    
        public void SetMeasurements(float temperature, float humidity, float pressure) 
        {
            this.temperature = temperature;
            this.humidity = humidity;
            this.pressure = pressure;
            MeasurementsChanged();
        }
    
        public float Temperature
        {
            get{ return temperature; }
        }
    
        public float Humidity
        {
            get{ return humidity; }
        }
    
        public float Pressure
        {
            get{ return pressure; }
        }
    }

    #endregion

    #region Observer

    // This is what the Java built-in Observer interface 
    // roughly looks like
    public interface IObserver 
    {
        void Update(object subject);
    }

    public interface IDisplayElement 
    {
        void Display();
    }

    public class ForecastDisplay : IObserver, IDisplayElement 
    {
        private float currentPressure = 29.92f;  
        private float lastPressure;

        public ForecastDisplay(Observable observable) 
        {
            observable.AddObserver(this);
        }

        public void Update(object subject) 
        {
            if (subject is WeatherData) 
            {
                WeatherData weatherData = (WeatherData)subject;
                lastPressure = currentPressure;
                currentPressure = weatherData.Pressure;
                Display();
            }
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

        public HeatIndexDisplay(Observable observable) 
        {
            observable.AddObserver(this);
        }

        public void Update(object subject) 
        {
            if (subject is WeatherData) 
            {
                WeatherData weatherData = (WeatherData)subject;
                float t = weatherData.Temperature;
                float rh = weatherData.Humidity;
                heatIndex = (float)
                    (
                    (16.923 + (0.185212 * t)) + 
                    (5.37941 * rh) - 
                    (0.100254 * t * rh) + 
                    (0.00941695 * (t * t)) + 
                    (0.00728898 * (rh * rh)) + 
                    (0.000345372 * (t * t * rh)) - 
                    (0.000814971 * (t * rh * rh)) +
                    (0.0000102102 * (t * t * rh * rh)) - 
                    (0.000038646 * (t * t * t)) + 
                    (0.0000291583 * (rh * rh * rh)) +
                    (0.00000142721 * (t * t * t * rh)) + 
                    (0.000000197483 * (t * rh * rh * rh)) - 
                    (0.0000000218429 * (t * t * t * rh * rh)) +
                    (0.000000000843296 * (t * t * rh * rh * rh)) -
                    (0.0000000000481975 * (t * t * t * rh * rh * rh)));
                Display();
            }
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
        private float tempSum= 0.0f;
        private int numReadings;

        public StatisticsDisplay(Observable observable) 
        {
            observable.AddObserver(this);
        }

        public void Update(object subject) 
        {
            if (subject is WeatherData) 
            {
                WeatherData weatherData = (WeatherData)subject;
                float temp = weatherData.Temperature;
                tempSum += temp;
                numReadings++;

                if (temp > maxTemp) 
                {
                    maxTemp = temp;
                }
 
                if (temp < minTemp) 
                {
                    minTemp = temp;
                }

                Display();
            }
        }

        public void Display() 
        {
            Console.WriteLine("Avg/Max/Min temperature = " + (tempSum / numReadings)
                + "/" + maxTemp + "/" + minTemp);
        }
    }

    public class CurrentConditionsDisplay : IObserver, IDisplayElement 
    {
        Observable observable;
        private float temperature;
        private float humidity;
    
        public CurrentConditionsDisplay(Observable observable) 
        {
            this.observable = observable;
            observable.AddObserver(this);
        }
    
        public void Update(object subject) 
        {
            if (subject is WeatherData) 
            {
                WeatherData weatherData = (WeatherData)subject;
                this.temperature = weatherData.Temperature;
                this.humidity = weatherData.Humidity;
                Display();
            }
        }
    
        public void Display()
        {
            Console.WriteLine("Current conditions: " + temperature 
                + "F degrees and " + humidity + "% humidity");
        }
    }
    #endregion
}
