using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Entity
{
    public class User
    {
        public long id { set; get; }
        public string name { set; get; }
        public string password { set; get; }
        public string email { set; get; }
        public int tomatolength { set; get; }
        public int status { set; get; }
        public int daygoal { set; get; }
        public int weekgoal { set; get; }
        public int monthgoal { set; get; }
    }
}
