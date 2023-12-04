namespace Lab3.storage;

public interface IDataStorage<T>
{
    void Save(string filePath);
    void Load(string filePath);

    void SaveToXml(string filePath);
    void LoadFromXml(string filePath);

    void SaveToSqLite(string pathToDatabase);
    void LoadFromSqLite(string pathToDatabase);
}