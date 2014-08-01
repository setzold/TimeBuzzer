using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zold.TimeBuzzer.Interface;

namespace zold.TimeBuzzer.Business
{
    public class Session : ISession
    {
        public DateTime Date { get; set; }
        
        public TimeSpan StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string Description { get; set; }

        public double TotalHours { get; set; }
    }
}
