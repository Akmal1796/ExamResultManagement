using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExamResultManagement.Core;
using ExamResultManagement.Core.Data;
using ExamResultManagement.Core.Models;
using System.Diagnostics;

namespace ExamResultManagement.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup dependency injection
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ExamResultContext>(options =>
                    options.UseSqlServer("Server=localhost;Database=your_database_name;User Id=root;Password='';"))
                .AddSingleton<GPACalculator>()
                .BuildServiceProvider();

            var context = serviceProvider.GetService<ExamResultContext>();
            var calculator = serviceProvider.GetService<GPACalculator>();

            // Ensure database is created
            context.Database.EnsureCreated();

            // Add some sample data (if not exists)
            if (!context.Grades.Any())
            {
                context.Grades.AddRange(
                    new Grade { StudentId = "S001", Course = "Math", GradeValue = "A" },
                    new Grade { StudentId = "S001", Course = "Science", GradeValue = "B+" },
                    new Grade { StudentId = "S002", Course = "Math", GradeValue = "A-" }
                );
                context.SaveChanges();
            }

            // Fetch grades for a specific student
            var studentGrades = context.Grades.Where(g => g.StudentId == "S001").Select(g => g.GradeValue).ToList();
            var gpa = calculator.CalculateGPA(studentGrades);

            Console.WriteLine($"GPA for student S001: {gpa}");
        }
    }
}
