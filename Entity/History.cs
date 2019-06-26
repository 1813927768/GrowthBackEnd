using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Entity
{
    public class History
    {
        public long userId { set; get; }
        public string taskName { set; get; }
        public DateTime startTime { set; get; }
        public DateTime endTime { set; get; }
        public int status { set; get; }
        public int tomatoLength { set; get; }
    }
}
