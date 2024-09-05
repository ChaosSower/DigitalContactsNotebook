using System.ComponentModel.DataAnnotations.Schema;

using DigitalContactsNotebook.Data;
using DigitalContactsNotebook.Interfaces;

namespace DigitalContactsNotebook.Models
{
    /// <summary>
    /// Строка информации о контакте таблицы <see cref="ApplicationContext.ContactsInfo"/>
    /// </summary>
    [Table("ContactsInfo")] // обязательное явное указание названия таблицы (по умолчанию EF читает как "ContactInfoes")
    internal class ContactInfo : IDatabase
    {
        public required int ID { get; set; }

        /// <summary>
        /// Имя контакта
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Фамилия контакта
        /// </summary>
        public required string Surname { get; set; }

        /// <summary>
        /// Отчество контакта
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Пол контакта
        /// </summary>
        public required char Sex { get; set; }
    }
}