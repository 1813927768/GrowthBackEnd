using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Growth.Entity;
//using Growth.Util;
//using Growth.Models;
using UserManage;
using Microsoft.AspNetCore.Mvc;

namespace Growth.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [Route("logIn")]
        public ActionResult LoginFunc(string username, string password)
        {
            // 检查用户是否存在
            User user = UserService.isUserExist(username);
            if (user == null)
            {
                return Json(new { code = -1 });
            }
            // 检查用户密码是否正确
            user = UserService.checkUserPwd(username, password);         
            if (user == null)
            {
                return Json(new { code = 0 });
            }
            else
            {
                return Json(new { code = user.id, data = user });
            }
                
        }

        [HttpPost]
        [Route("signUp")]
        public long SignupFunc(string username, string password)
        {
            // 检查用户是否存在
            User user = UserService.isUserExist(username);
            if (user != null)
            {
                return -1;
            }
            // 插入用户
            return UserService.insertUser(username, password);
        }

        [HttpGet]
        [Route("changeUsername")]
        public ActionResult changeName(string username, long userId)
        {
            // 检查用户是否存在
            User user = UserService.isUserExist(userId);
            if (user == null)
            {
                return Content("No User Found");
            }
            // 检查用户名是否重复
            user = UserService.isUserExist(username);
            if (user != null)
            {
                return Content("duplicate username");
            }
            // 修改用户
            if(UserService.updateUserName(username, userId) == -1)
            {
                return Content("unknow error");
            }
            return Content("success");
        }

        [HttpPost]
        [Route("changePassword")]
        public ActionResult changePass(long userId, string oldPassword, string newPassword)
        {
            // 检查用户是否存在
            User user = UserService.isUserExist(userId);
            if (user == null)
            {
                return Content("No User Found");
            }
            // 检查用户密码是否正确
            user = UserService.checkUserPwd(userId, oldPassword);
            if (user == null)
            {
                return Content("wrong password");
            }
            // 修改用户密码
            if (UserService.updateUserPass(newPassword, userId) == -1)
            {
                return Content("unknow error");
            }
            return Content("success");
        }

        [HttpGet]
        [Route("changeEmail")]
        public ActionResult changeEmail(long userId, string email)
        {
            // 检查用户是否存在
            User user = UserService.isUserExist(userId);
            if (user == null)
            {
                return Content("No User Found");
            }
            
            // 修改用户email
            if (UserService.updateUserEmail(email, userId) == -1)
            {
                return Content("unknow error");
            }
            return Content("success");
        }


        [HttpGet]
        [Route("changeTomatoLength")]
        public ActionResult changeTomatoLength(long userId, int tomatoLength)
        {
            // 检查用户是否存在
            User user = UserService.isUserExist(userId);
            if (user == null)
            {
                return Content("No User Found");
            }

            // 修改用户tomatoLength
            if (UserService.updateUserTomLen(tomatoLength, userId) == -1)
            {
                return Content("unknow error");
            }
            return Content("success");
        }

        [HttpGet]
        [Route("changeGoal")]
        public ActionResult changeGoal(long userId, int dayGoal, int weekGoal, int monthGoal)
        {
            // 检查用户是否存在
            User user = UserService.isUserExist(userId);
            if (user == null)
            {
                return Content("No User Found");
            }

            // 修改用户tomatoLength
            if (UserService.updateUserGoal(dayGoal, weekGoal, monthGoal, userId) == -1)
            {
                return Content("unknow error");
            }
            return Content("success");
        }
    }
}