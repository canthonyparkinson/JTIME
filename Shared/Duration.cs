using System;
using System.Collections.Generic;
using System.Text;

namespace JTIME
{
    public class Duration : ITemporalAmount
    {
        private long _amt;
        private ITemporalUnit _unit;

        public Duration(long amt, ITemporalUnit unit)
        {
            _amt = amt;
            _unit = unit;
        }

        public ITemporal addTo(ITemporal temporal)
        {
            return ((ITemporal)temporal).plus(this);
        }

        public long get(ITemporalUnit unit)
        {
            if (unit.ToString() == _unit.ToString())
                return _amt;

            throw new ArgumentException();
        }

        public IList<ITemporalUnit> getUnits()
        {
            return new List<ITemporalUnit>() { _unit };
        }

        public ITemporal subtractFrom(ITemporal temporal)
        {
            return ((ITemporal)temporal).minus(this);
        }
    }
}
