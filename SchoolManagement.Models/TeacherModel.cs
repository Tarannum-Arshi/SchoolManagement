using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models
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

        [Required]
        public int TotalLeave { get; set; }

        [Required]
        public int RemainingLeave { get; set; }

        public int LeaveDays { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
       
        [MaxLength(4)]
        public string Status { get; set; }
    }
}