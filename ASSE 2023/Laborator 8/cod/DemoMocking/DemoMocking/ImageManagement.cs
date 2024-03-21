using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoMocking
{
    public class ImageManagement
    {
        /// <summary>Gets the image for time of day.</summary>
        /// <param name="time">The time for which the image is requested.</param>
        /// <returns>
        ///   Path to image corresponding to given time.
        /// </returns>
        public string GetImageForTimeOfDay(IDateTime time)
        {
            int currentHour = time.GetHour();

            if (currentHour >= 7 && currentHour <= 20)
            {
                return "sun.jpg";
            }
            else
            {
                return "moon.jpg";
            }
        }
    }

}
