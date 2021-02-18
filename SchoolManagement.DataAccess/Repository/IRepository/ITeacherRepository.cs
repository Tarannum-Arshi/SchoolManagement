using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using SchoolManagement.Models;
using SchoolManagement.Models.ViewModels;

namespace SchoolManagement.DataAccess.Repository.IRepository
{
    public interface ITeacherRepository : IRepository<TeacherModel>
    {
        public IEnumerable<StudentUserDetails> GetStudentDetailsFunction();
        public IEnumerable<TeacherModel> UpdateLeave(TeacherModel teacherModel);

        public IEnumerable<Subject> GetResultFunction();
    }
}
