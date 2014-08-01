using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zold.TimeBuzzer.Interface
{
    public interface ISession
    {
        DateTime Date { get; set; }

        TimeSpan StartTime { get; set; }

        TimeSpan? EndTime { get; set; }
        
        string Description { get; set; }

        double TotalHours { get; set; }
    }
}
