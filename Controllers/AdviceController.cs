using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Growth.Models;
//using Growth.Entity;
using AdviceManage;
using AdviceService = AdviceManage.AdviceService;
using AnalyzedataBean = Growth.Entity.AnalyzedataBean;

namespace Growth.Controllers
{
    public class AdviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getTimeSuggestion")]
        public BestTime getTimeSuggestion(long userId){

            return AdviceService.getTimeData(userId);
        }

        [HttpGet]
        [Route("getWeekdaySuggestion")]
        public BestWeekDay getWeekdaySuggestion(long userId)
        {

            return AdviceService.getWeekdayData(userId);
        }

        [HttpGet]
        [Route("getOneYearHistoryData")]
        public List<AnalyzedataBean> getOneYearData(long userId, String date){
            HistoryDataService.getOneYearData(userId, date,3);
            return HistoryDataService.analyzedataBeans;
        }
}
}