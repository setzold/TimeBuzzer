using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zold.TimeBuzzer.Interface
{
    public interface ISession
    {
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }

        string Description { get; set; }
    }
}
