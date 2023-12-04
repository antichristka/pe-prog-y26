using System.Xml.Serialization;
using Lab3.storage;

namespace Lab3
{
    [Serializable]
    public class TodoList : IDataStorage<TodoList>
    {
        private SQLiteDataStorage<TodoList>? _sqLiteDataStorage = null;
        private List<Task> _tasks = new List<Task>();
        
        public TodoList() { }

        [XmlArray("Tasks")]
        [XmlArrayItem("Task")]
        public List<Task> Tasks
        {
            get => _tasks;
            set => _tasks = value;
        }

        // public IReadOnlyList<Task> CompletedTasks => (IReadOnlyList<Task>)Tasks.Where(t => t.IsComplete);
        // public IReadOnlyList<Task> UncompletedTasks => (IReadOnlyList<Task>)Tasks.Where(t => !t.IsComplete);

        public void Add(Task task)
        {
            if (Tasks.Contains(task)) { return; }
            Tasks.Add(task);
        }

        public void Remove(Task task)
        {
            if (!Tasks.Contains(task)) { return; }
            Tasks.Remove(task);
        }

        public IEnumerable<Task> Search(string[] tags)
        {
            List<Task> matchingTasks = new List<Task>();

            foreach (var task in from task in Tasks from tag in tags where task.HasTag(tag) select task)
            {
                matchingTasks.Add(task);
            }

            return matchingTasks;
        }

        public void DisplayTasks()
        {
            if (Tasks.Count() == 0)
            {
                Console.WriteLine("The list is empty.");
                return;
            }

            int counter = 1;
            Console.WriteLine("Actual tasks");

            foreach (Task task in Tasks)
            {
                if (!task.IsComplete)
                {
                    Console.WriteLine(counter + ". " + task.Title);
                    Console.WriteLine("Description: " + task.Description);
                    Console.WriteLine("Deadline: " + task.Deadline);
                    string tags = string.Join(", ", task.Tags);
                    Console.WriteLine("Tags: " + tags);
                    counter++;
                }
            }
        }

        public void Save(string filePath)
        {
            JsonDataStorage<TodoList> jsonDataStorage = new JsonDataStorage<TodoList>();
            jsonDataStorage.Save(filePath, this);
        }

        public void Load(string filePath)
        {
            JsonDataStorage<TodoList?> jsonDataStorage = new JsonDataStorage<TodoList?>();
            TodoList loadedFromJson = jsonDataStorage.Load(filePath);
            Tasks = new List<Task>(loadedFromJson.Tasks);
        }

        public void SaveToXml(string filePath)
        {
            XmlDataStorage<TodoList?> xmlDataStorage = new XmlDataStorage<TodoList?>();
            xmlDataStorage.SaveToXml(filePath, this);
        }

        public void LoadFromXml(string filePath)
        {
            XmlDataStorage<TodoList?> xmlDataStorage = new XmlDataStorage<TodoList?>();
            TodoList loadFromXml = xmlDataStorage.LoadFromXml(filePath);
            Tasks = new List<Task>(loadFromXml.Tasks);
        }

        public void SaveToSqLite(string pathToDatabase)
        {
            if (String.IsNullOrWhiteSpace(pathToDatabase)) throw new ArgumentException("Path can't be null or empty.");
            _sqLiteDataStorage ??= new SQLiteDataStorage<TodoList>(pathToDatabase);
            _sqLiteDataStorage.SaveToSQLite(this);
        }

        public void LoadFromSqLite(string pathToDatabase)
        {
            if (String.IsNullOrWhiteSpace(pathToDatabase)) throw new ArgumentException("Path can't be null or empty.");
            _sqLiteDataStorage ??= new SQLiteDataStorage<TodoList>(pathToDatabase);
            
            TodoList loadedTodoList = _sqLiteDataStorage.LoadFromSQLite();

            Tasks = new List<Task>(loadedTodoList.Tasks);
        }
    }
}
