using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeTrackingAP.Models;

namespace TimeTrackingAP.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email)
        {
            using (Context DB_context = new Context())
            {
                var email_is_db = DB_context.Users.Where(a => a.Email == email).FirstOrDefault();
                if (email_is_db != null)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);

                }
            }

        }

        [HttpPost]
        public JsonResult Create(Users user)
        {
            string result = "";

            if (ModelState.IsValid)
            {
                using (Context DB_context = new Context())
                {
                    Users user_is_exists = DB_context.Users.Where(a => a.Email == user.Email).FirstOrDefault();
                    if (user_is_exists == null)
                    {
                        Users user_add = new Users();
                        user_add.Email = user.Email;
                        user_add.Surname = user.Surname;
                        user_add.Name = user.Name;
                        user_add.Patronymic = user.Patronymic;
                        DB_context.Add(user_add);
                        DB_context.SaveChanges();
                        result = $"result:success";

                    }
                    else
                    {
                        result = $"result: user this email exists";

                    }
                }
            }
            else
            {
                result = string.Join(" | ", ModelState.Values
       .SelectMany(v => v.Errors)
       .Select(e => e.ErrorMessage));

            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult Update(Users user)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                using (Context DB_context = new Context())
                {
                    Users user_is_exists = DB_context.Users.Where(a => a.Email == user.Email).FirstOrDefault();
                    if (user_is_exists != null)
                    {


                        Users user_update = DB_context.Users.Where(a => a.Id == user.Id).FirstOrDefault();
                        if (user_update != null)
                        {
                            user_update.Surname = user.Surname;
                            user_update.Name = user.Name;
                            user_update.Patronymic = user.Patronymic;
                            DB_context.SaveChanges();

                            result = $"result: user data updated";
                        }
                        else
                        {
                            result = $"result: user is not found";

                        }
                    }
                    else
                    {
                        result = $"result: user this email exists";

                    }
                }
            }
            else
            {
                result = string.Join(" | ", ModelState.Values
       .SelectMany(v => v.Errors)
       .Select(e => e.ErrorMessage));

            }

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<Users> users_list = new List<Users>();
            using (Context DB_context = new Context())
            {
                users_list = DB_context.Users.OrderBy(a => a.Id).ToList();
                //jsonStr = JsonSerializer.Serialize(user_list);

            }


            return Json(users_list);
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            List<Users> users_list = new List<Users>();
            using (Context DB_context = new Context())
            {
                users_list = DB_context.Users.Where(a => a.Id == id).ToList();
                //jsonStr = JsonSerializer.Serialize(user_list);

            }


            return Json(users_list);
        }

        [HttpDelete]
        public JsonResult Delete(string email)
        {
            string result = "";
            try
            {
                if (email != "")
                {
                    using (Context DB_context = new Context())
                    {
                        Users user_delete = DB_context.Users.Where(a => a.Email == email).FirstOrDefault();
                        if (user_delete != null)
                        {
                            DB_context.Users.Remove(user_delete);
                            DB_context.SaveChanges();
                            result = $"result: user deleted";

                        }
                        else
                        {
                            result = $"result: user not found" + email + "  ";

                        }

                    }
                }
                else
                {
                    result = $"result: email empty";

                }
            }
            catch
            {
                result = $"result: error";

            }

            return Json(result);
        }

    }
}
