using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<HomeworkSubmission> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                
                builder.UseSqlServer(Configuration.connectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Course>()
                .Property(c => c.CourseId)
                .IsRequired(true);

            builder.Entity<Course>()
                .Property(c => c.Name)
                .HasMaxLength(80)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Entity<Course>()
                .Property(c => c.Description)
                .IsUnicode(false)
                .IsRequired(false)
                .IsRequired(false);

            builder.Entity<Course>()
                .Property(c => c.StartDate)
                .IsRequired(true);

            builder.Entity<Course>()
                .Property(c => c.EndDate)
                .IsRequired(true);

            builder.Entity<Course>()
                .Property(c => c.Price)
                .IsRequired(true);

            builder.Entity<Resource>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Entity<Resource>()
                .Property(p => p.Url)
                .IsUnicode(false)
                .IsRequired(true);

            builder.Entity<Resource>()
                .Property(c => c.CourseId)
                .IsRequired(true);

            builder.Entity<Resource>()
                .Property(c => c.ResourceId)
                .IsRequired(true);

            builder.Entity<Resource>()
                .Property(c => c.ResourceType)
                .IsRequired(true);

            builder.Entity<HomeworkSubmission>()
                .Property(hs => hs.Content)
                .HasColumnType("varchar(max)")
                .IsUnicode(false)
                .IsRequired(true);

            builder.Entity<HomeworkSubmission>()
                .Property(hs => hs.ContentType)
                .IsRequired(true);

            builder.Entity<HomeworkSubmission>()
                .Property(hs => hs.HomeworkSubmissionId)
                .IsRequired(true);

            builder.Entity<HomeworkSubmission>()
                .Property(hs => hs.SubmissionTime)
                .IsRequired(true);

            builder.Entity<HomeworkSubmission>()
                .Property(hs => hs.StudentId)
                .IsRequired(true);

            builder.Entity<HomeworkSubmission>()
                .Property(hs => hs.CourseId)
                .IsRequired(true);

            builder.Entity<Student>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Entity<Student>()
                .Property(s => s.PhoneNumber)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnType("char(10)");

            builder.Entity<Student>()
                .Property(s => s.Birtthday)
                .IsRequired(true);

            builder.Entity<Student>()
                .Property(s => s.StudentId)
                .IsRequired(true);

            builder.Entity<Student>()
                .Property(s => s.RegisteredOn)
                .IsRequired(true);

            builder.Entity<Student>()
                .HasMany(s => s.CourseEnrollments)
                .WithOne(c => c.Student)
                .HasForeignKey(fk => fk.StudentId);

            builder.Entity<Student>()
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(hs => hs.Student)
                .HasForeignKey(fk => fk.StudentId);

            builder.Entity<Course>()
                .HasMany(c => c.HomeworkSubmissions)
                .WithOne(hs => hs.Course)
                .HasForeignKey(fk => fk.CourseId);

            builder.Entity<Course>()
                .HasMany(c => c.Resources)
                .WithOne(s => s.Course)
                .HasForeignKey(fk => fk.CourseId);

            builder.Entity<Course>()
                .HasMany(c => c.StudentsEnrolled)
                .WithOne(se => se.Course)
                .HasForeignKey(fk => fk.CourseId);
        }
    }
}
