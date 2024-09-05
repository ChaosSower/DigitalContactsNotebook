using DigitalContactsNotebook.Data;
using DigitalContactsNotebook.Interfaces;

namespace DigitalContactsNotebook.Models
{
    /// <summary>
    /// Строка контакта таблицы <see cref="ApplicationContext.Contacts"/>
    /// </summary>
    internal class Contact : IDatabase
    {
        public required int ID { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public required string PhoneNumber { get; set; }

        private int? _PhoneTypeID;

        /// <summary>
        /// ID типа телефона из <see cref="PhoneType"/>
        /// </summary>
        public int? PhoneTypeID { get; set; }

        /// <summary>
        /// ID информации о контакте из <see cref="ContactInfo"/>
        /// </summary>
        public required int ContactInfoID { get; set; }
    }
}