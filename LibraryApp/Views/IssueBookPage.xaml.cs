using LibraryApp.Services;

namespace LibraryApp.Views;

public partial class IssueBookPage : ContentPage
{
    public IssueBookPage()
    {
        InitializeComponent();
        LoadData();
        DatePickerIssue.Date = DateTime.Now;
        DatePickerReturn.Date = DateTime.Now.AddDays(14);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData(); // Перезагружаем данные
    }

    private void LoadData()
    {
        // Загружаем только доступные книги
        PickerBook.Items.Clear();
        foreach (var book in LibraryDataService.Books.Where(b => b.IsAvailable))
        {
            PickerBook.Items.Add(book.Title);
        }

        // Загружаем всех читателей
        PickerReader.Items.Clear();
        foreach (var reader in LibraryDataService.Readers)
        {
            PickerReader.Items.Add($"{reader.FullName} (Билет №{reader.CardNumber})");
        }
    }

    private async void OnIssueBookClicked(object sender, EventArgs e)
    {
        if (PickerBook.SelectedIndex == -1 || PickerReader.SelectedIndex == -1)
        {
            LabelResult.TextColor = Colors.Red;
            LabelResult.Text = "Ошибка: Выберите книгу и читателя!";
            return;
        }

        string selectedBook = PickerBook.Items[PickerBook.SelectedIndex];
        string selectedReader = PickerReader.Items[PickerReader.SelectedIndex];

        // Проверяем даты
        if (!DatePickerIssue.Date.HasValue || !DatePickerReturn.Date.HasValue)
        {
            LabelResult.TextColor = Colors.Red;
            LabelResult.Text = "Ошибка: Выберите даты!";
            return;
        }

        // РЕАЛЬНАЯ ВЫДАЧА!
        bool success = LibraryDataService.IssueBook(selectedBook, selectedReader);

        if (success)
        {
            LabelResult.TextColor = Colors.Green;
            LabelResult.Text = $"Книга '{selectedBook}' выдана читателю {selectedReader}!";

            string issueDate = DatePickerIssue.Date.Value.ToString("dd.MM.yyyy");
            string returnDate = DatePickerReturn.Date.Value.ToString("dd.MM.yyyy");

            await DisplayAlert("Успех", $"Книга успешно выдана!\nДата выдачи: {issueDate}\nДата возврата: {returnDate}", "OK");

            // Перезагружаем списки
            LoadData();
        }
        else
        {
            LabelResult.TextColor = Colors.Red;
            LabelResult.Text = "Ошибка: Книга уже выдана или не найдена!";
        }
    }
}