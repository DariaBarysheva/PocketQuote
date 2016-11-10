using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketQuote
{
    public interface ISQLite
    {
        //Для хранения пути к БД. На разных платформах отличается - поэтому нужен интерфейс отдельный
        string GetDatabasePath(string filename);
    }
}
