using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Service_Id { get; set; }

        [Required]
        [Display(Name = "Service Name")]
        [StringLength(50)]
        public string SeviceName { get; set; }

        [Required]
        [Display(Name = "Service Description")]
        [StringLength(450)]
        public string ServiceDesc { get; set; }

    }
}