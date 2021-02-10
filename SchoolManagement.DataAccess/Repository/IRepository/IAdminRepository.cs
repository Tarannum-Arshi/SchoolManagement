using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SchoolManagement.Models.ViewModels;

namespace SchoolManagement.DataAccess.Repository.IRepository
{
    public interface IAdminRepository
    {
        public IEnumerable<GetUserIdName> getUserIdNameForDrop();
    }
}

