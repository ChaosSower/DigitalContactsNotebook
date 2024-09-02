using System.Windows.Controls;
using System.Windows;

namespace DigitalContactsNotebook.Pages
{
    /// <summary>
    /// <see cref="Page"/> отображения контактов
    /// </summary>
    public partial class ContactsPage : Page
    {
        private readonly Frame MainWindowFrame;

        public ContactsPage(Frame MainWindowFrame)
        {
            InitializeComponent();

            this.MainWindowFrame = MainWindowFrame;
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