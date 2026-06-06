using LibraryApp.Services;

namespace LibraryApp.Views;

public partial class ReadersPage : ContentPage
{
    public ReadersPage()
    {
        InitializeComponent();
        LoadReaders();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadReaders();
    }

    private void LoadReaders()
    {
        ReadersCollection.ItemsSource = LibraryDataService.Readers;
    }
}