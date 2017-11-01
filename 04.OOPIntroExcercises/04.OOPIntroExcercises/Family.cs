
    using System.Collections.Generic;
    using System.Linq;

class Family
{
    private List<Person> people;

    public List<Person> People
    {
        get { return this.people; }
        set { this.people =value; }      
    }

    public Family()
    {
        this.people =new List<Person>();
    }

    public void AddMember(Person member)
    {
        this.people.Add(member);
     
    }

    public Person GetOldestMember()
    {
        var person = this.people.OrderByDescending(e => e.Age).FirstOrDefault();
        return person;
    }

    public override string ToString()
    {
        return $"{this.GetOldestMember().Name} {this.GetOldestMember().Age}";
    }
}

