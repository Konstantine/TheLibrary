using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheLibrary.Services;
using TheLibrary.DAL;

namespace TheLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserService UserService = new UserService();
        private BookIssuanceService IssuanceService = new BookIssuanceService();
        private BookService BookService = new BookService();

        public MainWindow()
        {
            InitializeComponent();
            UsersComboBox.ItemsSource = UserService.GetAllUsers().Where(bu => !bu.IsBanned);
            BooksComboBox.ItemsSource = BookService.GetAllBooks();
        }

        private void AllAuthors_Click(object sender, RoutedEventArgs e)
        {
            AuthorsDataGrid.ItemsSource = (new AuthorService()).GetAllAuthors();
        }

        private void PopularInYear_Click(object sender, RoutedEventArgs e)
        {
            AuthorsDataGrid.ItemsSource = (new AuthorService()).GetPopularInYear(int.Parse(YearTextBox.Text));
        }

        private void AllBooksButton_Click(object sender, RoutedEventArgs e)
        {
            BooksDataGrid.ItemsSource = (new BookService()).GetAllBooks();
        }

        private void AuthorsBooksButton_Click(object sender, RoutedEventArgs e)
        {
            BooksDataGrid.ItemsSource = (new BookService()).GetAuthorBooks(AuthorTextBox.Text);
        }

        private void Namesakes_Click(object sender, RoutedEventArgs e)
        {
            AuthorsDataGrid.ItemsSource = (new AuthorService()).GetAllNamesakes();
        }

        private void AverageRateButton_Click(object sender, RoutedEventArgs e)
        {
            BooksDataGrid.ItemsSource = (new BookService()).GetAverageRateBook(int.Parse(YearRateTextBox.Text));
        }

        private void AllUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersDataGrid.ItemsSource = UserService.GetAllUsers();
        }

        private void BadUsersButton_Click(object sender, RoutedEventArgs e)
        {
            if (BadDatePicker.SelectedDate.HasValue)
            {
                UsersDataGrid.ItemsSource = UserService.GetBadUsers(BadDatePicker.SelectedDate.Value);
            }
            else
            {
                MessageBox.Show("Укажите дату", "Error");
            }
        }

        private void SaveUsersButton_Click(object sender, RoutedEventArgs e)
        {
            UserService.SaveChanges();
        }

        private void AllOperations_Click(object sender, RoutedEventArgs e)
        {
            OperationDataGrid.ItemsSource = IssuanceService.GetAllIssuance();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            IssuanceService.ReturnBook((BookIssuance)OperationDataGrid.SelectedItem);
            OperationDataGrid.ItemsSource = IssuanceService.GetAllIssuance();
        }

        private void OperationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReturnButton.IsEnabled = !((BookIssuance)OperationDataGrid.SelectedItem).RealReturnDate.HasValue;
        }

    }
}
