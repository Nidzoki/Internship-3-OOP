using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app
{
    public class Program
    {
        static void Main()
        {
            var projectsDictionary = new Dictionary<Project, List<Task>>()
            { 
                { new Project("Project Alpha", "Description for Project Alpha", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(30)), new List<Task>() { new Task("Task 1", "Project Alpha", DateTime.Now.AddDays(3), 60, "Task 1 description", PriorityLevel.High), new Task("Task 2", "Project Alpha", DateTime.Now.AddDays(8), 45, "Task 2 description", PriorityLevel.Medium), new Task("Task 3", "Project Alpha", DateTime.Now.AddDays(15), 120, "Task 3 description", PriorityLevel.Low) } },
                { new Project("Project Bravo", "Description for Project Bravo", DateTime.Now.AddDays(-20), DateTime.Now.AddDays(40)), new List<Task>() { new Task("Task 4", "Project Bravo", DateTime.Now.AddDays(2), 90, "Task 4 description", PriorityLevel.Medium), new Task("Task 5", "Project Bravo", DateTime.Now.AddDays(10), 30, "Task 5 description", PriorityLevel.High), new Task("Task 6", "Project Bravo", DateTime.Now.AddDays(25), 150, "Task 6 description", PriorityLevel.Low) } }, 
                { new Project("Project Charlie", "Description for Project Charlie", DateTime.Now.AddDays(-15), DateTime.Now.AddDays(45)), new List<Task>() { new Task("Task 7", "Project Charlie", DateTime.Now.AddDays(1), 60, "Task 7 description", PriorityLevel.Low), new Task("Task 8", "Project Charlie", DateTime.Now.AddDays(5), 120, "Task 8 description", PriorityLevel.High), new Task("Task 9", "Project Charlie", DateTime.Now.AddDays(20), 75, "Task 9 description", PriorityLevel.Medium) } } };
            var appInterface = new AppInterface();
            var quit = false;

            while (!quit)
            {
                if(appInterface.MainMenu(ref projectsDictionary) == "Exit")
                    quit = true;
            }

            Console.Clear();
            Console.WriteLine("\n Exiting project manager app...\n\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
