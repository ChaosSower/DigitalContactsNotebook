using System.Windows.Controls;
using System.Windows;

namespace DigitalContactsNotebook.Pages
{
    /// <summary>
    /// <see cref="Page"/> главного окна <see cref="MainWindow"/>
    /// </summary>
    public partial class MainWindowPage : Page
    {
        private readonly Frame MainWindowFrame;

        public MainWindowPage(Frame MainWindowFrame)
        {
            InitializeComponent();

            this.MainWindowFrame = MainWindowFrame;
        }

        /// <summary>
        /// Событие нажатия на кнопку "Просмотр контактов"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObserveContactsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new ContactsPage(MainWindowFrame));
        }

        /// <summary>
        /// Событие нажатия на кнопку "Добавить контакт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new CreateOrEditContactPage(MainWindowFrame));
        }
    }
}