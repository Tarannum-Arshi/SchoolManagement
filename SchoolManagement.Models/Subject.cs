using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(40)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required]
        [Range(1,7)]
        public int Class { get; set; }

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
