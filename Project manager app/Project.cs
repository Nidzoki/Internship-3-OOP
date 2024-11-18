using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }

        public Project(string name)
        {
            Name = name;
            Description = "";
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
            Status = ProjectStatus.Active;
        }

        public void PrintProjectInfo()
        {
            Console.WriteLine($" Project name: {Name}\n Description: {Description}\n Start date: {StartDate:dd/mm/yyyy}\n End date: {EndDate:dd/mm/yyyy}\n Status: {Status}");
        }
    }
}
