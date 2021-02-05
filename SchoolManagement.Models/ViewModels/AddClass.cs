using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class AddClass
    {
        [Key]
        public int AddClassId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1,12)]
        public int Class { get; set; }
        
        

        [Required]
        [Range(1000, 50000)]
        public int FeeCharge { get; set; }

    }
}