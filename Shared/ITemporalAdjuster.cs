using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public interface ITemporalAdjuster
    {
        ITemporal adjustInto(ITemporal temporal);
    }
}
