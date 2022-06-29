using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace ReflectionImprovements;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    private readonly Person _person = new(10);
    
    private readonly MethodInfo _getAgeMethod = typeof(Person)
        .GetMethod("GetAge", BindingFlags.NonPublic | BindingFlags.Instance)!;
    
    private readonly MethodInfo _setAgeMethod = typeof(Person)
        .GetMethod("SetAge", BindingFlags.NonPublic | BindingFlags.Instance)!;
    
    private readonly ConstructorInfo _ctor = typeof(Person)
        .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, new []{ typeof(int) })!;
    
    private readonly object?[] _ageParams = { 69 };

    [Benchmark]
    public Person Ctor()
    {
        return (Person)typeof(Person)
            .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, new []{ typeof(int) })!
            .Invoke(new object?[]{69})!;
    }
    
    [Benchmark]
    public Person CtorCachedCtorInfo()
    {
        return (Person)_ctor.Invoke(new object?[]{69})!;
    }
    
    [Benchmark]
    public Person CtorCachedCtorInfoCachedParams()
    {
        return (Person)_ctor.Invoke(_ageParams)!;
    }
    
    [Benchmark]
    public int GetAge()
    {
        return (int)typeof(Person)
            .GetMethod("GetAge", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(_person, Array.Empty<object?>())!;
    }
    
    [Benchmark]
    public int GetAgeCachedMethod()
    {
        return (int) _getAgeMethod
            .Invoke(_person, Array.Empty<object?>())!;
    }
    
    [Benchmark]
    public void SetAge()
    {
        typeof(Person)
            .GetMethod("SetAge", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(_person, new object?[] { 69 });
    }
    
    [Benchmark]
    public void SetAgeCachedMethod()
    {
        _setAgeMethod.Invoke(_person, new object?[] { 69 });
    }
    
    [Benchmark]
    public void SetAgeCachedMethodCachedParams()
    {
        _setAgeMethod.Invoke(_person, _ageParams);
    }
}