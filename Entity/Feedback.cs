using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Entity
{
    public class Feedback
    {
        public long userid { set; get; }
        public long id { set; get; }
        public DateTime time { set; get; }
        public String title { set; get; }
        public String content { set; get; }
        public String answer { set; get; }
        public int status { set; get; }
    }

    public class Authorfeedback
    {
        public long id { set; get; }
        public String name { set; get; }
        public String title { set; get; }
        public String content { set; get; }
        public String date { set; get; }
        public String answer { set; get; }
        public int status { set; get; }
    }
}
