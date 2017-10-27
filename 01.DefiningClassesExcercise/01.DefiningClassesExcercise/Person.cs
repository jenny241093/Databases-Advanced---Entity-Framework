public class Person
{
    private string name;
    private int age;
    public int Age { get; private set; }
    public string Name { get; private set; }

    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }
}