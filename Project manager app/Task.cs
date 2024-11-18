using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public int DurationInMinutes { get; set; }
        public string ParentProject { get; set; }

        public Task(string taskName, string parentProjectName)
        {
            Name = taskName;
            Description = string.Empty;
            Deadline = DateTime.Now.Date;
            Status = TaskStatus.Active;
            DurationInMinutes = 0;
            ParentProject = parentProjectName;

        }

    }
}
