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
    public class StudentRepository : Repository<StudentModel> , IStudentRepository
    {
        private readonly ApplicationDbContext _db;//for access of database
        private static string ConnectionString = "";//whenever we have to call stored procedure we need connection string

        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public IEnumerable<FeeDetails> FeeDetailFunction(string finUserId)
        {
            //GetUserIdName getUserIdName = new GetUserIdName();
            DynamicParameters param = new DynamicParameters();
            param.Add("inUserId", finUserId);
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<FeeDetails>(SD.FeeDetails, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void UpdateFeeDateFunction(string finUserId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("inUserId", finUserId);
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(SD.UpdateFeeDate, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        //public void SaveFeeDetailsFunction(string finUserId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
