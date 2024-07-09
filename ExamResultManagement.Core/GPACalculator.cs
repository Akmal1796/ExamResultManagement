using System;
using System.Collections.Generic;

namespace ExamResultManagement.Core
{
    public class GPACalculator
    {
        private readonly Dictionary<string, double> gradePoints = new Dictionary<string, double>
        {
            { "A+", 4.0 },
            { "A", 4.0 },
            { "A-", 3.7 },
            { "B+", 3.3 },
            { "B", 3.0 },
            { "B-", 2.7 },
            { "C+", 2.3 },
            { "C", 2.0 },
            { "C-", 1.7 },
            { "D+", 1.3 },
            { "D", 1.0 },
            { "F", 0.0 }
        };

        public double CalculateGPA(List<string> grades)
        {
            double totalPoints = 0;
            foreach (var grade in grades)
            {
                if (gradePoints.TryGetValue(grade, out var points))
                {
                    totalPoints += points;
                }
                else
                {
                    throw new ArgumentException($"Invalid grade: {grade}");
                }
            }
            return grades.Count > 0 ? totalPoints / grades.Count : 0;
        }
    }
}
