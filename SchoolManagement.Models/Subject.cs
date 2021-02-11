using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public UserModel UserModel { get; set; }
        [Required]
        [Range(0,100)]
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
