using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserModel { get; }

        IStudentRepository StudentModel { get; }

        ITeacherRepository TeacherModel { get; }

        ISubjectRepository Subject { get; }

        ISPCall SPCall { get; }
        void Save();
    }
}
