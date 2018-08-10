using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public class ValueRange
    {
        private long _min;
        private long _minSmallest;
        private long _minLargest;
        private long _max;
        private long _maxSmallest;
        private long _maxLargest;
        public delegate long? checkValidValueDel(long value, ITemporalField field);
        public event checkValidValueDel checkValidValueEvent;

        private ValueRange(long pMinSmallest, long pMinLargest, long pMin, long pMaxSmallest, long pMaxLargest, long pMax)
        {
            _minSmallest = pMinSmallest;
            _minLargest = pMinLargest;
            _min = pMin;
            _maxSmallest = pMaxSmallest;
            _maxLargest = pMaxLargest;
            _max = pMax;
        }

        public static ValueRange of(long pMin, long pMax)
        {
            return new ValueRange(pMin, pMin, pMin, pMax, pMax, pMax);
        }

        public static ValueRange of(long pMin, long pMaxSmallest, long pMaxLargest)
        {
            return new ValueRange(pMin, pMin, pMin, pMaxSmallest, pMaxLargest, pMaxLargest);
        }

        public static ValueRange of(long pMinSmallest, long pMinLargest, long pMaxSmallest, long pMaxLargest)
        {
            return new ValueRange(pMinSmallest, pMinLargest, pMinSmallest, pMaxSmallest, pMaxLargest, pMaxLargest);
        }

        public long getLargestMinimum()
        {
            return _minLargest;
        }

        public long getMinimum()
        {
            return _min;
        }

        public long getSmallestMaximum()
        {
            return _maxSmallest;
        }

        public long getMaximum()
        {
            return _max;
        }

        public bool isFixed()
        {
            return ((_minSmallest == _minLargest) && (_maxSmallest == _maxLargest));
        }

        public bool isIntValue()
        {
            long min = Math.Min(_min, Math.Min(_minSmallest, _minLargest));
            long max = Math.Max(_max, Math.Max(_maxSmallest, _maxLargest));

            if (min < int.MinValue)
                return false;

            if (max > int.MaxValue)
                return false;

            return true;
        }

        public bool isValidValue(long value)
        {
            return ((value >= _min) && (value <= _max));
        }

        public bool isValidIntValue(long value)
        {
            return ((isIntValue()) && (isValidValue(value)));
        }

        public long? checkValidValue(long value, ITemporalField field)
        {
            if (checkValidValueEvent != null) return checkValidValueEvent(value, field);

            if ((value < _min) || (value > _max))
                return null;

            return value;
        }

        public int? checkValidIntValue(long value, ITemporalField field)
        {
            if (! isValidIntValue(value))
                return null;

            long? rslt = checkValidValue(value, field);

            if (! rslt.HasValue)
                return null;

            return ((int)rslt.Value);
        }

        public override string ToString()
        {
            return $"{_min}<->{_max}";
        }

    }
}
