namespace ReflectionImprovements;

public class Person
{
    private int _age;

    internal Person(int age)
    {
        _age = age;
    }

    private int GetAge()
    {
        return _age;
    }

    private void SetAge(int age)
    {
        _age = age;
    }
}