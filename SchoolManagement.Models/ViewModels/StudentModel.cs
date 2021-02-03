using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class StudentModel
    {
        [Key]
        public int inStudentId { get; set; }

        [Required]
        [ForeignKey("ClassModel")]
        public int ClassId { get; set; }
        public ClassModel ClassModel { get; set; }

        [Required]
        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public UserModel UserModel { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastPaymentMonth { get; set; }

    
    }
}