using System.Runtime.InteropServices;

namespace MultiThreadingProject;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Processor: {Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")}");
        Console.WriteLine($"OS Description: {RuntimeInformation.OSDescription}");
        Console.WriteLine($"OS Architecture: {RuntimeInformation.OSArchitecture}");
        Console.WriteLine($"FrameworkDescription: {RuntimeInformation.FrameworkDescription}");


        //BenchmarkRunner.Run<ArraySumTest>();

        var test = new ArraySumTest
        {
            Min = 0,
            Max = 100,
            Size = 1000000
        };

        test.Setup();
        test.StandardSum();
        test.ThreadSum();
        test.PlinkSum();
    }
}
