using System;

public class TaskOne
{
    [Runnable]
    public void RunTask()
    {
        Console.WriteLine("TaskOne executed");
    }

    public void Ignore()
    {
        Console.WriteLine("Should not run");
    }
}