namespace Lab3
{
    class Program
    {
        private const string JsonFilePath = "C:\\Users\\kater\\source\\repos\\Lab3\\Lab3\\json\\todoList.json";
        private const string XmlFilePath = "C:\\Users\\kater\\source\\repos\\Lab3\\Lab3\\xml\\todoList.xml";
        private const string SQLitePath = "C:\\Users\\kater\\source\\repos\\Lab3\\Lab3\\sqlite\\todoList.db";

        public static void Main(string[] args)
        {
            TodoList todoList = new TodoList();
            Task testTask = new Task("a", "adf", DateTime.Parse("11/11/1111"), new List<string>());
            todoList.Add(testTask);
            todoList.Add(testTask);

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Search task");
                Console.WriteLine("3. Last tasks");
                Console.WriteLine("4. Save state");
                Console.WriteLine("5. Load state");
                Console.WriteLine("6. Exit");
                Console.Write("> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddTask(todoList);
                        break;
                    case "2":
                        Console.Write("Enter tags to search (separated by commas): ");
                        string tagsInput = Console.ReadLine();
                        string[] tags = tagsInput.Split(',').Select(tag => tag.Trim()).ToArray();
                        IEnumerable<Task> tasks = todoList.Search(tags);

                        if (tasks.Count() == 0)
                        {
                            Console.WriteLine("No such tasks.");
                        }
                        
                        int counter = 1;
                        foreach (Task task in tasks)
                        {
                            if (!task.IsComplete)
                            {
                                Console.WriteLine("(" + counter + ") " + task.Title);
                                counter++;
                            }
                        }
                        break;
                    case "3":
                        todoList.DisplayTasks();
                        break;
                    // save state
                    case "4":
                        Console.WriteLine("[SAVE] Choose the method:");
                        Console.WriteLine("1. Json");
                        Console.WriteLine("2. XML");
                        Console.WriteLine("3. SQLite");
                        Console.WriteLine("4. Back");
                        Console.Write("> ");
                        
                        string saveInput = Console.ReadLine();

                        switch (saveInput)
                        {
                            case "1":
                                todoList.Save(JsonFilePath);
                                break;
                            case "2":
                                todoList.SaveToXml(XmlFilePath);
                                break;
                            case "3":
                                todoList.SaveToSqLite(SQLitePath);
                                break;
                            case "4": break;
                            default: 
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    // load state
                    case "5":
                        Console.WriteLine("[LOAD] Choose the method:");
                        Console.WriteLine("1. Json");
                        Console.WriteLine("2. XML");
                        Console.WriteLine("3. SQLite");
                        Console.WriteLine("4. Back");
                        Console.Write("> ");
                        
                        string loadInput = Console.ReadLine();

                        switch (loadInput)
                        {
                            case "1":
                                todoList.Load(JsonFilePath);
                                break;
                            case "2":
                                todoList.LoadFromXml(XmlFilePath);
                                break;
                            case "3":
                                todoList.LoadFromSqLite(SQLitePath);
                                break;
                            case "4": break;
                            default: 
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        static void AddTask(TodoList todoList)
        {
            Console.Write("Enter the Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter the Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter the Deadline (DD/MM/YYYY): ");
            string deadlineInput = Console.ReadLine();
            DateTime deadline;
            if (!DateTime.TryParse(deadlineInput, out deadline))
            {
                Console.WriteLine("Invalid date format");
                return;
            }
            Console.Write("Enter tags (separated by commas): ");
            string tagsInput = Console.ReadLine();
            List<string> tags = tagsInput.Split(',').Select(tag => tag.Trim()).ToList();

            Task task = new Task(title, description, deadline, tags);
            todoList.Add(task);
            Console.WriteLine("Task added successfully");
        }
    }
}
