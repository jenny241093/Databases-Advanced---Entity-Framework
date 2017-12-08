using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Stations.Models.Enums
{
    [DefaultValue(OnTime)]
    public enum TripStatus
    {
        OnTime,
        Delayed,
        Early
    }
}
