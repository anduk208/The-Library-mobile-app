using LibraryApp.Models;
using LibraryApp.Services;

namespace LibraryApp.Views;

public partial class AddBookPage : ContentPage
{
    public AddBookPage()
    {
        InitializeComponent();
        DatePickerYear.Date = DateTime.Now;
    }

    private async void OnSaveBookClicked(object sender, EventArgs e)
    {
        // Валидация
        if (string.IsNullOrWhiteSpace(EntryTitle.Text) || string.IsNullOrWhiteSpace(EntryAuthor.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, заполните название и автора!", "OK");
            return;
        }

        if (PickerGenre.SelectedIndex == -1)
        {
            await DisplayAlert("Ошибка", "Выберите жанр книги!", "OK");
            return;
        }

        // Проверяем, что дата выбрана
        if (!DatePickerYear.Date.HasValue)
        {
            await DisplayAlert("Ошибка", "Выберите год издания!", "OK");
            return;
        }

        // Имитация загрузки
        LoadingIndicator.IsRunning = true;
        await Task.Delay(500);

        // РЕАЛЬНОЕ СОХРАНЕНИЕ!
        var newBook = new Book
        {
            Title = EntryTitle.Text.Trim(),
            Author = EntryAuthor.Text.Trim(),
            Year = DatePickerYear.Date.Value.Year, // БЕЗОПАСНО: проверили HasValue выше
            Genre = PickerGenre.Items[PickerGenre.SelectedIndex],
            IsAvailable = true
        };

        LibraryDataService.AddBook(newBook);

        LoadingIndicator.IsRunning = false;

        await DisplayAlert("Успех", $"Книга '{newBook.Title}' успешно добавлена в каталог!", "OK");

        // Очистка полей
        EntryTitle.Text = "";
        EntryAuthor.Text = "";
        PickerGenre.SelectedIndex = -1;
        DatePickerYear.Date = DateTime.Now;
    }
}