using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

Console.WriteLine("App started");

List<User> studentsList = new List<User>
{
    new User { UserName = "Mouaz", FirstName = "Mouaz", LastName = "Zakaria", EmailAddress = "test@test.com", PhoneNumber = "+963999999999", Role = "Student" },
    new User { UserName = "Fawzy", FirstName = "Fawzy", LastName = "Sukar", EmailAddress = "test2@test.com", PhoneNumber = "+963999999999", Role = "Student" },
    new User { UserName = "Mehyar", FirstName = "Mehyar", LastName = "Khuder", EmailAddress = "test3@test.com", PhoneNumber = "+963999999999", Role = "Student" },
    new User { UserName = "Zuhair", FirstName = "Zuhair", LastName = "Al-Homsi", EmailAddress = "test4@test.com", PhoneNumber = "+963999999999", Role = "Student" },
    new User { UserName = "Nawar", FirstName = "Nawar", LastName = "Al-Tibi", EmailAddress = "test5@test.com", PhoneNumber = "+963999999999", Role = "Student" }
};

List<User> teachersList = new List<User>
{
    new User { UserName = "Sami", FirstName = "Sami", LastName = "Hijazi", EmailAddress = "test6@test.com", PhoneNumber = "+963999999999", Role = "Teacher" },
    new User { UserName = "Feryal", FirstName = "Feryal", LastName = "", EmailAddress = "test7@test.com", PhoneNumber = "+963999999999", Role = "Teacher" }
};
List<Course> coursesList = [
    new Course { CourseName = "SQL", Teacher = teachersList[0], StartDate = DateTime.Now, EndDate = DateTime.Now },
    new Course { CourseName = "C#", Teacher = teachersList[0], StartDate = DateTime.Now, EndDate = DateTime.Now },
    new Course { CourseName = "Entity Framework", Teacher = teachersList[1], StartDate = DateTime.Now, EndDate = DateTime.Now },
    new Course { CourseName = "Web API", Teacher = teachersList[1], StartDate = DateTime.Now, EndDate = DateTime.Now },
    new Course { CourseName = "React", Teacher = teachersList[1], StartDate = DateTime.Now, EndDate = DateTime.Now },
];
List<Assignment> assignmentsList = new List<Assignment>();
Random random = new Random();
foreach (var course in coursesList)
{
    for (int j = 0; j < 5; j++)
    {
        assignmentsList.Add(new Assignment { AssignmentTitle = $"Assignment {j + 1} for {course.CourseName}", Course = course, Weight = 20, MaxGrade = 100, DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(random.Next(-10, 10))) });
    }
}
List<Comment> commentsList = new List<Comment>();
for (int i = 0; i < 10; i++)
{
    int assignmentIndex = random.Next(0, assignmentsList.Count - 1);
    commentsList.Add(new Comment { CreatedByUser = studentsList[random.Next(0, studentsList.Count - 1)], CreatedDate = DateTime.Now, Assignment = assignmentsList[assignmentIndex], CommentContent = $"This is comment {i + 1} for assignment {assignmentsList[assignmentIndex].AssignmentTitle}" });
}

List<Grade> gradesList = new List<Grade>();
foreach (var student in studentsList)
{
    foreach (var assignment in assignmentsList)
    {
        gradesList.Add(new Grade { Assignment = assignment, Student = student, Score = random.Next(0, assignment.MaxGrade) });
    }
}

foreach (var course in coursesList)
{
    course.Syllabus = new Syllabus { Description = "Syllabus description" };
}

using var dbContext = new UniversityDbContext();
dbContext.AddRange(studentsList);
dbContext.AddRange(teachersList);
dbContext.AddRange(coursesList);
dbContext.AddRange(assignmentsList);
dbContext.AddRange(commentsList);
dbContext.AddRange(gradesList);
try
{
    int rows = dbContext.SaveChanges();
    Console.WriteLine($"{rows} rows saved.");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);

    if (ex.InnerException != null)
    {
        Console.WriteLine("INNER ERROR:");
        Console.WriteLine(ex.InnerException.Message);
    }
}

List<Course> courses = dbContext.Courses.ToList();
foreach(var course in courses)
{
    Console.WriteLine($"Course: {course.CourseName}");
}

string courseName = "SQL";
int courseId = dbContext.Courses
    .Where(c => c.CourseName == courseName)
    .Select(c => c.Id)
    .FirstOrDefault();
List<Assignment> assignments = dbContext.Assignments
    .Where(a => a.CourseId == courseId)
    .ToList();
foreach(var assignment in assignments)
{
    Console.WriteLine($"Assignment: {assignment.AssignmentTitle} for course {courseName}");
}

List<User> students = dbContext.Users
    .Where(u => u.Role == "Student")
    .ToList();
foreach (var student in students)
{
    Console.WriteLine($"Student: {student.FirstName} {student.LastName}");
}

int assignmentId = 6;
List<Comment> comments = dbContext.Comments
    .Where(c => c.AssignmentId == assignmentId)
    .ToList();
foreach(var comment in comments)
{
    Console.WriteLine($"Comment: {comment.CommentContent} for assignment {assignmentId}");
}

string studentUserName = "Mouaz";
User? user = user = dbContext.Users
    .Include(u => u.Grades)
        .ThenInclude(g => g.Assignment)
    .First(u => u.UserName == studentUserName);
if (user != null)
{
    List<Grade> grades = user.Grades.ToList();
    foreach(var grade in grades)
    {
        Console.WriteLine($"Grade: {grade.Score} for assignment {grade.Assignment.AssignmentTitle}");
    }
}


List<Assignment> assignments1 = dbContext.Assignments
    .Include(a => a.Course)
        .ThenInclude(c => c.Teacher)
    .ToList();
foreach(var assignment in assignments1)
{
    Console.WriteLine($"Assignment: {assignment.AssignmentTitle}, in Course: {assignment.Course.CourseName}, Teacher's full name: {assignment.Course.Teacher.FirstName} {assignment.Course.Teacher.LastName}");
}

int CourseId = 1;
List<float> gradesInCourse = dbContext.Grades
    .Where(g => g.Assignment.CourseId == CourseId)
    .Select(g => g.Score)
    .ToList();
float avg = Enumerable.Average(gradesInCourse);
Console.WriteLine($"Avg in Course {CourseId}: {avg}");

string GetStudentPerformance(int StudentId)
{
    List<Grade> grades = dbContext.Grades
        .Where(g => g.StudentId == StudentId)
        .Include(g => g.Assignment)
        .ToList();
    if (!grades.Any())
    {
        return "N/A";
    }
    float totalWeightedScore = 0;
    float totalWeight = 0;
    foreach(var grade in grades)
    {
        float percentage = (grade.Score / grade.Assignment.MaxGrade) * 100;
        totalWeightedScore += percentage * grade.Assignment.Weight;
        totalWeight += grade.Assignment.Weight;
    }
    float finalPercentage = totalWeightedScore / totalWeight;
    return finalPercentage switch
    {
        >= 90 => "A",
        >= 80 => "B",
        >= 70 => "C",
        >= 60 => "D",
        _ => "F"
    };
}
int studentId = 2;

float CalculateGpa(int studentId)
{
    string letterGrade = GetStudentPerformance(studentId);
    return letterGrade switch
    {
        "A" => 4.0f,
        "B" => 3.0f,
        "C" => 2.0f,
        "D" => 1.0f,
        _ => 0.0f
    };
}
Console.WriteLine($"Student with ID: {studentId} performance: {GetStudentPerformance(studentId)} And GPA: {CalculateGpa(studentId)}");


string username = "Mouaz";
User? student1 = dbContext.Users
    .Where(u => u.UserName == username)
    .FirstOrDefault();

if(student1 != null)
{
    student1.Role = "Teacher";
    dbContext.Users.Update(student1);
    dbContext.SaveChanges();
}

int commentId = 1;
Comment? comment1 = dbContext.Comments.Find(commentId);
if(comment1 != null)
{
    dbContext.Remove(comment1);
    dbContext.SaveChanges();
}