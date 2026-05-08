using System;
using System.Linq;

class ArraysAndCollections
{
    static void Main()
    {
        // 1
        int[] arr1 = { 0, 10, 20, 30, 40, 50, 60 };
        ManualReverse(arr1);
        Console.WriteLine($"1: {string.Join(", ", arr1)}");

        // 2
        int[] arr2 = { 10, 40, 20, 60, 30, 50 };
        int secondMax = FindSecondMax(arr2);
        Console.WriteLine($"2: {secondMax}");

        // 3
        int[] arr3 = { 1, 2, 2, 3, 4, 4, 5, 1 };
        int[] unique = RemoveDuplicates(arr3);
        Console.WriteLine($"3: {string.Join(", ", unique)}");

        // 4
        int[] a = { 0, 10, 20, 30, 40, 50, 60 };
        var slice = a[2..5];
        Console.WriteLine($"4 (slice): {string.Join(", ", slice)}");
        Console.WriteLine($"4 (^2): {a[^2]}");
        Console.WriteLine($"4 (Length-2): {a[a.Length - 2]}");
    }

    // 1
    static void ManualReverse(int[] arr)
    {
        for (int i = 0; i < arr.Length / 2; i++)
        {
            (arr[i], arr[arr.Length - 1 - i]) = (arr[arr.Length - 1 - i], arr[i]);
        }
    }

    // 2
    static int FindSecondMax(int[] arr)
    {
        int max = int.MinValue;
        int secondMax = int.MinValue;
        foreach (int x in arr)
        {
            if (x > max)
            {
                secondMax = max;
                max = x;
            }
            else if (x > secondMax && x != max)
            {
                secondMax = x;
            }
        }
        return secondMax;
    }

    // 3
    static int[] RemoveDuplicates(int[] arr)
    {
        int[] temp = new int[arr.Length];
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < count; j++)
            {
                if (temp[j] == arr[i])
                {
                    exists = true;
                    break;
                }
            }
            if (!exists) temp[count++] = arr[i];
        }
        int[] result = new int[count];
        Array.Copy(temp, result, count);
        return result;
    }
}