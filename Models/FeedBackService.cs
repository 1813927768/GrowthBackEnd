using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Growth.Entity;
using Growth.Util;

namespace Growth.Models
{
    public class FeedBackService
    {
        public static bool addFeedback(long userid, String content, String title)
        {
            DateTime addTime = DateTime.Now.ToLocalTime();

            Feedback feedback = new Feedback()
            {
                userid = userid,
                time = addTime,
                status = 0,
                title = title,
                content = content
            };
            using (var db = SugarBase.GetIntance())
            {

                long id = db.Insertable(feedback).ExecuteReturnIdentity();
                return true;
            }

        }

        public static List<Feedback> getFeedbackByID(long userid)
        {
            using (var db = SugarBase.GetIntance())
            {
                return db.Queryable<Feedback>().Where(
                    it => it.userid == userid
                    ).ToList();
            }

        }

        public static List<Feedback> getAllFeedback()
        {
            using (var db = SugarBase.GetIntance())
            {
                return db.Queryable<Feedback>().ToList();
            }
        }

        public static bool handleFeedback(long id, String answer)
        {
            using (var db = SugarBase.GetIntance())
            {
                db.Updateable<Feedback>()
                     .SetColumns(it => new Feedback() { status = 1, answer = answer })
                     .Where(it => it.id == id).ExecuteCommand();
                return true;
            }
        }
    }
}
