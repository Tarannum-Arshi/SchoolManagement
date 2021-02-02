using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class Drop
    {
        [Key]
        public int DropId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

    }
}
