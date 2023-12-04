namespace Lab3.storage;

using System.Xml.Serialization;

public class XmlDataStorage<T> where T : class
{
    public void SaveToXml(string filePath, T data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        // Ensure the directory exists
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create or overwrite the file
        using (TextWriter writer = new StreamWriter(filePath, false))
        {
            serializer.Serialize(writer, data);
        }
        
        Console.WriteLine($"[XML] Successfully written the data to \n{filePath}!");
    }


    public T? LoadFromXml(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"[XML] An error has occured. The file \n> {filePath}\n doesn't exist.");
            throw new FileNotFoundException();
        } 
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (TextReader reader = new StreamReader(filePath))
        {
            Console.WriteLine($"[XML] Successfully loaded from \n{filePath}!");
            return (T)serializer.Deserialize(reader);
        }
    }
}