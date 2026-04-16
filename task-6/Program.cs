using System;
using System.Threading;
public delegate void ThresholdReachedHandler(int count);

class Counter
{
    private int _count;
    private int _threshold;
    public event ThresholdReachedHandler ThresholdReached;

    public Counter(int threshold)
    {
        _threshold = threshold;
        _count = 0;
    }

    public void Increment()
    {
        _count++;
        Console.WriteLine($"Counter Value: {_count}");
        if (_count == _threshold)
        {
            OnThresholdReached();
        }
    }

    protected virtual void OnThresholdReached()
    {

        ThresholdReached?.Invoke(_count);
    }
}

class Program
{
    static void AlertHandler(int count)
    {
        Console.WriteLine($"Alert! Threshold reached at {count}");
    }

    static void LogHandler(int count)
    {
        Console.WriteLine($"Logging: Counter hit {count}");
    }

    static void ResetSuggestionHandler(int count)
    {
        Console.WriteLine("Suggestion: Consider resetting the counter.");
    }

    static void Main(string[] args)
    {
        Counter counter = new Counter(5);
        counter.ThresholdReached += AlertHandler;
        counter.ThresholdReached += LogHandler;
        counter.ThresholdReached += ResetSuggestionHandler;
        for (int i = 0; i < 10; i++)
        {
            counter.Increment();
            Thread.Sleep(500);
        }

        Console.WriteLine("Program finished.");
    }
}