using SQLite;

namespace PocketQuote.Models
{
    //Класс, соответствующий таблице "Авторы" (Writers) в БД
    [Table("Writers")]
    public class Writer
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; } //Уникальный идентификатор

        [Column("Name")]
        public string Name { get; set; } //ФИО автора
    }
}
