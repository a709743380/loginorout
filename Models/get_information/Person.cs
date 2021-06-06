using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Person
    {
        public string userid { get; set; } = " ";
        [DisplayName("地址")]
        [Required]
        public string NewAddress { get; set; } = " ";

        [DisplayName("電話")]
        [Required]
        public string User_tel { get; set; } = " ";


    }
}
