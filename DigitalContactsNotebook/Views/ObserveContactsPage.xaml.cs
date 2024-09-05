using System.Windows.Controls;
using System.Windows;

using DigitalContactsNotebook.Data;
using DigitalContactsNotebook.ViewModels;
using DigitalContactsNotebook.Models;

namespace DigitalContactsNotebook.Views
{
    /// <summary>
    /// <see cref="Page"/> отображения контактов
    /// </summary>
    public partial class ObserveContactsPage : Page
    {
        private readonly Frame MainWindowFrame;

        /// <summary>
        /// Конструктор страницы отображения контактов
        /// </summary>
        /// <param name="MainWindowFrame"><see cref="Frame"/> окна <see cref="MainWindow"/></param>
        public ObserveContactsPage(Frame MainWindowFrame)
        {
            InitializeComponent();

            this.MainWindowFrame = MainWindowFrame;

            using ApplicationContext ApplicationContext = new();

            List<ContactViewModel> Contacts = [];

            foreach (Contact Contact in ApplicationContext.Contacts)
            {
                Contacts.Add(new(Contact));
            }

            ObserveContactsPageDataGrid.ItemsSource = Contacts;
        }

        /// <summary>
        /// Событие нажатия на кнопку "Назад"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.GoBack();
        }
    }
}