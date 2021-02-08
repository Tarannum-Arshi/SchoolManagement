using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Models.ViewModels
{
    public class TeacherUserDetails
    {
        [Key]
        public int TeacherUserId { get; set; }

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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Range(50000, 100000)]
        public int Salary { get; set; }

        public string ImageUrl { get; set; }
    }
}