using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public struct ChronoUnit : ITemporalUnit
    {
        private string _name;
        private TimeSpan? _timeSpan;
        private long? _days;
        private long? _months;
        private long? _years;
        private bool _isDateBased;
        private delegate ITemporal addToDel(ITemporal temporal, long amount);
        private delegate long betweenDel(ITemporal temporal1Inclusive, ITemporal temporal2Exclusive);
        private delegate bool durationEstimatedDel();
        private delegate bool supportedByDel(ITemporal temporal);
        private addToDel _addTo;
        private betweenDel _between;
        private durationEstimatedDel _durationEstimated;
        private supportedByDel _supportedBy;

        private ChronoUnit(string pName, TimeSpan? pTimeSpan, long? pDays, long? pMonths, long? pYears, addToDel pAddTo, betweenDel pBetween, durationEstimatedDel pDurationEstimated, supportedByDel pSupportedBy)
        {
            _name = pName;
            _timeSpan = pTimeSpan;
            _days = pDays;
            _months = pMonths;
            _years = pYears;
            _isDateBased = ! (_timeSpan.HasValue && _timeSpan.Value.TotalSeconds < 86400);
            _addTo = pAddTo;
            _between = pBetween;
            _durationEstimated = pDurationEstimated;
            _supportedBy = pSupportedBy;
        }

        public static readonly ChronoUnit DAYS = new ChronoUnit(nameof(DAYS), null, 1, null, null, 
            null, 
            null,
            () => { return true; }, 
            null);
        public static readonly ChronoUnit WEEKS = new ChronoUnit(nameof(WEEKS), null, 7, null, null, 
            null, 
            null,
            () => { return true; },
            null);
        public static readonly ChronoUnit MONTHS = new ChronoUnit(nameof(MONTHS), null, null, 1, null, 
            null, 
            null,
            () => { return true; },
            null);
        public static readonly ChronoUnit YEARS = new ChronoUnit(nameof(YEARS), null, null, null, 1, 
            null, 
            null,
            () => { return true; },
            null);
        public static readonly ChronoUnit DECADES = new ChronoUnit(nameof(DECADES), null, null, null, 10, 
            null, 
            null,
            () => { return true; },
            null);
        public static readonly ChronoUnit CENTURIES = new ChronoUnit(nameof(CENTURIES), null, null, null, 100, 
            null, 
            null,
            () => { return true; },
            null);
        public static readonly ChronoUnit MILLENNIA = new ChronoUnit(nameof(MILLENNIA), null, null, null, 1000, 
            null, 
            null,
            () => { return true; },
            null);

        public static readonly ChronoUnit HALF_DAYS = new ChronoUnit(nameof(HALF_DAYS), TimeSpan.FromHours(12), null, null, null, 
            null, 
            null,
            () => { return true; },
            null);
        public static readonly ChronoUnit HOURS = new ChronoUnit(nameof(HOURS), TimeSpan.FromHours(1), null, null, null, 
            null, 
            null,
            () => { return false; },
            null);
        public static readonly ChronoUnit MINUTES = new ChronoUnit(nameof(MINUTES), TimeSpan.FromMinutes(1), null, null, null, 
            null, 
            null,
            () => { return false; },
            null);
        public static readonly ChronoUnit SECONDS = new ChronoUnit(nameof(SECONDS), TimeSpan.FromSeconds(1), null, null, null, 
            null, 
            null,
            () => { return false; },
            null);
        public static readonly ChronoUnit MILLIS = new ChronoUnit(nameof(MILLIS), TimeSpan.FromMilliseconds(1), null, null, null, 
            null, 
            null,
            () => { return false; },
            null);
        public static readonly ChronoUnit MICROS = new ChronoUnit(nameof(MICROS), TimeSpan.FromTicks(10000), null, null, null,
            null,
            null,
            () => { return false; },
            null);
        public static readonly ChronoUnit NANOS = new ChronoUnit(nameof(NANOS), TimeSpan.FromTicks(10), null, null, null, 
            null, 
            null,
            () => { return false; },
            null);

        public static readonly ChronoUnit FOREVER = new ChronoUnit(nameof(FOREVER), null, null, null, long.MaxValue, 
            null, 
            null,
            () => { return true; },
            null);

        R ITemporalUnit.addTo<R>(R temporal, long amount)
        {
            return ((R)_addTo(temporal, amount));
        }

        long ITemporalUnit.between(ITemporal temporal1Inclusive, ITemporal temporal2Exclusive)
        {
            return _between(temporal1Inclusive, temporal2Exclusive);
        }

        bool ITemporalUnit.isDateBased()
        {
            return _isDateBased;
        }

        bool ITemporalUnit.isDurationEstimated()
        {
            return _durationEstimated();
        }

        bool ITemporalUnit.isSupportedBy(ITemporal temporal)
        {
            return _supportedBy(temporal);
        }

        bool ITemporalUnit.isTimeBased()
        {
            return !(_isDateBased);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
