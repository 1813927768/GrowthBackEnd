using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Growth.Models;
using TaskManage;
using Microsoft.AspNetCore.Mvc;
using Task = TaskManage.Task;


namespace Growth.Controllers
{
    [Route("task")]
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("addTask")]
        public long addTask(long userId,String taskName,
                       String description,DateTime setTime,
                         DateTime deadline,int expectedTomato, DateTime remindTime)
        {
            return TaskService.addTask(userId,taskName, description, expectedTomato, setTime, deadline, remindTime);
        }

        [HttpGet]
        [Route("modifyTask")]
        public Boolean modifyTask(long userId, String taskName, String property, String value)
        {
            return TaskService.modifyTask(userId, taskName,property, value);
        }

        [HttpGet]
        [Route("deleteTask")]
        public Boolean deleteTask(long userId, String taskName)
        {
            return TaskService.deleteTask(userId, taskName);
        }

        [HttpGet]
        [Route("getTask")]
        public List<Task> getTask(long userId)
        {
            return TaskService.getTask(userId);
        }

        [HttpGet]
        [Route("startTask")]
        public Boolean startTask(long userId, String taskName)
        {
            return true;
        }

        [HttpGet]
        [Route("endTask")]
        public Boolean endTask(long userId, String taskName, DateTime endTime)
        {
            return TaskService.endTask(userId,taskName,endTime);
        }

        [HttpGet]
        [Route("breakTask")]
        public Boolean breakTask(long userId, String taskName, DateTime breakTime)
        {
            return TaskService.breakTask(userId, taskName, breakTime);
        }

    }
}