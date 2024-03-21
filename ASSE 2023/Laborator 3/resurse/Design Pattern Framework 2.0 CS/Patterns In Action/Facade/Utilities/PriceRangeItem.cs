using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.BusinessLayer.Facade.Utilities
{
    /// <summary>
    /// A PriceRange item used in the PriceRange list.  PriceRanges are used for 
    /// searching the product catalog. 
    /// </summary>
    public class PriceRangeItem
    {
        private int _rangeId;
        private double _rangeFrom;
        private double _rangeThru;
        private string _rangeText;

        /// <summary>
        /// Constructor for PriceRangeItem.
        /// </summary>
        /// <param name="rangeId">Unique identifier for the price range.</param>
        /// <param name="rangeFrom">Lower end of the price range.</param>
        /// <param name="rangeThru">Higher end of the price range.</param>
        /// <param name="rangeText">Easy-to-read form of the price range.</param>
        public PriceRangeItem(int rangeId, double rangeFrom, double rangeThru, string rangeText)
        {
            this._rangeId = rangeId;
            this._rangeFrom = rangeFrom;
            this._rangeThru = rangeThru;
            this._rangeText = rangeText;
        }

        /// <summary>
        /// Gets the unique PriceRange identifier.
        /// </summary>
        public int RangeId
        {
            get { return _rangeId; }
        }

        /// <summary>
        /// Gets the low end of the PriceRange item.
        /// </summary>
        public double RangeFrom
        {
            get { return _rangeFrom; }
        }

        /// <summary>
        /// Gets the high end of the PriceRange item.
        /// </summary>
        public double RangeThru
        {
            get { return _rangeThru; }
        }

        /// <summary>
        /// Gets an easy-to-read form of the PriceRange item.
        /// </summary>
        public string RangeText
        {
            get { return _rangeText; }
        }
    }
}
