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

namespace TheLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
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
            UsersDataGrid.ItemsSource = (new UserService()).GetAllUsers();
        }
    }
}
