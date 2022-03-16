using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingAP.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указан e-mail")]
        //[Remote(action: "CheckEmail", controller: "User", ErrorMessage = "email уже существует")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public List<Reports> Report { get; set; }
    }
}
