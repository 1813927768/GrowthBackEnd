using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Growth.Models;
//using Growth.Entity;
using FeedBackManagement;
using FeedBackService = FeedBackManagement.FeedBackService;

namespace Growth.Controllers
{
    public class FeedBackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("submitFeedback")]
        public bool submitFeedback(String content,long userid, String title) 
        {
            return FeedBackService.addFeedback(userid, content, title);
        }

        [HttpGet]
        [Route("getMyFeedback")]
        public List<Feedback> getMyFeedback(long userId)
        {
            if (UserService.isUserExist(userId) != null)
            {
                return FeedBackService.getFeedbackByID(userId);
            }
            return new List<Feedback>();
        }

        

        [HttpGet]
        [Route("getAllFeedback")]
        public List<Feedback> getAllFeedback()
        {
            return FeedBackService.getAllFeedback();
        }

        [HttpPost]
        [Route("handleFeedback")]
        public bool handleFeedback(long id, String answer)
        {
            return FeedBackService.handleFeedback(id, answer);
        }
    }
}