using DigitalContactsNotebook.Data;
using DigitalContactsNotebook.Interfaces;

namespace DigitalContactsNotebook.Models
{
    /// <summary>
    /// Строка контакта таблицы <see cref="ApplicationContext.Contacts"/>
    /// </summary>
    public class Contact : IDatabase
    {
        /// <summary>
        /// Обязательное поле ID
        /// </summary>
        public required int ID { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public required string PhoneNumber { get; set; }

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