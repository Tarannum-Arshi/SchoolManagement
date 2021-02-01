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
    public class TeacherRepository : Repository<TeacherModel> , ITeacherRepository
    {
        private readonly ApplicationDbContext _db;

        public TeacherRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        
    }
}
