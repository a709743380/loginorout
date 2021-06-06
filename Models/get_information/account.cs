﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class account
    {
        public string Fid { get; set; } = " ";
        [DisplayName("姓名")]
        [Required]
        public string UName { get; set; } = " ";

        [DisplayName("賬號")]
        [Required]
        public string UserId { get; set; } = " ";

        [DisplayName("密碼")]
        [Required]
        public string Passwd { get; set; } = " ";
    }
}