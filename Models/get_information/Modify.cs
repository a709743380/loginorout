using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApplication5.Models
{
    public class Modify
    {
        public string UserId { get; set; } = " ";
        public string Oldpasswd { get; set; } = " ";
        public string Passwd { get; set; } = " ";
        public string NewPasswd { get; set; } = " ";
    }
}
