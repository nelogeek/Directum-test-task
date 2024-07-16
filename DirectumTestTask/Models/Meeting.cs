using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectumTestTask.Models
{
    public class Meeting
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Description { get; set; }
        public TimeSpan Reminder
        {
            get; set;
        }
    }
}
