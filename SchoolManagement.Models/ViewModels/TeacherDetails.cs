using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class TeacherDetails
    {
        [Key]
        public int TeacherDetailsId {get; set; }
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(4)]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required]
        
        [Range(50000, 100000)]
        public int Salary { get; set; }
        
    }
}