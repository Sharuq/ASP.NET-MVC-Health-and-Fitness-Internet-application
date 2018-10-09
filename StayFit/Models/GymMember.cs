using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class GymMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Member_Id { get; set; }

        [Required]
        [RegularExpression("[A-Za-z ]+",ErrorMessage =  "Name should only contains alphabets")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Name should only contains alphabets")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(90)]
        public string Address { get; set; }

        [Required]
        [Range(50, 250, ErrorMessage = "Enter height between 50 to 250")]
        [Display(Name = "Height (In Centimeters)")]
        public int Height { get; set; }

        [Required]
        [Range(50, 250, ErrorMessage = "Enter weight between 50 to 300")]
        [Display(Name = "Weight (In Kilograms)")]
        public int Weight { get; set; }

        [Required]
        [Display(Name = "Membership Tiers")]
        public string MembershipType { get; set; }



        public virtual ApplicationUser ApplicationUser { get; set; }
       
    }
}