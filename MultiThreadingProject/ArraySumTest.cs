using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace MultiThreadingProject;

//[MinIterationCount(15)]
//[MaxIterationCount(20)]
//[InvocationCount(10)]
[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90)]
public class ArraySumTest
{
    private int[] sourceArray;


    #region SetupParameters

    [Params(0)]
    public int Min { get; set; }

    [Params(100)]
    public int Max { get; set; }

    [Params(100000, 1000000, 10000000)]
    public int Size { get; set; }


    [GlobalSetup]
    public void Setup()
    {
        sourceArray = GenerateArrayOfInts(Min, Max, Size);
    }

    #endregion SetupParameters


    [Benchmark]
    public void StandardSum()
    {
        var sum = 0;
        foreach (var item in sourceArray)
        {
            sum += item;
        }

        //Console.WriteLine("Sum of sequential process: {0}", sum);
    }

    [Benchmark]
    public void ThreadSum()
    {
        //Разбиваем массив на куски.
        //Возьмём кол-во кусков = кол-ву ядер процессора.
        var partsCount = Environment.ProcessorCount;
        var chunkSize = sourceArray.Length / partsCount + 1;
        var chunks = sourceArray.Chunk(chunkSize)
                                .ToArray();

        var threads = new List<Thread>(chunks.Length);

        var sum = 0;

        // Каждый кусок обрабатываем в отдельном потоке.
        foreach (var chunk in chunks)
        {
            var chunkThread = new Thread(() =>
            {
                var partSum = chunk.Sum();
                Interlocked.Add(ref sum, partSum);
            });

            threads.Add(chunkThread);
            chunkThread.Start();
        }

        // Потоки нужно синхронизировать и получить общий результат
        foreach (var thread in threads)
        {
            thread.Join();
        }

        //Console.WriteLine("Sum of thread process: {0}", sum);
    }

    [Benchmark]
    public void PlinkSum()
    {
        var sum = sourceArray.AsParallel()
                             .AsOrdered()
                             .Sum();

        //Console.WriteLine("Sum of plink process: {0}", sum);
    }


    /// <summary>
    /// Метод генерит массив целых случайных чисел от min до max размером size.
    /// </summary>
    /// <param name="min">Минимальное число в массиве</param>
    /// <param name="max">Максимальное число в массиве</param>
    /// <param name="size">Размер массива</param>
    /// <returns>Итоговый массив целых чисел.</returns>
    static int[] GenerateArrayOfInts(int min, int max, int size)
    {
        var rand = new Random();

        return Enumerable.Repeat(0, size)
                         .Select(i => rand.Next(min, max))
                         .ToArray();
    }
}
