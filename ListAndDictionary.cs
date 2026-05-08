using System;
using System.Collections.Generic;
using System.Linq;

class ListAndDictionary
{
    static void Main()
    {
        
        var journal = new Dictionary<string, List<int>>
        {
            { "Поляков", new List<int> { 95, 92, 88 } },
            { "Ранюк", new List<int> { 65, 70, 58 } }, 
            { "Пiдлiсна", new List<int> { 80, 75, 82 } },
            { "Усов", new List<int> { 98, 95, 99 } },
            { "ван Россум", new List<int> { 62, 58, 60 } }  
        };

        
        Console.WriteLine("--- Рейтинг студентiв ---");
        var rating = journal
            .Select(s => new { Name = s.Key, Avg = s.Value.Average() })
            .OrderByDescending(s => s.Avg);

        foreach (var student in rating)
        {
            Console.WriteLine($"{student.Name,-12} | Середнiй бал: {student.Avg:F1}");
        }

        
        Console.WriteLine("\n--- Студенти з оцiнками нижче 60 ---");
        var atRiskStudents = journal
            .Where(s => s.Value.Any(grade => grade < 60))
            .Select(s => s.Key);

        foreach (var name in atRiskStudents)
        {
            Console.WriteLine($"Студент: {name}");
        }

        
        Console.WriteLine("\n--- Розподiл за дiапазонами ---");
        var ranges = new Dictionary<string, int>
        {
            { "60–74 ", 0 },
            { "75–89 ", 0 },
            { "90–100", 0 }
        };

        foreach (var grades in journal.Values)
        {
            double avg = grades.Average();
            if (avg >= 90) ranges["90–100"]++;
            else if (avg >= 75) ranges["75–89 "]++;
            else if (avg >= 60) ranges["60–74 "]++;
        }

        foreach (var range in ranges)
        {
            Console.WriteLine($"{range.Key}: {new string('█', range.Value)} ({range.Value} студ.)");
        }
    }
}