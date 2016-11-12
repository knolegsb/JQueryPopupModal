using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JQueryPopupModal.Entities
{
    public class Phone
    {
        [Display(Name = "Id")]
        [Key]
        public int PhoneId { get; set; }

        [Required]
        [Display(Name = "Model Name")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}