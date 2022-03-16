using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingAP.Models;

namespace TimeTrackingAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(Users user)
        {
            string result = "";

            if (ModelState.IsValid)
            {
                using (Context DB_context = new Context())
                {
                    Users user_is_exists = DB_context.Users.Where(a => a.Email == user.Email).FirstOrDefault();
                    if (user_is_exists == null)
                    {

                        DB_context.Add(user);
                        DB_context.SaveChanges();
                        result = $"result:success";

                    }
                    else
                    {
                        result = $"result:user this email exists";

                    }
                }
            }
            else
            {
                result = string.Join(" | ", ModelState.Values
       .SelectMany(v => v.Errors)
       .Select(e => e.ErrorMessage));

            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Users user)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                using (Context DB_context = new Context())
                {
                    Users user_is_exists = DB_context.Users.Where(a => a.Id == user.Id).FirstOrDefault();
                    Users user_is_exists_email = DB_context.Users.Where(a => a.Email == user.Email).FirstOrDefault();
                    if (user_is_exists != null)
                    {
                        if(user_is_exists.Id == user_is_exists_email.Id)
                        {
                            user_is_exists.Email = user.Email;
                            user_is_exists.Surname = user.Surname;
                            user_is_exists.Patronymic = user.Patronymic;
                            DB_context.SaveChanges();
                            result = $"result: success";

                        }
                        else
                        {
                            result = $"result: user with this email already exists " + user.Email;

                        }
                    }
                    else
                    {
                        result = $"result: user not found id " + user.Id;

                    }
                }
            }
            else
            {
                result = string.Join(" | ", ModelState.Values
       .SelectMany(v => v.Errors)
       .Select(e => e.ErrorMessage));

            }

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Users> users_list = new List<Users>();
            using (Context DB_context = new Context())
            {
                users_list = DB_context.Users.OrderBy(a => a.Id).ToList();
                //jsonStr = JsonSerializer.Serialize(user_list);
            }

            return Ok(users_list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Users> users_list = new List<Users>();
            using (Context DB_context = new Context())
            {
                users_list = DB_context.Users.Where(a => a.Id == id).ToList();
                //jsonStr = JsonSerializer.Serialize(user_list);

            }


            return Ok(users_list);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = "";
            try
            {

                using (Context DB_context = new Context())
                {
                    Users user_delete = DB_context.Users.Where(a => a.Id == id).FirstOrDefault();
                    if (user_delete != null)
                    {
                        DB_context.Users.Remove(user_delete);
                        DB_context.SaveChanges();
                        result = $"result: user deleted";

                    }
                    else
                    {
                        result = $"result: user not found id " + id + "  ";

                    }

                }

            }
            catch
            {
                result = $"result: error";

            }

            return Ok(result);
        }

    }
}
