using System;

namespace Csharp.OOP.Basics.Encapsulation._6.FootballTeamGenerator
{
    partial class Program
    {
        private class Skill
        {
            private int _amount;
            private SkillType Type { get; }

            public int Amount
            {
                get { return _amount; }
                private set
                {
                    if (value < 0 || value > 100)
                        throw new ArgumentException($"{this.Type} should be between 0 and 100.");

                    this._amount = value;
                }
            }

            public Skill(SkillType skillType, int val)
            {
                this.Type = skillType;
                this.Amount = val;
            }

            public enum SkillType
            {
                Endurance = 0,
                Sprint = 1,
                Dribble = 2,
                Passing = 3,
                Shooting = 4
            }
        }
    }
}