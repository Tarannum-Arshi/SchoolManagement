using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.DataAccess.Data.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            //UserRepository = new UserRepository(_db);
            
            //SPCall = new SPCall(_db);
        }

        //public IUserRepository UserRepository { get; private set; }
       
        //public ISPCall SPCall { get; private set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
