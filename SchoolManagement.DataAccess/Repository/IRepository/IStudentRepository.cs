using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using SchoolManagement.Models;
using SchoolManagement.Models.ViewModels;

namespace SchoolManagement.DataAccess.Repository.IRepository
{
    public interface IStudentRepository : IRepository<StudentModel>
    {
        public IEnumerable<FeeDetails> FeeDetailFunction(string finUserId);

        public void UpdateFeeDateFunction(string finUserId);

        //public void SaveFeeDetailsFunction(string finUserId);
    }
}
