using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace PocketQuote.Models
{
    //вспомогательный класс для обеспечения группировки при отображении списков
    public class Grouping<K, T> : ObservableCollection<T>
    {
        //ключ группы - поле, на основе которого будет формироваться группа (по сути общее поле для всех элементов группы)
        public K Key { get; private set; }

        //наименование клча, которое будет отображаться в интерфейсе (т.к. сам ключ - уникальный идентификатор, непонятный пользователю)
        public string KeyName { get; private set; }

        //конструктор - передаются ключ, коллекция с общим ключом и название ключа
        public Grouping(K key, IEnumerable<T> items, string keyName)
        {
            Key = key;
            KeyName = keyName;
            foreach (T item in items)
                Items.Add(item);
        }
    }
}
