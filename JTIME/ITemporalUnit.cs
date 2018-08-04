using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public interface ITemporalUnit
    {
        R addTo<R>(R temporal, long amount) where R: ITemporal;
        long between(ITemporal temporal1Inclusive, ITemporal temporal2Exclusive);
        bool isDateBased();
        bool isDurationEstimated();
        bool isSupportedBy(ITemporal temporal);
        bool isTimeBased();
    }
}
