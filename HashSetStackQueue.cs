using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class HashSetStackQueue
{
    static void Main()
    {
        
        string expression = "{[()]}";
        Console.WriteLine($"Рядок: {expression} | Баланс: {IsBalanced(expression)}");

        
        Queue<string> printQueue = new Queue<string>();
        printQueue.Enqueue("Звiт.pdf");
        printQueue.Enqueue("Фото.jpg");
        printQueue.Enqueue("Дипломна.docx");

        Console.WriteLine("\n--- Черга друку ---");
        while (printQueue.Count > 0)
        {
            Console.WriteLine($"Друкується: {printQueue.Dequeue()}... Готово.");
        }

        
        int[] arr1 = Enumerable.Range(0, 10000).ToArray();
        int[] arr2 = Enumerable.Range(5000, 10000).ToArray();
        BenchmarkIntersect(arr1, arr2);

        
        var phoneBook = new SortedDictionary<string, string>
        {
            { "Ранюк", "067-111-22-33" },
            { "Антонович", "050-444-55-66" },
            { "Ярмоленко", "093-777-88-99" }
        };

        Console.WriteLine("\n --- Телефонна книга (А-Я) --- ");
        foreach (var contact in phoneBook)
            Console.WriteLine($"{contact.Key,-10} : {contact.Value}");
    }

    static bool IsBalanced(string s)
    {
        Stack<char> stack = new Stack<char>();
        var pairs = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' } };

        foreach (char c in s)
        {
            if ("{[(".Contains(c)) stack.Push(c);
            else if (pairs.ContainsKey(c))
            {
                if (stack.Count == 0 || stack.Pop() != pairs[c]) return false;
            }
        }
        return stack.Count == 0;
    }

    static void BenchmarkIntersect(int[] a, int[] b)
    {
        Console.WriteLine("\n--- Порiвняння пошуку спiльних елементiв ---");
        Stopwatch sw = Stopwatch.StartNew();

        
        var set = new HashSet<int>(a);
        set.IntersectWith(b);
        sw.Stop();
        Console.WriteLine($"HashSet (IntersectWith): {sw.Elapsed.TotalMilliseconds} мс");

        
        sw.Restart();
        var result = new List<int>();
        foreach (var x in a)
            foreach (var y in b)
                if (x == y) { result.Add(x); break; }
        sw.Stop();
        Console.WriteLine($"Вкладенi цикли: {sw.Elapsed.TotalMilliseconds} мс");
    }
}