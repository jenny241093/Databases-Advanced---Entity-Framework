using System.ComponentModel;

namespace Stations.Models.Enums
{
    [DefaultValue(Normal)]
    public enum CardType
    {
        Pupil,
        Student,
        Elder,
        Debilitated,
        Normal
    }
}