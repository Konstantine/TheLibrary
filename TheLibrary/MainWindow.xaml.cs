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
        private AuthorService AuthorService = new AuthorService();

        public MainWindow()
        {
            InitializeComponent();
            GiveBookDatePicker.SelectedDate = DateTime.Now.AddMonths(1);
            AuthorsDataGrid.ItemsSource = AuthorService.GetAllAuthors();
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
            int year;
            if (int.TryParse(YearRateTextBox.Text, out year))
            {
                BooksDataGrid.ItemsSource = (new BookService()).GetAverageRateBook(year);
            }
            else {
                MessageBox.Show("Введите год", "Error");
            }
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

        private void MakeIssuanceButton_Click(object sender, RoutedEventArgs e)
        {
            IssuanceService.GiveBook((int)UsersComboBox.SelectedValue, (int)BooksComboBox.SelectedValue, GiveBookDatePicker.SelectedDate.Value);
              OperationDataGrid.ItemsSource = IssuanceService.GetAllIssuance();
        }
         
        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.OfType<TabItem>().Count() > 0)
            {
                TabItem selectedTabItem = e.AddedItems.OfType<TabItem>().First();
                if (selectedTabItem == AuthorsTabItem)
                {
                    AuthorsDataGrid.ItemsSource = AuthorService.GetAllAuthors();
                }
                if (selectedTabItem == UsersTabItem)
                {

                    UsersDataGrid.ItemsSource = UserService.GetAllUsers();
                }
                if (selectedTabItem == BooksTabItem)
                {
                    BooksDataGrid.ItemsSource = BookService.GetAllBooks();
                }
                if (selectedTabItem == OperationTabItem)
                {
                    OperationDataGrid.ItemsSource = IssuanceService.GetAllIssuance();
                    UsersComboBox.ItemsSource = UserService.GetAllUsers().Where(bu => !bu.IsBanned).Select(u => new { UserId = u.UserId, FullName = u.Name + " " + u.Lastname });
                    BooksComboBox.ItemsSource = BookService.GetAllBooks();
                }
            }
        }

        private void UndefinedSearchButton_Click(object sender, RoutedEventArgs e)
        {
            string publish = PublishYearTextBox.Text;
            string bookName = BookNameTextBox.Text;
            BooksDataGrid.ItemsSource = BookService.GetUndefinedBook(publish, bookName);           
        }

        private void BadTopButton_Click(object sender, RoutedEventArgs e)
        {
            int year;
            if (int.TryParse(BadTopYearTextBox.Text, out year) && year == DateTime.Now.Year)
            {
                UsersDataGrid.ItemsSource = UserService.GetTopBadUsers(DateTime.Now);
            }
            else
            {
                DateTime date = new DateTime(year, 12, 31);
                UsersDataGrid.ItemsSource = UserService.GetTopBadUsers(date);
            }
        }

    }
}
