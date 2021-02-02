using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class TeacherModel
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public UserModel UserModel { get; set; }

        [Required]
        [Range(50000, 100000)]
        public int Salary { get; set; }
    }
}