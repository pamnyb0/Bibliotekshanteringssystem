public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailable { get; set; }

    public Book(int bookId, string title, string author, string isbn, bool isAvailable)
    {
        BookId = bookId;
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = isAvailable;
    }
}
