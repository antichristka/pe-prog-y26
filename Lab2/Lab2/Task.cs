using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Task
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsComplete = false;

        private List<string> _tags = new List<string>();

        public Task(string title, string description, DateTime deadline, List<string> tags)
        {
            Title = title;
            Description = description;
            Deadline = deadline;

            foreach (string tag in tags)
            {
                AddTag(tag);
            }
        }

        public IEnumerable<string> Tags
        {
            get
            {
                return _tags.AsReadOnly();
            }
        }

        public void AddTag(string tag)
        {
            if (_tags.Contains(tag) || String.IsNullOrWhiteSpace(tag)) 
            {
                return; 
            }
            _tags.Add(tag);
        }
        public void RemoveTag(string tag)
        {
            _tags.Remove(tag);
        }
        public bool HasTag(string tag)
        {
            return _tags.Contains(tag);
        }

        public void MarkAsComplete()
        {
            this.IsComplete = true;
        }
    }
}
