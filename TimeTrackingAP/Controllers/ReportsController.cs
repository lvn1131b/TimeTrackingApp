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
    public class ReportsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(Reports report)
        {
            string result = "";

            if (ModelState.IsValid)
            {
                using (Context DB_context = new Context())
                {
                    int? userid = DB_context.Users.Where(a => a.Id == report.UserId).Select(a => a.Id).FirstOrDefault();
                    if (userid != null)
                    {
                        Reports new_report = new Reports();
                        new_report.NumberHour = report.NumberHour;
                        new_report.Date = report.Date;
                        new_report.Note = report.Note;
                        new_report.UserId = (int)userid;
                        DB_context.Reports.Add(report);
                        DB_context.SaveChanges();
                        result = $"result:success";

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
            List<Reports> report_list = new List<Reports>();
            using (Context DB_context = new Context())
            {
                report_list = DB_context.Reports.ToList();
            }

            return Ok(report_list);
        }

        [HttpGet("Month")]
        public IActionResult GetReportsMonth(int userid, DateTime? dateStart, DateTime? dateEnd)
        {
            IQueryable<Users> user;
            IList<Users> result;
            using (Context DB_context = new Context())
            {
                user = DB_context.Users;
                user = user.Where(a => a.Id == userid);
                if (dateStart.HasValue)
                {
                    user = user.Where(a => a.Report.FirstOrDefault().Date >= dateStart);
                }
                if (dateEnd.HasValue)
                {
                    user = user.Where(a => a.Report.FirstOrDefault().Date <= dateStart);
                }
                result = user.Select(a => new Users { Id = a.Id, Email = a.Email,Surname = a.Surname, Name = a.Name, Patronymic = a.Patronymic, Report = a.Report }).ToList();

            }


            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = "";
            try
            {

                using (Context DB_context = new Context())
                {
                    Reports report_delete = DB_context.Reports.Where(a => a.Id == id).FirstOrDefault();
                    if (report_delete != null)
                    {
                        DB_context.Reports.Remove(report_delete);
                        DB_context.SaveChanges();
                        result = $"result: report deleted";

                    }
                    else
                    {
                        result = $"result: report not found id " + id + "  ";

                    }

                }

            }
            catch
            {
                result = $"result: error";

            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Reports report)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                using (Context DB_context = new Context())
                {
                    Reports report_is_exists = DB_context.Reports.Where(a => a.Id == report.Id).FirstOrDefault();
                    if (report_is_exists != null)
                    {

                        report_is_exists.NumberHour = report.NumberHour;
                        report_is_exists.Date = report.Date;
                        report_is_exists.Note = report.Note;
                        DB_context.SaveChanges();
                        result = $"result: success";


                    }
                    else
                    {
                        result = $"result: user not found id " + report.Id;

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

    }
}
