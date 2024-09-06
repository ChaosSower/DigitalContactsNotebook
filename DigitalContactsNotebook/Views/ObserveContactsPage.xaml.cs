using System.Windows.Controls;
using System.Windows;

using DigitalContactsNotebook.Data;
using DigitalContactsNotebook.ViewModels;
using DigitalContactsNotebook.Models;
using UniversalClassLibrary;
using System.Windows.Input;
//using System.Windows.Controls.Primitives;

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

            ObserveContactsPageGrid.ProcessAllChildVisualControls(Control =>
            {
                if (Control is Button Button)
                {
                    Button.Cursor = Cursors.Hand;
                }
            });

            using ApplicationContext ApplicationContext = new();

            List<ContactViewModel> Contacts = [];

            foreach (Contact Contact in ApplicationContext.Contacts)
            {
                Contacts.Add(new(MainWindowFrame, Contact));
            }

            //List<Contact> customers = ApplicationContext.Contacts.ToList();
            ObserveContactsPageDataGrid.ItemsSource = Contacts;

            FilterSearchingButton.Click += (sender, e) =>
            {
                // Фильтрация с помощью отдельного окна (отмена / ок)
                //Popup popup = new Popup()
                //{
                //    StaysOpen = false,
                //    Width = 100,
                //    Height = 100,
                //    PlacementTarget = FilterSearchingButton,
                //    HorizontalAlignment = HorizontalAlignment.Center,
                //};
                //popup.IsOpen = true;
            };
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