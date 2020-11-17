using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.Enums
{
    public enum Status : int
    {
        Created = 0,
        Started = 1,
        Stopped = 2,
        Finished = 3
    }
}
