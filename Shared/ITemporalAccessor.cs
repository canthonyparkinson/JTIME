using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public interface ITemporalAccessor
    {
        int get(ITemporalField field);
        long getLong(ITemporalField field);
        bool isSupported(ITemporalField field);
        R query<V, R>(Func<V, R> del) where V : ITemporalAccessor;
        ValueRange range(ITemporalField field);
    }
}
