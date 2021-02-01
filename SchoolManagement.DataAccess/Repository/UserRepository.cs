using Microsoft.EntityFrameworkCore;
using SchoolManagement.DataAccess.Data.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SchoolManagement.DataAccess.Data.Repository
{
    public class UserRepository : Repository<UserModel> , IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        
    }
}
