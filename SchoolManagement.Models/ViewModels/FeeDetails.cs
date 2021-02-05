using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class FeeDetails
    {
        [Key]
        public int FeeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        
        public int Month { get; set; }

        [Required]
        public int FeeCharge { get; set; }
    }
}
