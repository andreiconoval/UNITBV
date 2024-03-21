using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.BusinessLayer.Facade.Utilities
{
    /// <summary>
    /// A utility class holding a list of price ranges. The price ranges are  
    /// used in building search criteria for the product catalog. 
    /// </summary>
    public static class PriceRange
    {
        private static IList<PriceRangeItem> _list = null;

        /// <summary>
        /// Static constructor for PriceRange.
        /// </summary>
        static PriceRange()
        {
            _list = new List<PriceRangeItem>();

            _list.Add(new PriceRangeItem(0, 0, 0, ""));
            _list.Add(new PriceRangeItem(1, 0, 50, "$0 - $50"));
            _list.Add(new PriceRangeItem(2, 51, 100, "$51 - $100"));
            _list.Add(new PriceRangeItem(3, 101, 250, "$101 - $250"));
            _list.Add(new PriceRangeItem(4, 251, 1000, "$251 - $1,000"));
            _list.Add(new PriceRangeItem(5, 1001, 2000, "$1,001 - $2,000"));
            _list.Add(new PriceRangeItem(6, 2001, 10000, "$2,001 - $10,000"));
        }

        /// <summary>
        /// Gets the list of price ranges.
        /// </summary>
        public static IList<PriceRangeItem> List
        {
            get { return _list; }
        }
    }
}
