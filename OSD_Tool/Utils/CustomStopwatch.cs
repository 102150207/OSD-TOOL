using System;
using System.Diagnostics;

namespace OSD_Tool.Utils
{
    public class CustomStopwatch : Stopwatch
    {
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }


        public new void Start()
        {
            StartAt = DateTime.Now;
            base.Start();
        }

        public new void Stop()
        {
            EndAt = DateTime.Now;
            base.Stop();
        }
    }
}
