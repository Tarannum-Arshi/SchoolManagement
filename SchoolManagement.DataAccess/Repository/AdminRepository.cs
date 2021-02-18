using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Utility;
using SchoolManagement.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Dapper;

namespace SchoolManagement.DataAccess.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _db;//for access of database
        private static string ConnectionString;//whenever we have to call stored procedure we need connection string
        public AdminRepository(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }
        public IEnumerable<GetUserIdName> getUserIdNameForDrop()
        {
            //GetUserIdName getUserIdName = new GetUserIdName();
            DynamicParameters param = new DynamicParameters();
            //param = null;
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            
                sqlCon.Open();
                return sqlCon.Query<GetUserIdName>(SD.Drop,  param ,  commandType: System.Data.CommandType.StoredProcedure);
            
        }

        //public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        //{
        //    using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
        //    {
        //        sqlCon.Open();
        //        return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

        //    }
        //}
        //public void Execute(string procedureName, DynamicParameters param = null)
        //{
        //    using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
        //    {
        //        sqlCon.Open();
        //        sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

        //    }
        //}

    }
}
