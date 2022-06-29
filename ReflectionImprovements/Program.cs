// See https://aka.ms/new-console-template for more information

using System.Reflection;
using ReflectionImprovements;

var person = new Person(10);

var age = (int)typeof(Person)
    .GetMethod("GetAge", BindingFlags.NonPublic | BindingFlags.Instance)!
    .Invoke(person, Array.Empty<object?>())!;
    
Console.WriteLine($"Age is {age}");