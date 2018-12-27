using System;
using System.Collections.Generic;
using System.Text;

namespace bbc.Data.Interfaces
{
    public interface ILocalDatabaseConnection
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
