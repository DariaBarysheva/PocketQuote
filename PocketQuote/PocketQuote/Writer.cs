using SQLite;

namespace PocketQuote
{
    //Класс, соответствующий таблице "Авторы" (Writers) в БД
    [Table("Writers")]
    public class Writer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } //Уникальный идентификатор

        public string Name { get; set; } //ФИО автора
    }
}
