using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.DataAccess.Data;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SchoolManagement.DataAccess.Repository
{
    public class TeacherRepository : Repository<TeacherModel> , ITeacherRepository
    {
        private readonly ApplicationDbContext _db;//for access of database
        private static string ConnectionString = "";//whenever we have to call stored procedure we need connection string

        public TeacherRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public IEnumerable<StudentUserDetails> GetStudentDetailsFunction()
        {
            DynamicParameters param = new DynamicParameters();
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<StudentUserDetails>(SD.GetStudentDetails, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TeacherModel> UpdateLeave(TeacherModel teacherModel)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("inTeacherId", teacherModel.TeacherId);
            parameters.Add("inLeaveDays", teacherModel.LeaveDays);
            parameters.Add("dtStartDate", teacherModel.StartDate);
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<TeacherModel>(SD.ApplyForLeave, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Subject> GetResultFunction()
        {
            DynamicParameters  param = new DynamicParameters();
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<Subject>(SD.GetStudentDetails, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
