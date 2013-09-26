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
using TheLibrary.DAL;

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AuthorsDataGrid.ItemsSource = (new AuthorService()).GetAllAuthors();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AuthorsDataGrid.ItemsSource = (new AuthorService()).GetPopularInYear(int.Parse(YearTextBox.Text));
        }
    }
}
