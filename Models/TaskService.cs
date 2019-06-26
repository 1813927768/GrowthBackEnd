using Growth.Util;
using Growth.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = Growth.Entity.Task;

namespace Growth.Models
{
    public class TaskService
    {
        static public long addTask(long userId,String name, String description, int expectedTomato, DateTime setTime, DateTime deadline, DateTime remindTime)
        {
            using (var db = SugarBase.GetIntance())
            {
                Task newTask = new Task()
                {
                    userId = userId,
                    taskName = name,
                    description = description,
                    expectedTomato = expectedTomato,
                    setTime =setTime,
                    deadline = deadline,
                    remindTime = remindTime,
                    status = 0
                };

                long id = db.Insertable(newTask).ExecuteReturnIdentity();
                return id;
            }
        }

        static public Boolean modifyTask(long userId, String taskName, String property, String value)
        {
            using (var db = SugarBase.GetIntance())
            {
                if (property == "description")
                {
                    db.Updateable<Task>()
                    .SetColumns(it => new Task() { description = value })
                    .Where(it => it.userId == userId && it.taskName == taskName).ExecuteCommand();
                   
                }
                else
                {

                    db.Updateable<Task>()
                     .SetColumns(it => new Task() {
                         tomatoCompleted = Int32.Parse(value),
                         status = 1
                     })
                     .Where(it => it.userId == userId && it.taskName == taskName).ExecuteCommand();
                }
                return true;
            }
           
        }

        static public Boolean deleteTask(long userId, String taskName)
        {
            using (var db = SugarBase.GetIntance())
            {
                db.Deleteable<Task>().Where(new Task() { userId = userId, taskName = taskName}).ExecuteCommand();
                return true;
            }
        }

        static public List<Task> getTask(long userId)
        {
            using (var db = SugarBase.GetIntance())
            {
                return db.Queryable<Task>().Where(
                    it => it.userId == userId
                    && it.status != -1
                    ).ToList();
            }
        }

        static public Boolean endTask(long userId, String taskName, DateTime endTime)
        {
            using (var db = SugarBase.GetIntance())
            {
                db.Updateable<Task>()
                     .SetColumns(it => new Task() { status = 2, endTime = endTime })
                     .Where(it => it.userId == userId && it.taskName == taskName).ExecuteCommand();
                return true;
            }
        }

        static public Boolean breakTask(long userId, String taskName, DateTime endTime)
        {
            using (var db = SugarBase.GetIntance())
            {
                db.Updateable<Task>()
                     .SetColumns(it => new Task() { status = -1, endTime = endTime })
                     .Where(it => it.userId == userId && it.taskName == taskName).ExecuteCommand();
                return true;
            }
        }
    }
}
