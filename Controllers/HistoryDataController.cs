using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Growth.Util;
using Growth.Entity;

namespace Growth.Controllers
{
    public class HistoryDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("viewYearHistory")]
        public Boolean viewYearHistory(long userId, String year)
        {
            //return TomatoService.setTomatoState(userId, startTime, breakTime, TomatoState.breakState);
            return false;
        }
    }
}