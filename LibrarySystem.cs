using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; } = true;

        public override string ToString() =>
            $"\"{Title}\" - {Author} ({Year}) [{Genre}] | {(IsAvailable ? "Доступна" : "Видана")}";
    }

    class Program
    {
        static List<Book> books = new List<Book>();
        static Dictionary<string, List<Book>> genreMap = new Dictionary<string, List<Book>>();
        static HashSet<string> uniqueAuthors = new HashSet<string>();
        static Stack<string> historyStack = new Stack<string>();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            InitializeLibrary();

            int choice;
            do
            {
                Console.WriteLine("\n--- МЕНЮ БiБЛiОТЕКИ ---");
                Console.WriteLine("1) Показати всi книги    2) Пошук за автором");
                Console.WriteLine("3) Пошук за жанром      4) Взяти книгу");
                Console.WriteLine("5) Повернути книгу      6) Статистика");
                Console.WriteLine("0) Вихiд");
                Console.Write("Вибiр: ");

                if (!int.TryParse(Console.ReadLine(), out choice)) continue;

                switch (choice)
                {
                    case 1: ShowAllBooks(); break;
                    case 2: SearchByAuthor(); break;
                    case 3: SearchByGenre(); break;
                    case 4: BorrowBook(); break;
                    case 5: ReturnBook(); break;
                    case 6: ShowStatistics(); break;
                }
            } while (choice != 0);
        }

        static void InitializeLibrary()
        {
            var initialBooks = new List<Book>
            {
                new Book { Title = "Мiф про Сiзiфа", Author = "Альбер Камю", Year = 1942, Genre = "Есе" },
                new Book { Title = "Залиш гидливiсть, з'їж цю жабу", Author = "Брайан Трейсi", Year = 2001, Genre = "Документальна проза" },
                new Book { Title = "Пригоди Тома Соєра", Author = "Марк Твен", Year = 1876, Genre = "Повiсть" },
                new Book { Title = "Вечори на хуторi бiля Диканьки", Author = "Микола Гоголь", Year = 1832, Genre = "Повiсть" },
                new Book { Title = "Федько халамидник", Author = "Володимир Винниченко", Year = 1912, Genre = "Оповiдання" },
                new Book { Title = "Чорна Рада", Author = "Пантелеймон Кулiш", Year = 1857, Genre = "iсторичний роман" },
                new Book { Title = "Тигролови", Author = "iван Багряний", Year = 1944, Genre = "Пригоди" },
                new Book { Title = "Перевтiлення", Author = "Франц Кафка", Year = 1915, Genre = "Повiсть" }
            };

            foreach (var b in initialBooks) AddBook(b);
        }

        static void AddBook(Book b)
        {
            books.Add(b);
            uniqueAuthors.Add(b.Author);
            if (!genreMap.ContainsKey(b.Genre)) genreMap[b.Genre] = new List<Book>();
            genreMap[b.Genre].Add(b);
        }

        static void AddToHistory(string query)
        {
            
            if (historyStack.Count >= 5)
            {
                var list = historyStack.ToList();
                list.RemoveAt(list.Count - 1);
                historyStack = new Stack<string>(Enumerable.Reverse(list));
            }
            historyStack.Push(query);
        }

        static void ShowAllBooks() => books.ForEach(Console.WriteLine);

        static void SearchByAuthor()
        {
            Console.Write("Введiть автора: ");
            string query = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(query)) return;

            AddToHistory($"Пошук автора: {query}");

            var found = books.FindAll(b => b.Author.Contains(query, StringComparison.OrdinalIgnoreCase));
            if (found.Count > 0) found.ForEach(Console.WriteLine);
            else Console.WriteLine("Нiчого не знайдено.");
        }

        static void SearchByGenre()
        {
            Console.Write("Введiть жанр: ");
            string genre = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(genre)) return;

            AddToHistory($"Пошук жанру: {genre}");

            var foundKey = genreMap.Keys.FirstOrDefault(k => k.Equals(genre, StringComparison.OrdinalIgnoreCase));
            if (foundKey != null) genreMap[foundKey].ForEach(Console.WriteLine);
            else Console.WriteLine("Жанр не знайдено.");
        }

        static void BorrowBook()
        {
            Console.Write("Введiть назву книги (можна частину): ");
            string title = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(title)) return;

            
            var book = books.FirstOrDefault(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            if (book != null)
            {
                AddToHistory($"Взято книгу: {book.Title}");
                if (book.IsAvailable)
                {
                    book.IsAvailable = false;
                    Console.WriteLine($"Ви взяли книгу: {book.Title}");
                }
                else Console.WriteLine("Книга вже видана.");
            }
            else Console.WriteLine("Книга не знайдена.");
        }

        static void ReturnBook()
        {
            Console.Write("Яку книгу повертаєте? ");
            string title = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(title)) return;

            var book = books.FirstOrDefault(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            if (book != null)
            {
                AddToHistory($"Повернення: {book.Title}");
                book.IsAvailable = true;
                Console.WriteLine($"Книгу \"{book.Title}\" повернено!");
            }
            else Console.WriteLine("Книга не знайдена.");
        }

        static void ShowStatistics()
        {
            Console.WriteLine("\n--- СТАТИСТИКА ---");
            Console.WriteLine($"Всього книг: {books.Count}");
            Console.WriteLine($"Унiкальних авторiв: {uniqueAuthors.Count}");

            double availablePct = books.Count > 0 ? (double)books.Count(b => b.IsAvailable) / books.Count * 100 : 0;
            Console.WriteLine($"Доступно: {availablePct:F1}%");

            var topGenre = genreMap.OrderByDescending(g => g.Value.Count).FirstOrDefault();
            Console.WriteLine($"Популярний жанр: {topGenre.Key} ({topGenre.Value?.Count} книг)");

            
            Console.WriteLine("Останнi запити:");
            if (historyStack.Count == 0) Console.WriteLine("  iсторiя порожня.");
            else
            {
                foreach (var item in historyStack)
                {
                    Console.WriteLine($"  -> {item}");
                }
            }
        }
    }
}