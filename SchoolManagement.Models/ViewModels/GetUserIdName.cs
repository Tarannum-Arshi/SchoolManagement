using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.ViewModels
{
    public class GetUserIdName
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
