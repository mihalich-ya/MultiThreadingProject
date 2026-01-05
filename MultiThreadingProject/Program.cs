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

        // Начал сам собирать инфу об окружении, но бенчмарк это делает уже хорошо для разных платформ.
        // Да и результаты в табличке показывет, поэтому собственно его решил выбрать.
        BenchmarkRunner.Run<ArraySumTest>();
    }
}
