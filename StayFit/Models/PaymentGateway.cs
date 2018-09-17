using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StayFit.Models
{
    public class PaymentGateway
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Payment_Id { get; set; }

        [Required]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Name should only contains alphabets")]
        [Display(Name = "Name On Card")]
        [StringLength(80)]
        public string NameOnCard { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [CreditCard]
        public int CardNumber { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}", ErrorMessage = "Only 3 Digits")]
        public int CVV { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime ExpiryDate { get; set; }



    }
}