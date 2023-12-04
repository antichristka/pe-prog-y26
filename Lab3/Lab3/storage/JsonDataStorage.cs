namespace Lab3.storage;
using System.IO;
using Newtonsoft.Json;

public class JsonDataStorage<T>
{
    public void Save(string filePath, T data)
    {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
        Console.WriteLine($"[JSON] Successfully written the data to \n{filePath}!");
    }

    public T? Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"[JSON] An error has occured. The file \n> {filePath}\n doesn't exist.");
            return default(T);
        }
        string jsonData = File.ReadAllText(filePath);
        Console.WriteLine($"[JSON] Successfully loaded from \n{filePath}!");
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}
