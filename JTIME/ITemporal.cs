using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public interface ITemporal : ITemporalAccessor
    {
        bool isSupported(ITemporalUnit field);
        ITemporal minus(long amountToSubtract, ITemporalUnit unit);
        ITemporal minus(ITemporalAmount amount);
        ITemporal plus(long amountToSubtract, ITemporalUnit unit);
        ITemporal plus(ITemporalAmount amount);
        long until(ITemporal endExclusive, ITemporalUnit unit);
        ITemporal with(ITemporalAdjuster adjuster);
        ITemporal with(ITemporalField field, long newValue);
    }
}
