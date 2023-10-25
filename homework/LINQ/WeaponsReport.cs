namespace homework.LINQ.WeaponsReport;

internal static class Program
{
    public static void Main(string[] args)
    {
        new MilitaryBase().Work();
    }
}

class MilitaryBase
{
    public void Work()
    {
    }
}

class Soldier
{
    public Soldier(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}