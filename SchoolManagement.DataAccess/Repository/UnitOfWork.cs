using SchoolManagement.DataAccess.Data;
using SchoolManagement.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            UserModel = new UserRepository(_db);
            TeacherModel = new TeacherRepository(_db);
            StudentModel = new StudentRepository(_db);
            
            SPCall = new SPCall(_db);
        }

        public IUserRepository UserModel { get; private set; }
        public ITeacherRepository TeacherModel { get; private set; }

        public IStudentRepository StudentModel { get; private set; }

        public ISPCall SPCall { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
