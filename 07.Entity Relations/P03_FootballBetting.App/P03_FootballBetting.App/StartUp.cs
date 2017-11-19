
namespace P03_FootballBetting.App
{

    using P03_FootballBetting.App.Data;

    public class Program
    {
        static void Main(string[] args)
        {
            using (var db=new FootballBettingContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
