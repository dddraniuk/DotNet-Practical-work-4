using System;
using System.Diagnostics;

class SortingAndSearch
{
    static void Main()
    {
        // 1, 4
        int[] data1 = { 64, 34, 25, 12, 22, 11, 90, 45 };
        int[] data2 = (int[])data1.Clone();

        var resBubble = BubbleSortWithStats(data1);
        var resSelection = SelectionSortWithStats(data2);

        Console.WriteLine("1, 4: Статистика сортувань:");
        Console.WriteLine($"Bubble Sort    -> Порiвнянь: {resBubble.comp}, Обмiнiв: {resBubble.swaps}");
        Console.WriteLine($"Selection Sort -> Порiвнянь: {resSelection.comp}, Обмiнiв: {resSelection.swaps}");

        // 2
        int target = 22;
        int index = LinearSearch(data1, target);
        Console.WriteLine($"\n2: iндекс елемента {target}: {index}");

        // 3
        int[] largeArray = new int[10000];
        Random rnd = new Random();
        for (int i = 0; i < largeArray.Length; i++)
        {
            largeArray[i] = rnd.Next(0, 10000);
        }
        int[] largeArrayCopy = (int[])largeArray.Clone();

        Stopwatch sw = new Stopwatch();

        sw.Start();
        BubbleSortWithStats(largeArray);
        sw.Stop();
        double bubbleTime = sw.Elapsed.TotalMilliseconds;

        sw.Restart();
        Array.Sort(largeArrayCopy);
        sw.Stop();
        double arraySortTime = sw.Elapsed.TotalMilliseconds;

        Console.WriteLine($"\n3: Час сортування 10 000 елементiв:");
        Console.WriteLine($"BubbleSort: {bubbleTime:F2} мс");
        Console.WriteLine($"Array.Sort: {arraySortTime:F4} мс");
    }

    // 4
    static (int comp, int swaps) BubbleSortWithStats(int[] arr)
    {
        int comp = 0, swaps = 0;
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                comp++;
                if (arr[j] > arr[j + 1])
                {
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    swaps++;
                }
            }
        }
        return (comp, swaps);
    }

    // 1
    static (int comp, int swaps) SelectionSortWithStats(int[] arr)
    {
        int comp = 0, swaps = 0;
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < n; j++)
            {
                comp++;
                if (arr[j] < arr[minIdx]) minIdx = j;
            }
            if (minIdx != i)
            {
                (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
                swaps++;
            }
        }
        return (comp, swaps);
    }

    // 2
    static int LinearSearch(int[] arr, int target)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == target) return i;
        }
        return -1;
    }
}