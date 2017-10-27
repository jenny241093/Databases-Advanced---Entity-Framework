public class Person
{
    private string name;
    private int age;
    public int Age { get; private set; }
    public string Name { get; private set; }


    public Person()
    {
        this.Name = "No name";
        this.Age = 1;
    }
    public Person(int age)
    {
        this.Name = "No name";
        this.Age = age;
    }
    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public override string ToString()
    {
        return this.Name + " - " + this.Age;
    }
}