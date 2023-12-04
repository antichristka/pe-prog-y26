using System.Data.SQLite;
using Newtonsoft.Json;

namespace Lab3.storage;

public class SQLiteDataStorage<T>
{
    private readonly string _connectionString;

    public SQLiteDataStorage(string pathToDatabase)
    {
        _connectionString = $"Data Source={pathToDatabase};Version=3;";
        CreateDatabase();
    }

    private void CreateDatabase()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                // Create a table named 'Data' with a column 'SerializedData' of type TEXT
                command.CommandText = "CREATE TABLE IF NOT EXISTS Data (Id INTEGER PRIMARY KEY, SerializedData TEXT)";
                command.ExecuteNonQuery();
            }
        }
    }

    public void SaveToSQLite(T data)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Data (SerializedData) VALUES (@SerializedData)";
                command.Parameters.AddWithValue("@SerializedData", JsonConvert.SerializeObject(data));
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine($"[SQLITE] Successfully written the data to \n{_connectionString}!");
    }

    public T LoadFromSQLite()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT SerializedData FROM Data ORDER BY Id DESC LIMIT 1";
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    Console.WriteLine($"[SQLITE] Successfully loaded from \n{_connectionString}!");   
                    return JsonConvert.DeserializeObject<T>(result.ToString());
                }
            }
        }
        Console.WriteLine($"[SQLITE] An error has occured. The db \n> {_connectionString}\n doesn't exist.");
        return default(T);
    }
}
