//using DigitalContactsNotebook.Interfaces;
using DigitalContactsNotebook.Models;
using DigitalContactsNotebook.Data;

namespace DigitalContactsNotebook.ViewModels
{
    internal class ContactViewModel
    {
        public int ID { get; }
        public string PhoneNumber { get; }
        public string? PhoneType { get; }
        public string? Name { get; }
        public string? Surname { get; }
        public string? Patronymic { get; }
        public char Sex { get; }

        public ContactViewModel(Contact Contact)
        {
            ID = Contact.ID;
            PhoneNumber = Contact.PhoneNumber;

            using ApplicationContext ApplicationContext = new();

            PhoneType = ApplicationContext.PhoneTypes?.FirstOrDefault(PhoneType => PhoneType.ID == Contact.PhoneTypeID)?.PhoneTypeText ?? null;

            ContactInfo? ContactInfo = ApplicationContext.ContactsInfo?.FirstOrDefault(ContactInfo => ContactInfo.ID == Contact.ContactInfoID) ?? null;
            Name = ContactInfo?.Name ?? null;
            Surname = ContactInfo?.Surname ?? null;
            Patronymic = ContactInfo?.Patronymic ?? "—";
            Sex = (ContactInfo?.Sex == '\0' ? '?' : ContactInfo?.Sex) ?? '?';
        }
    }
}