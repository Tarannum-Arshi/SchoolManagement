using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.ViewModels
{
    public class Leaves
    {
        [Key]
        public int LeaveId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        public int LeaveDays { get; set; }
    }
}
