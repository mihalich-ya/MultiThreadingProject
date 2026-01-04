using BenchmarkDotNet.Running;
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


        BenchmarkRunner.Run<ArraySumTest>();
    }
}
