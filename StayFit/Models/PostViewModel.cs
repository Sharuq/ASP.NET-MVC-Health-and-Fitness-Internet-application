using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayFit.Models
{
    public class PostViewModel
    {
        [Required]
        public string post_title { get; set; }
        [Required]
        [AllowHtml]
        public string post_message { get; set; }

    }
}