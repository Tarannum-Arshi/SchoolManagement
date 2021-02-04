using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ViewModels;

namespace SchoolManagement.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<StudentDetailsModel> StudentModel { get; set; }
        public DbSet<TeacherModel> TeacherModel { get; set; }
        public DbSet<ClassModel> ClassModel { get; set; }

     

        public DbSet<Drop> Drop { get; set; }
        public DbSet<Fee> Fee { get; set; }
        public DbSet<StudentDetails> StudentDetails { get; set; }

    }
}
