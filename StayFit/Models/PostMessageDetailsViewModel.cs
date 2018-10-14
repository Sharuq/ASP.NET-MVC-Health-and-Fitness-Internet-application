using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class PostMessageDetailsViewModel
    {
        [Required]
        public int post_id { get; set; }
        
        public IEnumerable<PostMessage> postMessages { get; set; }

        [Required]
        public string post_message { get; set; }

    }
}