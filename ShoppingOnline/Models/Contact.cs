using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.Models
{
    public class Contact
    {

        public int Id { get; set; }


        [Display(Name = "Name")]
        public string Name { get; set; }


        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        

        [Display(Name = "Location")]
        public string Location { get; set; }


        [Display(Name = "Query")]
        public string Query { get; set; }

    }
}
