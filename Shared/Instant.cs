using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public struct Instant : TemporalHelper.ITemporalHelper, IComparable<Instant>
    {
        private DateTimeOffset _Value;

        private Instant(DateTimeOffset Value)
        {
            _Value = Value;
        }

        private static long SecondsOfDayAsTicks(DateTimeOffset _V)
        {
            return ((long)Math.Floor(_V.TimeOfDay.TotalSeconds)) * TimeSpan.TicksPerSecond;
        }

        private static DateTime StartOfYear(DateTimeOffset _V)
        {
            return new DateTime(_V.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        int IComparable<Instant>.CompareTo(Instant other)
        {
            return _Value.CompareTo(other._Value);
        }

        Dictionary<string, Func<long>> TemporalHelper.ITemporalHelper.getHandlers
        {
            get
            {
                DateTimeOffset _V = _Value;
                return new Dictionary<string, Func<long>>()
                {
                    { nameof(ChronoField.YEAR), () => _V.Year },
                    { nameof(ChronoField.MONTH_OF_YEAR), () => _V.Month },
                    { nameof(ChronoField.DAY_OF_YEAR), () => (_V.Date -  StartOfYear(_V)).Days },
                    { nameof(ChronoField.DAY_OF_WEEK), () => (((int)_V.Date.DayOfWeek) == 0 ? 7 : ((int)_V.Date.DayOfWeek)) },
                    { nameof(ChronoField.DAY_OF_MONTH), () => _V.Day },
                    { nameof(ChronoField.CLOCK_HOUR_OF_AMPM), () => (_V.Hour >= 12 ? (_V.Hour == 12 ? 12 : _V.Hour - 12) : (_V.Hour == 0 ? 12 : _V.Hour)) },
                    { nameof(ChronoField.CLOCK_HOUR_OF_DAY), () => (_V.Hour + 1) },
                    { nameof(ChronoField.HOUR_OF_DAY), () => _V.Hour },
                    { nameof(ChronoField.HOUR_OF_AMPM), () => (_V.Hour > 11 ? _V.Hour - 12 : _V.Hour) },
                    { nameof(ChronoField.MINUTE_OF_DAY), () => ((long)(Math.Floor(_V.TimeOfDay.TotalMinutes))) },
                    { nameof(ChronoField.MINUTE_OF_HOUR), () => _V.Minute },
                    { nameof(ChronoField.SECOND_OF_DAY), () => ((long)(Math.Floor(_V.TimeOfDay.TotalSeconds))) },
                    { nameof(ChronoField.SECOND_OF_MINUTE), () => _V.Second },
                    { nameof(ChronoField.MILLI_OF_DAY), () => ((long)(Math.Floor(_V.TimeOfDay.TotalMilliseconds))) },
                    { nameof(ChronoField.MILLI_OF_SECOND), () => _V.Millisecond },
                    { nameof(ChronoField.MICRO_OF_DAY), () => (((long)(_V.TimeOfDay.Ticks)) / (TimeSpan.TicksPerMillisecond / 10)) },
                    { nameof(ChronoField.MICRO_OF_SECOND), () =>  (_V.TimeOfDay.Ticks - SecondsOfDayAsTicks(_V) / (TimeSpan.TicksPerMillisecond / 10))  },
                    { nameof(ChronoField.NANO_OF_DAY), () => (((long)(_V.TimeOfDay.Ticks)) * 100) },
                    { nameof(ChronoField.NANO_OF_SECOND), () =>  (_V.TimeOfDay.Ticks - SecondsOfDayAsTicks(_V) * 100) }
                };
            }
        }

        Dictionary<string, bool> TemporalHelper.ITemporalHelper.isSupportedHandlers
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

        Dictionary<string, Tuple<Func<long, ITemporal>, Func<long, ITemporal>>> TemporalHelper.ITemporalHelper.addSubrtactHandlers
        {
            get
            {
                DateTimeOffset _V = _Value;

                return new Dictionary<string, Tuple<Func<long, ITemporal>, Func<long, ITemporal>>>() {
                    { nameof(ChronoUnit.MILLENNIA), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.CENTURIES), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.DECADES), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.YEARS), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.MONTHS), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.DAYS), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(
                        (v) => new Instant(_V.AddDays(v)),
                        (v) => new Instant(_V.AddDays(-v))) },
                    { nameof(ChronoUnit.HOURS), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.MINUTES), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.SECONDS), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.MILLIS), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.MICROS), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) },
                    { nameof(ChronoUnit.NANOS), new Tuple<Func<long, ITemporal>, Func<long, ITemporal>>(null, null) }
                };
            }
        }

        int ITemporalAccessor.get(ITemporalField field)
        {
            return TemporalHelper.get(this, field);
        }

        long ITemporalAccessor.getLong(ITemporalField field)
        {
            return TemporalHelper.getLong(this, field);
        }

        bool ITemporalAccessor.isSupported(ITemporalField field)
        {
            return TemporalHelper.isSupported(this, field);
        }

        R ITemporalAccessor.query<V, R>(Func<V, R> del)
        {
            return TemporalHelper.query(this, del);
        }

        ValueRange ITemporalAccessor.range(ITemporalField field)
        {
            return TemporalHelper.range(this, field);
        }

        bool ITemporal.isSupported(ITemporalUnit field)
        {
            return TemporalHelper.isSupported(this, field);
        }

        ITemporal ITemporal.minus(long amountToSubtract, ITemporalUnit unit)
        {
            return TemporalHelper.minus(this, amountToSubtract, unit);
        }

        ITemporal ITemporal.minus(ITemporalAmount amount)
        {
            return TemporalHelper.minus(this, amount);
        }

        ITemporal ITemporal.plus(long amountToAdd, ITemporalUnit unit)
        {
            return TemporalHelper.plus(this, amountToAdd, unit);
        }

        ITemporal ITemporal.plus(ITemporalAmount amount)
        {
            return TemporalHelper.plus(this, amount);
        }

        long ITemporal.until(ITemporal endExclusive, ITemporalUnit unit)
        {
            return TemporalHelper.until(this, endExclusive, unit);
        }

        ITemporal ITemporal.with(ITemporalAdjuster adjuster)
        {
            return TemporalHelper.with(this, adjuster);
        }

        ITemporal ITemporal.with(ITemporalField field, long newValue)
        {
            return TemporalHelper.with(this, field, newValue);
        }

        ITemporal ITemporalAdjuster.adjustInto(ITemporal temporal)
        {
            return TemporalHelper.adjustInto(this, temporal);
        }
    }
}
