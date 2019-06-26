using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Growth.Util;
using Growth.Entity;
using Task = Growth.Entity.Task;
using SqlSugar;
using System.Threading;

namespace Growth.Models
{
    public class HistoryDataService
    {
        public static List<AnalyzedataBean> analyzedataBeans = new List<AnalyzedataBean>();
        public static void getOneYearData(long userId, String localTime, int threadCount)
        {
            DateTime curdate = DateTime.Parse(localTime);   //current date

            List<System.Threading.Thread> LThread = new List<System.Threading.Thread>();

            DateTime end = curdate.AddYears(1);
            DateTime earlytime = getEarlyTime(userId);
            int threadBet;
            List<Task> tasks;
            List<History> histories;
            if (earlytime == null)
            {
                Console.WriteLine("null time");
                return;
            }
            if (DateTime.Compare(earlytime, end) > 0)
            //after
            {
                Console.WriteLine("this year");
                return;
            }          
            else if(DateTime.Compare(earlytime, curdate) < 0)
            {//before
                TimeSpan diff = end - curdate;
                int bet = (int)diff.Days;
                threadBet = bet / threadCount;               

            }
            else
            {//middle

                TimeSpan diff = end - earlytime;
                int bet = (int)diff.Days;

                threadBet = bet / threadCount;
                curdate = earlytime;

                // return analyzedataBeans;
            }

            using (var db = SugarBase.GetIntance())
            {
                tasks = db.Queryable<Task>().Where(
                        it => it.userId == userId
                        ).ToList();
                histories = db.Queryable<History>().Where(
                            it => it.userId == userId
                            ).ToList();
            }
            for (int i = 0; i < threadCount; i++)
            {
                // DateTime线程不安全
                DateTime startTime = DateTime.Parse(curdate.AddDays(i * threadBet).ToString("yyyy-MM-dd"));
                //Console.WriteLine(startTime.ToString("yyyy-MM-dd"));
                Thread childThread = new Thread(() => multiThreadGetHis(startTime, threadBet, tasks, histories));
                LThread.Add(childThread);
                childThread.Start();
                Console.WriteLine("Start Thread" + (i + 1));
            }
            for (int i = 0; i < threadCount; i++)
            {
                Console.WriteLine("Wait Thread" + (i + 1));
                LThread[i].Join();
            }
        }

        public static void multiThreadGetHis(DateTime startTime, int span, List<Task> tasks, List<History> histories)
        {
            Console.WriteLine(startTime.ToString("yyyy-MM-dd"));


                for (int i = 0; i < span; ++i)
                {
                    AnalyzedataBean analyzedataBean = getDateData(tasks,histories, startTime.AddDays(i));                 
                    if (analyzedataBean.taskCount == 0 && analyzedataBean.tomatocount == 0) continue;
                    analyzedataBeans.Add(analyzedataBean);
                }
        }

        /*
    one day
     */
        public static AnalyzedataBean getDateData(List<Task> tasks, List<History> histories,DateTime current)
        {
            

                List<History> dateHistory = new List<History>();
                List<Task> dateTask = new List<Task>();


            if (histories != null && histories.Count > 0) dateHistory = getOneDayHistory(histories, current);
                if (tasks != null && tasks.Count > 0) dateTask = getOneDayTask(tasks, current);

                int tomatocount = 0;
                int taskCount = 0;


                if (dateHistory != null && dateHistory.Count > 0)
                {
                    for (int i = 0; i < dateHistory.Count; i++)
                    {
                        if (dateHistory[i].status == 1)
                            tomatocount++;
                    }
                }
                else
                {
                    tomatocount = 0;
                }

                taskCount = dateTask.Count;
                return new AnalyzedataBean(tomatocount, taskCount, current.ToString("yyyy-MM-dd"));
        

        }


        public static DateTime getEarlyTime(long Id)
        {
            // Query query= Query.query(Criteria.where("userId").is(Id));
            using (var db = SugarBase.GetIntance())
            {
                // get User tomatoLength
                User tomatoOwner = db.Queryable<User>().Single(f => f.id == Id);
                if (tomatoOwner == null)
                {
                    throw new Exception("no user found");
                }

                History history = new History();
                while (history.startTime == DateTime.MinValue)
                {
                    history = db.Queryable<History>().
                        Where(it => it.userId == Id).
                        OrderBy(it => it.startTime, OrderByType.Asc).First();
                    if (history == null) break;
                    if (history.startTime == null)
                        db.Deleteable<History>().Where(history).ExecuteCommand();
                }
                return history.startTime;
            }
        }

        private static List<History> getOneDayHistory(List<History> histories, DateTime day)
        {

            List<History> dayHistories = new List<History>();
            if (histories != null && histories.Count > 0)
            {
                for (int i = 0; i < histories.Count; i++)
                {
                    DateTime starttime = histories[i].startTime;
                    if (day.Date == starttime.Date)
                    {
                        History history = histories[i];
                        dayHistories.Add(history);
                    }

                }
            }
            return dayHistories;
        }

        private static List<Task> getOneDayTask(List<Task> tasks, DateTime day)
        {
            List<Task> dayTasks = new List<Task>();
            if (tasks != null && tasks.Count > 0)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    DateTime starttime = tasks[i].setTime;
                    if (starttime != null)
                    {
                        if (day.Date == starttime.Date)
                        {
                            Task task = tasks[i];
                            dayTasks.Add(task);

                        }
                    }

                }
            }
            return dayTasks;
        }
    }
}
