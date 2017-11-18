


namespace P01_StudentSystem
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data;
    using P01_StudentSystem.Generators;

    public class DatabaseInitializier
    {
        private static Random rnd=new Random();
        public static void ResetDatabase()
        {
            using (var db=new StudentSystemContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
                InitialSeed(db);
            }
        }

        public  static void InitialSeed(StudentSystemContext db)
        {
            SeedStudents(db, 100);
            SeedCourses(db, 30);
            SeedStudentCourses(db, 120);
            SeedHomework(db, 150);
            SeedResourses(db);
        }

        private static void SeedResourses(StudentSystemContext db)
        {
            ResouceGenerator.InitialResourseSeed(db);
        }

        private static void SeedHomework(StudentSystemContext db, int count)
        {
            HomeworkGenerator.InitialHomeworkSeed(db,count);
        }

        private static void SeedStudentCourses(StudentSystemContext db, int count)
        {
            StudentsCoursesGenerator.InitialStudentCoursesSeed(db, count);
        }

        private static void SeedCourses(StudentSystemContext db, int count)
        {
          CourseGenerator.InitialCourseSeed(db,count);
        }

        private static void SeedStudents(StudentSystemContext db, int count)
        {
            StudentGenetaror.InitialStudentSeed(db, count);
        }

    }
}
