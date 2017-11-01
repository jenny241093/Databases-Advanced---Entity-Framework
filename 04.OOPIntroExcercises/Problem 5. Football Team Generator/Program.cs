using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp.OOP.Basics.Encapsulation._6.FootballTeamGenerator
{
    partial class Program
    {
        public static void Main()
        {
            var teams = new HashSet<Team>();

            string teamName;

            while (true)
            {
                var input = Console.ReadLine().Trim().Split(';');

                if (input[0].Equals("end", StringComparison.OrdinalIgnoreCase))
                    break;

                teamName = input[1];
                var team = teams.FirstOrDefault(x => x.Name.Equals(teamName));

                try
                {
                    string playerName;

                    if (!input[0].Equals("team", StringComparison.OrdinalIgnoreCase))
                        if (team == null)
                            throw new ArgumentException($"Team {teamName} does not exist.");

                    if (input[0].Equals("add", StringComparison.OrdinalIgnoreCase))
                    {
                        playerName = input[2];

                        var skills = (Skill.SkillType[])typeof(Skill.SkillType).GetEnumValues();

                        var playerSkills = new List<Skill>(skills.Length);

                        for (int i = 0; i < skills.Length; i++)
                        {
                            var val = int.Parse(input[i + 3]);
                            playerSkills.Add(new Skill(skills[i], val));
                        }

                        team.AddPlayer(new Player(playerName, playerSkills));
                    }
                    else if (input[0].Equals("remove", StringComparison.OrdinalIgnoreCase))
                    {
                        playerName = input[2];

                        team.RemovePlayer(playerName);
                    }
                    else if (input[0].Equals("rating", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine(team);
                    }
                    else
                    {
                        team = new Team(teamName);
                        teams.Add(team);
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }
    }
}