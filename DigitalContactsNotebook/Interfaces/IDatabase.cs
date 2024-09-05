namespace DigitalContactsNotebook.Interfaces
{
    /// <summary>
    /// Интерфейс для таблиц БД
    /// </summary>
    internal interface IDatabase
    {
        /// <summary>
        /// Обязательное поле "ID"
        /// </summary>
        public int ID { get; set; }
    }
}