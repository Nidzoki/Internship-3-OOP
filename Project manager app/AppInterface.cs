using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    EvaluateProjectDeletionSuccess(ref projects);
                    return " Exiting project deletion...";
                case "4":
                    DisplayTasksInNextSevenDays(projects); // needs to be tested when add tasks has been implemented
                    return " Exiting next 7 days tasks display...";
                case "5":
                    DisplayProjectsByStatus(projects);
                    return " Exiting disply projects by status..."; // not yet implemented
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
            var newProjectData = GetUserInput.GetNewProjectData();

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

        // Project deletion section

        public void EvaluateProjectDeletionSuccess(ref Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            if (projects.Count == 0)
            {
                Console.WriteLine("\n DELETE PROJECT\n\n There is no projects to delete.\n\n Press any key to continue...");
                Console.ReadKey();
                return;
            }
            var deletionSuccess = DeleteProject(ref projects);

            Console.Clear();
            Console.WriteLine(deletionSuccess ? "\n DELETE PROJECT SUCCESS\n\n Selected project has been deleted successfully.\n\n Press any key to continue..."
                : "\n DELETE PROJECT ERROR\n\n An error has occured or the project deletion has been abandoned! Your project hasn't been deleted.\n\n Press any key to continue...");
            Console.ReadKey();
        }

        public bool DeleteProject(ref Dictionary<Project, List<Task>> projects)
        {
            var projectInfo = GetUserInput.GetProjectToDelete(projects);

            if (projectInfo == null) return false;

            return projects.Remove(projectInfo);
        }

        // Display all tasks within 7 days section

        public void DisplayTasksInNextSevenDays(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();
            Console.WriteLine("\n DISPLAY TASKS IN NEXT 7 DAYS\n\n");

            var isThereAnyTasks = false;

            foreach (var taskList in projects.Values)
            {
                foreach (var task in taskList)
                {
                    if (DateTime.Now < task.Deadline && DateTime.Now.AddDays(7.0) > task.Deadline)
                    {
                        Printer.PrintTask(task);
                        isThereAnyTasks = true;
                    }
                }
            }

            if (!isThereAnyTasks)
                Console.WriteLine("\n There is no tasks to display.");

            Console.WriteLine("\n \n Press any key to continue...");
            Console.ReadKey();
        }

        // Display projects filtered by status

        public void DisplayProjectsByStatus(Dictionary<Project, List<Task>> projects)
        {
            Console.Clear();

            if (projects.Count == 0)
                Console.WriteLine("\n DISPLAY PROJECTS FILTERED BY STATUS\n\n There is no projects.");

            var selectedStatus = GetUserInput.GetProjectStatus();

            if (selectedStatus == null)
            {
                Console.Clear();
                Console.WriteLine("\n Error while selecting project status!\n\n Press any key to continue...");
                Console.ReadKey();
            }

            var selectedProjects = projects.Where(x => x.Key.Status == selectedStatus);

            foreach (var project in selectedProjects)
            {
                Console.WriteLine("\n");
                Printer.PrintProject(project);
            }

            Console.WriteLine("\n\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
