using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Growth.Models;
//using Growth.Util;
using TomatoManage;
using UtilModule;

namespace Growth.Controllers
{
   
    public class TomatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("startTomato")]
        public Boolean startTomato(long userId, String taskName, DateTime startTime)
        {
            return TomatoService.addTomato(userId, taskName, startTime);
        }

        [HttpGet]
        [Route("breakTomato")]
        public Boolean breakTomato(long userId, DateTime breakTime, DateTime startTime)
        {
            return TomatoService.setTomatoState(userId, startTime, breakTime, TomatoState.breakState);
        }

        [HttpGet]
        [Route("endTomato")]
        public Boolean endTomato(long userId, DateTime endTime, DateTime startTime, String taskName, bool needAssociation)
        {
            if (needAssociation)
            {
                TomatoService.setTomatoAssociation(userId, startTime, taskName);
            }
            return TomatoService.setTomatoState(userId, startTime, endTime, TomatoState.endState);
        }
    }
}