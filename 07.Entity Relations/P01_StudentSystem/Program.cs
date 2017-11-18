


namespace P01_StudentSystem
{

    using P01_StudentSystem.Data;
    public class Program
    {
        static void Main()
        {

            DatabaseInitializier.ResetDatabase();
            //using (var db = new StudentSystemContext())
            //{
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();
            //}
        }
    }
}
