using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayFit.Models
{
    public class NewsletterViewModel
    {
        [Required]
        public string News_title { get; set; }
        [Required]
        [AllowHtml]
        public string News_content { get; set; }
    }
}