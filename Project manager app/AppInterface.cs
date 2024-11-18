using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app
{
    public class AppInterface
    {
        public AppInterface() { }

        public string MainMenu(ref Dictionary<Project, List<Task>> projects)
        {
            Printer.PrintMainMenu(false);

            string[] mainMenuOptions = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };

            var input = Console.ReadLine();

            while (!mainMenuOptions.Contains(input))
            {
                Console.Clear();
                Printer.PrintMainMenu(true);
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "1":
                    DisplayProjects(projects);
                    return " Exiting project list...";
                case "2":
                    EvaluateProjectCreationSuccess(ref projects);
                    return " Exiting project creation...";
                case "3":
                    return " You have selected -> Delete a project"; // not yet implemented
                case "4":
                    return " You have selected -> Show all tasks with a deadline in the next 7 days"; // not yet implemented
                case "5":
                    return " You have selected -> Display projects filtered by status"; // not yet implemented
                case "6":
                    return " You have selected -> Manage individual projects"; // not yet implemented
                case "7":
                    return " You have selected -> Manage individual tasks";
                case "8":
                    return "Exit";
                default:
                    return " Error!";
            };
        }

        // Display projects section

        public void DisplayProjects(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            Console.WriteLine($"\n DISPLAY ALL PROJECTS\n");

            if (projects.Count == 0) 
                Console.WriteLine(" Oh no, seems like you don't have any projects created.\n You can create one by choosing option 2 in the main menu.\n");
            else
                Printer.PrintProjectsAndTasks(projects);
            
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        // Project creation section

        public void EvaluateProjectCreationSuccess(ref Dictionary<Project, List<Task>> projects)
        {
            var creationSuccess = HandleProjectCreation(ref projects);

            Console.Clear();
            Console.WriteLine(creationSuccess ? "\n CREATE NEW PROJECT SUCCESS\n\n Your project has been created successfully.\n\n Press any key to continue..." 
                : "\n CREATE NEW PROJECT ERROR\n\n An error has occured! Your project hasn't been created. Be careful with data input.\n\n Press any key to continue...");
            Console.ReadKey();
        }

        public bool HandleProjectCreation(ref Dictionary<Project, List<Task>> projects)
        {
            var newProjectData = GetNewProjectData();

            if (newProjectData == null)
                return false;

            return AddNewProject(ref projects, newProjectData);
        }

        public bool AddNewProject(ref Dictionary<Project, List<Task>> projects, Project newProject)
        {
            if (projects.Keys.Select(x => x.Name).Contains(newProject.Name))
                return false;

            projects.Add(newProject, new List<Task>());

            return true;
        }

        public Project GetNewProjectData()
        {
            Console.Clear();
            Console.WriteLine("\n CREATE NEW PROJECT\n\n");
            Console.Write(" Project name: ");
            var projectName = Console.ReadLine().Trim();

            if (projectName.Length == 0) 
                return null;

            Console.Write("\n\n Project description: ");
            var projectDescription = Console.ReadLine().Trim();

            try
            {
                Console.Write("\n\n Start date in dd-mm-yyyy format: ");
                var startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("\n\n End date in dd-mm-yyyy format: ");                
                var endDate = DateTime.Parse(Console.ReadLine());

                if (startDate > endDate)
                    return null;

                return new Project(projectName, projectDescription, startDate, endDate);
            }
            catch 
            {
                return null;
            }
        }
    }
}
