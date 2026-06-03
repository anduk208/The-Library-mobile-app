using LibraryApp.Services;

namespace LibraryApp.Views;

public partial class CatalogPage : ContentPage
{
    public CatalogPage()
    {
        InitializeComponent();
        LoadBooks();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadBooks(); // Перезагружаем книги при каждом открытии страницы
    }

    private void LoadBooks()
    {
        BooksList.Children.Clear();

        foreach (var book in LibraryDataService.Books)
        {
            var border = new Border
            {
                Stroke = Color.FromArgb("#2C3E50"),
                StrokeThickness = 2,
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 10 },
                BackgroundColor = Colors.White,
                Padding = 15,
                Margin = new Thickness(0, 0, 0, 15)
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto)
                },
                RowDefinitions =
                {
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto)
                }
            };

            // Название книги
            var titleLabel = new Label
            {
                Text = book.Title,
                FontSize = 22,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black
            };
            Grid.SetRow(titleLabel, 0);
            Grid.SetColumn(titleLabel, 0);
            grid.Children.Add(titleLabel);

            // Статус
            var statusLabel = new Label
            {
                Text = book.IsAvailable ? "Доступна" : "Выдана",
                TextColor = book.IsAvailable ? Colors.Green : Colors.Red,
                FontAttributes = FontAttributes.Bold,
                FontSize = 16
            };
            Grid.SetRow(statusLabel, 0);
            Grid.SetColumn(statusLabel, 1);
            grid.Children.Add(statusLabel);

            // Автор и год
            var authorLabel = new Label
            {
                Text = $"{book.Author}, {book.Year} г.",
                TextColor = Color.FromArgb("#2C3E50"),
                FontAttributes = FontAttributes.Bold
            };
            Grid.SetRow(authorLabel, 1);
            Grid.SetColumn(authorLabel, 0);
            grid.Children.Add(authorLabel);

            // Жанр
            var genreLabel = new Label
            {
                Text = book.Genre,
                FontSize = 14,
                TextColor = Color.FromArgb("#2C3E50"),
                FontAttributes = FontAttributes.Bold
            };
            Grid.SetRow(genreLabel, 1);
            Grid.SetColumn(genreLabel, 1);
            grid.Children.Add(genreLabel);

            border.Content = grid;
            BooksList.Children.Add(border);
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        // Простая реализация поиска
        string searchText = e.NewTextValue?.ToLower() ?? "";

        BooksList.Children.Clear();

        var filteredBooks = string.IsNullOrWhiteSpace(searchText)
            ? LibraryDataService.Books
            : LibraryDataService.Books.Where(b =>
                b.Title.ToLower().Contains(searchText) ||
                b.Author.ToLower().Contains(searchText));

        foreach (var book in filteredBooks)
        {
            var border = new Border
            {
                Stroke = Color.FromArgb("#2C3E50"),
                StrokeThickness = 2,
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 10 },
                BackgroundColor = Colors.White,
                Padding = 15,
                Margin = new Thickness(0, 0, 0, 15)
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto)
                },
                RowDefinitions =
                {
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto)
                }
            };

            var titleLabel = new Label
            {
                Text = book.Title,
                FontSize = 22,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black
            };
            Grid.SetRow(titleLabel, 0);
            Grid.SetColumn(titleLabel, 0);
            grid.Children.Add(titleLabel);

            var statusLabel = new Label
            {
                Text = book.IsAvailable ? "Доступна" : "Выдана",
                TextColor = book.IsAvailable ? Colors.Green : Colors.Red,
                FontAttributes = FontAttributes.Bold,
                FontSize = 16
            };
            Grid.SetRow(statusLabel, 0);
            Grid.SetColumn(statusLabel, 1);
            grid.Children.Add(statusLabel);

            var authorLabel = new Label
            {
                Text = $"{book.Author}, {book.Year} г.",
                TextColor = Color.FromArgb("#2C3E50"),
                FontAttributes = FontAttributes.Bold
            };
            Grid.SetRow(authorLabel, 1);
            Grid.SetColumn(authorLabel, 0);
            grid.Children.Add(authorLabel);

            border.Content = grid;
            BooksList.Children.Add(border);
        }
    }
}