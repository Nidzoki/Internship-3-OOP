using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Project p = new Project("Novi projekt");

            p.PrintProjectInfo();

            Console.ReadKey();
        }
    }
}
