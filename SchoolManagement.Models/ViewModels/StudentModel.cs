﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class StudentDetailsModel
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("ClassModel")]
        public int Class { get; set; }
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