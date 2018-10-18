using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class PostMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Post_Message_Id { get; set; }

        [Required]
        public string Post_Message { get; set; }


        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public virtual Post Post { get; set; }

        public virtual Image Image { get; set; }
    }
}