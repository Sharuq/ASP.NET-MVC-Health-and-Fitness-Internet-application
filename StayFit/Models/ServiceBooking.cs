using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class ServiceBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Booking_Id { get; set; }


        [Required]
        [Display(Name = "Select A Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BookingDate { get; set; }

        [Required]
        public Boolean BookingStatus { get; set; }
        

        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

       
        public virtual Service Service { get; set; }
    }
}