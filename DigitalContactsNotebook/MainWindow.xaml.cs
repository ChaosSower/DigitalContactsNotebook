using System.Windows.Controls;
using System.Windows;

using DigitalContactsNotebook.Views;

namespace DigitalContactsNotebook
{
    /// <summary>
    /// Главное окно с <see cref="Frame"/>
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            MainWindowFrame.Navigate(new MainWindowPage(MainWindowFrame));
        }
    }
}