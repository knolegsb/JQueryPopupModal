using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JQueryPopupModal
{
    [MetadataType(typeof(ContactMetaData))]
    public partial class Contact
    {
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }

    public class ContactMetaData
    {
        [Required(ErrorMessage = "Contact Name Required", AllowEmptyStrings = false)]
        [Display(Name ="Contact Person")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage ="Contact No Required", AllowEmptyStrings =false)]
        [Display(Name ="Contact No")]        
        public string ContactNo { get; set; }

        [Required(ErrorMessage ="Country Required")]
        [Display(Name ="Country")]
        public int CountryId { get; set; }

        [Required(ErrorMessage ="State Required")]
        [Display(Name ="State")]
        public int StateId { get; set; }
    }
}