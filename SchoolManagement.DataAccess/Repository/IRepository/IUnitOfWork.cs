using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        //ISPCall SPCall { get; }
        void Save();
    }
}
