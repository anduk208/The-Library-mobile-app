using LibraryApp.Models;
using LibraryApp.Services;

namespace LibraryApp.Views;

public partial class AddReaderPage : ContentPage
{
    public AddReaderPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterReaderClicked(object sender, EventArgs e)
    {
        // Валидация
        if (string.IsNullOrWhiteSpace(EntryFullName.Text))
        {
            await DisplayAlert("Ошибка", "Введите ФИО читателя!", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(EntryCardNumber.Text))
        {
            await DisplayAlert("Ошибка", "Введите номер читательского билета!", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(EntryPhone.Text))
        {
            await DisplayAlert("Ошибка", "Введите контактный телефон!", "OK");
            return;
        }

        LoadingIndicator.IsRunning = true;
        await Task.Delay(500);

        // РЕАЛЬНОЕ СОХРАНЕНИЕ!
        var newReader = new Reader
        {
            FullName = EntryFullName.Text.Trim(),
            CardNumber = EntryCardNumber.Text.Trim(),
            Phone = EntryPhone.Text.Trim()
        };

        LibraryDataService.AddReader(newReader);

        LoadingIndicator.IsRunning = false;

        await DisplayAlert("Успех", $"Читатель '{newReader.FullName}' успешно зарегистрирован!", "OK");

        // Очистка полей
        EntryFullName.Text = "";
        EntryCardNumber.Text = "";
        EntryPhone.Text = "";
    }
}