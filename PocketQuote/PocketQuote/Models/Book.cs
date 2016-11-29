using SQLite;

namespace PocketQuote.Models
{
    //Класс, соответствующий таблице "Книги" (Books) в БД
    [Table("Books")]
    public class Book
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; } //уникальный идентификатор

        [Column("Name")]
        public string Name { get; set; } //название книги
        
        [Column("Writer_id")]
        public int Writer_id { get; set; } //уникальный идентификатор автора (Writer.id)
        
        public string Writer_name { get; set; } //ФИО автора - поле, отсутствующее в БД (берем из связанной таблицы "Авторы" (Writers) при выборке)
    }
}
