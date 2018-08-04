using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public class Instant : BaseTemporal, ITemporal, IComparable<Instant>
    {
        private DateTimeOffset _Value;
        private Dictionary<string, Func<long>> _getHandlers;

        private long SecondsOfDayAsTicks
        {
            get
            {
                return ((long)Math.Floor(_Value.TimeOfDay.TotalSeconds)) * TimeSpan.TicksPerSecond;
            }
        }

        private DateTime StartOfYear
        {
            get
            {
                return new DateTime(_Value.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        int IComparable<Instant>.CompareTo(Instant other)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, Func<long>> getHandlers
        {
            get
            {
                return new Dictionary<string, Func<long>>()
                {
                    { nameof(ChronoField.YEAR), () => _Value.Year },
                    { nameof(ChronoField.MONTH_OF_YEAR), () => _Value.Month },
                    { nameof(ChronoField.DAY_OF_YEAR), () => (_Value.Date -  StartOfYear).Days },
                    { nameof(ChronoField.DAY_OF_WEEK), () => (((int)_Value.Date.DayOfWeek) == 0 ? 7 : ((int)_Value.Date.DayOfWeek)) },
                    { nameof(ChronoField.DAY_OF_MONTH), () => _Value.Day },
                    { nameof(ChronoField.CLOCK_HOUR_OF_AMPM), () => (_Value.Hour >= 12 ? (_Value.Hour == 12 ? 12 : _Value.Hour - 12) : (_Value.Hour == 0 ? 12 : _Value.Hour)) },
                    { nameof(ChronoField.CLOCK_HOUR_OF_DAY), () => (_Value.Hour + 1) },
                    { nameof(ChronoField.HOUR_OF_DAY), () => _Value.Hour },
                    { nameof(ChronoField.HOUR_OF_AMPM), () => (_Value.Hour > 11 ? _Value.Hour - 12 : _Value.Hour) },
                    { nameof(ChronoField.MINUTE_OF_DAY), () => ((long)(Math.Floor(_Value.TimeOfDay.TotalMinutes))) },
                    { nameof(ChronoField.MINUTE_OF_HOUR), () => _Value.Minute },
                    { nameof(ChronoField.SECOND_OF_DAY), () => ((long)(Math.Floor(_Value.TimeOfDay.TotalSeconds))) },
                    { nameof(ChronoField.SECOND_OF_MINUTE), () => _Value.Second },
                    { nameof(ChronoField.MILLI_OF_DAY), () => ((long)(Math.Floor(_Value.TimeOfDay.TotalMilliseconds))) },
                    { nameof(ChronoField.MILLI_OF_SECOND), () => _Value.Millisecond },
                    { nameof(ChronoField.MICRO_OF_DAY), () => (((long)(_Value.TimeOfDay.Ticks)) / (TimeSpan.TicksPerMillisecond / 10)) },
                    { nameof(ChronoField.MICRO_OF_SECOND), () =>  (_Value.TimeOfDay.Ticks - SecondsOfDayAsTicks / (TimeSpan.TicksPerMillisecond / 10))  },
                    { nameof(ChronoField.NANO_OF_DAY), () => (((long)(_Value.TimeOfDay.Ticks)) * 100) },
                    { nameof(ChronoField.NANO_OF_SECOND), () =>  (_Value.TimeOfDay.Ticks - SecondsOfDayAsTicks * 100) }
                };
            }
        }

        public override Dictionary<string, bool> isSupportedHandlers
        {
            get
            {
                return new Dictionary<string, bool>()
                {
                    { nameof(ChronoField.YEAR), true },
                    { nameof(ChronoField.MONTH_OF_YEAR), true },
                    { nameof(ChronoField.DAY_OF_YEAR), true },
                    { nameof(ChronoField.DAY_OF_WEEK), true },
                    { nameof(ChronoField.DAY_OF_MONTH), true },
                    { nameof(ChronoField.CLOCK_HOUR_OF_AMPM), true },
                    { nameof(ChronoField.CLOCK_HOUR_OF_DAY), true },
                    { nameof(ChronoField.HOUR_OF_DAY), true },
                    { nameof(ChronoField.HOUR_OF_AMPM), true },
                    { nameof(ChronoField.MINUTE_OF_DAY), true },
                    { nameof(ChronoField.MINUTE_OF_HOUR), true },
                    { nameof(ChronoField.SECOND_OF_DAY), true },
                    { nameof(ChronoField.SECOND_OF_MINUTE), true },
                    { nameof(ChronoField.MILLI_OF_DAY), true },
                    { nameof(ChronoField.MILLI_OF_SECOND), true },
                    { nameof(ChronoField.MICRO_OF_DAY), true },
                    { nameof(ChronoField.MICRO_OF_SECOND), true },
                    { nameof(ChronoField.NANO_OF_DAY), true },
                    { nameof(ChronoField.NANO_OF_SECOND), true },
                    { nameof(ChronoUnit.DAYS), true },
                    { nameof(ChronoUnit.WEEKS), true },
                    { nameof(ChronoUnit.MONTHS), true },
                    { nameof(ChronoUnit.YEARS), true },
                    { nameof(ChronoUnit.DECADES), true },
                    { nameof(ChronoUnit.CENTURIES), true },
                    { nameof(ChronoUnit.MILLENNIA), true },
                    { nameof(ChronoUnit.HALF_DAYS), true },
                    { nameof(ChronoUnit.HOURS), true },
                    { nameof(ChronoUnit.MINUTES), true },
                    { nameof(ChronoUnit.SECONDS), true },
                    { nameof(ChronoUnit.MILLIS), true },
                    { nameof(ChronoUnit.MICROS), true },
                    { nameof(ChronoUnit.NANOS), true }
                };
            }
        }

        public override ITemporal adjustInto(ITemporal temporal)
        {
            throw new NotImplementedException();
        }

        public override bool isSupported(ITemporalUnit field)
        {
            throw new NotImplementedException();
        }

        public override long until(ITemporal endExclusive, ITemporalUnit unit)
        {
            throw new NotImplementedException();
        }

        public override ITemporal with(ITemporalAdjuster adjuster)
        {
            throw new NotImplementedException();
        }

        public override ITemporal with(ITemporalField field, long newValue)
        {
            throw new NotImplementedException();
        }
    }
}
