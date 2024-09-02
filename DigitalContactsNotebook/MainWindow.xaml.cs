using System.Windows.Controls;
using System.Windows;

using DigitalContactsNotebook.Pages;

namespace DigitalContactsNotebook
{
    /// <summary>
    /// Главное окно с <see cref="Frame"/>
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowFrame.Navigate(new MainWindowPage(MainWindowFrame));
        }
    }
}