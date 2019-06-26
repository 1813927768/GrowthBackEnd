using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Growth.Util;
using Growth.Entity;
using Task = Growth.Entity.Task;

namespace Growth.Models
{
    public class AdviceService
    {
        //判断当前时间是否在工作时间段内
        private static bool getTimeSpan(DateTime time, String start, String end)
        {           
            string _strWorkingDayAM = start;    // "08:30"
            string _strWorkingDayPM = end;
            TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            TimeSpan dspNow = time.TimeOfDay;
            if (dspNow > dspWorkingDayAM && dspNow < dspWorkingDayPM)
            {
                return true;
            }
            return false;
        }

        // 获取某周周一
        private static DateTime getMonday(DateTime now)
        {
            DateTime temp = new DateTime(now.Year, now.Month, now.Day);
            int count = now.DayOfWeek - DayOfWeek.Monday;
            if (count == -1) count = 6;

            return temp.AddDays(-count);
        }


        /*
    哪个时段的完成数较多
     */
        public static BestTime getTimeData(long userId)
        {
            int[] time = new int[3];
            var newTimeDataList = new List<newTimeData>();
            int mor = 0;
            int aft = 0;
            int eve = 0;

            using (var db = SugarBase.GetIntance())
            {
                List<History> histories =  db.Queryable<History>().Where(
                    it => it.userId == userId
                    && it.status == 1
                    ).ToList();
                DateTime now;
                foreach (History history in histories)
                {
                    now = history.startTime.ToLocalTime();
                    if (getTimeSpan(now, "00:00", "11:00"))
                        mor++;
                    else if (getTimeSpan(now, "11:00", "18:00"))
                        aft++;
                    else
                        eve++;

                }
                time[0] = mor;
                time[1] = aft;
                time[2] = eve;
            }

            newTimeData newTimeData1 = new newTimeData("上午", mor);
            newTimeData newTimeData2 = new newTimeData("下午", aft);
            newTimeData newTimeData3 = new newTimeData("晚上", eve);

            newTimeDataList.Add(newTimeData1);
            newTimeDataList.Add(newTimeData2);
            newTimeDataList.Add(newTimeData3);

           

            int best = time[0];
            int maxtime = 0;
            for(int i = 1; i < 3; i++)
            {
                if(time[i] >= best)
                {
                    maxtime = i;
                }
            }

            String daytime = "";
            if (maxtime == 0) daytime = "上午";
            else if (maxtime == 2) daytime = "晚上";
            else if (maxtime == 1) daytime = "下午";

            BestTime res = new BestTime();
            res.data = newTimeDataList;
            res.result = daytime;

            return res;

        }


        /*
    用完成番茄数来衡量平均的一周中 哪天完成的数量比较多，前四周的数据
     */
        public static BestWeekDay getWeekdayData(long userId)
        {
            int Mon = 0, Tues = 0, Wed = 0, Thur = 0, Fri = 0, Sat = 0, Sun = 0;
            int tMon = 0, tTues = 0, tWed = 0, tThur = 0, tFri = 0, tSat = 0, tSun = 0;
            List<newData> newDataList = new List<newData>();
            int[] day = new int[7];

            using (var db = SugarBase.GetIntance())
            {
                List<History> histories = db.Queryable<History>().Where(
                    it => it.userId == userId
                    && it.status == 1
                    ).ToList();
                foreach (History history in histories)
                {
                    int cur = (int)history.startTime.ToLocalTime().DayOfWeek;
                    if (cur == 1) Mon++;
                    else if (cur == 2) Tues++;
                    else if (cur == 3) Wed++;
                    else if (cur == 4) Thur++;
                    else if (cur == 5) Fri++;
                    else if (cur == 6) Sat++;
                    else if (cur == 0) Sun++;
                }
                List<Task> tasks = db.Queryable<Task>().Where(
                    it => it.userId == userId
                    && it.status == 1
                    ).ToList();
                foreach (Task task in tasks)
                {
                    int cur = (int)task.setTime.ToLocalTime().DayOfWeek;
                    if (cur == 1) tMon++;
                    else if (cur == 2) tTues++;
                    else if (cur == 3) tWed++;
                    else if (cur == 4) tThur++;
                    else if (cur == 5) tFri++;
                    else if (cur == 6) tSat++;
                    else if (cur == 0) tSun++;
                }
            }


            day[0] = Mon + tMon;
            day[1] = Tues + tTues;
            day[2] = Wed + tWed;
            day[3] = Thur + Thur;
            day[4] = Fri + tFri;
            day[5] = Sat + tSat;
            day[6] = Sun + tSun;

            newData Mondata = new newData("星期一", Mon, tMon);
            newData Tuesdata = new newData("星期二", Tues, tTues);
            newData Weddata = new newData("星期三", Wed, tWed);
            newData Thurdata = new newData("星期四", Thur, tThur);
            newData Fridata = new newData("星期五", Fri, tFri);
            newData Satdata = new newData("星期六", Sat, tSat);
            newData Sundata = new newData("星期日", Sun, tSun);

            newDataList.Add(Mondata);
            newDataList.Add(Tuesdata);
            newDataList.Add(Weddata);
            newDataList.Add(Thurdata);
            newDataList.Add(Fridata);
            newDataList.Add(Satdata);
            newDataList.Add(Sundata);

            int best = day[0];
            int dd = 0;
            for (int i = 1; i < 7; i++)
            {
                if (day[i] >= best)
                {
                    dd = i;
                }
            }

            //完成番茄+任务数最多的天
            String ddd = "";
            if (dd == 1) ddd = "星期一";
            else if (dd == 2) ddd = "星期二";
            else if (dd == 3) ddd = "星期三";
            else if (dd == 5) ddd = "星期五";
            else if (dd == 6) ddd = "星期六";
            else if (dd == 7) ddd = "星期日";

            BestWeekDay analyData = new BestWeekDay();
            analyData.data = newDataList;
            analyData.result = ddd;
            return analyData;

        }

    }
}
