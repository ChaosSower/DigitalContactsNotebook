using System.Data.Entity;

using DigitalContactsNotebook.Models;

namespace DigitalContactsNotebook.Data
{
    /// <summary>
    /// Контекст БД, содержащий все таблицы
    /// </summary>
    internal class ApplicationContext : DbContext
    {
        /// <summary>
        /// Таблица контактов из БД
        /// </summary>
        public DbSet<Contact>? Contacts { get; set; }

        /// <summary>
        /// Таблица типов телефонов из БД
        /// </summary>
        public DbSet<PhoneType>? PhoneTypes { get; set; }

        /// <summary>
        /// Таблица информации о контактах из БД
        /// </summary>
        public DbSet<ContactInfo>? ContactsInfo { get; set; }

        /// <summary>
        /// Конструктор контекста БД
        /// </summary>
        public ApplicationContext() : base("Data Source=DESKTOP-MGFG0K5;Initial Catalog=ContactsNotebookDB;Integrated Security=True;TrustServerCertificate=True")
        {
            Database.CreateIfNotExists();
        }
    }
}