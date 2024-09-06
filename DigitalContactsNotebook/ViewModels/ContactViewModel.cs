using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

using DigitalContactsNotebook.Models;
using DigitalContactsNotebook.Data;
using DigitalContactsNotebook.Views;

namespace DigitalContactsNotebook.ViewModels
{
    /// <summary>
    /// Класс ViewModel полной информации о контакте <see cref="Contact"/>
    /// </summary>
    public class ContactViewModel
    {
        /// <summary>
        /// Обязательное поле ID
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        /// Тип телефона
        /// </summary>
        public string? PhoneType { get; }

        /// <summary>
        /// Имя контакта
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Фамилия контакта
        /// </summary>
        public string? Surname { get; }

        /// <summary>
        /// Отчество контакта
        /// </summary>
        public string? Patronymic { get; }
        //public char Sex { get; } // EF не считывает char(1) из БД — по умолчанию ставит нулевой символ 0 '\0'
        
        /// <summary>
        /// Пол контакта
        /// </summary>
        public string? Sex { get; }

        public ICommand CheckContactCommand { get; set; }
        public ICommand EditContactCommand { get; set; }
        public ICommand DeleteContactCommand { get; set; }

        private readonly Frame MainWindowFrame;
        public Contact Contact { get; }

        public ContactViewModel(Frame MainWindowFrame, Contact Contact)
        {
            this.MainWindowFrame = MainWindowFrame;
            this.Contact = Contact;

            ID = Contact.ID;
            PhoneNumber = Contact.PhoneNumber;

            using ApplicationContext ApplicationContext = new();

            PhoneType = ApplicationContext.PhoneTypes?.FirstOrDefault(PhoneType => PhoneType.ID == Contact.PhoneTypeID)?.PhoneTypeText ?? null;

            ContactInfo? ContactInfo = ApplicationContext.ContactsInfo?.FirstOrDefault(ContactInfo => ContactInfo.ID == Contact.ContactInfoID) ?? null;
            Name = ContactInfo?.Name ?? null;
            Surname = ContactInfo?.Surname ?? null;
            Patronymic = ContactInfo?.Patronymic ?? "—";
            //Sex = (ContactInfo?.Sex == '\0' ? '?' : ContactInfo?.Sex) ?? '?';
            Sex = ContactInfo?.Sex ?? null;

            CheckContactCommand = new RelayCommand(CheckContact);
            EditContactCommand = new RelayCommand(EditContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);
        }

        /// <summary>
        /// Команда просмотра контакта
        /// </summary>
        /// <param name="parameter"></param>
        private void CheckContact(object parameter)
        {
            MainWindowFrame.Navigate(new CreateOrEditContactPage(MainWindowFrame, this));
        }

        /// <summary>
        /// Команда изменения контакта
        /// </summary>
        /// <param name="parameter"></param>
        private void EditContact(object parameter)
        {
            MainWindowFrame.Navigate(new CreateOrEditContactPage(MainWindowFrame, this, true));
        }

        /// <summary>
        /// Команда удаления контакта
        /// </summary>
        /// <param name="parameter"></param>
        private void DeleteContact(object parameter)
        {
            MessageBox.Show("Вы уверены?");
        }
    }
}