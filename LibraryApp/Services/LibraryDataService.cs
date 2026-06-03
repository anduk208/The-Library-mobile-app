using System.Collections.ObjectModel;
using LibraryApp.Models;

namespace LibraryApp.Services;

public static class LibraryDataService
{
    // Коллекции для хранения данных
    public static ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();
    public static ObservableCollection<Reader> Readers { get; } = new ObservableCollection<Reader>();

    // Статический конструктор - добавляем начальные данные
    static LibraryDataService()
    {
        // Начальные книги
        Books.Add(new Book
        {
            Title = "Война и мир",
            Author = "Л.Н. Толстой",
            Year = 1869,
            Genre = "Классика",
            IsAvailable = true
        });

        Books.Add(new Book
        {
            Title = "Мастер и Маргарита",
            Author = "М.А. Булгаков",
            Year = 1967,
            Genre = "Классика",
            IsAvailable = true
        });

        Books.Add(new Book
        {
            Title = "Преступление и наказание",
            Author = "Ф.М. Достоевский",
            Year = 1866,
            Genre = "Классика",
            IsAvailable = true
        });

        // Начальные читатели
        Readers.Add(new Reader
        {
            FullName = "Иванов Иван Иванович",
            CardNumber = "12345",
            Phone = "+7(999)000-00-00"
        });

        Readers.Add(new Reader
        {
            FullName = "Петрова Анна Сергеевна",
            CardNumber = "45678",
            Phone = "+7(999)111-11-11"
        });
    }

    // Метод добавления книги
    public static void AddBook(Book book)
    {
        Books.Add(book);
    }

    // Метод добавления читателя
    public static void AddReader(Reader reader)
    {
        Readers.Add(reader);
    }

    // Метод выдачи книги
    public static bool IssueBook(string bookTitle, string readerName)
    {
        var book = Books.FirstOrDefault(b => b.Title == bookTitle && b.IsAvailable);
        if (book != null)
        {
            book.IsAvailable = false;
            return true;
        }
        return false;
    }
}