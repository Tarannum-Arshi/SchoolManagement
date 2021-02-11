using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Models;

namespace SchoolManagement.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<StudentModel> StudentModel { get; set; }
        public DbSet<TeacherModel> TeacherModel { get; set; }
        public DbSet<ClassModel> ClassModel { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Payments> Payments { get; set; }


    }
}
