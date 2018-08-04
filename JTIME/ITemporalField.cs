using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace JTIME
{
    public interface ITemporalField
    {
        R adjustInto<R>(R temporal, long newValue) where R : ITemporal;
        ITemporalUnit getBaseUnit();
        string getDisplayName();
        string getDisplayName(CultureInfo locale);
        long getFrom(ITemporalAccessor temporal);
        ITemporalUnit getRangeUnit();
        bool isDateBased();
        bool isSupportedBy(ITemporalAccessor temporal);
        bool isTimeBased();
        ValueRange range();
        ValueRange rangeRefinedBy(ITemporalAccessor temporal);
        ITemporalAccessor resolve(IDictionary<ITemporalField, long> fieldValues, ITemporalAccessor partialTemporal, ResolverStyle resolverStyle);
    }
}
