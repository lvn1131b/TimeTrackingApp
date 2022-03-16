using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingAP.Models
{
    public class Reports
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано количество часов")]
        public int NumberHour { get; set; }
        [Required(ErrorMessage = "Не указана дата")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Не указано примечание")]
        public string Note { get; set; }
        public int UserId { get; set; }      // внешний ключ
        public Users User { get; set; }
    }
}
