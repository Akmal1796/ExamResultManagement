namespace ExamResultManagement.Core.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string Course { get; set; }
        public string GradeValue { get; set; }
    }
}
