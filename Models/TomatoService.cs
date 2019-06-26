using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Growth.Util;
using Growth.Entity;

namespace Growth.Models
{
    public class TomatoService
    {
        static public Boolean addTomato(long userId, String name, DateTime startTime)
        {
            using (var db = SugarBase.GetIntance())
            {
                // get User tomatoLength
                User tomatoOwner = db.Queryable<User>().Single(f => f.id == userId);
                if (tomatoOwner == null)
                {
                    throw new Exception("no user found");
                }

                History newTomato = new History()
                {
                    userId = userId,
                    taskName = name,
                    startTime = startTime,
                    status = (int)TomatoState.runState,
                    tomatoLength = tomatoOwner.tomatolength
                };

                long id = db.Insertable(newTomato).ExecuteReturnIdentity();
                return true;
            }
        }

        static public Boolean setTomatoState(long userId, DateTime startTime, DateTime endTime, TomatoState state)
        {
            using (var db = SugarBase.GetIntance())
            {
                db.Updateable<History>()
                     .SetColumns(it => new History()
                     {
                         status = (int)state,
                         endTime = endTime
                     })
                     .Where(it => it.userId == userId && it.startTime == startTime).ExecuteCommand();
                return true;
            }
        }

        static public Boolean setTomatoAssociation(long userId, DateTime startTime, string taskName)
        {
            using (var db = SugarBase.GetIntance())
            {
                db.Updateable<History>()
                     .SetColumns(it => new History()
                     {
                         taskName = taskName
                     })
                     .Where(it => it.userId == userId && it.startTime == startTime).ExecuteCommand();
                return true;
            }
        }


        static public List<Object> getYearHis(long userId, string year)
        {
            using (var db = SugarBase.GetIntance())
            {
                return new List<object>();
            }
        }
    }
}
