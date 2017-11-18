

using System.Linq;
using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class StudentsCoursesGenerator
    {
        private static Random rnd = new Random();

        private static List<string> sownPairs = new List<string>();

        internal static void InitialStudentCoursesSeed(StudentSystemContext db, int count)
        {
            for (int i = 0; i < count; i++)
            {
                db.StudentCourses.Add(NewStudentCourse());
                db.SaveChanges();
            }
        }

        private static StudentCourse NewStudentCourse()
        {
            using (var db = new StudentSystemContext())
            {
                var allStudentsIds = db.Students.Select(s => s.StudentId).ToArray();
                var allCoursesIds = db.Courses.Select(c => c.CourseId).ToArray();

                var studentCourse = new StudentCourse()
                {
                    StudentId = allStudentsIds[rnd.Next(0, allStudentsIds.Length)],
                    CourseId = allCoursesIds[rnd.Next(0, allCoursesIds.Length)]
                };

                
                var currentPairToSeed = studentCourse.StudentId.ToString() + "-" + studentCourse.CourseId.ToString();

                
                if (sownPairs.Contains(currentPairToSeed))
                {
                    return NewStudentCourse();
                }

                sownPairs.Add(currentPairToSeed);
                return studentCourse;
            }
        }
    }
}
