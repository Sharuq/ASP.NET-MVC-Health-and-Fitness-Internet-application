using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class ServiceTimings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Timing_Id { get; set; }

        [Required]
        public string Timing { get; set; }

        [Required]
        public virtual Service Service { get; set; }
    }
}