using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public abstract class BaseTemporal : ITemporalAccessor, ITemporalAdjuster, ITemporal
    {
        public abstract Dictionary<string, Func<long>> getHandlers
        {
            get;
        }

        public abstract Dictionary<string, bool> isSupportedHandlers
        {
            get;
        }

        public virtual Dictionary<string, Func<ValueRange>> rangeHandlers
        {
            get
            {
                return new Dictionary<string, Func<ValueRange>>() {
                    { nameof(ChronoField.YEAR), () => ((ITemporalField)ChronoField.YEAR).rangeRefinedBy(this) },
                    { nameof(ChronoField.MONTH_OF_YEAR), () => ((ITemporalField)ChronoField.MONTH_OF_YEAR).rangeRefinedBy(this) },
                    { nameof(ChronoField.DAY_OF_YEAR), () => ((ITemporalField)ChronoField.DAY_OF_YEAR).rangeRefinedBy(this) },
                    { nameof(ChronoField.DAY_OF_WEEK), () => ((ITemporalField)ChronoField.DAY_OF_WEEK).rangeRefinedBy(this) },
                    { nameof(ChronoField.DAY_OF_MONTH), () => ((ITemporalField)ChronoField.DAY_OF_MONTH).rangeRefinedBy(this) },
                    { nameof(ChronoField.CLOCK_HOUR_OF_AMPM), () => ((ITemporalField)ChronoField.CLOCK_HOUR_OF_AMPM).rangeRefinedBy(this) },
                    { nameof(ChronoField.CLOCK_HOUR_OF_DAY), () => ((ITemporalField)ChronoField.CLOCK_HOUR_OF_DAY).rangeRefinedBy(this) },
                    { nameof(ChronoField.HOUR_OF_DAY), () => ((ITemporalField)ChronoField.HOUR_OF_DAY).rangeRefinedBy(this) },
                    { nameof(ChronoField.HOUR_OF_AMPM), () => ((ITemporalField)ChronoField.HOUR_OF_AMPM).rangeRefinedBy(this) },
                    { nameof(ChronoField.MINUTE_OF_DAY), () => ((ITemporalField)ChronoField.MINUTE_OF_DAY).rangeRefinedBy(this) },
                    { nameof(ChronoField.MINUTE_OF_HOUR), () => ((ITemporalField)ChronoField.MINUTE_OF_HOUR).rangeRefinedBy(this) },
                    { nameof(ChronoField.SECOND_OF_DAY), () => ((ITemporalField)ChronoField.SECOND_OF_DAY).rangeRefinedBy(this) },
                    { nameof(ChronoField.SECOND_OF_MINUTE), () => ((ITemporalField)ChronoField.SECOND_OF_MINUTE).rangeRefinedBy(this) },
                    { nameof(ChronoField.MILLI_OF_DAY), () => ((ITemporalField)ChronoField.MILLI_OF_DAY).rangeRefinedBy(this) },
                    { nameof(ChronoField.MILLI_OF_SECOND), () => ((ITemporalField)ChronoField.MILLI_OF_SECOND).range() },
                    { nameof(ChronoField.MICRO_OF_DAY), () => ((ITemporalField)ChronoField.MICRO_OF_DAY).rangeRefinedBy(this) },
                    { nameof(ChronoField.MICRO_OF_SECOND), () => ((ITemporalField)ChronoField.MICRO_OF_SECOND).rangeRefinedBy(this) },
                    { nameof(ChronoField.NANO_OF_DAY), () => ((ITemporalField)ChronoField.NANO_OF_DAY).rangeRefinedBy(this) },
                    { nameof(ChronoField.NANO_OF_SECOND), () => ((ITemporalField)ChronoField.NANO_OF_SECOND).rangeRefinedBy(this) }
                };
            }
        }

        public virtual int get(ITemporalField field)
        {
            int? rslt = getIntFromLong(getLong(field));
            if (rslt.HasValue)
            {
                return rslt.Value;
            }
            throw new NotImplementedException();
        }

        public virtual long getLong(ITemporalField field)
        {
            if (field is ChronoField)
            {
                long? rslt = get(getHandlers, (ChronoField)field);
                if (rslt.HasValue)
                {
                    return rslt.Value;
                }
            }
            throw new ArgumentException("Invalid argument type", nameof(field));
        }

        public virtual bool isSupported(ITemporalField field)
        {
            if (field is ChronoField)
            {
                return isSupported(isSupportedHandlers, (ChronoField)field);
            }
            throw new ArgumentException("Invalid argument type", nameof(field));
        }

        public virtual R query<V, R>(Func<V, R> del) where V : ITemporalAccessor
        {
            return del((V)(ITemporalAccessor)this);
        }

        public abstract ITemporal adjustInto(ITemporal temporal);

        public virtual bool isSupported(ITemporalUnit field)
        {
            if (field is ChronoUnit)
            {
                return isSupported(isSupportedHandlers, (ChronoUnit)field);
            }
            throw new ArgumentException("Invalid argument type", nameof(field));
        }

        public virtual ITemporal minus(long amountToSubtract, ITemporalUnit unit)
        {
            return ((ITemporal)this).minus(new Duration(amountToSubtract, unit));
        }

        public virtual ITemporal minus(ITemporalAmount amount)
        {
            return amount.subtractFrom(this);
        }

        public virtual ITemporal plus(long amountToSubtract, ITemporalUnit unit)
        {
            return ((ITemporal)this).plus(new Duration(amountToSubtract, unit));
        }

        public virtual ITemporal plus(ITemporalAmount amount)
        {
            return amount.addTo(this);
        }

        public abstract long until(ITemporal endExclusive, ITemporalUnit unit);

        public abstract ITemporal with(ITemporalAdjuster adjuster);

        public abstract ITemporal with(ITemporalField field, long newValue);

        public virtual ValueRange range(ITemporalField field)
        {
            if (field is ChronoField)
            {
                ValueRange rslt = range(rangeHandlers, (ChronoField)field);
                if (rslt != null)
                {
                    return rslt;
                }
            }
            throw new ArgumentException("Invalid argument type", nameof(field));
        }

        protected long? get(Dictionary<string, Func<long>> getHandler, ChronoField field)
        {
            if (getHandler.ContainsKey(field.ToString()))
            {
                return getHandler[field.ToString()]();
            }

            return null;
        }

        protected bool isSupported(Dictionary<string, bool> isSupportedHandler, ChronoField field)
        {
            if (isSupportedHandler.ContainsKey(field.ToString()))
            {
                return isSupportedHandler[field.ToString()];
            }

            return false;
        }

        protected bool isSupported(Dictionary<string, bool> isSupportedHandler, ChronoUnit field)
        {
            if (isSupportedHandler.ContainsKey(field.ToString()))
            {
                return isSupportedHandler[field.ToString()];
            }

            return false;
        }

        protected ValueRange range(Dictionary<string, Func<ValueRange>> rangeHandler, ChronoField field)
        {
            if (rangeHandler.ContainsKey(field.ToString()))
            {
                return rangeHandler[field.ToString()]();
            }

            return null;
        }

        protected int? getIntFromLong(long? v)
        {
            if (! v.HasValue)
            {
                return null;
            }

            if ((v.Value < int.MinValue) || (v.Value > int.MaxValue))
            {
                return null;
            }

            return ((int)v.Value);
        }

    }
}
