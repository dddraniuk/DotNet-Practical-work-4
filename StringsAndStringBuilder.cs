using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

class StringsAndStringBuilder
{
    static void Main()
    {
        
        string testStr = "Я несу гусеня";
        Console.WriteLine($"Рядок: \"{testStr}\"");
        Console.WriteLine($"Це палiндром: {IsPalindrome(testStr)}\n");

       
        string text = "Шахтар перемiг Динамо";
        CountLetters(text);

      
        string original = "Hello World!";
        int key = 3;
        string encrypted = CaesarCipher(original, key);
        Console.WriteLine($"\nОригiнал: {original}");
        Console.WriteLine($"Шифр (K={key}): {encrypted}\n");

        
        RunBenchmark(10_000);
        RunBenchmark(100_000);
    }

    static bool IsPalindrome(string s)
    {
        
        string clean = new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();
        string reversed = new string(clean.Reverse().ToArray());
        return clean == reversed;
    }

    static void CountLetters(string s)
    {
        var dict = new Dictionary<char, int>();
        foreach (char c in s.ToLower().Where(char.IsLetter))
        {
            if (dict.ContainsKey(c)) dict[c]++;
            else dict[c] = 1;
        }

        Console.WriteLine("Частота лiтер (спадання):");
        foreach (var entry in dict.OrderByDescending(x => x.Value))
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    static string CaesarCipher(string text, int k)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in text)
        {
            if (!char.IsLetter(c)) { result.Append(c); continue; }

            char offset = char.IsUpper(c) ? 'A' : 'a';
            
            result.Append((char)((((c + k) - offset) % 26) + offset));
        }
        return result.ToString();
    }

    static void RunBenchmark(int iterations)
    {
        Console.WriteLine($"--- Бенчмарк: {iterations} iтерацiй ---");

        Stopwatch sw = Stopwatch.StartNew();
        string s = "";
        for (int i = 0; i < iterations; i++) s += "a";
        sw.Stop();
        long timeStr = sw.ElapsedMilliseconds;

        sw.Restart();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < iterations; i++) sb.Append("a");
        sw.Stop();

        Console.WriteLine($"string +=     : {timeStr} мс");
        Console.WriteLine($"StringBuilder : {sw.ElapsedMilliseconds} мс\n");
    }
}