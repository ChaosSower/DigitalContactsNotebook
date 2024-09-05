using System.ComponentModel.DataAnnotations.Schema;

using DigitalContactsNotebook.Data;
using DigitalContactsNotebook.Interfaces;

namespace DigitalContactsNotebook.Models
{
    /// <summary>
    /// Строка типа телефона таблицы <see cref="ApplicationContext.PhoneTypes"/>
    /// </summary>
    internal class PhoneType : IDatabase
    {
        public required int ID { get; set; }

        /// <summary>
        /// Тип телефона
        /// </summary>
        [Column("PhoneType")] // имя столбца из БД
        public required string PhoneTypeText { get; set; }
    }
}