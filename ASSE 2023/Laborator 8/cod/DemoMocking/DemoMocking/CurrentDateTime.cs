using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoMocking
{
    class CurrentDateTime : IDateTime
    {
        #region IDateTime Members

        /// <summary>Gets the hour.</summary>
        /// <returns>
        ///   Current hour.
        /// </returns>
        public int GetHour()
        {
            return DateTime.Now.Hour;
        }

        #endregion
    }
}
