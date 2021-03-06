﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models
{
    public class ClassModel
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [Range(1,12)]
        public int Class { get; set; }
        [Required]
        [ForeignKey("TeacherModel")]
        public int TeacherId { get; set; }
        public TeacherModel TeacherModel { get; set; }    

        [Required]
        [Range(1000, 50000)]
        public int FeeCharge { get; set; }

    }
}