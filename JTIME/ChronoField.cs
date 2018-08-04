using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace JTIME
{
    public struct ChronoField : ITemporalField
    {

        private string _name;
        private string _displayName;
        private ITemporalUnit _baseUnit;
        private ITemporalUnit _rangeUnit;
        private delegate ValueRange rangeDel1();
        private delegate ValueRange rangeDel2(ITemporalAccessor temporal);
        private delegate ITemporal adjustDel(ITemporal temporal, long newValue);
        private delegate ITemporalAccessor resolveDel(IDictionary<ITemporalField, long> fieldValues, ITemporalAccessor partialTemporal, ResolverStyle resolverStyle);
        private delegate long fromDel(ITemporalAccessor temporal);
        private delegate bool supportedByDel(ITemporalAccessor temporal);
        private rangeDel1 _range1;
        private rangeDel2 _range2;
        private adjustDel _adjust;
        private resolveDel _resolve;
        private fromDel _from;
        private supportedByDel _supportedBy;

        private ChronoField(string pName, string pDisplayName, ITemporalUnit pBaseUnit, ITemporalUnit pRangeUnit, rangeDel1 pRange1, rangeDel2 pRange2, adjustDel pAdjust, resolveDel pResolve, fromDel pFrom, supportedByDel pSupportedBy) {
            _name = pName;
            _displayName = pDisplayName;
            _baseUnit = pBaseUnit;
            _rangeUnit = pRangeUnit;
            _range1 = pRange1;
            _range2 = pRange2;
            _adjust = pAdjust;
            _resolve = pResolve;
            _from = pFrom;
            _supportedBy = pSupportedBy;
        }

        private static ValueRange rangeDayOfMonth(ITemporalAccessor temporal)
        {
            int max = 31;
            switch (temporal.get(MONTH_OF_YEAR))
            {
                case 2:
                    if (DateTime.IsLeapYear(temporal.get(YEAR)))
                    {
                        max = 29;
                    }
                    else
                    {
                        max = 28;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    max = 30;
                    break;
            }

            return ValueRange.of(1, 28, max);
        }

        private static ValueRange rangeDayOfYear(ITemporalAccessor temporal)
        {

            return ValueRange.of(1, 365, (DateTime.IsLeapYear(temporal.get(YEAR)) ? 366 : 365));
        }

        public static readonly ChronoField DAY_OF_MONTH = new ChronoField(nameof(DAY_OF_MONTH), "Day of month", ChronoUnit.DAYS, ChronoUnit.MONTHS, 
            () => { return ValueRange.of(1, 28, 31); }, 
            (x) => { return rangeDayOfMonth(x); }, 
            null, 
            null, 
            null, 
            null);
        public static readonly ChronoField DAY_OF_YEAR = new ChronoField(nameof(DAY_OF_YEAR), "Day of year", ChronoUnit.DAYS, ChronoUnit.YEARS,
            () => { return ValueRange.of(1, 365, 366); },
            (x) => { return rangeDayOfYear(x); }, 
            null, 
            null, 
            null, 
            null);
        public static readonly ChronoField EPOCH_DAY = new ChronoField(nameof(EPOCH_DAY), "Epoch day", ChronoUnit.DAYS, ChronoUnit.FOREVER,
            () => { return ValueRange.of(1, long.MaxValue); },
            (x) => { return ValueRange.of(1, long.MaxValue); }, 
            null, 
            null, 
            null, 
            null);
        public static readonly ChronoField HOUR_OF_DAY = new ChronoField(nameof(HOUR_OF_DAY), "XXX", ChronoUnit.HOURS, ChronoUnit.DAYS,
            () => { return ValueRange.of(0, 23); },
            (x) => { return ValueRange.of(0, 23); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField INSTANT_SECONDS = new ChronoField(nameof(INSTANT_SECONDS), "XXX", ChronoUnit.SECONDS, ChronoUnit.FOREVER,
            () => { return ValueRange.of(0, long.MaxValue); },
            (x) => { return ValueRange.of(0, long.MaxValue); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField NANO_OF_SECOND = new ChronoField(nameof(NANO_OF_SECOND), "XXX", ChronoUnit.NANOS, ChronoUnit.SECONDS,
            () => { return ValueRange.of(0, 1000000000-1); },
            (x) => { return ValueRange.of(0, 1000000000 - 1); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField NANO_OF_DAY = new ChronoField(nameof(NANO_OF_DAY), "XXX", ChronoUnit.NANOS, ChronoUnit.DAYS,
            () => { return ValueRange.of(0, 86400 * 1000000000L - 1); },
            (x) => { return ValueRange.of(0, 86400 * 1000000000L - 1); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField MICRO_OF_SECOND = new ChronoField(nameof(MICRO_OF_SECOND), "XXX", ChronoUnit.MILLIS, ChronoUnit.SECONDS,
            () => { return ValueRange.of(0, 1000000 - 1); },
            (x) => { return ValueRange.of(0, 1000000 - 1); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField MICRO_OF_DAY = new ChronoField(nameof(MICRO_OF_DAY), "XXX", ChronoUnit.MICROS, ChronoUnit.DAYS,
            () => { return ValueRange.of(0, 86400 * 1000000L - 1); },
            (x) => { return ValueRange.of(0, 86400 * 1000000L - 1); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField MILLI_OF_SECOND = new ChronoField(nameof(MILLI_OF_SECOND), "XXX", ChronoUnit.MILLIS, ChronoUnit.SECONDS,
            () => { return ValueRange.of(0, 1000 - 1); },
            (x) => { return ValueRange.of(0, 1000 - 1); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField MILLI_OF_DAY = new ChronoField(nameof(MILLI_OF_DAY), "XXX", ChronoUnit.MILLIS, ChronoUnit.DAYS,
            () => { return ValueRange.of(0, 86400 * 1000L - 1); },
            (x) => { return ValueRange.of(0, 86400 * 1000L - 1); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField SECOND_OF_MINUTE = new ChronoField(nameof(SECOND_OF_MINUTE), "XXX", ChronoUnit.SECONDS, ChronoUnit.MINUTES,
            () => { return ValueRange.of(0, 59); },
            (x) => { return ValueRange.of(0, 59); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField SECOND_OF_DAY = new ChronoField(nameof(SECOND_OF_DAY), "XXX", ChronoUnit.SECONDS, ChronoUnit.DAYS,
            () => { return ValueRange.of(0, 86400 - 1); },
            (x) => { return ValueRange.of(0, 86400 - 1); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField MINUTE_OF_HOUR = new ChronoField(nameof(MINUTE_OF_HOUR), "XXX", ChronoUnit.MINUTES, ChronoUnit.HOURS,
            () => { return ValueRange.of(0, 59); },
            (x) => { return ValueRange.of(0, 59); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField MINUTE_OF_DAY = new ChronoField(nameof(MINUTE_OF_DAY), "XXX", ChronoUnit.MINUTES, ChronoUnit.DAYS,
            () => { return ValueRange.of(0, 1440 - 1); },
            (x) => { return ValueRange.of(0, 1440 - 1); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField HOUR_OF_AMPM = new ChronoField(nameof(HOUR_OF_AMPM), "XXX", ChronoUnit.HOURS, ChronoUnit.HALF_DAYS,
            () => { return ValueRange.of(0, 11); },
            (x) => { return ValueRange.of(0, 11); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField CLOCK_HOUR_OF_AMPM = new ChronoField(nameof(CLOCK_HOUR_OF_AMPM), "XXX", ChronoUnit.HOURS, ChronoUnit.HALF_DAYS,
            () => { return ValueRange.of(1, 12); },
            (x) => { return ValueRange.of(1, 12); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField CLOCK_HOUR_OF_DAY = new ChronoField(nameof(CLOCK_HOUR_OF_DAY), "XXX", ChronoUnit.HOURS, ChronoUnit.DAYS,
            () => { return ValueRange.of(1, 24); },
            (x) => { return ValueRange.of(1, 24); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField AMPM_OF_DAY = new ChronoField(nameof(AMPM_OF_DAY), "XXX", ChronoUnit.HALF_DAYS, ChronoUnit.DAYS,
            () => { return ValueRange.of(0, 1); },
            (x) => { return ValueRange.of(0, 1); },
            null,
            null,
            null,
            null);
        //  Monday (1) to Sunday (7)
        public static readonly ChronoField DAY_OF_WEEK = new ChronoField(nameof(DAY_OF_WEEK), "XXX", ChronoUnit.DAYS, ChronoUnit.WEEKS,
            () => { return ValueRange.of(1, 7); },
            (x) => { return ValueRange.of(1, 7); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField MONTH_OF_YEAR = new ChronoField(nameof(MONTH_OF_YEAR), "XXX", ChronoUnit.MONTHS, ChronoUnit.YEARS,
            () => { return ValueRange.of(1, 12); },
            (x) => { return ValueRange.of(1, 12); },
            null,
            null,
            null,
            null);
        public static readonly ChronoField YEAR = new ChronoField(nameof(YEAR), "YEAR", ChronoUnit.YEARS, ChronoUnit.FOREVER,
            () => { return ValueRange.of(0, long.MaxValue); },
            (x) => { return ValueRange.of(0, long.MaxValue); },
            null,
            null,
            null,
            null);

        R ITemporalField.adjustInto<R>(R temporal, long newValue)
        {
            return ((R)_adjust(temporal, newValue));
        }

        ITemporalUnit ITemporalField.getBaseUnit()
        {
            return _baseUnit;
        }

        string ITemporalField.getDisplayName()
        {
            return ((ITemporalField)this).getDisplayName(CultureInfo.CurrentCulture);
        }

        string ITemporalField.getDisplayName(CultureInfo locale)
        {
            return _displayName;
        }

        long ITemporalField.getFrom(ITemporalAccessor temporal)
        {
            return _from(temporal);
        }

        ITemporalUnit ITemporalField.getRangeUnit()
        {
            return _rangeUnit;
        }

        bool ITemporalField.isDateBased()
        {
            return _baseUnit.isDateBased(); 
        }

        bool ITemporalField.isSupportedBy(ITemporalAccessor temporal)
        {
            return _supportedBy(temporal);
        }

        bool ITemporalField.isTimeBased()
        {
            return _baseUnit.isTimeBased();
        }

        ValueRange ITemporalField.range()
        {
            return _range1();
        }

        ValueRange ITemporalField.rangeRefinedBy(ITemporalAccessor temporal)
        {
            return _range2(temporal);
        }

        ITemporalAccessor ITemporalField.resolve(IDictionary<ITemporalField, long> fieldValues, ITemporalAccessor partialTemporal, ResolverStyle resolverStyle)
        {
            return _resolve(fieldValues, partialTemporal, resolverStyle);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
