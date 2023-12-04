using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class TodoList
    {
        private List<Task> _tasks = new List<Task>();

        public IEnumerable<Task> Tasks
        {
            get
            {
                return _tasks.AsReadOnly();
            }
        }

        public IEnumerable<Task> CompletedTasks
        {
            get
            {
                return _tasks.Where(t => t.IsComplete);
            }
        }

        public IEnumerable<Task> UncompletedTasks
        {
            get
            {
                return _tasks.Where(t => !t.IsComplete);
            }
        }

        public void Add(Task task)
        {
            if (task == null || _tasks.Contains(task)) { return; }
            _tasks.Add(task);
        }

        public void Remove(Task task)
        {
            if (task == null || !_tasks.Contains(task)) { return; }
            _tasks.Remove(task);
        }

        public IEnumerable<Task> Search(string[] tags)
        {
            List<Task> matchingTasks = new List<Task>();

            foreach (Task task in _tasks)
            {
                foreach (string tag in tags)
                {
                    if (task.HasTag(tag))
                    {
                        matchingTasks.Add(task);
                        continue;
                    }
                }
            }
            

            return matchingTasks;
        }

        public void DisplayTasks()
        {
            if (_tasks.Count() == 0)
            {
                Console.WriteLine("The list is empty.");
                return;
            }

            int counter = 1;
            Console.WriteLine("Actual tasks");

            foreach (Task task in _tasks)
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
    }
}
