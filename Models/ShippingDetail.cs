using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechProduct.Models
{
    public class ShippingDetail
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please Enter your FirstName")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your LastName")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your Adress")]
        public string Adress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your City")]
        public string City { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your State")]
        public string State { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your Zip")]
        public string Zip { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter your Country")]
        public string Country { get; set; }

    }
}
