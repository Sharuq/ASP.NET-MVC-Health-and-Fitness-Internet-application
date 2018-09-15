using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class MemberProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int member_Id { get; set; }

        [Required]
       //[RegularExpression("[A-Za-z ]+",ErrorMessage =  Name should only contains alphabets)]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
       
    }
}