using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public static class TemporalHelper 
    {
        public interface ITemporalHelper : ITemporal, ITemporalAdjuster
        {
            Dictionary<string, Func<long>> getHandlers { get; }
            Dictionary<string, bool> isSupportedHandlers { get; }
            Dictionary<string, Tuple<Func<long, ITemporal>, Func<long, ITemporal>>> addSubrtactHandlers { get; }
        }

        public struct rangeHandlerStruct
        {
            public Dictionary<string, Func<ValueRange>> this[ITemporalHelper me]
            {
                get
                {
                        return new Dictionary<string, Func<ValueRange>>() {
                        { nameof(ChronoField.YEAR), () => ((ITemporalField)ChronoField.YEAR).rangeRefinedBy(me) },
                        { nameof(ChronoField.MONTH_OF_YEAR), () => ((ITemporalField)ChronoField.MONTH_OF_YEAR).rangeRefinedBy(me) },
                        { nameof(ChronoField.DAY_OF_YEAR), () => ((ITemporalField)ChronoField.DAY_OF_YEAR).rangeRefinedBy(me) },
                        { nameof(ChronoField.DAY_OF_WEEK), () => ((ITemporalField)ChronoField.DAY_OF_WEEK).rangeRefinedBy(me) },
                        { nameof(ChronoField.DAY_OF_MONTH), () => ((ITemporalField)ChronoField.DAY_OF_MONTH).rangeRefinedBy(me) },
                        { nameof(ChronoField.CLOCK_HOUR_OF_AMPM), () => ((ITemporalField)ChronoField.CLOCK_HOUR_OF_AMPM).rangeRefinedBy(me) },
                        { nameof(ChronoField.CLOCK_HOUR_OF_DAY), () => ((ITemporalField)ChronoField.CLOCK_HOUR_OF_DAY).rangeRefinedBy(me) },
                        { nameof(ChronoField.HOUR_OF_DAY), () => ((ITemporalField)ChronoField.HOUR_OF_DAY).rangeRefinedBy(me) },
                        { nameof(ChronoField.HOUR_OF_AMPM), () => ((ITemporalField)ChronoField.HOUR_OF_AMPM).rangeRefinedBy(me) },
                        { nameof(ChronoField.MINUTE_OF_DAY), () => ((ITemporalField)ChronoField.MINUTE_OF_DAY).rangeRefinedBy(me) },
                        { nameof(ChronoField.MINUTE_OF_HOUR), () => ((ITemporalField)ChronoField.MINUTE_OF_HOUR).rangeRefinedBy(me) },
                        { nameof(ChronoField.SECOND_OF_DAY), () => ((ITemporalField)ChronoField.SECOND_OF_DAY).rangeRefinedBy(me) },
                        { nameof(ChronoField.SECOND_OF_MINUTE), () => ((ITemporalField)ChronoField.SECOND_OF_MINUTE).rangeRefinedBy(me) },
                        { nameof(ChronoField.MILLI_OF_DAY), () => ((ITemporalField)ChronoField.MILLI_OF_DAY).rangeRefinedBy(me) },
                        { nameof(ChronoField.MILLI_OF_SECOND), () => ((ITemporalField)ChronoField.MILLI_OF_SECOND).rangeRefinedBy(me) },
                        { nameof(ChronoField.MICRO_OF_DAY), () => ((ITemporalField)ChronoField.MICRO_OF_DAY).rangeRefinedBy(me) },
                        { nameof(ChronoField.MICRO_OF_SECOND), () => ((ITemporalField)ChronoField.MICRO_OF_SECOND).rangeRefinedBy(me) },
                        { nameof(ChronoField.NANO_OF_DAY), () => ((ITemporalField)ChronoField.NANO_OF_DAY).rangeRefinedBy(me) },
                        { nameof(ChronoField.NANO_OF_SECOND), () => ((ITemporalField)ChronoField.NANO_OF_SECOND).rangeRefinedBy(me) }
                    };
                }
            }
        }

        public static rangeHandlerStruct rangeHandlers
        {
            get
            {
                return new rangeHandlerStruct();
            }
        }

        public static int get(ITemporalHelper me, ITemporalField field)
        {
            int? rslt = getIntFromLong(getLong(me, field));
            if (rslt.HasValue)
            {
                return rslt.Value;
            }
            throw new ArgumentException("Invalid argument type", nameof(field));
        }

        public static long getLong(ITemporalHelper me, ITemporalField field)
        {
            if (field is ChronoField)
            {
                long? rslt = get(me.getHandlers, (ChronoField)field);
                if (rslt.HasValue)
                {
                    return rslt.Value;
                }
            }
            throw new ArgumentException("Invalid argument type", nameof(field));
        }

        public static bool isSupported(ITemporalHelper me, ITemporalField field)
        {
            if (field is ChronoField)
            {
                return isSupported(me.isSupportedHandlers, (ChronoField)field);
            }
            throw new ArgumentException("Invalid argument type", nameof(field));
        }

        public static R query<V, R>(ITemporalHelper me, Func<V, R> del) where V : ITemporalAccessor
        {
            return del((V)(ITemporalAccessor)me);
        }

        public static ITemporal adjustInto(ITemporalHelper me, ITemporal temporal)
        {
            throw new NotImplementedException();
        }

        public static bool isSupported(ITemporalHelper me, ITemporalUnit unit)
        {
            if (unit is ChronoUnit)
            {
                return isSupported(me.isSupportedHandlers, (ChronoUnit)unit);
            }
            throw new ArgumentException("Invalid argument type", nameof(unit));
        }

        public static ITemporal minus(ITemporalHelper me, long amountToSubtract, ITemporalUnit unit)
        {
            return ((ITemporal)me).minus(new Duration(amountToSubtract, unit));
        }

        public static ITemporal minus(ITemporalHelper me, ITemporalAmount amount)
        {
            return minus(me.addSubrtactHandlers, amount);
        }

        public static ITemporal plus(ITemporalHelper me, long amountToAdd, ITemporalUnit unit)
        {
            return ((ITemporal)me).plus(new Duration(amountToAdd, unit));
        }

        public static ITemporal plus(ITemporalHelper me, ITemporalAmount amount)
        {
            return plus(me.addSubrtactHandlers, amount);
        }

        public static long until(ITemporalHelper me, ITemporal endExclusive, ITemporalUnit unit)
        {
            return unit.between(me, endExclusive);
        }

        public static ITemporal with(ITemporalHelper me, ITemporalAdjuster adjuster)
        {
            throw new NotImplementedException();
        }

        public static ITemporal with(ITemporalHelper me, ITemporalField field, long newValue)
        {
            throw new NotImplementedException();
        }

        public static ValueRange range(ITemporalHelper me, ITemporalField field)
        {
            if (field is ChronoField)
            {
                ValueRange rslt = range(rangeHandlers[me], (ChronoField)field);
                if (rslt != null)
                {
                    return rslt;
                }
            }
            throw new ArgumentException("Invalid argument type", nameof(field));
        }

        private static long? get(Dictionary<string, Func<long>> getHandler, ChronoField field)
        {
            if (getHandler.ContainsKey(field.ToString()))
            {
                return getHandler[field.ToString()]();
            }

            return null;
        }

        private static bool isSupported(Dictionary<string, bool> isSupportedHandler, ChronoField field)
        {
            if (isSupportedHandler.ContainsKey(field.ToString()))
            {
                return isSupportedHandler[field.ToString()];
            }

            return false;
        }

        private static bool isSupported(Dictionary<string, bool> isSupportedHandler, ChronoUnit field)
        {
            if (isSupportedHandler.ContainsKey(field.ToString()))
            {
                return isSupportedHandler[field.ToString()];
            }

            return false;
        }

        private static ITemporal plus(Dictionary<string, Tuple<Func<long, ITemporal>, Func<long, ITemporal>>> addSubrtactHandler, ITemporalAmount amt)
        {
            var unit = amt.getUnits()[0];
            if (addSubrtactHandler.ContainsKey(unit.ToString()))
            {
                return addSubrtactHandler[unit.ToString()].Item1(amt.get(unit));
            }

            return null;
        }

        private static ITemporal minus(Dictionary<string, Tuple<Func<long, ITemporal>, Func<long, ITemporal>>> addSubrtactHandler, ITemporalAmount amt)
        {
            var unit = amt.getUnits()[0];
            if (addSubrtactHandler.ContainsKey(unit.ToString()))
            {
                return addSubrtactHandler[unit.ToString()].Item2(amt.get(unit));
            }

            return null;
        }

        private static ValueRange range(Dictionary<string, Func<ValueRange>> rangeHandler, ChronoField field)
        {
            if (rangeHandler.ContainsKey(field.ToString()))
            {
                return rangeHandler[field.ToString()]();
            }

            return null;
        }

        private static int? getIntFromLong(long? v)
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
