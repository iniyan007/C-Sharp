using System;
using System.Reflection;

public class RunnerService
{
    public void ExecuteAll()
    {
        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (method.GetCustomAttribute(typeof(RunnableAttribute)) != null)
                {
                    Console.WriteLine($"Running: {type.Name}.{method.Name}");

                    object instance = Activator.CreateInstance(type);
                    method.Invoke(instance, null);
                }
            }
        }
    }
}