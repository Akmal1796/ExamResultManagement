using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ExamResultManagement;
using ExamResultManagement.Core.Models;
using ExamResultManagement.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExamResultManagement.Tests
{
    public class GPACalculatorTests
    {
        private ExamResultContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ExamResultContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ExamResultContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public void CalculateGPA_ShouldReturnCorrectGPA()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Grades.AddRange(
                new Grade { StudentId = "S001", Course = "Math", GradeValue = "A" },
                new Grade { StudentId = "S001", Course = "Science", GradeValue = "B+" },
                new Grade { StudentId = "S001", Course = "History", GradeValue = "C" },
                new Grade { StudentId = "S001", Course = "PE", GradeValue = "A-" }
            );
            context.SaveChanges();

            var calculator = new GPACalculator();
            var grades = context.Grades.Where(g => g.StudentId == "S001").Select(g => g.GradeValue).ToList();

            // Act
            var gpa = calculator.CalculateGPA(grades);

            // Assert
            Assert.Equal(3.25, gpa, 2); // Allow a precision of 2 decimal places
        }

        [Fact]
        public void CalculateGPA_ShouldHandleEmptyList()
        {
            // Arrange
            var calculator = new GPACalculator();
            var grades = new List<string>();

            // Act
            var gpa = calculator.CalculateGPA(grades);

            // Assert
            Assert.Equal(0, gpa);
        }

        [Fact]
        public void CalculateGPA_ShouldThrowExceptionForInvalidGrade()
        {
            // Arrange
            var calculator = new GPACalculator();
            var grades = new List<string> { "A", "B+", "X" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.CalculateGPA(grades));
        }
    }
}
