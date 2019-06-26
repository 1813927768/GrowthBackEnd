using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Entity
{
    public class newTimeData
    {
        public newTimeData(String _tm, int _count)
        {
            time = _tm;
            taskcount = _count;
        }
        public String time { set; get; }
        public int taskcount { set; get; }
    }

    public class newData
    {
        public newData(String _day, int toma_count, int task_count)
        {
            weekday = _day;
            tomatocount = toma_count;
            taskcount = task_count;
        }

        public String weekday { set; get; }

        public int tomatocount { set; get; }

        public int taskcount { set; get; }
    }

    public class BestTime
    {
        public List<newTimeData> data { set; get; }
        public String result { set; get; }
    }

    public class BestWeekDay
    {
        public List<newData> data { set; get; }
        public String result { set; get; }
    }
}
