using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class Fee
    {
        [Key]
        public int FeeId { get; set; }

        [Required]
        public int FeeCharge { get; set; }
    }
}
