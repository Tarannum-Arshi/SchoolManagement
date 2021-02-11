using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class StudentSubjectDetails
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Range(1,7)]
        public int Class { get; set; }
        [Required]
        [Range(0, 100)]
        public int Maths { get; set; }
        [Required]
        [Range(0, 100)]
        public int Science { get; set; }
        [Required]
        [Range(0, 100)]
        public int English { get; set; }
        [Required]
        [Range(0, 100)]
        public int Hindi { get; set; }
        [Required]
        [Range(0, 100)]
        public int Computer { get; set; }
    }
}