using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Growth.Entity;
using Growth.Util;

namespace Growth.Models
{
    public class UserService
    {
        static public User isUserExist(string username)
        {
            using (var db = SugarBase.GetIntance())
            {
                return db.Queryable<User>().Single(f => f.name == username);
            }
        }

        static public User isUserExist(long userId)
        {
            using (var db = SugarBase.GetIntance())
            {
                return db.Queryable<User>().Single(f => f.id == userId);
            }
        }

        static public User checkUserPwd(string username, string password)
        {
            using (var db = SugarBase.GetIntance())
            {
                return db.Queryable<User>().Single(f => f.name == username && f.password == password);
            }
        }

        static public User checkUserPwd(long userId, string password)
        {
            using (var db = SugarBase.GetIntance())
            {
                return db.Queryable<User>().Single(f => f.id == userId && f.password == password);
            }
        }

        static public long insertUser(string username, string password)
        {
            using (var db = SugarBase.GetIntance())
            {
                User newUser = new User()
                {
                    name = username,
                    password = password
                };
                try
                {
                    long id = db.Insertable(newUser).ExecuteReturnIdentity();
                    return id;
                }
                catch
                {
                    return -1;
                }
            }
        }

        static public long updateUserName(string username, long userId)
        {
            using (var db = SugarBase.GetIntance())
            {
                try
                {
                    db.Updateable<User>()
                    .SetColumns(it => new User() { name = username})
                    .Where(it => it.id == userId).ExecuteCommand();
                    return userId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("修改用户异常" + ex.ToString());
                    return -1;
                }
            }
        }

        static public long updateUserEmail(string email, long userId)
        {
            using (var db = SugarBase.GetIntance())
            {
                try
                {
                    db.Updateable<User>()
                    .SetColumns(it => new User() { email = email })
                    .Where(it => it.id == userId).ExecuteCommand();
                    return userId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("修改用户异常" + ex.ToString());
                    return -1;
                }
            }
        }

        static public long updateUserTomLen(int len, long userId)
        {
            using (var db = SugarBase.GetIntance())
            {
                try
                {
                    db.Updateable<User>()
                    .SetColumns(it => new User() { tomatolength = len })
                    .Where(it => it.id == userId).ExecuteCommand();
                    return userId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("修改用户异常" + ex.ToString());
                    return -1;
                }
            }
        }

        static public long updateUserGoal(int dayGoal, int weekGoal,int  monthGoal,long userId)
        {
            using (var db = SugarBase.GetIntance())
            {
                try
                {
                    db.Updateable<User>()
                    .SetColumns(it => new User()
                    {
                        daygoal = dayGoal,
                        weekgoal = weekGoal,
                        monthgoal = monthGoal
                    })
                    .Where(it => it.id == userId).ExecuteCommand();
                    return userId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("修改用户异常" + ex.ToString());
                    return -1;
                }
            }
        }

        static public long updateUserPass(string pwd, long userId)
        {
            using (var db = SugarBase.GetIntance())
            {
                try
                {
                    db.Updateable<User>()
                    .SetColumns(it => new User() { password = pwd })
                    .Where(it => it.id == userId).ExecuteCommand();
                    return userId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("修改用户异常" + ex.ToString());
                    return -1;
                }
            }
        }

    }
}
