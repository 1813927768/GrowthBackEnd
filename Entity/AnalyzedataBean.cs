using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Entity
{
    public class AnalyzedataBean
    {
        public String date { set; get; }

        public int tomatocount { set; get; }

        public int taskCount { set; get; }

        public AnalyzedataBean(int tomatocount, int taskCount, String date)
        {
            this.tomatocount = tomatocount;
            this.taskCount = taskCount;
            this.date = date;
        }
    }
}
