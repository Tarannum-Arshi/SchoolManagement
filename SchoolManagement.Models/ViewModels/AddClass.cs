using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class AddClass
    {
       

        [Required]
        public int UserId { get; set; }

        [Required]
        public int Class { get; set; }
        
        

        [Required]
        public int FeeCharge { get; set; }

    }
}