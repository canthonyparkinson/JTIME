using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public interface ITemporalAmount
    {
        ITemporal addTo(ITemporal temporal);
        ITemporal subtractFrom(ITemporal temporal);
        IList<ITemporalUnit> getUnits();
        long get(ITemporalUnit unit);
    }
}
