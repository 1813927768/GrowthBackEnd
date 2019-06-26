using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Entity
{
    public class Task
    {
        public long userId { set; get; }
        public string taskName { set; get; }
        public string description { set; get; }
        public int expectedTomato { set; get; }
        public int tomatoCompleted { set; get; }
        public int status { set; get; }
        public DateTime setTime { set; get; }
        public DateTime deadline { set; get; }
        public DateTime remindTime { set; get; }
        public DateTime endTime { set; get; }
    }
}
