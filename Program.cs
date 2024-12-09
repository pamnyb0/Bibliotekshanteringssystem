using System;
using System.Collections.Generic;

class Program
{
    static List<User> users = new List<User>
    {
        new User(1, "admin", "adminpass", true), // Fördefinierad admin
        new User(2, "user", "userpass", false) // Fördefinierad användare
    };

    static List<Book> books = new List<Book>
    {
        new Book(1, "Bok 1", "Författare 1", "123-ABC", true),
        new Book(2, "Bok 2", "Författare 2", "456-DEF", true),
        new Book(3, "Bok 3", "Författare 3", "789-GHI", true),
    };

    static User? loggedInUser = null;

    static void Main()
    {
        while (loggedInUser == null)
        {
            Console.WriteLine("1. Registrera");
            Console.WriteLine("2. Logga in");
            Console.Write("Välj ett alternativ: ");
            string choice = Console.ReadLine();

            if (choice == "1")
                RegisterUser();
            else if (choice == "2")
                LoginUser();
            else
                Console.WriteLine("Ogiltigt val. Försök igen.");
        }

        MainMenu();
    }

    static void RegisterUser()
    {
        Console.Write("Användarnamn: ");
        string username = Console.ReadLine();
        Console.Write("Lösenord: ");
        string password = Console.ReadLine();
        int newUserId = users.Count + 1;

        users.Add(new User(newUserId, username, password, false));
        Console.WriteLine("Registrering lyckades! Logga in för att fortsätta.");
    }

    static void LoginUser()
    {
        Console.Write("Användarnamn: ");
        string username = Console.ReadLine();
        Console.Write("Lösenord: ");
        string password = Console.ReadLine();

        foreach (var user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                loggedInUser = user;
                Console.WriteLine($"Välkommen, {loggedInUser.Username}!");
                return;
            }
        }
        Console.WriteLine("Ogiltigt användarnamn eller lösenord.");
    }

    static void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\nHuvudmeny:");
            Console.WriteLine("1. Visa böcker");
            Console.WriteLine("2. Låna bok");
            Console.WriteLine("3. Återlämna bok");
            if (loggedInUser.IsAdmin)
            {
                Console.WriteLine("4. Lägg till bok");
                Console.WriteLine("5. Ta bort bok");
            }
            Console.WriteLine("0. Avsluta");
            Console.Write("Välj ett alternativ: ");

            string? choice = Console.ReadLine();

            if (choice == null)
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
                continue;
            }

            switch (choice)
            {
                case "1":
                    ShowBooks();
                    break;
                case "2":
                    BorrowBook();
                    break;
                case "3":
                    ReturnBook();
                    break;
                case "4":
                    if (loggedInUser.IsAdmin) AddBook();
                    else Console.WriteLine("Ogiltigt val.");
                    break;
                case "5":
                    if (loggedInUser.IsAdmin) RemoveBook();
                    else Console.WriteLine("Ogiltigt val.");
                    break;
                case "0":
                    Console.WriteLine("Avslutar programmet. Hejdå!");
                    return;
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }
        }
    }

    static void ShowBooks()
    {
        Console.WriteLine("\nTillgängliga böcker:");
        foreach (var book in books)
        {
            Console.WriteLine($"ID: {book.BookId}, Titel: {book.Title}, Författare: {book.Author}, Tillgänglig: {book.IsAvailable}");
        }
    }

    static void BorrowBook()
    {
        Console.Write("\nAnge ID för boken du vill låna: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Ogiltigt ID. Försök igen.");
            return;
        }

        var book = books.Find(b => b.BookId == bookId);

        if (book != null && book.IsAvailable)
        {
            book.IsAvailable = false;
            Console.WriteLine($"Du har lånat boken \"{book.Title}\".");
        }
        else
        {
            Console.WriteLine("Boken är inte tillgänglig eller hittades inte.");
        }
    }

    static void ReturnBook()
    {
        Console.Write("\nAnge ID för boken du vill lämna tillbaka: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Ogiltigt ID. Försök igen.");
            return;
        }

        var book = books.Find(b => b.BookId == bookId);

        if (book != null && !book.IsAvailable)
        {
            book.IsAvailable = true;
            Console.WriteLine($"Du har återlämnat boken \"{book.Title}\".");
        }
        else
        {
            Console.WriteLine("Boken är redan tillgänglig eller hittades inte.");
        }
    }

    static void AddBook()
    {
        Console.Write("\nAnge titel på boken: ");
        string title = Console.ReadLine();
        Console.Write("Ange författare: ");
        string author = Console.ReadLine();
        Console.Write("Ange ISBN: ");
        string isbn = Console.ReadLine();

        int newBookId = books.Count + 1;
        books.Add(new Book(newBookId, title, author, isbn, true));

        Console.WriteLine($"Boken \"{title}\" har lagts till i biblioteket.");
    }

    static void RemoveBook()
    {
        Console.Write("\nAnge ID för boken du vill ta bort: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Ogiltigt ID. Försök igen.");
            return;
        }

        var book = books.Find(b => b.BookId == bookId);

        if (book != null)
        {
            books.Remove(book);
            Console.WriteLine($"Boken \"{book.Title}\" har tagits bort från biblioteket.");
        }
        else
        {
            Console.WriteLine("Boken hittades inte.");
        }
    }
}
