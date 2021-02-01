using Microsoft.EntityFrameworkCore;
using SchoolManagement.DataAccess.Data;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SchoolManagement.DataAccess.Repository
{
    public class UserRepository : Repository<UserModel> , IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public void Add(StudentModel studentmodel)
        {
            throw new NotImplementedException();
        }

        public void Update(UserModel usermodel)
        {
            var objFromDb = _db.UserModel.FirstOrDefault(s => s.UserId == usermodel.UserId);
            if (objFromDb != null)
            {
                objFromDb.FirstName = usermodel.FirstName;
                _db.SaveChanges();
            }
        }

    }
}
