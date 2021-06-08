using System;
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
        [StringLength(7,ErrorMessage = "名字至少2個字最多7個字",MinimumLength=2)]

        public string UName { get; set; } = " ";


        [DisplayName("賬號")]
        [Required]
        [StringLength(10, ErrorMessage = "賬號至少6個字最多10個字", MinimumLength = 6)]
        public string UserId { get; set; } = " ";

        [DisplayName("密碼")]
        [Required]
        [StringLength(11, ErrorMessage = "賬號至少6個字最多10個字", MinimumLength = 5)]
        public string Passwd { get; set; } = " ";
    }
}
