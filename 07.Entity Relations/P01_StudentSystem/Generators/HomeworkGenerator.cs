

using System.Linq;
using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class HomeworkGenerator
    {
        private static Random rnd = new Random();

        private static string[] contents =
        {
                "01.FirstTopic",
                "02.SecondTopic",
                "03.ThirdTopic",
                "04.FourthTopic",
                "05.FifthTopic",
                "06.SixthTopic"
        };

        internal static void InitialHomeworkSeed(StudentSystemContext db, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var homework = NewHomework();

                db.HomeworkSubmissions.Add(homework);
                db.SaveChanges();
            }
        }

        private static Homework NewHomework()
        {
            Homework homework = new Homework()
            {
                Content = NewContent(),
                SubmissionTime = DateGenerator.GenerateDate(),
                ContentType = GetRandomContentType(),
                StudentId = GetRandomStudentFromDb(),
                CourseId = GetRandomCourseFromDb()
            };

            return homework;
        }

        private static int GetRandomCourseFromDb()
        {
            using (var db = new StudentSystemContext())
            {
                var coursesIds = db
                    .Courses
                    .Select(c => c.CourseId)
                    .ToArray();

                return coursesIds[rnd.Next(0, coursesIds.Length)];
            }
        }

        private static int GetRandomStudentFromDb()
        {
            using (var db = new StudentSystemContext())
            {
                var studentsIds = db
                    .Students
                    .Select(s => s.StudentId)
                    .ToArray();

                return studentsIds[rnd.Next(0, studentsIds.Length)];
            }
        }

        private static ContentType GetRandomContentType()
        {
            ContentType[] types = new ContentType[]
            {
                ContentType.Application,
                ContentType.Pdf,
                ContentType.Zip
            };

            return types[rnd.Next(0, types.Length)];
        }

        private static string NewContent()
        {
            return contents[rnd.Next(0, contents.Length)];
        }
    }
}
